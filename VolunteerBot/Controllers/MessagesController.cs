using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Rest;

namespace VolunteerBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                // Filter for command style messages instead of natural language processing
                await Conversation.SendAsync(activity, () => new VolunteerOutreachDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async void HandleSystemMessage(Activity activity)
        {
            if (activity.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                StateClient stateClient = activity.GetStateClient();

                try
                {
                    BotData userConversationData = await stateClient.BotState.GetPrivateConversationDataAsync(activity.ChannelId, activity.Conversation.Id, activity.From.Id);
                    var counter = userConversationData.GetProperty<int>("counter");

                    // create a reply message   
                    Activity reply = activity.CreateReply($"Welcome! This is the [{++counter}] time I've encountered you in this conversation.");

                    // save our new counter by adding it to the outgoing message
                    userConversationData.SetProperty<int>("counter", counter);
                    await stateClient.BotState.SetPrivateConversationDataAsync(activity.ChannelId, activity.Conversation.Id, activity.From.Id, userConversationData);

                    // return our reply to the user
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                catch (HttpOperationException e)
                {
                    Activity reply = activity.CreateReply($"Yikes, hit the exception [{e.Message}], sorry!");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                    throw e;
                }
            }
            else if (activity.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (activity.Type == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
            }
            else if (activity.Type == ActivityTypes.Ping)
            {
            }

            return;
        }
    }
}