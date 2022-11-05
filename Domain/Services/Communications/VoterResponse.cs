using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;

namespace VotingSistem.Domain.Services.Communications
{
    public class VoterResponse : BaseResponse<Voter>
    {
        public VoterResponse(Voter resource) : base(resource)
        {
        }

        public VoterResponse(string message) : base(message)
        {
        }
    }
}
