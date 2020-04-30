using System;
using System.Threading.Tasks;

namespace Tags
{
    public interface IAuthService
    {
        string GetAuthInfo();
        Task<bool> AuthenticateMe();
    }
}
