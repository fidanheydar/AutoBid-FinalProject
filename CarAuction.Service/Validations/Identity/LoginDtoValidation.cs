using CarAuction.Service.DTOs.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations.Identity
{
    public class LoginDtoValidation : AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(l => l.EmailorUserName).NotEmpty().NotNull().WithMessage("Enter Email or UserName ");
            RuleFor(l => l.Password).NotEmpty().NotNull().WithMessage("Enter Password");
        }
    }
}
