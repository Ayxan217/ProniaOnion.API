using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryDtoValidator(ICategoryRepository repository)
        {
           _repository = repository;

            RuleFor(c => c.Name).NotEmpty().WithMessage("data must not be empty")
                .MaximumLength(100).Matches("^[A-Za-z\\s0-9]*$");
                //.MustAsync(CheckName);
        }

        private async Task<bool> CheckName(string name,CancellationToken token)
        {
            return !await _repository.AnyAsync(c=>c.Name == name);
        }
    }
}
