using System;
namespace PB503Project.Models
{
	public class Borrower : BaseEntity
	{
		public string Name { get; set; } 

		public string Email { get; set; } 

		public List<Loan> Loans { get; set; }

		public DateTime UpdateTime { get; set; }
	}
}


