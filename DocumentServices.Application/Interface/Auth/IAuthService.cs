using DocumentService.DTOs;

namespace DocumentService.Interface
{
    public interface IAuthService
    {
        LoginResponse Authenticate(LoginRequest request);
    }

}
