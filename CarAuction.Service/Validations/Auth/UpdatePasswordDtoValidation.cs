using CarAuction.Service.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations.Auth
{
	public class UpdatePasswordDtoValidation:AbstractValidator<UpdatePasswordDTO>
	{
        public UpdatePasswordDtoValidation()
        {
			RuleFor(u => u.UserId)
			   .NotEmpty().NotNull().WithMessage("Enter UserId");
			RuleFor(u => u.ResetToken)
				.NotEmpty().NotNull().WithMessage("Enter Reset Token");
			RuleFor(u => u.NewPassword)
				.NotEmpty().NotNull().WithMessage("Enter New Password");
			RuleFor(u => u.ConfirmedNewPassword)
				.Equal(u => u.NewPassword).WithMessage("Password and Confirmed Password must be same")
				.NotEmpty().NotNull().WithMessage("Enter Confirmed Password");
		}
    }
}
