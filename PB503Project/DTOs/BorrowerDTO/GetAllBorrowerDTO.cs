using System;
using PB503Project.DTOs.BookDTO;

namespace PB503Project.DTOs.BorrowerDTO
{
	public class GetAllBorrowerDTO
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<GetAllBooksDTO> BorrowedBooks { get; set; } = new List<GetAllBooksDTO>();
    }
}


