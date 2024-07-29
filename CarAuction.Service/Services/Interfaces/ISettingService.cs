using CarAuction.Service.DTOs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface ISettingService: IService<SettingPostDto, SettingUpdateDto>
    {
    }
}
