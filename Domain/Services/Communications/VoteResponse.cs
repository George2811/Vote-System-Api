using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;

namespace VotingSistem.Domain.Services.Communications
{
    public class VoteResponse : BaseResponse<Vote>
    {
        public VoteResponse(Vote resource) : base(resource)
        {
        }

        public VoteResponse(string message) : base(message)
        {
        }
    }
}
