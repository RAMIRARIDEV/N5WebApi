using FluentValidation;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Domain.Exceptions;

namespace N5WebApi.Application.App.Permissions.Validators;

internal sealed class CreatePermissionsValidator : AbstractValidator<CreatePermissionRequest>
{
    public CreatePermissionsValidator()
    {
        RuleFor(r => r.EmployeeForename)
        .NotEmpty()
        .WithMessage(e => ValidatorException<CreatePermissionRequest>.NotEmptyMessage(nameof(e.EmployeeForename)));

        RuleFor(r => r.EmployeeSurename)
        .NotEmpty()
        .WithMessage(e => ValidatorException<CreatePermissionRequest>.NotEmptyMessage(nameof(e.EmployeeSurename)));

        RuleFor(r => r.PermissionDate)
        .NotEmpty()
        .NotEqual(e => default)
        .WithMessage(e => ValidatorException<CreatePermissionRequest>.NotEmptyMessage(nameof(e.PermissionDate)));

        RuleFor(r => r.PermisionType)
        .NotEmpty()
        .NotEqual(e => default)
        .WithMessage(e => ValidatorException<CreatePermissionRequest>.NotEmptyMessage(nameof(e.PermisionType)));
    }
}