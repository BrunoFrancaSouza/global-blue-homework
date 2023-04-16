using Microsoft.Extensions.Configuration;

namespace GlobalBlueHomework.Application.Services.Vat;

// TODO: Refactor this in order to get the country valid VAT rates from an external source.
// Hardcoded just for exercise's purpose.
public class GetCountryValidVatRatesService : IGetCountryValidVatRatesService
{
    //private readonly IOptions<AppOptions> _appOptions;
    private readonly IConfiguration _configuration;

    public GetCountryValidVatRatesService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public float[] Execute(GetCountryValidVatRatesServiceInput input)
    {
        if (input == null || string.IsNullOrWhiteSpace(input.CountryCode))
            return null;

        var key = $"CountriesVatRates:{input.CountryCode}";
        var response = _configuration.GetSection(key).Get<float[]>();
        return response;
    }
}