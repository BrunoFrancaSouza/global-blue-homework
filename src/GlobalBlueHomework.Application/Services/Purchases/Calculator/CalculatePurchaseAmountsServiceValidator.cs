using FluentValidation;
using GlobalBlueHomework.Application.Services.Vat.CheckIsValidRate;

namespace GlobalBlueHomework.Application.Services.Purchases.Calculator;

public class CalculatePurchaseAmountsServiceValidator : AbstractValidator<CalculatePurchaseAmountsServiceInput>
{
    public CalculatePurchaseAmountsServiceValidator(ICheckVatRateIsValidService checkVatRateIsValidService)
    {
        RuleFor(x => x)
           .Must(HaveAtLeastOneAmountInput)
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
            .Must((model, vatRate, context) =>
            {
                context.MessageFormatter.AppendArgument("CountryCode", model.CountryCode);
                return checkVatRateIsValidService.Execute(model.CountryCode, vatRate);
            }).WithMessage("'{PropertyValue}' is not a valid VAT rate for the given country code '{CountryCode}'.");
    }

    private bool HaveAtLeastOneAmountInput(CalculatePurchaseAmountsServiceInput input)
    {
        var response = input.NetAmount.HasValue || input.GrossAmount.HasValue || input.VatAmount.HasValue;
        return response;
    }

    private bool NotHaveMoreThanOneAmountInput(CalculatePurchaseAmountsServiceInput input)
    {
        if (!input.NetAmount.HasValue && !input.GrossAmount.HasValue && !input.VatAmount.HasValue)
            return true;

        var count = 0;

        if (input.NetAmount.HasValue)
            count += 1;

        if (input.GrossAmount.HasValue)
            count += 1;

        if (input.VatAmount.HasValue)
            count += 1;

        var response = count <= 1;
        return response;
    }
}