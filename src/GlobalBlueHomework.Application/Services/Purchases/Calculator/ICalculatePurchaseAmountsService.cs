using GlobalBlueHomework.Application.Models;
using GlobalBlueHomework.Domain.Entities;
using OneOf;

namespace GlobalBlueHomework.Application.Services.Purchases.Calculator;

/// <summary>
/// Service to calculate <see cref="Purchase.NetAmount"/>, <see cref="Purchase.GrossAmount"/> and <see cref="Purchase.VatAmount"/>.
/// </summary>
public interface ICalculatePurchaseAmountsService
{
    OneOf<CalculatePurchaseAmountsServiceOutput, ValidationFailed> Execute(CalculatePurchaseAmountsServiceInput input);
}