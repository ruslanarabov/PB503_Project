

using PB503Project.Services.Impelementations;
using PB503Project.Services.Interfaces;

IBookService bookService = new BookService();
IAuthorService authorService = new AuthorService();
IBorrowedService borrowedService = new BorrowerService();
//ILoanService loanService = new LoanService();

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
        
        default:
            break;
    }
}
