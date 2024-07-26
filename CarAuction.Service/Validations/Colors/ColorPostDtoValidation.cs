using CarAuction.Service.DTOs.Colors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations
{
    public class ColorPostDtoValidation:AbstractValidator<ColorPostDto>
    {
        public ColorPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(20);


            RuleFor(x => x.Code)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(7);

        }
    }
}
