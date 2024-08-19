using ToDoodleBlazor.DTOs;
using static ToDoodleBlazor.Responses.CustomResponses;

namespace ToDoodleBlazor.Repos
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> LoginAsync(LoginDTO model);
    }
}
