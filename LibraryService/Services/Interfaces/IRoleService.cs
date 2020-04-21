using LibraryService.Dto;

namespace LibraryService.Services.Interfaces
{
    public interface IRoleService
    {
        RoleDto GetReaderRole();
        RoleDto GetAdminRole();
    }
}