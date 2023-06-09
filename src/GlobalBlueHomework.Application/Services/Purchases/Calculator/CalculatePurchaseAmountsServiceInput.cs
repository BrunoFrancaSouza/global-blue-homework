﻿namespace GlobalBlueHomework.Application.Services.Purchases.Calculator;

public class CalculatePurchaseAmountsServiceInput
{
    public decimal? NetAmount { get; set; }

    public decimal? GrossAmount { get; set; }

    public decimal? VatAmount { get; set; }

    public float? VatRate { get; set; }

    public string? CountryCode { get; set; }

    public CalculatePurchaseAmountsServiceInput(decimal? netAmount, decimal? grossAmount, decimal? vatAmount, float? vatRate, string? countryCode)
    {
        NetAmount = netAmount;
        GrossAmount = grossAmount;
        VatAmount = vatAmount;
        VatRate = vatRate;
        CountryCode = countryCode;
    }
}