using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReviewApp.Authorization;
using ReviewApp.Common;
using ReviewApp.Configuration;
using ReviewApp.Models;
using ReviewApp.Services;
using System.IO;
using System.Threading.Tasks;

namespace ReviewApp
{
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly IConfigurationContext _configuration;
        private readonly IAuthorizationService _authorizationService;

        public WebhookController(IConfigurationContext configuration, IAuthorizationService authorizationService)
        {
            Requires.NotNull(configuration, nameof(configuration));
            _configuration = configuration;

            Requires.NotNull(authorizationService, nameof(authorizationService));
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost("~/webhook")]
        [Consumes(System.Net.Mime.MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ProcessCallback()
        {
            await _authorizationService.Authorize(Request.HttpContext);

            FacebookWebhookRequest request = null;
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();

                request = JsonConvert.DeserializeObject<FacebookWebhookRequest>(body);
            }

            var requestProcessor = new FacebookRequestProcessor();

            // Fire and forget this service call since the response isn't dependent on the result of this
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            requestProcessor.ProcessReqeust(request);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            return Ok();
        }

        /// <summary>
        /// Subscription endpoint for Facebook to be 
        /// </summary>
        [HttpGet("~/webhook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Verification(
                [FromQuery(Name = "hub.mode")] string mode,
                [FromQuery(Name = "hub.verify_token")] string token,
                [FromQuery(Name = "hub.challenge")] string challenge
            )
        {
            if (string.IsNullOrWhiteSpace(mode) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            if (mode == "subscribe" && token == _configuration.FacebookVerificationToken)
            {
                return Ok(challenge);
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }
    }
}
