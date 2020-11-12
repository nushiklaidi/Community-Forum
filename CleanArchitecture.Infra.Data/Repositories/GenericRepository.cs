using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            this.dbSet = _appDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            List<T> query = await dbSet.ToListAsync<T>();
            return query;
        }

        public async Task<IEnumerable<T>> GetAll(string includeProperties)
        {
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var arrProperties = includeProperties.Split(',');
                return await GetQuery(includeProperties: arrProperties).ToListAsync<T>();
            }
            else
            {
                List<T> query = await dbSet.ToListAsync<T>();
                return query;
            }
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter, string[] includeProperties)
        {
            return GetQuery(filter, includeProperties).ToList();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter, string includeProperties = "")
        {
            var properties = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return Get(filter, properties);
        }

        public async Task<T> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
             dbSet.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            T entityToDelete = await dbSet.FindAsync(id);
            await Delete(entityToDelete);
        }

        public async Task Delete(T entity)
        {
             dbSet.Remove(entity);
             await _appDbContext.SaveChangesAsync();
        }

        protected virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null, string[] includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);            
            //Included properties
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return query;
        }
    }
}
