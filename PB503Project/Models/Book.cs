using System;
using PB503Project.Models;

namespace PB503Project
{
	public class Book : BaseEntity
	{

		public string Title { get; set; } 

		public string Desc { get; set; } 

		public int PublishYear { get; set; }

		public List<Author> Authors { get; set; }

		public LoanItem LoanItem { get; set; }


	}
}

