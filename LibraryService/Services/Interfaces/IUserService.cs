using LibraryService.Dto;

namespace LibraryService.Services.Interfaces
{
    public interface IUserService : IServiceRead<UserDto>, IServiceWrite<UserDto>
    {
        UserDto GetByEmail(string email);
    }
}