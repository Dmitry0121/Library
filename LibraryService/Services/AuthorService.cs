using AutoMapper;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Interfaces;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<AuthorDto> Get()
        {
            return _mapper.Map<List<AuthorDto>>(
                _repository.Get(b => b.Books));
        }

        public AuthorDto Get(int id)
        {
            return _mapper.Map<AuthorDto>(_repository.Get(
                    x => x.Id.Equals(id), b => b.Books
                ).FirstOrDefault());
        }

        public bool Create(AuthorDto item)
        {
            return _repository.Create(_mapper.Map<Author>(item));
        }

        public bool Update(AuthorDto item)
        {
            return _repository.Update(_mapper.Map<Author>(item));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}