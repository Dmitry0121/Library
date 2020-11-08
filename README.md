# Library

# Review notes

## Repo

1. .gitignore is absent
  Repo size is 50.19 MiB. It contains all binaries, packages, and user files.

## Solution

1. Contains an invalid project link - solution is not buildable
  Wrong project: `E:\projects\Library\Library\LibrarySendEmail\LibrarySendEmail.csproj`

2. Unclear project types
Why `LibraryDataAccess`, `LibraryService` are console applications?

3. No unit tests

4. No integration/IU tests

## Code styles

1. Fields are not marker with `readonly`
2. Trailing spaces :)

## Implementation

1. Any user can mange books and authors. Shouldn't it be at least two roles:
   library admin, and reader?

2. Some classes/interfaces might be generic.
    In the current implementation `IAuthorService`,  `IBookService`,
    `IUserService` have the same set of methods:

    ```cs
    void Create(UserDto item);
    void Update(UserDto item);
    void Delete(int id);
    ```

    These methods call repositories, and repositories call context. The code of
    repositories call the same methods from the context but with different generic
    parameters.

3. Why the context properties (Authors, Books, etc.) are not used? I'd
understand if the repositories were generic :)

4. Having test data in the production code is not a good idea
    (`MyDatabaseContextInitializer`)

5. Why mapper is not configured once and registered in DI for all controllers?
    What is the purpose to configure it each time?

6. Email regex are copy/pasted

7. No validation in controllers, services, and repositories.
    It seems that it is possible to get books even if available count is 0:)

8. No transactions and thread safety
    What will happen if database is go offline just after the history record is
    created (before the book record is updated)? What will happen if a book is
    deleted after the history record is created? What will happen if two or more
    parallel requests will come to take a book?
