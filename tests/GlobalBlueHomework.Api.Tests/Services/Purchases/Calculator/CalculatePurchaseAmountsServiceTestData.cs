using GlobalBlueHomework.Application.Constants;
using GlobalBlueHomework.Application.Services.Purchases.Calculator;
using System.Collections;

namespace GlobalBlueHomework.Api.Tests.Unit.Services.Purchases.Calculator;

public class CalculatePurchaseAmountsServiceTestData : IEnumerable<object[]>
{
    private static IEnumerable<object[]> _validatedData =>
        new List<object[]>
        {
            new object[]
            {
                new CalculatePurchaseAmountsServiceInput(netAmount: 1231.97m, grossAmount: null, vatAmount: null, vatRate: 13, CountryCodes.Austria), 
                new CalculatePurchaseAmountsServiceOutput(NetAmount: 1231.97m, GrossAmount: 1392.13m, VatAmount: 160.16m, VatRate: 13) 
            },
            new object[]
            {
                new CalculatePurchaseAmountsServiceInput(netAmount: null, grossAmount: 9200.99m, vatAmount: null, vatRate: 20, CountryCodes.Austria),
                new CalculatePurchaseAmountsServiceOutput(NetAmount: 7667.49m, GrossAmount: 9200.99m, VatAmount: 1533.50m, VatRate: 20)
            },
            new object[]
            {
                new CalculatePurchaseAmountsServiceInput(netAmount: null, grossAmount: null, vatAmount: 199.20m, vatRate: 10, CountryCodes.Austria),
                new CalculatePurchaseAmountsServiceOutput(NetAmount: 1992.00m, GrossAmount: 2191.20m, VatAmount: 199.20m, VatRate: 10)
            },
        };

    public IEnumerator<object[]> GetEnumerator()
    {
        return _validatedData.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
