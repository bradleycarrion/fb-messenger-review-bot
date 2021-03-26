using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewApp.MockDataStore
{
    public abstract class MockDataStoreBase<TKey, TValue> : IMockDataStore<TKey, TValue>
    {
        public abstract bool TryGet(TKey k, out HashSet<TValue> v);

        public abstract Task Put(TKey k, TValue v);
    }
}
