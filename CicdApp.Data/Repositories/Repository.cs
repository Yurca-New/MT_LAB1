using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CicdApp.Data.Data;

namespace CicdApp.Data.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly AppDbContext context;
        protected readonly DbSet<T> dbSet;

        public Repository(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dbSet = this.context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await this.dbSet.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync().ConfigureAwait(false);
        }

        public async Task AddAsync(T entity)
        {
            await this.dbSet.AddAsync(entity).ConfigureAwait(false);
        }

        public Task UpdateAsync(T entity)
        {
            this.dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await this.GetByIdAsync(id).ConfigureAwait(false);
            if (entity != null)
            {
                this.dbSet.Remove(entity);
            }
        }
    }
}