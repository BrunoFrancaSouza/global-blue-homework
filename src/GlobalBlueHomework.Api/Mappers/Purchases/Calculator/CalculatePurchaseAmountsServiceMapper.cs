using AutoMapper;
using GlobalBlueHomework.Api.Dtos.V1.Purchases;
using GlobalBlueHomework.Api.Dtos.V1.Purchases.Calculator;
using GlobalBlueHomework.Application.Services.Purchases.Calculator;

namespace GlobalBlueHomework.Api.Mappers.Purchases.Calculator;

public class CalculatePurchaseAmountsServiceMapper : Profile
{
    public CalculatePurchaseAmountsServiceMapper()
    {
        CreateMap<CalculatePurchaseAmountsRequestDto, CalculatePurchaseAmountsServiceInput>();
        CreateMap<CalculatePurchaseAmountsServiceOutput, PurchaseDto>();
    }
}