using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Persistence.Contexts;

namespace VotingSistem.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
