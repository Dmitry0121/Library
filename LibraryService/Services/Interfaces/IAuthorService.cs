using LibraryService.Dto;

namespace LibraryService.Services.Interfaces
{
    public interface IAuthorService : IServiceRead<AuthorDto>, IServiceWrite<AuthorDto>
    {
    }
}