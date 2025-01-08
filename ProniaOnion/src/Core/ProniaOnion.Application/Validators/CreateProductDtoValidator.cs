using FluentValidation;
using ProniaOnion.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public const int MAX_NAME_LENGTH = 100;
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name cant be Empty")
                .MaximumLength(MAX_NAME_LENGTH).WithMessage("Name must be less than 100 charecters");

            RuleFor(p => p.SKU).NotEmpty().MinimumLength(3).MaximumLength(10);

            RuleFor(p => p.Description).NotEmpty();

            RuleFor(p => p.Price).NotEmpty().GreaterThanOrEqualTo(3).LessThanOrEqualTo(9999.99m);

            RuleFor(p => p.CategoryId).NotEmpty().Must(id=>id>0);
            RuleFor(p => p.ColorIds).NotEmpty();
            RuleFor(p => p.ColorIds).NotEmpty().Must(colorId=>colorId.Count>0);

        }
    }
}
