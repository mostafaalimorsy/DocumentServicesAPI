using DocumentService.DTOs;

namespace DocumentService.Interface
{
    public interface IIAMAuthService
    {
        Task<TokenResponse> AuthenticateAsync(string username, string password);
    }

}
