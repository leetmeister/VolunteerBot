using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Configuration;

// PROTOTYPE: For convenience, this reuses the Entity Framework definitions for the Volunteer data model.
using VolunteerDataWebApi.Models;

namespace VolunteerBot
{
    [LuisModel("ec28aeb2-bf7b-40ce-84bd-21e3b706838c", "a9b2b734425a4594a7159349612bc36a")]
    [Serializable]
    public class VolunteerOutreachDialog : LuisDialog<object>
    {
        private readonly string volunteerDataBaseUri;

        public VolunteerOutreachDialog()
        {
            var rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            this.volunteerDataBaseUri = rootWebConfig.AppSettings.Settings["VolunteerDataBaseUri"].Value;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand: " + result.Query + "\n It resulted in intents: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("LookupVolunteerEvents")]
        public async Task LookupVolunteerEvents(IDialogContext context, LuisResult result)
        {
            string eventStrings = String.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(volunteerDataBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Get all events between now and the next 90 days
                HttpResponseMessage response = await client.GetAsync($"api/events?startDate={DateTime.UtcNow}&endDate={DateTime.UtcNow + new TimeSpan(90,0,0,0)}");
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<Event> events = await response.Content.ReadAsAsync<IEnumerable<Event>>();
                    // PROTOTYPE: This is inefficient, consider exposing OData style order by and filter functionality on data side instead. 
                    var orderedEvents = events.OrderBy(p => p.StartDateTime);
                    foreach (Event e in orderedEvents)
                    {
                        var date = e.StartDateTime.ToLocalTime();
                        eventStrings += $"{e.Name} [{date:M/d/yyyy}, {date:h:mmtt}-{date + e.Duration:h:mmtt}]\n\r";
                    }

                    if (String.IsNullOrEmpty(eventStrings))
                    {
                        eventStrings = "No events have been scheduled right now, but check back soon!\n\r";
                    }
                }
                else
                {
                    eventStrings = $"Failed to get events from DB with error: {response.StatusCode}";
                }
            }
            string message = $"I think you wanted me to tell you about upcoming volunteer events when you said: " + result.Query;
            message += $"\n\rHere are the upcoming events:\n\r {eventStrings}";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("RegisterAsVolunteer")]
        public async Task RegisterAsVolunteer(IDialogContext context, LuisResult result)
        {
            string message = $"I think you wanted to register as a volunteer when you said: " + result.Query;
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("VolunteerForEvent")]
        public async Task VolunteerForEvent(IDialogContext context, LuisResult result)
        {
            string message = $"I think you wanted to volunteer for an event when you said: " + result.Query;
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("UnregisterAsVolunteer")]
        public async Task UnregisterAsVolunteer(IDialogContext context, LuisResult result)
        {
            string message = $"I think you wanted to unregister as a volunteer when you said: " + result.Query;
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        
        [LuisIntent("WithdrawFromEvent")]
        public async Task WithdrawFromEvent(IDialogContext context, LuisResult result)
        {
            string message = $"I think you wanted to stop volunteering for an event when you said: " + result.Query;
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
    }
}