using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewApp.MockDataStore
{
    public interface IMockDataStore<TKey, TValue>
    {
        Task Put(TKey k, TValue v);

        bool TryGet(TKey k, out HashSet<TValue> v);
    }
}
