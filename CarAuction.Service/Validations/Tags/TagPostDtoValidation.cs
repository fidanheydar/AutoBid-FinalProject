using CarAuction.Service.DTOs.Tags;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations
{
    public class TagPostDtoValidation:AbstractValidator<TagPostDto>
    {
        public TagPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(30);
        }
    }
}
