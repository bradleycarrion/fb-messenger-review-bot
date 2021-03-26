using System.Threading.Tasks;

namespace ReviewApp.Services
{
    public abstract class RequestProcessorBase<T> : IReqeustProcessor<T>
    {
        public abstract Task ProcessReqeust(T request);
    }
}
