using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewApp.MockDataStore.InternalModels
{
    public class FacebookReview
    {
        public string MessageId { get; set; }

        public string Review { get; set; }

        public Rating Rating { get; set; } 
    }

    public enum Rating
    {
        None,
        ThumbsUp,
        ThumbsDown
    }
}
