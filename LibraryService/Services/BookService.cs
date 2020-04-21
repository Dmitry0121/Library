using AutoMapper;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Interfaces;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using Logger;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<BookDto> Get()
        {
            return _mapper.Map<List<BookDto>>(
                _repository.Get(a => a.Authors));
        }

        public BookDto Get(int id)
        {
            return _mapper.Map<BookDto>(_repository.Get(
                    x => x.Id.Equals(id), a => a.Authors
                ).FirstOrDefault());
        }

        public bool Give(int bookId, int userId)
        {
            var item = Get(bookId);
            if (item == null)
            {
                NLogger.Write().Error($"Book by Id '{bookId}' not found");
                return false;
            }

            var book = new Book()
            {
                Id = bookId,
                Count = --item.Count
            };
            var history = new History(bookId, userId);
            return _repository.Give(book, history);
        }

        public bool Return(int bookId, int historyId)
        {
            var item = Get(bookId);
            if (item == null)
            {
                NLogger.Write().Error($"Book by Id '{bookId}' not found");
                return false;
            }

            var book = new Book()
            {
                Id = bookId,
                Count = ++item.Count
            };
            var history = new History(historyId);
            return _repository.Return(book, history);
        }

        public bool Create(BookDto item)
        {
            return _repository.Create(_mapper.Map<Book>(item));
        }

        public bool Update(BookDto item)
        {
            return _repository.Update(_mapper.Map<Book>(item));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}