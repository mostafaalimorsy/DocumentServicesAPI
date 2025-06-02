using DocumentService.DTOs;

namespace DocumentService.Interface
{
    public interface IIAMAuthService
    {
        Task<LoginResponse> AuthenticateAsync(string username, string password);
    }

}
