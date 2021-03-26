using Microsoft.AspNetCore.Http;
using ReviewApp.Common;
using ReviewApp.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ReviewApp.Authorization
{
    public class FacebookWebhookAuthorizationService : IAuthorizationService
    {
        private readonly IConfigurationContext _configuration;

        public FacebookWebhookAuthorizationService(IConfigurationContext configuration)
        {
            Requires.NotNull(configuration, nameof(configuration));
            _configuration = configuration;
        }

        public Task Authorize(HttpContext context)
        {
            if (!context.TryGetHttpHeader(Constants.FacebookXHubSignature, out string header))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            // We need to enable request buffering so that we 
            context.Request.EnableBuffering();

            // Leave the body open so the next middleware can read it.
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024 * 45,
                leaveOpen: true))
            {
                string body = reader.ReadToEndAsync().Result;

                string expectedSignature = ComputeSignature(body, _configuration.FacebookAppSecret);

                if (IsSignatureValid(header, expectedSignature))
                {
                    // Reset the request body stream position so that subsequent services can read it
                    context.Request.Body.Position = 0;

                    return Task.CompletedTask;
                }

                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Essentially a string comparison of the computed signature against the
        /// provided signature from Facebook
        /// </summary>
        /// <param name="header">
        /// header: "sha1=****..."
        /// </param>
        private static bool IsSignatureValid(string header, string expectedSignature)
        {
            if (header.Length == 45 && header.StartsWith("sha1="))
            {
                string providedSignature = header.Substring(5, 40);
                if (string.Equals(providedSignature, expectedSignature))
                {
                    return true;
                }
            }

            return false;
        }

        private static string ComputeSignature(string data, string secret)
        {
            byte[] arr = Encoding.UTF8.GetBytes(data);
            byte[] key = Encoding.UTF8.GetBytes(secret);
            using (var sha1 = new HMACSHA1(key))
            {
                byte[] signature = sha1.ComputeHash(arr);
                return ConvertToHexadecimal(signature);
            }
        }

        private static string ConvertToHexadecimal(IEnumerable<byte> bytes)
        {
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
