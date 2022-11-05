using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Domain.Services;

namespace VotingSistem.Controllers
{
    [Route("api/voters")]
    [ApiController]
    public class VotersController : ControllerBase
    {
        private readonly IVoterService _voterService;

        public VotersController(IVoterService voterService)
        {
            _voterService = voterService;
        }

        /************************ List All Votes ************************/
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Voter>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IEnumerable<Voter>> GetAllAsync()
        {
            return await _voterService.ListAsync();
        }

        /************************ Read Excel ************************/
        [HttpPost]
        public async Task<IEnumerable<Voter>> Import(IFormFile file)
        {
            return await _voterService.ImportData(file);
        }
    }
}
