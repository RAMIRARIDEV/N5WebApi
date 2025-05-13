using FluentValidation;
using N5WebApi.Application.App.PermissionsTypes.Dtos;
using N5WebApi.Domain.Exceptions;

namespace N5WebApi.Application.App.PermissionsTypes.Validators;

internal sealed class CreatePermissionTypeValidator : AbstractValidator<CreatePermissionTypeRequest>
{
    public CreatePermissionTypeValidator()
    {
        RuleFor(e => e.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage(e => ValidatorException<CreatePermissionTypeRequest>.NotEmptyMessage(nameof(e.Description)));
    }
}
