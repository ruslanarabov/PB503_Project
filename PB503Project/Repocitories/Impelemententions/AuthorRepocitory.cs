using System;
using Microsoft.EntityFrameworkCore;
using PB503Project.Data;
using PB503Project.Models;
using PB503Project.Repocitories.Interfaces;

namespace PB503Project.Repocitories.Impelemententions
{
    public class AuthorRepocitory : GenericRepository<Author>, IAuthorRepocitory
    {
        AppDbContext _appDbContext;

        public AuthorRepocitory()
        {
            _appDbContext = new AppDbContext();
        }

        public List<Author> GetAllAuthors()
        {
            return _appDbContext.Set<Author>().Include(x => x.Books).ToList();
        }

        public Author GetAuthorById(int id)
        {
            return _appDbContext.Set<Author>().Include(x => x.Books).FirstOrDefault(x => x.Id == id);
        }
    }
}

