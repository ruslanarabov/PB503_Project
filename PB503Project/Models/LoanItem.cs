using System;
namespace PB503Project.Models
{
	public class LoanItem : BaseEntity
	{
		public int BookId { get; set; }

		public int LoanId { get; set; }

		public Loan Loan { get; set; }

		public Book Book { get; set; }


	}
}

