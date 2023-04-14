namespace GlobalBlueHomework.Api.Dtos.V1.Purchases;

public class PurchaseDto
{
    /// <summary>
    /// Purchase net amount.
    /// </summary>
    /// <example>100.00</example>
    public decimal? NetAmount { get; set; }

    /// <summary>
    /// Purchase gross amount.
    /// </summary>
    /// <example>100.00</example>
    public decimal? GrossAmount { get; set; }

    /// <summary>
    /// Purchase Value Added Tax (VAT) amount.
    /// </summary>
    /// <example>My beautiful post</example>
    public decimal? VatAmount { get; set; }

    /// <summary>
    /// Value Added Tax (VAT) rate applied.
    /// </summary>
    /// <example>My beautiful post</example>
    public int VatRate { get; set; }
}
