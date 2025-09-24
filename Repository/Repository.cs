using System;
using Bulky.Models;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyBookWeb.Repository.IRepository;
using BulkyBookWeb.Data;
using BulkyBookWeb.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BulkyBookWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }


        public void Add(T entity) {
            dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filter) {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();

        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }


        public void Remove(T entity) {

            dbSet.Remove(entity);


        }
        public void RemoveRange(IEnumerable<T> entity) {
            dbSet.RemoveRange(entity);
        }
    }
} 

