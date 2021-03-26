using ReviewApp.Common;
using ReviewApp.MockDataStore.InternalModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewApp.MockDataStore
{
    public class FacebookMockDataStore : MockDataStoreBase<string, FacebookReview>
    {
        private static FacebookMockDataStore _instance = null;
        private static readonly object _lock = new object();

        private FacebookMockDataStore() { }

        public static FacebookMockDataStore Store
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new FacebookMockDataStore();
                        }
                    }
                }

                return _instance;
            }
        }

        private readonly ConcurrentDictionary<string, HashSet<FacebookReview>> _cache = 
            new ConcurrentDictionary<string, HashSet<FacebookReview>>();

        public override bool TryGet(string key, out HashSet<FacebookReview> review)
        {
            Requires.NotNullOrWhiteSpace(key, nameof(key));
            
            if (_cache.TryGetValue(key, out review))
            {
                return true;
            }

            return false;
        }

        public override Task Put(string key, FacebookReview review)
        {
            Requires.NotNullOrWhiteSpace(key, nameof(key));
            Requires.NotNull(review, nameof(review));

            if (_cache.ContainsKey(key))
            {
                _cache[key]?.Add(review);

                return Task.CompletedTask;
            }
            else
            {
                if (_cache.TryAdd(key, new HashSet<FacebookReview>() { review }))
                {
                    return Task.CompletedTask;
                }
            }

            throw new Exception("Failed to insert Facebook review");
        }

        public IDictionary<string, HashSet<FacebookReview>> GetAllReviews()
        {
            return new Dictionary<string, HashSet<FacebookReview>>(_cache);
        }
    }
}
