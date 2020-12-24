<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# Advanced Call Forwarding with Twilio and ASP.NET MVC

![](https://github.com/TwilioDevEd/call-forwarding-csharp/workflows/NetFx/badge.svg)

Learn how to use [Twilio](https://www.twilio.com) to forward a series of phone calls to your state senators.

## Local Development

To run the app locally, follow these steps:

1. Clone this repository and `cd` into it.
    ```bash
    git clone https://github.com/TwilioDevEd/call-forwarding-csharp.git
    cd call-forwarding-csharp
    ```

1. Build the solution.

1. Run `Update-Database` at [Package Manager
   Console](https://docs.nuget.org/consume/package-manager-console) to execute the migrations.

1. Run the solution. You should see the Call Forward welcome page come up at http://localhost:8080/

1. Expose your application to the internet using [ngrok](https://www.twilio.com/blog/2015/09/6-awesome-reasons-to-use-ngrok-when-testing-webhooks.html). In a separate terminal session, start ngrok with:
    ```bash
    ngrok http --host-header="localhost:8080" 8080
    ```
    Once you have started ngrok, update your TwiML application's voice URL setting to use your ngrok hostname. It will look something like this in your Twilio [console](https://www.twilio.com/console/phone-numbers/):
    ```
    https://d06f533b.ngrok.io/callcongress/welcome
    ```

1. Run the application.

    Once ngrok is running, give your Twilio phone number a call.


## Meta
* No warranty expressed or implied. Software is as is. Diggity.
* [MIT License](https://opensource.org/licenses/mit-license.html)
* Lovingly crafted by Twilio Developer Education.
