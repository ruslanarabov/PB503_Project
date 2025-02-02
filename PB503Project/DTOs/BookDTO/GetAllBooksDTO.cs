using System;
namespace PB503Project.DTOs.BookDTO
{
	public class GetAllBooksDTO
	{
		public int Id { get; set; }

		public List<string> Authors { get; set; }

		public bool IsBorow { get; set; }

		public string Title { get; set; }

		public string Descriptions { get; set; }

		public int PublishYear { get; set; }

	}

}


