using FluentAssertions;
using FluentValidation;
using GlobalBlueHomework.Application.Services.Purchases.Calculator;
using MoqMeUp;
using Xunit;
using Moq;
using GlobalBlueHomework.Application.Models;
using AutoFixture;
using FluentValidation.Results;
using AutoMapper;

namespace GlobalBlueHomework.Api.Tests.Unit.Services.Purchases.Calculator;

public class CalculatePurchaseAmountsServiceTests : MoqMeUp<CalculatePurchaseAmountsService>
{
    private readonly IMapper _mapper;

    public CalculatePurchaseAmountsServiceTests()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new CalculatePurchaseAmountsServiceMapper());
        });

        _mapper = mappingConfig.CreateMapper();
        this.Set<IMapper>(_mapper);
    }

    [Theory]
    [ClassData(typeof(CalculatePurchaseAmountsServiceTestData))]
    public void Execute_Default_ShouldReturnPurchaseObjectWithCalculatedProperties(
        CalculatePurchaseAmountsServiceInput input,
        CalculatePurchaseAmountsServiceOutput output)
    {
        // Arrange
        var validationResult = new ValidationResult();
        var validatorMock = this.Get<IValidator<CalculatePurchaseAmountsServiceInput>>();

        validatorMock.Setup(x => x.Validate(It.IsAny<CalculatePurchaseAmountsServiceInput>()))
                     .Returns(validationResult);

        var sut = this.Build();

        // Act
        var result = sut.Execute(input);

        // Assert
        result.Value.Should().BeOfType(typeof(CalculatePurchaseAmountsServiceOutput));
        var resultValue = (CalculatePurchaseAmountsServiceOutput)result.Value;
        resultValue.VatRate.Should().Be(output.VatRate);
        resultValue.NetAmount.Should().Be(output.NetAmount);
        resultValue.GrossAmount.Should().Be(output.GrossAmount);
        resultValue.VatAmount.Should().Be(output.VatAmount);
    }

    [Fact]
    public void Execute_WhitValidatorReturningErrors_ShouldReturnValidationErrors()
    {
        // Arrange
        var fixture = new Fixture();
        var serviceInput = fixture.Create<CalculatePurchaseAmountsServiceInput>();

        var expectedValidationErrors = new List<ValidationFailure>()
        {
            new(fixture.Create<string>(), fixture.Create<string>()),
            new(fixture.Create<string>(), fixture.Create<string>())
        };

        var expectedValidationResult = new ValidationResult(expectedValidationErrors);
        var validatorMock = this.Get<IValidator<CalculatePurchaseAmountsServiceInput>>();
        validatorMock.Setup(x => x.Validate(It.IsAny<CalculatePurchaseAmountsServiceInput>()))
                     .Returns(expectedValidationResult);

        var sut = this.Build();

        // Act
        var result = sut.Execute(serviceInput);

        // Assert
        result.Value.Should().BeOfType(typeof(ValidationFailed));
        var resultValue = (ValidationFailed)result.Value;
        resultValue.Errors.Should().BeEquivalentTo(expectedValidationErrors);
    }
}