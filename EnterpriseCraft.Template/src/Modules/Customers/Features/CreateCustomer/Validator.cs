using FluentValidation;

namespace EnterpriseCraft.Template.Modules.Customers.Features.CreateCustomer;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be valid.")
            .MaximumLength(320);

        RuleFor(x => x.Phone)
            .MaximumLength(50);
    }
}