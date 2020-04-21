using LibraryDataAccess.Entities;
using System.Data.Entity;

namespace LibraryDataAccess.Configuration
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("MyLibraryDB")
        {
        }
        static LibraryContext()
        {
            Database.SetInitializer<LibraryContext>(new MyDatabaseContextInitializer());
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        internal class MyDatabaseContextInitializer : DropCreateDatabaseIfModelChanges<LibraryContext>
        {
            protected override void Seed(LibraryContext context)
            {
                //var roles = new List<Role>() 
                //{
                //    new Role() {
                //        Id = 1,
                //        Name = "Admin"
                //    },
                //    new Role() {
                //        Id = 2,
                //        Name = "Reader"
                //    }
                //};
                //context.Roles.AddRange(roles);

                //var users = new List<User>()
                //{
                //    new User() {
                //        Id = 1,
                //        FirstName = "Jacob",
                //        LastName = "Sophia",
                //        Email = "JacobSophia@test.com",
                //        RoleId = 1,
                //        Role = roles[0]
                //    },
                //    new User() {
                //        Id = 2,
                //        FirstName = "Mason",
                //        LastName = "Isabella",
                //        Email = "MasonIsabella@test.com",
                //        RoleId = 1,
                //        Role = roles[0]
                //    },
                //    new User() {
                //        Id = 3,
                //        FirstName = "William",
                //        LastName = "Emma",
                //        Email = "WilliamEmma@test.com",
                //        RoleId = 1,
                //        Role = roles[0]
                //    },
                //    new User() {
                //        Id = 4,
                //        FirstName = "Jayden",
                //        LastName = "Olivia",
                //        Email = "JaydenOlivia@test.com",
                //        RoleId = 2,
                //        Role = roles[1]
                //    },
                //};
                //context.Users.AddRange(users);

                //var authors = new List<Author>()
                //{
                //    new Author()
                //    {
                //        Id = 1,
                //        FirstName = "Alexander",
                //        LastName = "Lloyd",
                //        Books = new List<Book>()
                //    },
                //    new Author()
                //    {
                //        Id = 2,
                //        FirstName = "Anders",
                //        LastName = "Jane",
                //        Books = new List<Book>()
                //    },
                //    new Author()
                //    {
                //        Id = 3,
                //        FirstName = "Jacob",
                //        LastName = "Sophia",
                //        Books = new List<Book>()
                //    },
                //};
                //var books = new List<Book>()
                //{
                //    new Book()
                //    {
                //        Id = 1,
                //        Count = 50,
                //        Name = "The Book of Three",
                //        Authors = new List<Author>()
                //    },
                //    new Book()
                //    {
                //        Id = 2,
                //        Count = 2,
                //        Name = "The Black Cauldron",
                //        Authors = new List<Author>()
                //    },
                //};

                //books[0].Authors.Add(authors[0]);
                //books[0].Authors.Add(authors[1]);
                //books[1].Authors.Add(authors[1]);
                //books[1].Authors.Add(authors[2]);

                //authors[0].Books.Add(books[0]);
                //authors[1].Books.Add(books[0]);
                //authors[1].Books.Add(books[1]);
                //authors[2].Books.Add(books[1]);

                //context.Books.AddRange(books);
                //context.Authors.AddRange(authors);

                //var history = new List<History>()
                //{
                //    new History()
                //    {
                //        Id = 1,
                //        DateReceiving = DateTime.Now.AddDays(-2),
                //        ReturnDate = null,
                //        BookId = books[0].Id,
                //        Book = books[0],
                //        UserId = users[0].Id,
                //        User = users[0]
                //    },
                //    new History()
                //    {
                //        Id = 2,
                //        DateReceiving = DateTime.Now.AddDays(-3),
                //        ReturnDate = DateTime.Now.AddDays(-1),
                //        BookId = books[1].Id,
                //        Book = books[1],
                //        UserId = users[1].Id,
                //        User = users[1]
                //    }
                //};
                //context.History.AddRange(history);
            }
        }
    }
}