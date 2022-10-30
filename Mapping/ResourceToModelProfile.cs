using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Resources;

namespace VotingSistem.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveVoteResource, Vote>();
        }
    }
}
