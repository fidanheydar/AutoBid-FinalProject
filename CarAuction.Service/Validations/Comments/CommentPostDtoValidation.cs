using CarAuction.Service.DTOs.Comments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations
{
    public class CommentPostDtoValidation:AbstractValidator<CommentPostDto>
    {
        public CommentPostDtoValidation()
        {
            RuleFor(x => x.Text)
               .NotEmpty()
               .NotNull().WithMessage("Text can not be null")
               .MinimumLength(3)
               .MaximumLength(40);

            RuleFor(x => x.BlogId)
              .NotNull();
        }
    }
}
