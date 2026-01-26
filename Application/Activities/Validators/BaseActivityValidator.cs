using System;
using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Activities.Validators;

public class BaseActivityValidator<T, TDto> : AbstractValidator<T> where TDto
    : BaseActivityDto
{
    public BaseActivityValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title Must Not Exceed 100 Characters!");
        RuleFor(x => selector(x).Description)
            .NotEmpty().WithMessage("Description is required.");
        RuleFor(x => selector(x).Date)
            .GreaterThan(DateTime.UtcNow).WithMessage("Date Must Be In The Future!");
        RuleFor(x => selector(x).Category)
            .NotEmpty().WithMessage("Category is required.");
        RuleFor(x => selector(x).City)
            .NotEmpty().WithMessage("City is required.");
        RuleFor(x => selector(x).Venue)
            .NotEmpty().WithMessage("Venue is required.");
        RuleFor(x => selector(x).Latitude)
            .NotEmpty().WithMessage("Latitude Is Required!")
            .InclusiveBetween(-90, 90).WithMessage("Latitude Must Be Between -90 And 90.");
        RuleFor(x => selector(x).Longitude)
            .NotEmpty().WithMessage("Longitude Is Required!")
            .InclusiveBetween(-180, 180).WithMessage("Longitude Must Be Between -180 And 180.");
    }
}
