using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;

namespace VotingSistem.Domain.Services
{
    public interface IVoterService
    {
        Task<IEnumerable<Voter>> ListAsync();
        Task<IEnumerable<Voter>> ImportData(IFormFile file);

    }
}
