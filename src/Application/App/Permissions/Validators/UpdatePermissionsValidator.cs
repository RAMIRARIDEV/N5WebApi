using FluentValidation;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Domain.Exceptions;

namespace N5WebApi.Application.App.Permissions.Validators;

internal sealed class UpdatePermissionsValidator : AbstractValidator<UpdatePermissionRequest>
{
    public UpdatePermissionsValidator()
    {
        RuleFor(r => r.Id)
        .NotEmpty()
        .WithMessage(e => ValidatorException<UpdatePermissionRequest>.NotEmptyMessage(nameof(e.Id)));
    }
}
