using System;
using PB503Project.DTOs.LoanDTO;

namespace PB503Project.Services.Interfaces
{
	public interface ILoanService
	{
		void Add(CreateLoanDTO createLoanDTO);

		void Remove(int Id);

		void Update(int Id, UpdateLoanDTO updateLoanDTO);

		List<GetAllLoanDTO> GetAll();
	}
}

