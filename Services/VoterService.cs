using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS.Core;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Domain.Persistence.Repositories;
using VotingSistem.Domain.Services;
using VotingSistem.Domain.Services.Communications;

namespace VotingSistem.Services
{
    public class VoterService : IVoterService
    {
        private readonly IVoterRepository _voterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VoterService(IVoterRepository voterRepository, IUnitOfWork unitOfWork)
        {
            _voterRepository = voterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Voter>> ImportData(IFormFile file)
        {
            var list = new List<Voter>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowcount; row++)
                    {
                        Voter newVoter = new Voter
                        {
                            VoterId = 0,
                            Name = worksheet.Cells[row, 1].Value.ToString().Trim()
                        };
                        try
                        {
                            await _voterRepository.AddAsync(newVoter);
                            await _unitOfWork.CompleteAsync();
                            list.Add(newVoter);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"An error ocurred while saving the voter: {ex.Message}");
                        }

                    }
                }
            }
            return list;
        }

        public async Task<IEnumerable<Voter>> ListAsync()
        {
            return await _voterRepository.ListAsync();
        }
    }
}
