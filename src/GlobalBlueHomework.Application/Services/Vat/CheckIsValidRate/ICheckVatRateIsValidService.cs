namespace GlobalBlueHomework.Application.Services.Vat.CheckIsValidRate;

/// <summary>
/// Checks if the provided VAT rate is valid for the given Country.
/// </summary>
public interface ICheckVatRateIsValidService
{
    /// <summary>
    /// Checks if the provided VAT rate is valid for the given Country.
    /// </summary>
    /// <param name="countryCode">Alpha-3 country code defined by ISO 3166.</param>
    /// <param name="vatRate">VAT rate.</param>
    /// <returns></returns>
    bool Execute(string countryCode, float? vatRate);
}