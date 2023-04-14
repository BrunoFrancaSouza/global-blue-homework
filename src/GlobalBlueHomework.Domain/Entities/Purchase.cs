using FluentResults;

namespace GlobalBlueHomework.Domain.Entities;

public class Purchase
{
    private const int DecimalPlaces = 2;

    internal Purchase(decimal? NetAmount, decimal? GrossAmount, decimal? VatAmount, float VatRate)
    {
        CalculateAmountValues(NetAmount, GrossAmount, VatAmount, VatRate);
    }

    public static Result<Purchase> Create(decimal? NetAmount, decimal? GrossAmount, decimal? VatAmount, float VatRate)
    {
        var response = new Purchase(NetAmount, GrossAmount, VatAmount, VatRate);
        return response;
    }

    /// <summary>
    /// Purchase Net amount.
    /// </summary>
    public decimal NetAmount { get; private set; }

    /// <summary>
    /// Purchase Gross amount.
    /// </summary>
    public decimal GrossAmount { get; private set; }

    /// <summary>
    /// Purchase VAT amount.
    /// </summary>
    public decimal VatAmount { get; private set; }

    /// <summary>
    /// VAT Rate applied.
    /// </summary>
    public float VatRate { get; private set; }

    /// <summary>
    /// Calculates Net, Gross and VAT amounts base on the provided information.
    /// Calculates missing net, gross and VAT values.
    /// </summary>
    /// <remarks>
    /// If it receives one of the <see cref="NetAmount"/>, <see cref="GrossAmount"/> or <see cref="VatAmount"/> amounts and additionally a valid <see cref="VatRate"/>,
    /// the other two missing amounts (<see cref="NetAmount"/>/<see cref="GrossAmount"/>/<see cref="VatAmount"/>) are calculated.
    /// </remarks>
    private void CalculateAmountValues(decimal? NetAmount, decimal? GrossAmount, decimal? VatAmount, float VatRate)
    {
        this.VatRate = VatRate;
        this.NetAmount = NetAmount.HasValue ? NetAmount.Value : 0;
        this.GrossAmount = GrossAmount.HasValue ? GrossAmount.Value : 0;
        this.VatAmount = VatAmount.HasValue ? VatAmount.Value : 0;

        if (NetAmount.HasValue)
        {
            this.VatAmount = Math.Round(NetAmount.Value * (decimal)VatRate / 100, DecimalPlaces);
            this.GrossAmount = Math.Round(this.VatAmount + NetAmount.Value, DecimalPlaces);
        }

        if (GrossAmount.HasValue)
        {
            this.NetAmount = Math.Round(GrossAmount.Value / (decimal)(1 + VatRate / 100), DecimalPlaces);
            this.VatAmount = Math.Round(GrossAmount.Value - this.NetAmount, DecimalPlaces);
        }

        if (VatAmount.HasValue)
        {
            this.NetAmount = Math.Round(VatAmount.Value / (decimal)VatRate * 100, DecimalPlaces);
            this.GrossAmount = Math.Round(VatAmount.Value + this.NetAmount, DecimalPlaces);
        }
    }
}