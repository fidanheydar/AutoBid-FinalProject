using CarAuction.Service.DTOs.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations.Identity
{
    internal class RegisterDtoValidation : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull().WithMessage("Enter Name");
            RuleFor(r => r.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).NotNull().WithMessage("Enter Email");
            RuleFor(r => r.UserName).NotEmpty().NotNull().WithMessage("Enter UserName");
            RuleFor(r => r.Surname).NotEmpty().NotNull().WithMessage("Enter Surname");
            RuleFor(r => r.Password).NotEmpty().NotNull().WithMessage("Enter Password");
            RuleFor(r => r.ConfirmedPassword).Equal(r => r.Password).NotEmpty().NotNull().WithMessage("Enter Confirmed Password");
        }
    }
}
