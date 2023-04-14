using FluentValidation.Results;

namespace GlobalBlueHomework.Application.Models;

public sealed record ValidationFailed(IEnumerable<ValidationFailure> Errors)
{
    public ValidationFailed(ValidationFailure error) : this(new[] { error })
    {
    }
}