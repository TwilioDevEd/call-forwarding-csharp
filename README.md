<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# Advanced Call Forwarding with Twilio and ASP.NET MVC

[![Build status](https://ci.appveyor.com/api/projects/status/htn0vhetbombyiu5?svg=true)](https://ci.appveyor.com/project/TwilioDevEd/call-forwarding-csharp)

Learn how to use [Twilio](https://www.twilio.com) to forward a series of phone calls to your state senators.

## Local Development

To run the app locally, follow these steps:

1. Clone this repository and `cd` into it.
    ```bash
    git clone git@github.com:TwilioDevEd/call-forwarding-csharp.git
    cd call-forwarding-csharp
    ```

1. Build the solution.

1. Run `Update-Database` at [Package Manager
   Console](https://docs.nuget.org/consume/package-manager-console) to execute the migrations.

1. Expose your application to the internet using [ngrok](https://www.twilio.com/blog/2015/09/6-awesome-reasons-to-use-ngrok-when-testing-webhooks.html). In a separate terminal session, start ngrok with:
    ```bash
    ngrok http 9292
    ```
    Once you have started ngrok, update your TwiML application's voice URL setting to use your ngrok hostname. It will look something like this in your Twilio [console](https://www.twilio.com/console/phone-numbers/):
    ```
    https://d06f533b.ngrok.io/callcongress/welcome
    ```

1. Run the application.

    Once ngrok is running, open up your browser and go to your ngrok URL.


## Meta
* No warranty expressed or implied. Software is as is. Diggity.
* [MIT License](https://opensource.org/licenses/mit-license.html)
* Lovingly crafted by Twilio Developer Education.
