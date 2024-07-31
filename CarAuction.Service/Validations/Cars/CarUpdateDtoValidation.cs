using CarAuction.Service.DTOs.Cars;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Validations.Cars
{
	public class CarUpdateDtoValidation : AbstractValidator<CarUpdateDto>
	{
		public CarUpdateDtoValidation()
		{
			RuleFor(x => x.Vin)
			.NotEmpty()
			.NotNull().WithMessage("Vin can not be null")
			.MinimumLength(14)
			.MaximumLength(17);
			RuleFor(x => x.Description)
			.NotEmpty()
			.NotNull().WithMessage("Description can not be null")
			.MinimumLength(30);
			RuleFor(x => x.ModelId)
		   .NotEmpty();

			RuleFor(x => x)
			 .Custom((x, context) =>
			 {
				 if (x.FabricationYear <= 2010)
				 {
					 context.AddFailure("FabricationYear", "FabricationYear must bigger than 2010");
				 }
			 });
			RuleFor(x => x.Odometer)
		   .NotEmpty();
			RuleFor(x => x.FuelId)
		   .NotEmpty()
		   .NotNull();
			RuleFor(x => x.NoGears)
			  .NotEmpty()
		   .NotNull();
			RuleFor(x => x.Power)
				 .NotEmpty()
		   .NotNull();

			RuleFor(x => x.Transmission)
		 .NotEmpty()
		   .NotNull();
			RuleFor(x => x.FuelCity)
		.NotEmpty()
		   .NotNull();
			RuleFor(x => x.FuelWay)
	.NotEmpty()
		   .NotNull();
			RuleFor(x => x.Motor)
		.NotEmpty()
		   .NotNull();
			RuleFor(x => x.ColorId)
   .NotEmpty()
		   .NotNull();
			RuleFor(x => x.InitialPrice)
   .NotEmpty()
		   .NotNull();
			RuleFor(x => x.Description)
   .NotEmpty()
		   .NotNull();
			RuleFor(x => x.BanId)
.NotEmpty()
		.NotNull();
			RuleFor(x => x)
			.Custom((x, context) =>
			{
				if (x.Images is not null)
				{
					foreach (var file in x.Images)
					{
						if (file != null)
						{
							if (!Helper.isImage(file))
							{
								context.AddFailure("Images", "The type of file must be image");
							}
							if (!Helper.isSizeOk(file, 2))
							{
								context.AddFailure("Images", "The size of image less than 2 mb");
							}
						}
					}
				}
				if (x.AuctionDate < DateTime.Now)
				{
					context.AddFailure("AuctionDate", "Time is not correct");
				}
				if (x.FinishDate < DateTime.Now || x.FinishDate < x.AuctionDate)
				{
					context.AddFailure("FinishDate", "Time is not correct and must be bigger than Auction date");
				}
			});
		}
	}
}
