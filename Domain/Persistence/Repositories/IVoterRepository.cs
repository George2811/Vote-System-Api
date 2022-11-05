using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;

namespace VotingSistem.Domain.Persistence.Repositories
{
    public interface IVoterRepository
    {
        Task<IEnumerable<Voter>> ListAsync();
        Task AddAsync(Voter _voter);
    }
}
