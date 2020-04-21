using AutoMapper;
using LibraryDataAccess.Repositories.Interfaces;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _repository;
        private readonly IMapper _mapper;

        public HistoryService(IHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<HistoryDto> Get()
        {
            return _mapper.Map<List<HistoryDto>>(_repository.Get(b => b.Book, u => u.User));
        }

        public HistoryDto Get(int id)
        {
            return _mapper.Map<HistoryDto>(_repository.Get(
                    x => x.Id.Equals(id), b => b.Book, u => u.User
                ).FirstOrDefault());
        }

        public List<HistoryDto> GetUserHistory(int id)
        {
            return _mapper.Map<List<HistoryDto>>(_repository.Get(
                    x => x.UserId.Equals(id), b => b.Book, u => u.User));
        }

        public List<HistoryDto> GetUserActiveHistory(int id)
        {
            return _mapper.Map<List<HistoryDto>>(_repository.Get(
                    x => x.UserId.Equals(id), b => b.Book, u => u.User)
                .Where(x => x.ReturnDate.Equals(null)));
        }

        public List<HistoryDto> GetBookHistory(int id)
        {
            return _mapper.Map<List<HistoryDto>>(_repository.Get(
                    x => x.BookId.Equals(id), b => b.Book, u => u.User));
        }
    }
}