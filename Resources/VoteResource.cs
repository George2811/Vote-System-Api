using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSistem.Resources
{
    public class VoteResource
    {
        public long VoteId { get; set; }
        public string Image { get; set; }
        public string Choise { get; set; }
        public DateTime VotingDate { get; set; }
    }
}
