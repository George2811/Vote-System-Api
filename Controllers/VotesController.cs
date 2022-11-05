using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Domain.Services;
using VotingSistem.Extensions;
using VotingSistem.Resources;

namespace VotingSistem.Controllers
{
    [Route("api/votes")]
    [Produces("application/json")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService _voteService;
        private readonly IMapper _mapper;

        public VotesController(IVoteService voteService, IMapper mapper)
        {
            _voteService = voteService;
            _mapper = mapper;
        }

        /************************ List All Votes ************************/
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VoteResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IEnumerable<VoteResource>> GetAllAsync()
        {
            var votes = await _voteService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Vote>, IEnumerable<VoteResource>>(votes);
            return resources;
        }

        /************************ Count Votes By Choise ************************/
        [HttpGet("counter")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<int> GetVotes(string choise)
        {
            var votes = await _voteService.VoteCounterAsync(choise);
            return votes;
        }

        /************************ Save Vote ************************/
        [HttpPost]
        [ProducesResponseType(typeof(VoteResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveVoteResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var _vote = _mapper.Map<SaveVoteResource, Vote>(resource);
            var result = await _voteService.SaveAsync(_vote);

            if (!result.Success)
                return BadRequest(result.Message);

            var voteResource = _mapper.Map<Vote, VoteResource>(result.Resource);
            return Ok(voteResource);
        }

    }
}
