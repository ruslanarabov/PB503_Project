

using PB503Project;
using PB503Project.DTOs.AuthorDTO;
using PB503Project.DTOs.BookDTO;
using PB503Project.Services.Impelementations;
using PB503Project.Services.Interfaces;

IBookService bookService = new BookService();
IAuthorService authorService = new AuthorService();
IBorrowedService borrowedService = new BorrowerService();
ILoanService loanService = new LoanService();

while (true)
{
    Console.Clear();
    Console.WriteLine("Library Management System");
    Console.WriteLine("1 - Author Actions");
    Console.WriteLine("2 - Book Actions");
    Console.WriteLine("3 - Borrower Actions");
    Console.WriteLine("4 - Borrow Book");
    Console.WriteLine("5 - Return Book");
    Console.WriteLine("6 - Most Borrowed Book");
    Console.WriteLine("7 - Overdue Borrowers");
    Console.WriteLine("8 - Borrower History");
    Console.WriteLine("9 - Filter Books by Title");
    Console.WriteLine("10 - Filter Books by Author");
    Console.WriteLine("0 - Exit");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1": AuthorActions(authorService); break;
        case "2": BookActions(bookService); break;
        //case "3": BorrowerActions(borrowerService); break;
        //case "4": BorrowBook(loanService, bookService, borrowerService); break;
        //case "5": ReturnBook(loanService); break;
        //case "6": GetMostBorrowedBook(loanService); break;
        //case "7": GetOverdueBorrowers(loanService); break;
        //case "8": GetBorrowerHistory(loanService); break;
        //case "9": FilterBooksByTitle(bookService); break;
        //case "10": FilterBooksByAuthor(bookService); break;
        case "0": return;
        default: Console.WriteLine("Invalid option, try again."); break;
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}


static void AuthorActions(IAuthorService authorService)
{
    Console.Clear();
    Console.WriteLine("1 - List Authors");
    Console.WriteLine("2 - Create Author");
    Console.WriteLine("3 - Edit Author");
    Console.WriteLine("4 - Delete Author");
    Console.WriteLine("0 - Exit");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1": authorService.GetAll().ForEach(a => Console.WriteLine($"{a.Id} - {a.Name}")); break;
        case "2":
            Console.Write("Enter author name: ");
            string authorname = Console.ReadLine();
            if (authorname.Any(char.IsDigit))
            {
                Console.WriteLine("Author name cannot contain number.");
            }
            else
            {
                authorService.Add(new CreateAuthorDTO { Name = Console.ReadLine() });
                Console.WriteLine("Author added succesfully!");
            }
            break;
        case "3":
            Console.Write("Enter author ID to update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter new name: ");
            string newName = Console.ReadLine();
            if(newName.Any(char.IsDigit))
            {
                Console.WriteLine("Author name cannot contain number.");
            }
            else
            {
                authorService.Update(id, new UpdateAuthorDTO { Name = Console.ReadLine() });
                Console.WriteLine("Author updated succesfully!");
            }
            break;
        case "4":
            Console.Write("Enter author ID to delete: ");
            authorService.Remove(int.Parse(Console.ReadLine()));
            Console.WriteLine("Author deleted succesfully!");
            break;
        case "0": return;
        default: Console.WriteLine("Invalid option"); break;
    }
}

//static void BookActions(IBookService bookService)
//{
//    Console.Clear();
//    Console.WriteLine("1 - List Books");
//    Console.WriteLine("2 - Create Book");
//    Console.WriteLine("3 - Edit Book");
//    Console.WriteLine("4 - Delete Book");
//    Console.WriteLine("0 - Exit");

//    string choice = Console.ReadLine();
//    switch(choice)
//    {
//        case "1":
//            var books = bookService.GetAll();
//            if(books.Count == 0)
//            {
//                Console.WriteLine("There is no books");
//            }
//            else
//            {
//                books.ForEach(book => Console.WriteLine($"{book.Id} - {book.Title} - {book.PublishYear} - {book.Authors}"));
//            }
//            break;
//        case "2":

//            Console.WriteLine("Enter book name: ");
//            string title = Console.ReadLine();
//            Console.WriteLine("Enter book description: ");
//            string desc = Console.ReadLine();
//            Console.WriteLine("Enter publish year: ");
//            int publishYear = int.Parse(Console.ReadLine());
//            Console.WriteLine("Enter Author's ID: ");
//            int authorId = int.Parse(Console.ReadLine());

