using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Domain.Persistence.Repositories;
using VotingSistem.Domain.Services;
using VotingSistem.Domain.Services.Communications;
using VotingSistem.Services.Algorithms;

namespace VotingSistem.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private DesEncryption _desEncryption;
        private RsaEncryption _rsaEncryption;
        private HybridEncryption _hybridEncryption;

        public VoteService(IVoteRepository voteRepository, IUnitOfWork unitOfWork)
        {
            _voteRepository = voteRepository;
            _unitOfWork = unitOfWork;
            _desEncryption = new DesEncryption();
            _rsaEncryption = new RsaEncryption();
            _hybridEncryption = new HybridEncryption();
        }

        public async Task<IEnumerable<Vote>> ListAsync()
        {
            List<Vote> votes = new List<Vote>();
            IEnumerable<Vote> cypher_votes = await _voteRepository.ListAsync();
            foreach (Vote el in cypher_votes)
            {
                Vote new_vote = new Vote();

                new_vote.VoteId = el.VoteId;
               // new_vote.Image = _desEncryption.decrypt(el.Image);
               // new_vote.Choise = _desEncryption.decrypt(el.Choise);
                new_vote.Image = _hybridEncryption.Decrypt(el.Image);
                new_vote.Choise = _hybridEncryption.Decrypt(el.Choise);
                new_vote.VotingDate = el.VotingDate;

                votes.Add(new_vote);
            }
            return votes;
        }

        public async Task<VoteResponse> SaveAsync(Vote _vote)
        {
            Vote newVote = new Vote();
            //newVote.Image = _desEncryption.encrypt(_vote.Image);
           // newVote.Choise = _desEncryption.encrypt(_vote.Choise.ToLower());

            newVote.Image = _hybridEncryption.Encrypt(_vote.Image);
            newVote.Choise = _hybridEncryption.Encrypt(_vote.Choise.ToLower());
            newVote.VotingDate = _vote.VotingDate;

            try
            {
                await _voteRepository.AddAsync(newVote);
                await _unitOfWork.CompleteAsync();

                _vote.VoteId = newVote.VoteId;
                return new VoteResponse(_vote);
            }
            catch (Exception ex)
            {
                return new VoteResponse($"An error ocurred while saving the vote: {ex.Message}");
            }

        }

        public async Task<int> VoteCounterAsync(string choise)
        {
            return await _voteRepository.CountByChoise(_hybridEncryption.Encrypt(choise.ToLower()));
        }

    }
}
