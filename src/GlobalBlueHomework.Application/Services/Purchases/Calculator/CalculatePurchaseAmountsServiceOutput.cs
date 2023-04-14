namespace GlobalBlueHomework.Application.Services.Purchases.Calculator;

public record CalculatePurchaseAmountsServiceOutput(decimal? NetAmount, decimal? GrossAmount, decimal? VatAmount, float? VatRate);
