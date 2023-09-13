using Source.Models.DTOs;

namespace Source.Services
{
    public interface ILoginRegisterService
    {
        Task<bool> Register(UserDTO request);
        Task<string> Login(UserDTO request);
    }
}
