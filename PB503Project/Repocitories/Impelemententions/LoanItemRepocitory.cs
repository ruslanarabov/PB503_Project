using System;
using Microsoft.EntityFrameworkCore;
using PB503Project.Data;
using PB503Project.Models;
using PB503Project.Repocitories.Interfaces;

namespace PB503Project.Repocitories.Impelemententions
{
	public class LoanItemRepocitory : GenericRepository<LoanItem>, ILoanItemRepocitory
	{
        AppDbContext _appDbContext;

        public LoanItemRepocitory()
        {
            _appDbContext = new AppDbContext();
        }

        public List<LoanItem> GetAllLoanItem()
        {
            return _appDbContext.Set<LoanItem>().Include(x => x.Loan).Include(x=> x.Book).ToList();
        }

        public LoanItem GetLoanItemById(int id)
        {
            return _appDbContext.Set<LoanItem>().Include(x => x.Loan).Include(x=> x.Book).FirstOrDefault(x => x.Id == id);
        }
    }
}

