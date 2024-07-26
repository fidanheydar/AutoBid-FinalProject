using CarAuction.Service.DTOs.Fuels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations
{
    public class FuelUpdateDtoValidation:AbstractValidator<FuelUpdateDto>
    {
        public FuelUpdateDtoValidation()
        {
			RuleFor(x => x.Name)
			 .NotEmpty()
			 .NotNull().WithMessage("Name can not be null")
			 .MinimumLength(3)
			 .MaximumLength(40);
		}
    }
}
