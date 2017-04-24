using CallForwarding.Web.Controllers;
using CallForwarding.Web.Models;
using CallForwarding.Web.Models.Repository;
using CallForwarding.Web.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.XPath;
using TestStack.FluentMVCTesting;

namespace CallForwarding.Web.Tests.Controllers
{
    class CallCongressControllerTest
    {
        private CallCongressController _controller;

        [SetUp]
        public void setUp ()
        {
            _controller = new CallCongressController();
        }

        [Test]
        public void WelcomeRouteTriggersLookupStateWhenNoFromStateParameter()
        {
            _controller.WithCallTo(c => c.Welcome(null))
                .ShouldReturnTwiMLResult(data =>
                {
                    StringAssert.Contains("please enter your 5 - digit zip code", 
                        data.XPathSelectElement("Response/Say").Value);
                    StringAssert.AreEqualIgnoringCase("/callcongress/statelookup",
                        data.XPathSelectElement("Response/Gather").Attribute("action").Value);
                    StringAssert.AreEqualIgnoringCase("5",
                        data.XPathSelectElement("Response/Gather").Attribute("numDigits").Value);
                });
        }

        [Test]
        public void WelcomeRouteTriggerSetStateWhenFromStateParameterIsPresent()
        {
            _controller.WithCallTo(c => c.Welcome("PR"))
                .ShouldReturnTwiMLResult(data =>
                {
                    StringAssert.Contains("If this is correct, please press 1. Press 2 if",
                        data.XPathSelectElement("Response/Say").Value);
                    StringAssert.AreEqualIgnoringCase("/callcongress/setstate",
                        data.XPathSelectElement("Response/Gather").Attribute("action").Value);
                    StringAssert.AreEqualIgnoringCase("1",
                        data.XPathSelectElement("Response/Gather").Attribute("numDigits").Value);
                });
        }

        [Test]
        public void CollectZipRouteTriggersStateLookup()
        {
            _controller.WithCallTo(c => c.CollectZip())
                .ShouldReturnTwiMLResult(data =>
                {
                    StringAssert.Contains("enter your 5-digit zip code.",
                        data.XPathSelectElement("Response/Say").Value);
                    StringAssert.AreEqualIgnoringCase("/callcongress/statelookup",
                        data.XPathSelectElement("Response/Gather").Attribute("action").Value);
                    StringAssert.AreEqualIgnoringCase("5",
                        data.XPathSelectElement("Response/Gather").Attribute("numDigits").Value);
                });
        }


        [Test]
        public void StateLookupRouteRedirectsToCallSenators()
        {
            // given
            var mockRepository = new Mock<IRepository<Zipcode>>();
            mockRepository.Setup(r => r.FirstOrDefault(It.IsAny<Func<Zipcode, bool>>()))
                .Returns(new Zipcode() { State= "PR" });
            var controller = new CallCongressController(mockRepository.Object, null, null);

            // when
            var result = (RedirectToRouteResult)controller.StateLookup(12345);

            // then
            Assert.AreEqual("CallSenators", result.RouteValues["action"]);
            Assert.AreEqual("PR", result.RouteValues["callerState"]);
            Assert.IsNull(result.RouteValues["controller"]);
        }

        [Test]
        public void testSetStateRouteRedirectToCallSenatorsWhenParametersIs1()
        {
            // when
            var result = (RedirectToRouteResult)_controller.SetState("1", "PR");

            // then
            Assert.AreEqual("CallSenators", result.RouteValues["action"]);
            Assert.AreEqual("PR", result.RouteValues["CallerState"]);
            Assert.IsNull(result.RouteValues["controller"]);
        }

        [Test]
        public void SetStateRouteRedirectToCollectZipcodeWhenParametersIs2()
        {
            // when
            var result = (RedirectToRouteResult)_controller.SetState("2", "PR");

            // then
            Assert.AreEqual("CollectZip", result.RouteValues["action"]);
            Assert.IsNull(result.RouteValues["controller"]);
        }

        [Test]
        public void CallSenatorsRouteTriggersCallToFirstSenator()
        {
            // given
            var senators = new List<Senator>()
            {
                new Senator() { Name = "senator1", Phone = "phone1" },
                new Senator() { Id = 33, Name = "senator2" }
            };
            var mockRepository = new Mock<IRepository<State>>();
            mockRepository.Setup(r => r.FirstOrDefault(It.IsAny<Func<State, bool>>()))
                .Returns(new State() { Senators = senators });
            var controller = new CallCongressController(null, null, mockRepository.Object);


            // when
            controller.WithCallTo(c => c.CallSenators(""))
                .ShouldReturnTwiMLResult(data =>
                {
                    // then
                    StringAssert.Contains("Connecting you to senator1.",
                        data.XPathSelectElement("Response/Say").Value);
                    StringAssert.Contains("be re-directed to senator2.",
                        data.XPathSelectElement("Response/Say").Value);
                    StringAssert.AreEqualIgnoringCase("/callcongress/callsecondsenator/33",
                        data.XPathSelectElement("Response/Dial").Attribute("action").Value);
                    StringAssert.AreEqualIgnoringCase("phone1",
                        data.XPathSelectElement("Response/Dial").Value);
                });
        }


        [Test]
        public void CallSecondSenatorsRouteTriggersCallToSecondSenator()
        {
            // given
            var mockRepository = new Mock<IRepository<Senator>>();
            mockRepository.Setup(r => r.Find(It.IsAny<int>()))
                .Returns(new Senator() { Name = "senator", Phone = "phone" });
            var controller = new CallCongressController(null, mockRepository.Object, null);

            // when
            controller.WithCallTo(c => c.CallSecondSenator(1))
                .ShouldReturnTwiMLResult(data =>
                {
                    // then
                    StringAssert.Contains("Connecting you to senator.",
                        data.XPathSelectElement("Response/Say").Value);
                    StringAssert.AreEqualIgnoringCase("/callcongress/goodbye",
                        data.XPathSelectElement("Response/Dial").Attribute("action").Value);
                    StringAssert.AreEqualIgnoringCase("phone",
                        data.XPathSelectElement("Response/Dial").Value);
                });
        }

        [Test]
        public void GoodbyeRouteTriggersHangupCall()
        {
            // when
            _controller.WithCallTo(c => c.Goodbye())
                .ShouldReturnTwiMLResult(data =>
                {
                    // then
                    StringAssert.Contains("Your voice makes a difference. Goodbye.",
                        data.XPathSelectElement("Response/Say").Value);
                    Assert.IsNotNull(data.XPathEvaluate("Response/Dial"));
                });
        }
       
    }
}
