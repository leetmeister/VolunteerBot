using Microsoft.Bot.Builder.Dialogs;

using Microsoft.Bot.Builder.FormFlow;

using Microsoft.Bot.Builder.FormFlow.Advanced;

using Newtonsoft.Json.Linq;

using System;

using System.Collections.Concurrent;

using System.Collections.Generic;

using System.Globalization;

using System.IO;

using System.Linq;

using System.Reflection;

using System.Threading;



namespace VolunteerBot
{
    [Serializable]
    public class VolunteerFormFlow
    {

        //[Prompt("What is your full name?")]
        public string FullName;

        //[Prompt("To inform you of events near you, we'd like to know the area you live in. What is your zip code?")]
        public string ZipCode;

        //[Prompt("So that we can help you through some next steps, what is your e-mail address?")]
        public string EmailAddress;

        public static IForm<VolunteerFormFlow> BuildForm()
        {
            OnCompletionAsyncDelegate<VolunteerFormFlow> processOrder = async (context, state) =>
            {
                await context.PostAsync("Thank you for submitting!");
            };

            return new FormBuilder<VolunteerFormFlow>()
                        .Message("I can help with that! I'm going to ask you a few questions to help you get connected.")
                        .Field(nameof(VolunteerFormFlow.FullName))
                        .Field(nameof(VolunteerFormFlow.ZipCode))
                        .Field(nameof(VolunteerFormFlow.EmailAddress))
                        .Build();
        }

    }
}