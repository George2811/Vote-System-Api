using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Domain.Services.Communications;

namespace VotingSistem.Domain.Services
{
    public interface IVoteService
    {
        Task<IEnumerable<Vote>> ListAsync();
        Task<int> VoteCounterAsync(string choise);
        Task<VoteResponse> SaveAsync(Vote _vote);
    }
}
