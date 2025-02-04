using System;
namespace PB503Project.Models
{
	public class Author : BaseEntity
	{

        public string Name { get; set; } 

        public List<Book> Books { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }

}

