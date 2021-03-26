using Microsoft.Extensions.Configuration;
using ReviewApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewApp.Configuration
{
    public class ConfigurationContext : IConfigurationContext
    {
        private readonly IConfiguration _configuration;

        public ConfigurationContext(IConfiguration configuration)
        {
            Requires.NotNull(configuration, nameof(configuration));
            _configuration = configuration;
        }

        public string FacebookVerificationToken => _configuration.GetValue<string>(Constants.FacebookVerificationToken);

        public string FacebookMessengerSendAPIAccessToken => _configuration.GetValue<string>(Constants.FacebookMessengerSendAPIAccessToken);

        public string FacebookAppSecret => _configuration.GetValue<string>(Constants.FacebookAppSecret);
    }
}
