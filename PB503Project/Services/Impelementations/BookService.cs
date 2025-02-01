using System;
using PB503Project.DTOs.BookDTO;
using PB503Project.Services.Interfaces;

namespace PB503Project.Services.Impelementations
{
    public class BookService : IBookService
    {
        public void Add(CreateBookDTO createBookDTO)
        {
            
        }

        public List<GetAllBooksDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, UpdateBookDTO updateBookDTO)
        {
            throw new NotImplementedException();
        }
    }
}

