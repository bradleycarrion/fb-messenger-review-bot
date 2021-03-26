using ReviewApp.Common;
using ReviewApp.MockDataStore;
using ReviewApp.MockDataStore.InternalModels;
using ReviewApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewApp.Services
{
    public class FacebookRequestProcessor : RequestProcessorBase<FacebookWebhookRequest>
    {
        public async override Task ProcessReqeust(FacebookWebhookRequest request)
        {
            Requires.NotNull(request, nameof(request));

            Messaging messaging = request.Entry?.FirstOrDefault()?.Messaging?.FirstOrDefault();

            if (messaging != null)
            {
                string customerFacebookId = messaging.Sender.Id;
                Message message = messaging.Message;

                FacebookReview review = new FacebookReview()
                {
                    MessageId = message.MessageId,
                    Rating = Rating.None,
                    Review = message.Text
                };

                await FacebookMockDataStore.Store.Put(customerFacebookId, review);
            }
        }
    }
}
