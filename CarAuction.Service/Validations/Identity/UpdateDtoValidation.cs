using CarAuction.Service.DTOs.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations.Identity
{
    public class UpdateDtoValidation : AbstractValidator<RegisterDto>
    {
        public UpdateDtoValidation()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull().WithMessage("Enter Name");
            RuleFor(r => r.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).NotNull().WithMessage("Enter Email");
            RuleFor(r => r.Surname).NotEmpty().NotNull().WithMessage("Enter Surname");
            RuleFor(r => r.UserName).NotEmpty().NotNull().WithMessage("Enter UserName");
            RuleFor(r => r).Custom((x, context) =>
            {
                if (!string.IsNullOrWhiteSpace(x.Password))
                {
                    if (x.Password != x.ConfirmedPassword)
                    {
                        context.AddFailure("ConfirmedPassword", "Confirm password invalid");
                    }
                }
            });
        }
    }
}
