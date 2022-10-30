using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSistem.Resources
{
    public class SaveVoteResource
    {
        [Required]
        [MaxLength(250)]
        public string Image { get; set; }

        [Required]
        [MaxLength(250)]
        public string Choise { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime VotingDate { get; set; }
    }
}
