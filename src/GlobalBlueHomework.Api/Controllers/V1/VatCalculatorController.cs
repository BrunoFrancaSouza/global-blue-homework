using AutoMapper;
using GlobalBlueHomework.Api.Dtos.V1.Purchases;
using GlobalBlueHomework.Api.Dtos.V1.Purchases.Calculator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using GlobalBlueHomework.Application.Services.Purchases.Calculator;
using GlobalBlueHomework.Application.Constants;

namespace GlobalBlueHomework.Api.Controllers.V1
{
    /// <summary>
    /// Controller to manage Purchase related operations.
    /// Controller to manage all Value Added Tax (VAT) operations.
    /// </summary>
    [ApiController, ApiVersion("1.0")]
    [Route("api/vat-calculator")]
    [Produces("application/json")]
    public class VatCalculatorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<VatCalculatorController> _logger;
        private readonly ICalculatePurchaseAmountsService _calculatePurchaseAmountsService;

        public VatCalculatorController(ILogger<VatCalculatorController> logger, IMapper mapper, ICalculatePurchaseAmountsService calculatePurchaseAmountsService)
        {
            _logger = logger;
            _mapper = mapper;
            _calculatePurchaseAmountsService = calculatePurchaseAmountsService;
        }

        /// <summary>
        /// Calculates Net, Gross and Value Added Tax (VAT) amounts for purchases in Austria.
        /// </summary>
        /// <remarks>
        /// Receives one of the net, gross or VAT amounts and additionally a valid Austrian VAT rate (10%, 13%, 20%).
        /// 
        /// The other two missing amounts (net/gross/VAT) are calculated and returned.
        /// </remarks>
        /// <param name="request"></param>
        [HttpPost()]
        [SwaggerResponse(StatusCodes.Status200OK, "Purchase details.", Type = typeof(PurchaseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request.", Type = typeof(ProblemDetails))]
        public IActionResult CalculateAustrianPurchaseAmounts(CalculatePurchaseAmountsRequestDto request)
        {
            var serviceInput = _mapper.Map<CalculatePurchaseAmountsServiceInput>(request);
            serviceInput.CountryCode = CountryCodes.Austria;
            var serviceResult = _calculatePurchaseAmountsService.Execute(input: serviceInput);

            return serviceResult.Match<IActionResult>(
                serviceOutput => Ok(_mapper.Map<PurchaseDto>(serviceOutput)),
                validationFailed => BadRequest(validationFailed.Errors)
            );
        }
    }
}