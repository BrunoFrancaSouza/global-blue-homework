namespace GlobalBlueHomework.Api.Dtos.V1.Purchases;

/// <summary>
/// Purchase data transfer object.
/// </summary>
public class PurchaseDto
{
    /// <summary>
    /// Purchase net amount.
    /// </summary>
    /// <example>1231.97</example>
    public decimal? NetAmount { get; set; }

    /// <summary>
    /// Purchase gross amount.
    /// </summary>
    /// <example>1392.13</example>
    public decimal? GrossAmount { get; set; }

    /// <summary>
    /// Purchase Value Added Tax (VAT) amount.
    /// </summary>
    /// <example>160.16</example>
    public decimal? VatAmount { get; set; }

    /// <summary>
    /// Value Added Tax (VAT) rate applied.
    /// </summary>
    /// <example>13</example>
    public int VatRate { get; set; }
}