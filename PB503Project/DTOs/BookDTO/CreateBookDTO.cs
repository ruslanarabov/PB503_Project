using System;
using PB503Project.Services.Interfaces;

namespace PB503Project.DTOs.BookDTO
{
    public class CreateBookDTO 
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public int PublishYear { get; set; }

        public List<int> AuthorsId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}


