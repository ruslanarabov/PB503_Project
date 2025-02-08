using System;
namespace PB503Project.Models
{
	public class Loan : BaseEntity
	{
		public int BookId { get; set; }

		public int BorrowId { get; set; }

		public Borrower Borrower { get; set; }

		public DateTime LoanDate { get; set; }

		public DateTime MustReturnDate { get; set; }

		public DateTime? ReturnTime { get; set; }

		public List<LoanItem> LoanItems { get; set; }

		public DateTime BorrowDate { get; set; }
	}
}





