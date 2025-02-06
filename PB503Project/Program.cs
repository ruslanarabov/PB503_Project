

using PB503Project;
using PB503Project.DTOs.AuthorDTO;
using PB503Project.DTOs.BookDTO;
using PB503Project.DTOs.BorrowerDTO;
using PB503Project.Services.Impelementations;
using PB503Project.Services.Interfaces;

IBookService bookService = new BookService();
IAuthorService authorService = new AuthorService();
IBorrowedService borrowerService = new BorrowerService();
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
        case "3": BorrowerActions(borrowerService); break;
        case "4": BorrowBook(loanService, bookService, borrowerService); break;
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
            int uptdAuthorId;
            while (!int.TryParse(Console.ReadLine(), out uptdAuthorId))
            {
                Console.WriteLine("Invalid input! Please enter a valid Author ID:");
            }
            
            Console.Write("Enter new name: ");
            string newName = Console.ReadLine();
            if(newName.Any(char.IsDigit))
            {
                Console.WriteLine("Author name cannot contain number.");
            }
            else
            {
                authorService.Update(uptdAuthorId, new UpdateAuthorDTO
                {
                    Name = Console.ReadLine()
                });
                Console.WriteLine("Author updated succesfully!");
            }
            break;
        case "4":
            Console.Write("Enter author ID to delete: ");
            int deleteAuthorId;
            while (!int.TryParse(Console.ReadLine(), out deleteAuthorId))
            {
                Console.WriteLine("Invalid input! Please enter a valid Author ID:");
            }
            authorService.Remove(deleteAuthorId);
            Console.WriteLine("Author deleted succesfully!");
            break;
        case "0": return;
        default: Console.WriteLine("Invalid option"); break;
    }
}



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
                Console.WriteLine("There is no books");
            }
            else
            {
                books.ForEach(book => Console.WriteLine($"{book.Id} - {book.Title} - {book.PublishYear} - {book.Authors}"));
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

            Console.WriteLine("Enter Author IDs: ");
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
                Console.WriteLine("Invalid input! Please enter a valid year:");            //BOS BOSUNA YAZDIGIM KOD ISLEMIR BU (isledi 100 dene sey deyisennen sora(supheli) )
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


static void BorrowerActions(IBorrowedService borrowerService)
{
    Console.Clear();
    Console.WriteLine("1 - List Borrower");
    Console.WriteLine("2 - Create Borrower");
    Console.WriteLine("3 - Edit Borrower");
    Console.WriteLine("4 - Delete Borrower");
    Console.WriteLine("0 - Exit");
    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            var borrowers = borrowerService.GetAll();
            if (borrowers.Count == 0)
            {
                Console.WriteLine("There is no borrower!");
            }
            else
            {
                borrowers.ForEach(borrower => Console.WriteLine($"Borrwer Id: {borrower.Id} - Borrower Name: {borrower.Name} - Borrower e-mail: {borrower.Email}"));
            }
            break;
        case "2":
            Console.WriteLine("Enter borrower name: ");
            string borrowerName = Console.ReadLine();
            if(borrowerName.Any(char.IsDigit))
            {
                Console.WriteLine("Borrower name cannot contain number.");
            }
            else
            {
                borrowerService.Add(new CreateBorrowDTO { Name = Console.ReadLine() } );
                Console.WriteLine("Borrower name succesfully created!");
            }

            Console.WriteLine("Enter Borrower e-mail: ");
            string borrowerEmail = Console.ReadLine();
            borrowerService.Add(new CreateBorrowDTO { Email = Console.ReadLine() });
            Console.WriteLine("Borrower e-mail succesfully created!");
            break;

        case "3":
            Console.Write("Enter borrower ID to update: ");
            int uptdBorrowerId;
            while (!int.TryParse(Console.ReadLine(), out uptdBorrowerId))
            {
                Console.WriteLine("Invalid input! Please enter a valid Borrower ID: ");
            }
            Console.WriteLine("Enter new name: ");
            string newBorrowerName = Console.ReadLine();
            if (newBorrowerName.Any(char.IsDigit))
            {
                Console.WriteLine("Borrower name cannot contain number.");
            }
            Console.WriteLine("Enter new E-mail: ");
            string newBorrowerEmail = Console.ReadLine();

            borrowerService.Update(uptdBorrowerId, new UpdateBorrowerDTO
            {
                Name = newBorrowerName,
                Email = newBorrowerEmail
            });

            Console.WriteLine("Borrower name and e-mail updated succesfully!");
            break;
        case "4":
            Console.WriteLine("Enter Borrower Id to delete: ");
            int deleteBorrower;
            while (!int.TryParse(Console.ReadLine(), out deleteBorrower))
            {
                Console.WriteLine("Invalid input! Please enter a valid Borrower ID:");
            }

            borrowerService.Remove(deleteBorrower);
            Console.WriteLine("Borrower deleted succesfully!");
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid option");
            break;
    }

}


static void BorrowBook(ILoanService loanService, IBookService bookService, IBorrowedService borrowerService)
{
    Console.Clear();
    Console.WriteLine("Borrow a Book");
    Console.Write("Enter Borrower ID: ");
    int borrowerId = int.Parse(Console.ReadLine());
    Console.Write("Enter Book ID: ");
    int bookId = int.Parse(Console.ReadLine());

    try
    {
        loanService.BorrowBook(borrowerId, bookId);
        Console.WriteLine("Book borrowed successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}



