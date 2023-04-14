﻿namespace GlobalBlueHomework.Application.Services.Purchases.Calculator;

//public record CalculatePurchaseAmountsServiceInput(decimal? NetAmount, decimal? GrossAmount, decimal? VatAmount, float? VatRate, string? CountryCode = null);

public class CalculatePurchaseAmountsServiceInput
{
    public decimal? NetAmount { get; set; }

    public decimal? GrossAmount { get; set; }

    public decimal? VatAmount { get; set; }

    public float? VatRate { get; set; }

    public string? CountryCode { get; set; }
}