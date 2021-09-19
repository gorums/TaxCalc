using FluentValidation;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.ToCountry)
                .NotEmpty()
                .WithMessage(o => $"The parameter {nameof(o.ToCountry)} is required");

            RuleFor(order => order.Shipping)
               .GreaterThan(0)
               .WithMessage(o => $"The parameter {nameof(o.Shipping)} has to be greater than 0");

            RuleFor(order => order.ToZip)
                .NotEmpty()
                .When(o => "US".Equals(o.ToCountry))
                .WithMessage(o => $"The parameter {nameof(o.ToZip)} is required");

            RuleFor(order => order.ToState)
                .NotEmpty()
                .When(o => "US".Equals(o.ToCountry))
                .WithMessage(o => $"The parameter {nameof(o.ToState)} is required");

        }
    }
}
