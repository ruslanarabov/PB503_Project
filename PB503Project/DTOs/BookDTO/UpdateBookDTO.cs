using System;
namespace PB503Project.DTOs.BookDTO
{
	public class UpdateBookDTO
	{
		public string Descriptions { get; set; }

		public string Title { get; set; }

		public int PublishYear { get; set; }

		public List<int> AuthorsId { get; set; }

		
	}
}


