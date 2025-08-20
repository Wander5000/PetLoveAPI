using PetLoveAPI.DTOs.Auth;

namespace PetLoveAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<ResponseDTO> LoginAsync(LoginDTO request);
        Task<ResponseDTO> RegisterAsync(RegisterDTO request);
    }
}
