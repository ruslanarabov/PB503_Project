using System;
using PB503Project.Services.Interfaces;

namespace PB503Project.DTOs.BookDTO
{
    public class CreateBookDTO 
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public int PublishYear { get; set; }
    }
}

