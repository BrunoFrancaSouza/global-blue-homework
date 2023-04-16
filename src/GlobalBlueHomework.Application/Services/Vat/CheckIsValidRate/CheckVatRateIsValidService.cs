namespace GlobalBlueHomework.Application.Services.Vat.CheckIsValidRate;

/// <inheritdoc />
public class CheckVatRateIsValidService : ICheckVatRateIsValidService
{
    private readonly IGetCountryValidVatRatesService _getCountryValidVatRatesService;

    public CheckVatRateIsValidService(IGetCountryValidVatRatesService getCountryValidVatRatesService)
    {
        _getCountryValidVatRatesService = getCountryValidVatRatesService;
    }

    public bool Execute(string countryCode, float? vatRate)
    {
        if (!vatRate.HasValue)
            throw new ArgumentException($"'{nameof(vatRate)}' cannot be null.", nameof(countryCode));

        if (string.IsNullOrWhiteSpace(countryCode))
            throw new ArgumentException($"'{nameof(countryCode)}' cannot be null or whitespace.", nameof(countryCode));

        var validVatRates = GetCountryValidVatRates(countryCode);
        return validVatRates.Contains(vatRate.Value);
    }

    private float[] GetCountryValidVatRates(string countryCode)
    {
        var serviceInput = new GetCountryValidVatRatesServiceInput(countryCode);
        var response = _getCountryValidVatRatesService.Execute(serviceInput);
        return response;
    }
}