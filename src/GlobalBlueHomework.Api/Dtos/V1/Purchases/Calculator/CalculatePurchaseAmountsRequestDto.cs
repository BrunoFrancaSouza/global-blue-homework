namespace GlobalBlueHomework.Api.Dtos.V1.Purchases.Calculator;

public class CalculatePurchaseAmountsRequestDto
{
    /// <summary>
    /// Purchase net amount.
    /// </summary>
    /// <example>100.00</example>
    public decimal? NetAmount { get; set; }

    /// <summary>
    /// Purchase gross amount.
    /// </summary>
    /// <example>null</example>
    public decimal? GrossAmount { get; set; }

    /// <summary>
    /// Purchase Value Added Tax (VAT) amount.
    /// </summary>
    /// <example>null</example>
    public decimal? VatAmount { get; set; }

    /// <summary>
    /// Value Added Tax (VAT) rate applied.
    /// </summary>
    /// <example>20.00</example>
    public float? VatRate { get; set; }
}