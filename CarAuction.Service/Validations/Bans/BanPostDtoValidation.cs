using CarAuction.Service.DTOs.Bans;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations
{
    public class BanPostDtoValidation:AbstractValidator<BanPostDto>
    {
        public BanPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(40);
            RuleFor(x => x)
            .Custom((x, context) =>
            {
                if (x.Image != null)
                {
                    if (!Helper.isImage(x.Image))
                    {
                        context.AddFailure("Image", "The type of file must be image");
                    }
                    if (!Helper.isSizeOk(x.Image, 2))
                    {
                        context.AddFailure("Image", "The size of image less than 2 mb");
                    }
                }
            });
        }
    }
}
