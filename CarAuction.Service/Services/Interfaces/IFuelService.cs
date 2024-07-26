using CarAuction.Service.DTOs.Fuels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface IFuelService: IService<FuelPostDto, FuelUpdateDto>
    {
    }
}
