using CarAuction.Service.DTOs.Models;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Miles.Service.Validations.Messages
{
    public class ModelUpdateDtoValidation:AbstractValidator<ModelUpdateDto>
    {
        public ModelUpdateDtoValidation()
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

