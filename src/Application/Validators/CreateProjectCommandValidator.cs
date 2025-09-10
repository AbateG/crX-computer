using FluentValidation;
using CR_COMPUTER.Application.Commands;

namespace CR_COMPUTER.Application.Validators
{
    /// <summary>
    /// Validator for CreateProjectCommand
    /// </summary>
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name is required")
                .MaximumLength(200).WithMessage("Project name must not exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Project description is required")
                .MaximumLength(1000).WithMessage("Project description must not exceed 1000 characters");

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Client ID is required");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Start date cannot be in the past");

            RuleFor(x => x.Budget)
                .GreaterThan(0).WithMessage("Budget must be greater than 0");

            RuleFor(x => x.ProjectManagerId)
                .NotEmpty().When(x => x.ProjectManagerId.HasValue)
                .WithMessage("Project manager ID must be valid when provided");
        }
    }
}
