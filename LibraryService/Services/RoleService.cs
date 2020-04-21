using AutoMapper;
using LibraryDataAccess.Repositories.Interfaces;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using System.Linq;

namespace LibraryService.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public RoleDto GetAdminRole()
        {
            return _mapper.Map<RoleDto>(_repository.Get(
                    x => x.Name.Equals("Admin")
                ).FirstOrDefault());
        }

        public RoleDto GetReaderRole()
        {
            return _mapper.Map<RoleDto>(_repository.Get(
                    x => x.Name.Equals("Reader")
                ).FirstOrDefault());
        }
    }
}