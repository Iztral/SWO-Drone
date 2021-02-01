using SWO.Shared.Models.AuthModels;
using System.Threading.Tasks;

namespace SWO.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
    }
}
