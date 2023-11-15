using EcommerceApplication.Common.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        DbSet<T> GetEntities<T>() where T : class;
        void Add<T>(T obj) where T : class;
        Task AddAsync<T>(T obj) where T : class;
        void Update<T>(T obj) where T : class;
        void Remove<T>(T obj) where T : class;
        Task<bool> CommitAsync();
        Task<int> CommitAsync(CancellationToken cancellationToken);


    }
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(assignmentDb context)
        {
            Context = context;
        }

        public assignmentDb Context { get; private set; }

        public void Add<T>(T obj)
                   where T : class
        {
            var set = Context.Set<T>();
            set.Add(obj);
        }
        public async Task AddAsync<T>(T obj)
            where T : class
        {
            var set = Context.Set<T>();
            await set.AddAsync(obj);
        }
        public void Update<T>(T obj)
            where T : class
        {
            var set = Context.Set<T>();
            set.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
        }
        void IUnitOfWork.Remove<T>(T obj)
        {
            var set = Context.Set<T>();
            set.Remove(obj);
        }
        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> CommitAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

        public DbSet<T> GetEntities<T>() where T : class
        {
            return Context.Set<T>();
        }

        public void Dispose()
        {
            Context = null;
        }
        public IQueryable<T> Query<T>()
           where T : class
        {
            return Context.Set<T>();
        }

        public bool Commit()
        {
            return Context.SaveChanges() > 0;
        }
    }
}
