using CarAuction.Service.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations.Auth
{
	internal class ResetPasswordDtoValidation : AbstractValidator<ResetPasswordDTO>
	{
		public ResetPasswordDtoValidation()
		{
			RuleFor(r => r.Email).EmailAddress().NotEmpty().NotNull().WithMessage("Enter Email");
		}
	}

}
