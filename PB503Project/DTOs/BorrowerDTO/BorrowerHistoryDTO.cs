using System;
namespace PB503Project.DTOs.BorrowerDTO
{
	public class BorrowerHistoryDTO
	{
        public string BookTitle { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}

