using System;
using Microsoft.EntityFrameworkCore;
using PB503Project.Data;
using PB503Project.Models;
using PB503Project.Repocitories.Interfaces;

namespace PB503Project.Repocitories.Impelemententions
{
    public class BookRepocitory : GenericRepository<Book>, IBookRepocitory
    {
        AppDbContext _appDbContext;

        public BookRepocitory()
        {
            _appDbContext = new AppDbContext();
        }

        public List<Book> GetAllBook()
        {
            return _appDbContext.Set<Book>().Include(x => x.Authors).ToList();
        }

        public Book GetBookById(int id)
        {
            return _appDbContext.Set<Book>().Include(x => x.Authors).FirstOrDefault(x => x.Id == id);
        }
    }
}

