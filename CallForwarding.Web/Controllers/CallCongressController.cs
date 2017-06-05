using CallForwarding.Web.Models;
using CallForwarding.Web.Models.Repository;
using System.Linq;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace CallForwarding.Web.Controllers
{
    public class CallCongressController : TwilioController
    {
        private readonly IRepository<Zipcode> _zipcodesRepository;
        private readonly IRepository<State> _statesRepository;
        private readonly IRepository<Senator> _senatorsRepository;

        public CallCongressController()
        {
            _zipcodesRepository = new ZipcodesRepository();
            _statesRepository = new StatesRepository();
            _senatorsRepository = new SenatorsRepository();
        }

        public CallCongressController(
            IRepository<Zipcode> zipcodesRepository,
            IRepository<Senator> senatorsRepository,
            IRepository<State> statesRepository)
        {
            _zipcodesRepository = zipcodesRepository;
            _statesRepository = statesRepository;
            _senatorsRepository = senatorsRepository;
        }

        // Verify or collect State information.
        [HttpPost]
        public ActionResult Welcome(string fromState)
        {
            var voiceResponse = new VoiceResponse();
            if (!string.IsNullOrEmpty(fromState))
            {
                voiceResponse.Say("Thank you for calling congress! It looks like " + 
                                  $"you\'re calling from {fromState}. " +
                                  "If this is correct, please press 1. Press 2 if " +
                                  "this is not your current state of residence.");
                voiceResponse.Gather(numDigits: 1, action: "/callcongress/setstate", method: "POST");
            }
            else
            {
                voiceResponse.Say("Thank you for calling Call Congress! If you wish to " +
                                "call your senators, please enter your 5 - digit zip code.");
                voiceResponse.Gather(numDigits: 5, action: "/callcongress/statelookup", method: "POST");
            }
            return TwiML(voiceResponse);
        }

        // If our state guess is wrong, prompt user for zip code.
        [AcceptVerbs("GET", "POST")]
        public ActionResult CollectZip()
        {
            var voiceResponse = new VoiceResponse();

            voiceResponse.Say("If you wish to call your senators, please " +
                    "enter your 5-digit zip code.");
            voiceResponse.Gather(numDigits: 5, action: "/callcongress/statelookup", method: "POST");

            return TwiML(voiceResponse);
        }

        // Look up state from given zipcode.
        // Once state is found, redirect to call_senators for forwarding.
        [AcceptVerbs("GET", "POST")]
        public ActionResult StateLookup(int digits)
        {
            // NB: We don't do any error handling for a missing/erroneous zip code
            // in this sample application. You, gentle reader, should to handle that
            // edge case before deploying this code.
            Zipcode zipcodeObject = _zipcodesRepository.FirstOrDefault(z => z.ZipcodeNumber == digits);

            return RedirectToAction("CallSenators", new { callerState = zipcodeObject.State });
        }


        // Set state for senator call list.
        // Set user's state from confirmation or user-provided Zip.
        // Redirect to call_senators route.
        [AcceptVerbs("GET", "POST")]
        public ActionResult SetState(string digits, string callerState)
        {
            if (digits.Equals("1"))
            {
                return RedirectToAction("CallSenators", new { callerState = callerState });
            }
            else
            {
                return RedirectToAction("CollectZip");
            }
        }

        // Route for connecting caller to both of their senators.
        [AcceptVerbs("GET", "POST")]
        public ActionResult CallSenators(string callerState)
        {
            var senators = _statesRepository
                .FirstOrDefault(s => s.name == callerState)
                .Senators.ToList();

            var voiceResponse = new VoiceResponse();
            var firstCall = senators[0];
            var secondCall = senators[1];
            var sayMessage = $"Connecting you to {firstCall.Name}. " +
                             "After the senator's office ends the call, you will " +
                             $"be re-directed to {secondCall.Name}.";

            voiceResponse.Say(sayMessage);
            voiceResponse.Dial(number: firstCall.Phone,
                action: "/callcongress/callsecondsenator/" + secondCall.Id);

            return TwiML(voiceResponse);
        }

        // Forward the caller to their second senator.
        [AcceptVerbs("GET", "POST")]
        public ActionResult CallSecondSenator(int id)
        {
            var senator = _senatorsRepository.Find(id);

            var voiceResponse = new VoiceResponse();
            voiceResponse.Say($"Connecting you to {senator.Name}.");
            voiceResponse.Dial(number: senator.Phone, action: "/callcongress/goodbye");

            return TwiML(voiceResponse);
        }

        // Thank user & hang up.
        [AcceptVerbs("GET", "POST")]
        public ActionResult Goodbye()
        {
            var voiceResponse = new VoiceResponse();
            voiceResponse.Say("Thank you for using Call Congress! " +
                "Your voice makes a difference. Goodbye.");
            voiceResponse.Hangup();

            return TwiML(voiceResponse);
        }
    }
}
