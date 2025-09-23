﻿using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IUserService
    {
        Task<ResponseDTO> CreateUserAsync(CreateUserDTO dto);
        Task<ResponseDTO> DeleteUserAsync();
    }
}