//            bookService.Add(new CreateBookDTO {  Title = title, Description = desc, PublishYear = publishYear, AuthorsId = authorId, });
//            Console.WriteLine("Book added succesfully!");
//            break;
//        case "3":
//            Console.Write("Enter book ID to update: ");
//            int bookId = int.Parse(Console.ReadLine());
//            Console.Write("Enter new title: ");
//            string newTitle = Console.ReadLine();
//            Console.WriteLine("Enter new description: ");
//            string newDesc = Console.ReadLine();
//            Console.WriteLine("Enter new publish year: ");
//            int newPublishYear = int.Parse(Console.ReadLine());
//            bookService.Update(bookId, new UpdateBookDTO { Title = newTitle, Descriptions = newDesc, PublishYear = newPublishYear });

//            Console.WriteLine("Book updated succesfully!");
//            break;
//        case "4":
//            Console.WriteLine("Enter Book Id to delete: ");
//            bookService.Remove(int.Parse( Console.ReadLine() ));
//            Console.WriteLine("Book deleted succesfully!");
//            break;
//        case "0": return;
//        default: Console.WriteLine("Invalid option"); break;
//    }

//}


static void BookActions(IBookService bookService)
{
    Console.Clear();
    Console.WriteLine("1 - List Books");
    Console.WriteLine("2 - Create Book");
    Console.WriteLine("3 - Edit Book");
    Console.WriteLine("4 - Delete Book");
    Console.WriteLine("0 - Exit");

    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            var books = bookService.GetAll();
            if (books.Count == 0)
            {
                Console.WriteLine("There are no books.");
            }
            else
            {
                books.ForEach(book =>
                {
                    string authorNames = book.Authors != null && book.Authors.Count > 0? string.Join(", ", book.Authors.Select(a => a.))
                        : "No Authors";

                    Console.WriteLine($"{book.Id} - {book.Title} - {book.PublishYear} - {authorNames}");
                });
            }
            break;

        case "2":
            Console.WriteLine("Enter book name: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter book description: ");
            string desc = Console.ReadLine();
            Console.WriteLine("Enter publish year: ");
            int publishYear;
            while (!int.TryParse(Console.ReadLine(), out publishYear))
            {
                Console.WriteLine("Invalid input! Please enter a valid year:");
            }

            Console.WriteLine("Enter Author IDs (comma-separated): ");
            string authorsInput = Console.ReadLine();

            List<int> authorIds = authorsInput.Split(',')
                                              .Select(id => int.TryParse(id.Trim(), out int parsedId) ? parsedId : -1)
                                              .Where(id => id > 0)
                                              .ToList();

            if (!authorIds.Any())
            {
                Console.WriteLine("Invalid authors!.");
                return;
            }

            bookService.Add(new CreateBookDTO
            {
                Title = title,
                Description = desc,
                PublishYear = publishYear,
                AuthorsId = authorIds
            });
            Console.WriteLine("Book added successfully!");
            break;

        case "3":
            Console.Write("Enter book ID to update: ");
            int bookId;
            while (!int.TryParse(Console.ReadLine(), out bookId))
            {
                Console.WriteLine("Invalid input! Please enter a valid Book ID:");
            }

            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine();
            Console.WriteLine("Enter new description: ");
            string newDesc = Console.ReadLine();
            Console.WriteLine("Enter new publish year: ");
            int newPublishYear;
            while (!int.TryParse(Console.ReadLine(), out newPublishYear))
            {
                Console.WriteLine("Invalid input! Please enter a valid year:");
            }

            bookService.Update(bookId, new UpdateBookDTO
            {
                Title = newTitle,
                Descriptions = newDesc,
                PublishYear = newPublishYear
            });

            Console.WriteLine("Book updated successfully!");
            break;

        case "4":
            Console.WriteLine("Enter Book ID to delete: ");
            int deleteBookId;
            while (!int.TryParse(Console.ReadLine(), out deleteBookId))
            {
                Console.WriteLine("Invalid input! Please enter a valid Book ID:");
            }
            bookService.Remove(deleteBookId);
            Console.WriteLine("Book deleted successfully!");
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid option");
            break;
    }
}
