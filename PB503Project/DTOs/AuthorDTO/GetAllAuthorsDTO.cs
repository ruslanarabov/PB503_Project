using System;
namespace PB503Project.DTOs.AuthorDTO
{
	public class GetAllAuthorsDTO
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> BookTitles { get; set; }
    }
}

