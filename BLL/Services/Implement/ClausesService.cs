
using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTOs;
using Common.Messages;
using DAL.Entities;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ClausesService : IClauseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;

        public ClausesService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        public Task<ResponseDTO> DeleteClauseAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

