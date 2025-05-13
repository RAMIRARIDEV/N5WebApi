using FluentValidation;
using N5WebApi.Application.App.PermissionsTypes.Dtos;
using N5WebApi.Domain.Exceptions;

namespace N5WebApi.Application.App.PermissionsTypes.Validators;

internal sealed class UpdatePermissionTypeValidator : AbstractValidator<UpdatePermissionTypeRequest>
{
    public UpdatePermissionTypeValidator()
    {
        RuleFor(e => e.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage(e => ValidatorException<UpdatePermissionTypeRequest>.NotEmptyMessage(nameof(e.Description)));
    }
}
