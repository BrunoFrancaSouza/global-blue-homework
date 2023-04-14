using AutoMapper;
using GlobalBlueHomework.Domain.Entities;

namespace GlobalBlueHomework.Application.Services.Purchases.Calculator;

public class CalculatePurchaseAmountsServiceMapper : Profile
{
    public CalculatePurchaseAmountsServiceMapper()
    {
        CreateMap<Purchase, CalculatePurchaseAmountsServiceOutput>();
    }
}