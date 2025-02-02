using System;
using Microsoft.EntityFrameworkCore;
using PB503Project.Data;
using PB503Project.Models;
using PB503Project.Repocitories.Interfaces;

namespace PB503Project.Repocitories.Impelemententions
{
	public class LoanRepocitory : GenericRepository<Loan>, ILoanRepocitory
	{
        AppDbContext _appDbContext;

        public LoanRepocitory()
        {
            _appDbContext = new AppDbContext();
        }

        public List<Loan> GetAllLoan()
        {
            return _appDbContext.Set<Loan>().Include(x => x.LoanItems).Include(x=> x.Borrower).ToList();
        }

        public Loan GetLoanById(int id)
        {
            return _appDbContext.Set<Loan>().Include(x => x.LoanItems).Include(x => x.Borrower).FirstOrDefault(x => x.Id == id);
        }
    }
}

