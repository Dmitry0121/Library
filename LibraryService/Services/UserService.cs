using AutoMapper;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Interfaces;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<UserDto> Get()
        {
            return _mapper.Map<List<UserDto>>(
                _repository.Get(r => r.Role));
        }

        public UserDto Get(int id)
        {
            return _mapper.Map<UserDto>(_repository.Get(
                    x => x.Id.Equals(id), r => r.Role
                ).FirstOrDefault());
        }

        public UserDto GetByEmail(string email)
        {
            return _mapper.Map<UserDto>(_repository.Get(
                    x => x.Email.Equals(email), r => r.Role
                ).FirstOrDefault());
        }

        public bool Create(UserDto item)
        {
            return _repository.Create(_mapper.Map<User>(item));
        }

        public bool Update(UserDto item)
        {
            return _repository.Update(_mapper.Map<User>(item));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}