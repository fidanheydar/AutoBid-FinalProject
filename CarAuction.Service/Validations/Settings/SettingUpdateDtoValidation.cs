using CarAuction.Service.DTOs.Settings;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations.Settings
{
	public class SettingUpdateDtoValidation : AbstractValidator<SettingUpdateDto>
	{
		public SettingUpdateDtoValidation()
		{
			RuleFor(x => x.Address)
						   .NotEmpty()
						   .NotNull().WithMessage("Address can not be null")
						   .MinimumLength(3)
						   .MaximumLength(40);

			RuleFor(x => x.WorkHours)
			 .NotEmpty()
			 .NotNull();
			RuleFor(x => x)
			  .Custom((x, context) =>
			  {
				  Regex re = new Regex("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$");
				  if (x.Phone1 != null)
				  {
					  if (!re.IsMatch(x.Phone1))
						  context.AddFailure("phone1", "Phone format must be correct");
				  }
				  if (x.Phone2 != null)
				  {
					  if (!re.IsMatch(x.Phone2))
						  context.AddFailure("phone2", "Phone format must be correct");
				  }
			  }
			  );
			RuleFor(x => x.Email).
			 NotNull();
			RuleFor(x => x)
		   .Custom((x, context) =>
		   {
			   if (x.Logo != null)
			   {
				   if (!Helper.isImage(x.Logo))
				   {
					   context.AddFailure("logo", "The type of file must be image");
				   }
				   if (!Helper.isSizeOk(x.Logo, 2))
				   {
					   context.AddFailure("logo", "The size of image less than 2 mb");
				   }
			   }
		   });
			RuleFor(x => x)
		   .Custom((x, context) =>
		   {
			   if (x.AboutImage != null)
			   {
				   if (!Helper.isImage(x.AboutImage))
				   {
					   context.AddFailure("AboutImage", "The type of file must be image");
				   }
				   if (!Helper.isSizeOk(x.AboutImage, 2))
				   {
					   context.AddFailure("AboutImage", "The size of image less than 2 mb");
				   }
			   }
		   });
		}
	}
}
