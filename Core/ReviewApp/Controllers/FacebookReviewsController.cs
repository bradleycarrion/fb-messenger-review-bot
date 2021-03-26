using Microsoft.AspNetCore.Mvc;
using ReviewApp.MockDataStore;
using ReviewApp.MockDataStore.InternalModels;
using System.Collections.Generic;

namespace ReviewApp.Controllers
{
    // TODO: Add authorization
    [ApiController]
    public class FacebookReviewsController : ControllerBase
    {
        [HttpGet]
        [Route("~/reviews")]
        public IDictionary<string, HashSet<FacebookReview>> GetAllReviews()
        {
            return FacebookMockDataStore.Store.GetAllReviews();
        }
    }
}
