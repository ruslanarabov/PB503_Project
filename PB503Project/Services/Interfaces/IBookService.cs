using System;
using PB503Project.DTOs.BookDTO;

namespace PB503Project.Services.Interfaces
{
	public interface IBookService
	{
		void Add(CreateBookDTO createBookDTO);

		void Remove(int Id);

		void Update(int Id, UpdateBookDTO updateBookDTO );

		List<GetAllBooksDTO> GetAll();
	}
}

