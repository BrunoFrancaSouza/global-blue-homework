using AutoMapper;
using FluentValidation;
using GlobalBlueHomework.Application.Models;
using GlobalBlueHomework.Application.Services.Vat;
using GlobalBlueHomework.Domain.Entities;
using OneOf;

namespace GlobalBlueHomework.Application.Services.Purchases.Calculator;

public class CalculatePurchaseAmountsService : ICalculatePurchaseAmountsService
{
    private readonly IValidator<CalculatePurchaseAmountsServiceInput> _validator;
    private readonly IMapper _mapper;
    private readonly IGetCountryValidVatRatesService _getCountryValidVatRatesService;

    public CalculatePurchaseAmountsService(
        IValidator<CalculatePurchaseAmountsServiceInput> validator,
        IMapper mapper,
        IGetCountryValidVatRatesService getCountryValidVatRatesService)
    {
        _validator = validator;
        _mapper = mapper;
        _getCountryValidVatRatesService = getCountryValidVatRatesService;
    }

    public OneOf<CalculatePurchaseAmountsServiceOutput, ValidationFailed> Execute(CalculatePurchaseAmountsServiceInput input)
    {
        var validationResult = _validator.Validate(input);
        if (!validationResult.IsValid)
            return new ValidationFailed(validationResult.Errors);

        var createPurchaseResult = Purchase.Create(input.NetAmount, input.GrossAmount, input.VatAmount, input.VatRate.Value);

        if (createPurchaseResult.IsFailed)
            return new ValidationFailed(validationResult.Errors);

        var purchase = createPurchaseResult.Value;

        var response = _mapper.Map<CalculatePurchaseAmountsServiceOutput>(purchase);
        return response;
    }
}