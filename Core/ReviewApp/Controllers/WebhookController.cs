using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Authorization;
using ReviewApp.Common;
using ReviewApp.Configuration;
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

            // some async task here
            // ...


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
