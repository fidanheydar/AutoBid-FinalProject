using CarAuction.Service.DTOs.Brands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations
{
    public class BrandUpdateDtoValidation:AbstractValidator<BrandUpdateDto>
    {
        public BrandUpdateDtoValidation()
        {
			RuleFor(x => x.Name)
			 .NotEmpty()
			 .NotNull().WithMessage("Name can not be null")
			 .MinimumLength(3)
			 .MaximumLength(40);
		}
    }
}
