using System;
using PB503Project.AllExceptions;
using PB503Project.DTOs.AuthorDTO;
using PB503Project.DTOs.BorrowerDTO;
using PB503Project.Models;
using PB503Project.Repocitories.Impelemententions;
using PB503Project.Repocitories.Interfaces;
using PB503Project.Services.Interfaces;

namespace PB503Project.Services.Impelementations
{
    public class BorrowerService : IBorrowedService
    {
        private readonly IBorrowerRepocitory _borrowerRepocitory;

        public BorrowerService()
        {
            _borrowerRepocitory = new BorrowerRepocitory();                      
        }
        

        public void Add(CreateBorrowDTO createBorrowDTO)
        {
            var borrower = new Borrower { Name = createBorrowDTO.Name };
            _borrowerRepocitory.Add(borrower);
            _borrowerRepocitory.Commit();
        }

        public List<GetAllBorrowerDTO> GetAll()
        {
            var borrower = _borrowerRepocitory.GetAll();
            if (borrower is null)
            {
                throw new InvalidInputException("There is no borrower!");
            }
            return _borrowerRepocitory.GetAll().Select(borrower => new GetAllBorrowerDTO
            {
                Id = borrower.Id,
                Email = borrower.Email,
                Name = borrower.Name
                
            }).ToList();
        }

        public void Remove(int Id)
        {
            var borrower = _borrowerRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (borrower is null)
            {
                throw new InvalidIdException("Borrower ID not found to delete!");
            }
            _borrowerRepocitory.Delete(borrower);
            _borrowerRepocitory.Commit();
        }

        public void Update(int Id, UpdateBorrowerDTO updateBorrowerDTO)
        {
            var borrower = _borrowerRepocitory.GetAll().FirstOrDefault(b => b.Id == Id);
            if (borrower is null)
            {
                throw new InvalidIdException("Borrower ID not found to update!");
            }
            borrower.Name = updateBorrowerDTO.Name;
            _borrowerRepocitory.Commit();
        }
    }
}

