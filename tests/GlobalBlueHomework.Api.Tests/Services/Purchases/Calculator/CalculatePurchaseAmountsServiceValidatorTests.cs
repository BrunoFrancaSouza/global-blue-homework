using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using GlobalBlueHomework.Application.Constants;
using GlobalBlueHomework.Application.Services.Purchases.Calculator;
using GlobalBlueHomework.Application.Services.Vat.CheckIsValidRate;
using Moq;
using MoqMeUp;
using Xunit;

namespace GlobalBlueHomework.Api.Tests.Unit.Services.Purchases.Calculator;

public class CalculatePurchaseAmountsServiceValidatorTests : MoqMeUp<CalculatePurchaseAmountsServiceValidator>
//public class CalculatePurchaseAmountsServiceValidatorTests
{
    public CalculatePurchaseAmountsServiceValidatorTests()
    {
    }

    [Fact]
    public void Ctor_Default_ShouldNotReturnErrors()
    {
        // Arrange
        var fixture = new Fixture();
        var checkVatRateIsValidServiceMock = this.Get<ICheckVatRateIsValidService>();
        checkVatRateIsValidServiceMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<float>()))
                                      .Returns(true);

        var model = new CalculatePurchaseAmountsServiceInput(netAmount: 1, grossAmount: null, vatAmount: null, vatRate: 10, CountryCodes.Austria);
        var sut = this.Build();

        // Act
        var result = sut.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(model => model);
        result.ShouldNotHaveValidationErrorFor(model => model.NetAmount);
        result.ShouldNotHaveValidationErrorFor(model => model.GrossAmount);
        result.ShouldNotHaveValidationErrorFor(model => model.VatAmount);
        result.ShouldNotHaveValidationErrorFor(model => model.VatRate);
        result.ShouldNotHaveValidationErrorFor(model => model.CountryCode);
    }

    [Fact]
    public void Ctor_WithNoAmountInput_ShouldReturnHaveAtLeastOneAmountInputError()
    {
        // Arrange
        const string ExpectedErrorMessage = "Please provide at least one of the following: gross, net or VAT amount.";
        var checkVatRateIsValidServiceMock = this.Get<ICheckVatRateIsValidService>();
        checkVatRateIsValidServiceMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<float>()))
                                      .Returns(true);

        var model = new CalculatePurchaseAmountsServiceInput(netAmount: null, grossAmount: null, vatAmount: null, vatRate: 13, CountryCodes.Austria);

        var sut = this.Build();

        // Act
        var result = sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(model => model).WithErrorMessage(ExpectedErrorMessage);
        result.Errors.Should().HaveCount(1);
    }

    [Theory]
    [InlineData(null, 1, 2)]
    [InlineData(1, null, 3)]
    [InlineData(1, 2, null)]
    [InlineData(1, 2, 3)]
    public void Ctor_WithMoreThanOneAmountInput_ShouldReturnHaveMoreThanOneAmountInputError(decimal netAmount, decimal grossAmount, decimal vatAmount)
    {
        // Arrange
        const string ExpectedErrorMessage = "Please provide only one of the following: gross, net or VAT amount.";
        var checkVatRateIsValidServiceMock = this.Get<ICheckVatRateIsValidService>();
        checkVatRateIsValidServiceMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<float>()))
                                      .Returns(true);

        var model = new CalculatePurchaseAmountsServiceInput(netAmount, grossAmount, vatAmount, vatRate: 13, CountryCodes.Austria);

        var sut = this.Build();

        // Act
        var result = sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(model => model).WithErrorMessage(ExpectedErrorMessage);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void Ctor_WithNoGrossAndVatAmounts_AndInvalidNetAmount_ShouldReturnInvalidNetAmountError()
    {
        // Arrange
        var netAmount = new decimal(new Random().Next(int.MinValue, -1));
        const string ExpectedErrorMessage = "'Net Amount' must be greater than or equal to '0'.";
        var checkVatRateIsValidServiceMock = this.Get<ICheckVatRateIsValidService>();
        checkVatRateIsValidServiceMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<float>()))
                                      .Returns(true);

        var model = new CalculatePurchaseAmountsServiceInput(netAmount: netAmount, grossAmount: null, vatAmount: null, vatRate: 20, CountryCodes.Austria);

        var sut = this.Build();

        // Act
        var result = sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(model => model.NetAmount).WithErrorMessage(ExpectedErrorMessage);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void Ctor_WithNoNetAndVatAmounts_AndInvalidGrossAmount_ShouldReturnInvalidGrossAmountError()
    {
        // Arrange
        var grossAmount = new decimal(new Random().Next(int.MinValue, -1));
        const string ExpectedErrorMessage = "'Gross Amount' must be greater than or equal to '0'.";
        var checkVatRateIsValidServiceMock = this.Get<ICheckVatRateIsValidService>();
        checkVatRateIsValidServiceMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<float>()))
                                      .Returns(true);

        var model = new CalculatePurchaseAmountsServiceInput(netAmount: null, grossAmount: grossAmount, vatAmount: null, vatRate: 10, CountryCodes.Austria);

        var sut = this.Build();

        // Act
        var result = sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(model => model.GrossAmount).WithErrorMessage(ExpectedErrorMessage);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void Ctor_WithNoNetAndGrossAmounts_AndInvalidVatAmount_ShouldReturnInvalidGrossAmountError()
    {
        // Arrange
        var vatAmount = new decimal(new Random().Next(int.MinValue, -1));
        const string ExpectedErrorMessage = "'Vat Amount' must be greater than or equal to '0'.";
        var checkVatRateIsValidServiceMock = this.Get<ICheckVatRateIsValidService>();
        checkVatRateIsValidServiceMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<float>()))
                                      .Returns(true);

        var model = new CalculatePurchaseAmountsServiceInput(netAmount: null, grossAmount: null, vatAmount: vatAmount, vatRate: 13, CountryCodes.Austria);

        var sut = this.Build();

        // Act
        var result = sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(model => model.VatAmount).WithErrorMessage(ExpectedErrorMessage);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void Ctor_WithCheckVatRateIsValidServiceReturningFalse_ShouldReturnInvalidVatRateError()
    {
        // Arrange
        var fixture = new Fixture();
        var vatRate = fixture.Create<float>();
        const string CountryCode = CountryCodes.Austria;
        var expectedErrorMessage = $"'{vatRate}' is not a valid VAT rate for the given country code '{CountryCode}'.";
        var checkVatRateIsValidServiceMock = this.Get<ICheckVatRateIsValidService>();
        checkVatRateIsValidServiceMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<float>()))
                                      .Returns(false);

        var model = new CalculatePurchaseAmountsServiceInput(netAmount: 1291.00m, grossAmount: null, vatAmount: null, vatRate: vatRate, CountryCode);

        var sut = this.Build();

        // Act
        var result = sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(model => model.VatRate).WithErrorMessage(expectedErrorMessage);
        result.Errors.Should().HaveCount(1);
    }
}
