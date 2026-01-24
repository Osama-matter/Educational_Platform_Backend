using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Admin
{
    public interface IAdminAuthService
    {
        Task<bool> IsAdminAsync(string token);
    }
}
