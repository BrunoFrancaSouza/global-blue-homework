using FluentValidation;
using GlobalBlueHomework.Application.Options;
using Microsoft.Extensions.Options;

namespace GlobalBlueHomework.Application.Services.Purchases.Calculator;

public class CalculatePurchaseAmountsServiceValidator : AbstractValidator<CalculatePurchaseAmountsServiceInput>
{
    private readonly IOptions<AppOptions> _appOptions;

    public CalculatePurchaseAmountsServiceValidator(IOptions<AppOptions> appOptions)
    {
        _appOptions = appOptions ?? throw new ArgumentNullException(nameof(appOptions));

        RuleFor(x => x)
           .Must(HaveAtLeastOneInput)
           .WithMessage("Please provide at least one of the following: gross, net or VAT amount.");

        RuleFor(x => x)
           .Must(NotHaveMoreThanOneAmountInput)
           .WithMessage("Please provide only one of the following: gross, net or VAT amount.");

        RuleFor(x => x.NetAmount)
            .GreaterThanOrEqualTo(0).When(x => !x.GrossAmount.HasValue && !x.VatAmount.HasValue);

        RuleFor(x => x.GrossAmount)
            .GreaterThanOrEqualTo(0).When(x => !x.NetAmount.HasValue && !x.VatAmount.HasValue);

        RuleFor(x => x.VatAmount)
            .GreaterThanOrEqualTo(0).When(x => !x.NetAmount.HasValue && !x.GrossAmount.HasValue);

        RuleFor(x => x.VatRate)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .Must(BeValidAustrianVatRate).WithMessage("'{PropertyValue}' is not a valid {PropertyName} for Austria.");
    }

    private bool HaveAtLeastOneInput(CalculatePurchaseAmountsServiceInput input)
    {
        var response = input.GrossAmount.HasValue || input.NetAmount.HasValue || input.VatAmount.HasValue;
        return response;
    }

    private bool NotHaveMoreThanOneAmountInput(CalculatePurchaseAmountsServiceInput input)
    {
        var response = input.GrossAmount.HasValue ^ input.NetAmount.HasValue ^ input.VatAmount.HasValue;
        return response;
    }

    private bool BeValidAustrianVatRate(float? vatRate)
    {
        var response = Array.IndexOf(_appOptions.Value.AustrianVatRates, vatRate) > -1;
        return response;
    }
}