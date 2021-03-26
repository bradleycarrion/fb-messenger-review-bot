using System.Threading.Tasks;

namespace ReviewApp.Services
{
    public interface IReqeustProcessor<T>
    {
        Task ProcessReqeust(T request);
    }
}
