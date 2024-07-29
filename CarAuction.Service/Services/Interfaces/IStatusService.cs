﻿using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Statuses;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface IStatusService : IService<StatusPostDto,StatusUpdateDto>
    {
    }
}
