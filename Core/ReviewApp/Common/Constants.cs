using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewApp.Common
{
    public static class Constants
    {
        #region Configuration
        public const string FacebookVerificationToken = "FacebookVerificationToken";
        public const string FacebookMessengerSendAPIAccessToken = "FacebookMessengerSendAPIAccessToken";
        public const string FacebookAppSecret = "FacebookAppSecret";
        #endregion

        #region Startup/Bootstrap
        public const string FacebookWebhookAuthorizationPolicy = "FacebookWebhookAuthorizationPolicy";
        #endregion

        #region HTTP Constants
        public const string FacebookXHubSignature = "X-Hub-Signature";
        #endregion
    }
}
