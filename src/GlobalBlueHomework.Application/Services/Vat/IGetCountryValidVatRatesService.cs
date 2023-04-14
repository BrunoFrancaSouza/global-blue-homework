namespace GlobalBlueHomework.Application.Services.Vat;

public interface IGetCountryValidVatRatesService
{
    float[] Execute(GetCountryValidVatRatesServiceInput input);
}