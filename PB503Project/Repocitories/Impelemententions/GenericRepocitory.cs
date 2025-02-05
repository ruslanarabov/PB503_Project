using System;
using Microsoft.EntityFrameworkCore;
using PB503Project.Data;
using PB503Project.Models;
using PB503Project.Repocitories.Interfaces;

namespace PB503Project.Repocitories.Impelemententions
{
    public class GenericRepository<T> : IGenericRepocitory<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public void Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }

        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public List<T> GetAll()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _appDbContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }
    }
}

