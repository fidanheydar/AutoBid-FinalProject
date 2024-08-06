using CarAuction.Service.DTOs.Blogs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations.Blogs
{
	public class BlogUpdateDtoValidation : AbstractValidator<BlogUpdateDto>
	{
		public BlogUpdateDtoValidation()
		{
			RuleFor(x => x.Title)
			  .NotEmpty()
			  .NotNull().WithMessage("Name can not be null")
			  .MinimumLength(3)
			  .MaximumLength(80);

			RuleFor(x => x.Description)
			.NotEmpty()
			.NotNull().WithMessage("Description can not be null")
			.MinimumLength(30);


			RuleFor(x => x.Fact)
		   .NotEmpty()
			.NotNull().WithMessage("Fact can not be null")
			.MinimumLength(3)
			.MaximumLength(300);

			RuleFor(x => x.CategoryId)
		   .NotEmpty()
			.NotNull();


			RuleFor(x => x)
			.Custom((x, context) =>
			{
				if (x.BaseImage != null)
				{
					if (!Helper.isImage(x.BaseImage))
					{
						context.AddFailure("BaseImage", "The type of file must be image");
					}
					if (!Helper.isSizeOk(x.BaseImage, 2))
					{
						context.AddFailure("BaseImage", "The size of image less than 2 mb");
					}
				}

				if (x.SectionImage != null)
				{
					if (!Helper.isImage(x.SectionImage))
					{
						context.AddFailure("SectionImage", "The type of file must be image");
					}
					if (!Helper.isSizeOk(x.SectionImage, 2))
					{
						context.AddFailure("SectionImage", "The size of image less than 2 mb");
					}
				}
			});
		}
	}
}
