using BankingChallenge.Core.Abstractions;
using BankingChallenge.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BankingChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("Calculate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public PaymentOverviewDto Calculate([FromQuery]LoanParametersDto request)
        {
            return _loanService.CalculateLoan(request);
        }
    }
}
