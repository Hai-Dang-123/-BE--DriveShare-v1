
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

        //public async Task<ResponseDTO> DeleteClauseAsync(Guid id)
        //{
        //    var clause = await _unitOfWork.ClausesRepo.GetByIdAsync(id);
        //    if (clause == null)
        //    {
        //        return new ResponseDTO ("Not found clause", 400, false);
        //    }
        //    try
        //    {
        //        await _unitOfWork.ClausesRepo.DeleteAsync(id);
        //        await _unitOfWork.SaveAsync();
        //    }catch (Exception ex)
        //    {
        //        return new ResponseDTO ($"Delete clause failed: {ex.Message}", 500, false);
        //    }

        //    return new ResponseDTO ("Delete clause successfully", 200, true);
        //}
    }
}

