using CarAuction.Service.DTOs.Bids;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations
{
    public class BidPostDtoValidation : AbstractValidator<BidPostDto>
    {
        public BidPostDtoValidation()
        {
            RuleFor(x => x.CarId)
                .NotNull();

            RuleFor(x => x.Count)
              .NotNull();
        }
    }
}
