//using BLL.Services.Interface;
//using BLL.Utilities;
//using Common.DTOs;
//using Common.Messages;
//using DAL.Entities;
//using DAL.UnitOfWork;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BLL.Services.Implement
//{
//    public class ClausesService : IClausesService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly UserUtility _userUtility;

//        public ClausesService(IUnitOfWork unitOfWork, UserUtility userUtility)
//        {
//            _unitOfWork = unitOfWork;
//            _userUtility = userUtility;
//        }

//        public async Task<ResponseDTO> CreateClauseAsync(CreateClauseDTO createClauseDTO)
//        {
//            var userId = _userUtility.GetUserIdFromToken();
//            if (userId == Guid.Empty)
//            {
//                return new ResponseDTO("Unauthorized", 401, false);
//            }
//            if (createClauseDTO == null)
//            {
//                return new ResponseDTO("Invalid input", 400, false);
//            }
//            try
//            {
//                var newClause = new ClauseTemplate
//                {
//                    ClauseId = Guid.NewGuid(),
//                    Version = createClauseDTO.Version,
//                    Title = createClauseDTO.Description,

//                };
                
//                await _unitOfWork.ClausesRepo.AddAsync(newClause);
//                await _unitOfWork.SaveAsync();
                
//            }
//            catch (Exception ex)
//            {
//                return new ResponseDTO($"Error: {ex.Message}", 500, false);
//            }

//            return new ResponseDTO(ClauseMessages.ClauseCreated, 201, true);

//        }

//        public  async Task<ResponseDTO> DeleteClauseAsync(Guid id)
//        {
//           var userId = _userUtility.GetUserIdFromToken();
//            if (userId == Guid.Empty)
//            {
//                return new ResponseDTO("Unauthorized", 401, false);
//            }
//            var clause = await _unitOfWork.ClausesRepo.GetByIdAsync(id);
//            if (clause == null)
//            {
//                return new ResponseDTO("Clause not found", 404, false);
//            }
//            try
//            {
//                _unitOfWork.ClausesRepo.Delete(clause);
//                await _unitOfWork.SaveAsync();
//            }
//            catch (Exception ex)
//            {
//                return new ResponseDTO($"Error: {ex.Message}", 500, false);
//            }

//            return new ResponseDTO(ClauseMessages.ClauseDeleted, 200, true);
//        }

//        public async Task<ResponseDTO> GetAllClauseAsync()
//        {
//           var userId = _userUtility.GetUserIdFromToken();
//            if (userId == Guid.Empty)
//            {
//                return new ResponseDTO("Unauthorized", 401, false);
//            }
//            try
//            {
//                var clauses = await _unitOfWork.ClausesRepo.GetAll().ToListAsync();

//                var clauseDtos = clauses.Select(c => new GetClauseDTO
//                {
//                    ClauseId = c.ClauseId,
//                    Description = c.Title,
//                    Version = c.Version
//                }).ToList();

//                return new ResponseDTO(ClauseMessages.GetClause, 200, true, clauseDtos);

//            }
//            catch (Exception ex)
//            {
//                return new ResponseDTO($"Error: {ex.Message}", 500, false);
//            }
//        }

//        public async Task<ResponseDTO> GetClauseByIdAsync(Guid id)
//        {
//            var userId = _userUtility.GetUserIdFromToken();
//            if (userId == Guid.Empty)
//            {
//                return new ResponseDTO("Unauthorized", 401, false);
//            }
//            var clause = await _unitOfWork.ClausesRepo.GetByIdAsync(id);
//            if (clause == null)
//            {
//                return new ResponseDTO("Clause not found", 404, false);
//            }

//            var clauseDto = new GetClauseDTO
//            {
//                ClauseId = clause.ClauseId,
//                Version = clause.ClauseVersion,
//                Description = clause.ClauseContent
//            };

//            return new ResponseDTO(ClauseMessages.GetClause, 200, true, clauseDto);
//        }

//        public async Task<ResponseDTO> UpdateClauseAsync(UpdateClauseDTO updateClauseDTO)
//        {
//            var userId = _userUtility.GetUserIdFromToken();
//            if (userId == Guid.Empty)
//            {
//                return new ResponseDTO("Unauthorized", 401, false);
//            }
//            var clause = await _unitOfWork.ClausesRepo.GetByIdAsync(updateClauseDTO.ClauseId);
//            if (clause == null)
//            {
//                return new ResponseDTO("Clause not found", 404, false);
//            }
//            try
//            {
//                clause.ClauseVersion = updateClauseDTO.Version;
//                clause.ClauseContent = updateClauseDTO.Description;
//                await _unitOfWork.ClausesRepo.UpdateAsync(clause);
//                await _unitOfWork.SaveAsync();
//            }
//            catch (Exception ex)
//            {
//                return new ResponseDTO($"Error: {ex.Message}", 500, false);
//            }
//            return new ResponseDTO(ClauseMessages.ClauseUpdated, 200, true);
//        }
//    }
//}
