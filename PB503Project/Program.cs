

using System.ComponentModel;
using PB503Project;
using PB503Project.AllExceptions;
using PB503Project.DTOs.AuthorDTO;
using PB503Project.DTOs.BookDTO;
using PB503Project.DTOs.BorrowerDTO;
using PB503Project.Services.Impelementations;
using PB503Project.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        case "5": ReturnBook(loanService); break;
        case "6": GetMostBorrowedBook(loanService); break;
        case "7": GetOverdueBorrowers(loanService); break;
        case "8": GetBorrowerHistory(loanService); break;
        case "9": FilterBooksByTitle(bookService); break;
        case "10": FilterBooksByAuthor(bookService); break;
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
            if(newName.Any(char.IsDigit) || string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Author name cannot contain number.");
            }
            try
            {
                authorService.Update(uptdAuthorId, new UpdateAuthorDTO { Name = newName});
                Console.WriteLine("Author updated succesfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            break;
        case "4":
            Console.Write("Enter author ID to delete: ");
            int deleteAuthorId;
            while (!int.TryParse(Console.ReadLine(), out deleteAuthorId))
            {
                Console.WriteLine("Invalid input! Please enter a valid Author ID:");
            }
            try
            {
                authorService.Remove(deleteAuthorId);
                Console.WriteLine("Author deleted succesfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
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
                throw new InvalidInputException("There is no book!");
            }
            else
            {
                books.ForEach(book =>
                {
                    string authorNames = book.Authors != null && book.Authors.Any()
                    ? string.Join(", ", book.Authors.Select(a => a)) : "No Author";
                    Console.WriteLine($"{book.Id} - {book.Title} - {book.Descriptions} - {book.PublishYear} - Authors: {authorNames}");
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
                Console.WriteLine("Invalid input! Please enter a valid year:");
            }
            try
            {
                bookService.Update(bookId, new UpdateBookDTO
                {
                    Title = newTitle,
                    Descriptions = newDesc,
                    PublishYear = newPublishYear
                });

                Console.WriteLine("Book name, Desc and Publish Year updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            break;

        case "4":
            Console.WriteLine("Enter Book ID to delete: ");
            int deleteBookId;
            while (!int.TryParse(Console.ReadLine(), out deleteBookId))
            {
                Console.WriteLine("Invalid input! Please enter a valid Book ID:");
            }
            try
            {
                bookService.Remove(deleteBookId);
                Console.WriteLine("Book deleted succesfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            break; ;

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
            Console.Write("Enter borrower name: ");
            string borrowerName = Console.ReadLine()?.Trim();            
            if (string.IsNullOrWhiteSpace(borrowerName) || borrowerName.Any(char.IsDigit))
            {
                Console.WriteLine("Borrower name cannot be empty or contain numbers.");
                return;
            }

            Console.Write("Enter borrower e-mail: ");
            string borrowerEmail = Console.ReadLine()?.Trim();          
            if (string.IsNullOrWhiteSpace(borrowerEmail))
            {
                Console.WriteLine("Borrower email cannot be empty.");
                return;
            }         
            borrowerService.Add(new CreateBorrowDTO
            {
                Name = borrowerName,
                Email = borrowerEmail
            });

            Console.WriteLine("Borrower successfully created!");
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
                break;
            }
            Console.WriteLine("Enter new E-mail: ");
            string newBorrowerEmail = Console.ReadLine();
            try
            {
                borrowerService.Update(uptdBorrowerId, new UpdateBorrowerDTO
                {
                    Name = newBorrowerName,
                    Email = newBorrowerEmail
                });

                Console.WriteLine("Borrower name and e-mail updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            break;
        case "4":
            Console.WriteLine("Enter Borrower Id to delete: ");
            int deleteBorrower;
            while (!int.TryParse(Console.ReadLine(), out deleteBorrower))
            {
                Console.WriteLine("Invalid input! Please enter a valid Borrower ID:");
            }
            try
            {
                borrowerService.Remove(deleteBorrower);
                Console.WriteLine("Borrower deleted succesfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
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



static void ReturnBook(ILoanService loanService)
{
    try
    {
        Console.Clear();
        Console.Write("Enter Loan ID: ");
        int loanId;
        if (!int.TryParse(Console.ReadLine(), out loanId))
        {
            Console.WriteLine("Invalid Loan ID format. Please enter a valid number.");
            return;
        }
        loanService.ReturnBook(loanId);

        Console.WriteLine("Book returned successfully.");
    }
    catch (InvalidOperationException ex) 
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (Exception ex) 
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}


static void GetMostBorrowedBook(ILoanService loanService)
{
    var book = loanService.GetMostBorrowedBook();

    Console.WriteLine($"Most borrowed book: {book.Title} - Borrowed {book.LoanItem}");
}

static void GetOverdueBorrowers(ILoanService loanService)
{
    var overdueBorrowers = loanService.GetOverdueBorrowers();
    Console.WriteLine("Overdue Borrowers:");
    overdueBorrowers.ForEach(b => Console.WriteLine($"{b.Id} - {b.Name}"));
}

static void GetBorrowerHistory(ILoanService loanService)
{
    try
    {
        Console.Clear();
        Console.Write("Enter Borrower ID: ");        
        int borrowerId;
        if (!int.TryParse(Console.ReadLine(), out borrowerId))
        {
            Console.WriteLine("Invalid Borrower ID format. Please enter a valid number.");
            return;
        }     
        var history = loanService.GetBorrowerHistory(borrowerId);

        if (history == null || history.Count == 0)
        {
            Console.WriteLine("No history found for the given borrower.");
        }
        else
        {
            Console.WriteLine("Borrower History:");
            history.ForEach(h => Console.WriteLine($"Book: {h.BookTitle}, Borrowed On: {h.BorrowDate}, Returned On: {h.ReturnDate}"));
        }
    }
    catch (ArgumentException ex) 
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (Exception ex) 
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}



static void FilterBooksByTitle(IBookService bookService)
{
    Console.Clear();
    Console.Write("Search by title: ");
    var keyWord = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(keyWord) || keyWord.Any(char.IsDigit))
    {
        Console.WriteLine("Search keyword cannot be empty or contains number, Please try again.");
        return;
    }

    var datas = bookService.GetAll()
                           .Where(b => b.Title.Trim().ToLower()
                           .Contains(keyWord.Trim().ToLower()))
                           .ToList();

    if (datas.Count == 0)
    {
        Console.WriteLine("Book not found.");
        return;
    }

    Console.Clear();
    foreach (var data in datas)
    {
        Console.WriteLine($"{data.Id} - {data.Title} - {data.PublishYear} - {string.Join(", ", data.Authors)}");
    }
}

static void FilterBooksByAuthor(IBookService bookService)
{
    Console.Clear();
    Console.Write("Search by author name: ");
    var keyWord = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(keyWord) || keyWord.Any(char.IsDigit))
    {
        Console.WriteLine("Author name cannot be empty or contain numbers. Please try again.");
        return;
    }

    var books = bookService.GetAll()
                       .Where(b => b.Authors.Any(a => a.Trim().ToLower()
                       .Contains(keyWord.Trim().ToLower())))
                       .ToList();

    if (books.Count == 0)
    {
        Console.WriteLine("No books found for this author.");
        return;
    }

    Console.Clear();
    foreach (var book in books)
    {

        Console.WriteLine($"{book.Id} - {book.Title} - {book.PublishYear} - {string.Join(", ", book.Authors)}");
    }
}



