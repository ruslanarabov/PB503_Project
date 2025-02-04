using System;
namespace PB503Project.DTOs.LoanDTO
{
	public class GetAllLoanDTO
	{       
        public int BorrowerId { get; set; }       

        public DateTime LoanDate { get; set; }

        public DateTime MustReturnDate { get; set; }
     
        public List<string> BookTitles { get; set; }
    }
}


