using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Domain.Persistence.Contexts;
using VotingSistem.Domain.Persistence.Repositories;

namespace VotingSistem.Persistence.Repositories
{
    public class VoteRepository : BaseRepository, IVoteRepository
    {
        public VoteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Vote _vote)
        {
            await _context.Votes.AddAsync(_vote);
        }

        public async Task<int> CountByChoise(string choise)
        {
            IEnumerable<Vote> votes = await _context.Votes
                .Where(vo => vo.Choise == choise)
                .ToListAsync();

            return votes.Count();
        }

        public async Task<IEnumerable<Vote>> ListAsync()
        {
            return await _context.Votes.ToListAsync();
        }
    }
}
