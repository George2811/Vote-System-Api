using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Domain.Persistence.Contexts;
using VotingSistem.Domain.Persistence.Repositories;

namespace VotingSistem.Persistence.Repositories
{
    public class VoterRepository : BaseRepository, IVoterRepository
    {
        public VoterRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Voter _voter)
        {
            await _context.Voters.AddAsync(_voter);
        }

        public async Task<IEnumerable<Voter>> ListAsync()
        {
            return await _context.Voters.ToListAsync();
        }
    }
}
