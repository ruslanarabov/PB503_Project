using System;
using Microsoft.EntityFrameworkCore;
using PB503Project.Data;
using PB503Project.Models;
using PB503Project.Repocitories.Interfaces;

namespace PB503Project.Repocitories.Impelemententions
{
	public class BorrowerRepocitory : GenericRepository<Borrower>, IBorrowerRepocitory
	{
		AppDbContext _appDbContext;

		public BorrowerRepocitory()
		{
            _appDbContext = new AppDbContext();
        }

		public List<Borrower> GetAllBorrower()
		{
			return _appDbContext.Set<Borrower>().Include(x => x.Loans).ToList();
		}

		public Borrower GetBorrowerById(int id)
		{
			return _appDbContext.Set<Borrower>().Include(x => x.Loans).FirstOrDefault(x => x.Id == id);
		}
	}
}

