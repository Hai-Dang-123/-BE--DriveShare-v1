using DAL.Context;
using DAL.Repositories.Implement;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DriverShareAppContext _context;
        private bool _disposed = false;
        private IDbContextTransaction? _transaction;

        

        public UnitOfWork(DriverShareAppContext context)
        {
            _context = context;
        }

        

        // Transaction methods
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await _transaction.CommitAsync();
                }
                catch
                {
                    await RollbackTransactionAsync();
                    throw;
                }
                finally
                {
                    await _transaction.DisposeAsync();
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        // Save changes
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await SaveAsync() > 0;
        }

        // Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
