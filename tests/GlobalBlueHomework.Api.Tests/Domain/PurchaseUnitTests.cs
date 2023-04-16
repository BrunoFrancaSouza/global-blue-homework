using FluentAssertions;
using GlobalBlueHomework.Domain.Entities;
using Xunit;

namespace GlobalBlueHomework.Api.Tests.Unit.Domain;

public class PurchaseUnitTests
{
    public PurchaseUnitTests()
    {
    }

    [Fact]
    public void Create_WhenNetAmountGiven_ReturnsExpectedGrossAndVatAmounts()
    {
        // Arrange
        const float VatRate = 13f;
        const decimal NetAmount = 1231.97m;
        const decimal ExpectedGrossAmount = 1392.13m;
        const decimal ExpectedVatAmount = 160.16m;

        // Act
        var createPurchaseResult = Purchase.Create(NetAmount, null, null, VatRate);
        var sut = createPurchaseResult.Value;

        // Assert
        sut.VatRate.Should().Be(VatRate, "because it needs to have the same value as the provided as parameter");
        sut.NetAmount.Should().Be(NetAmount, "because it needs to have the same value as the provided as parameter");
        sut.GrossAmount.Should().Be(ExpectedGrossAmount);
        sut.VatAmount.Should().Be(ExpectedVatAmount);
    }

    [Fact]
    public void Create_WhenGrossAmountGiven_ReturnsExpectedNetAndVatAmounts()
    {
        // Arrange
        const float VatRate = 13f;
        const decimal GrossAmount = 1392.13m;
        const decimal ExpectedNetAmount = 1231.97m;
        const decimal ExpectedVatAmount = 160.16m;

        // Act
        var createPurchaseResult = Purchase.Create(null, GrossAmount, null, VatRate);
        var sut = createPurchaseResult.Value;

        // Assert
        sut.VatRate.Should().Be(VatRate, "because it needs to have the same value as the provided as parameter");
        sut.NetAmount.Should().Be(ExpectedNetAmount);
        sut.GrossAmount.Should().Be(GrossAmount, "because it needs to have the same value as the provided as parameter");
        sut.VatAmount.Should().Be(ExpectedVatAmount);
    }

    [Fact]
    public void Create_WhenVatAmountGiven_ReturnsExpectedNetAndGrossAmounts()
    {
        // Arrange
        const float VatRate = 13f;
        const decimal ExpectedNetAmount = 1232m;
        const decimal ExpectedGrossAmount = 1392.16m;
        const decimal VatAmount = 160.16m;

        // Act
        var createPurchaseResult = Purchase.Create(null, null, VatAmount, VatRate);
        var sut = createPurchaseResult.Value;

        // Assert
        sut.VatRate.Should().Be(VatRate, "because it needs to have the same value as the provided as parameter");
        sut.NetAmount.Should().Be(ExpectedNetAmount);
        sut.GrossAmount.Should().Be(ExpectedGrossAmount);
        sut.VatAmount.Should().Be(VatAmount, "because it needs to have the same value as the provided as parameter");
    }
}