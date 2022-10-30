using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Persistence.Contexts;
using VotingSistem.Domain.Persistence.Repositories;

namespace VotingSistem.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
