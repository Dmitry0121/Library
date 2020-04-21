using LibraryDataAccess.Configuration;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            //user
            var userRepository = new UserRepository(new LibraryContext());
            var users = userRepository.Get(x => x.Role);
            var user2 = userRepository.Get(x => x.Id.Equals(2), r => r.Role);
            var userEmail = userRepository.Get(x => x.Email.Equals("JaydenOlivia@test.com"), r => r.Role);
            userRepository.Create(new User()
            {
                Email = "UserRepository.com",
                FirstName = "UserRepository",
                LastName = "UserRepository",
                RoleId = 1
            });
            userEmail.First().LastName = "UserRepository Update";
            userRepository.Update(userEmail.First());
            userRepository.Delete(5);

            //author
            var authorRepository = new AuthorRepository(new LibraryContext());
            var authors = authorRepository.Get(x => x.Books);
            var author2 = authorRepository.Get(x => x.Id.Equals(2), b => b.Books);
            authorRepository.Create(new Author()
            {
                FirstName = "AuthorRepository",
                LastName = "AuthorRepository",
                Books = new List<Book>()
            });
            author2.First().LastName = "AuthorRepository Update";
            authorRepository.Update(author2.First());
            authorRepository.Delete(4);

            //book
            var bookRepository = new BookRepository(new LibraryContext());
            var books = bookRepository.Get(x => x.Authors);
            var book2 = bookRepository.Get(x => x.Id.Equals(2), b => b.Authors);
            bookRepository.Create(new Book()
            {
                Count = 1,
                Name = "BookRepository",
                Authors = new List<Author>()
            });
            book2.First().Name = "BookRepository Update";
            bookRepository.Update(book2.First());
            bookRepository.Delete(3);
            var historyGive = new History(books.First().Id, users.First().Id);
            var bookGive = new Book()
            {
                Id = books.First().Id,
                Count = books.First().Count - 1
            };
            bookRepository.Give(bookGive, historyGive);
            bookGive.Count++;
            historyGive.ReturnDate = DateTime.Now;
            bookRepository.Return(bookGive, historyGive);

            //history
            var historyRepository = new HistoryRepository(new LibraryContext());
            var history = historyRepository.Get(b => b.Book, u => u.User);
            var history2 = historyRepository.Get(x => x.Id.Equals(2), b => b.Book, u => u.User);
        }
    }
}