namespace GlobalBlueHomework.Application.Services.Vat;

/// <summary>
/// Gets valid VAT rates for the given Country.
/// </summary>
public interface IGetCountryValidVatRatesService
{
    /// <summary>
    /// Gets valid VAT rates for the given Country.
    /// </summary>
    /// <param name="input">Service input.</param>
    /// <returns></returns>
    float[] Execute(GetCountryValidVatRatesServiceInput input);
}