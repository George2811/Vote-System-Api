using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;

namespace VotingSistem.Domain.Persistence.Repositories
{
    public interface IVoteRepository
    { //  La key de encriptación debe ser configurada como variable de entorno
        Task<IEnumerable<Vote>> ListAsync();
        Task<int> CountByChoise(string choise); // Devuelve el numero de personas que votaron por esa elección
        Task AddAsync(Vote _vote); // Encripta y guarda el voto
    }
}
