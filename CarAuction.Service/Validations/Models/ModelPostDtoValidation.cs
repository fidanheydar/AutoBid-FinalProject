using CarAuction.Service.DTOs.Models;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations
{
    public class ModelPostDtoValidation:AbstractValidator<ModelPostDto>
    {
        public ModelPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(50);
            RuleFor(x => x.BrandId).
                NotNull();
        }
    }
}

