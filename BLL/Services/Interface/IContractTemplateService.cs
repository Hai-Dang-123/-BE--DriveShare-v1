﻿using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IContractTemplateService
    {
        Task<ResponseDTO>CreateContractTemplateAsync(ContractTemplateDTO contractTemplateDTO);
    }
}
