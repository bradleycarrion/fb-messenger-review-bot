using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ReviewApp.Authorization
{
    public interface IAuthorizationService
    {
        Task Authorize(HttpContext context);
    }
}
