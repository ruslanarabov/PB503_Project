using System;
using PB503Project.DTOs.BorrowerDTO;

namespace PB503Project.Services.Interfaces
{
	public interface IBorrowedService
	{
        void Add(CreateBorrowDTO createBorrowDTO);

        void Remove(int Id);

        void Update(int Id, UpdateBorrowerDTO updateBorrowerDTO);

        List<GetAllBorrowerDTO> GetAll();
    }
}

