using TopStyle.Domain.DTO;

namespace TopStyle.Core.Interfaces

{
    public interface IUserService
    {
        Task<TokenDto> AuthenticateAsync(string username, string password);
        Task<bool> RegisterUserAsync(UserRegisterDto userDto);
    }
}
