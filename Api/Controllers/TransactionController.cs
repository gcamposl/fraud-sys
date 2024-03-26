using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] TransactionDTO transactionDTO)
        {
            var result = await _transactionService.CreateAsync(transactionDTO);
            if (result.IsSuccess)
                return Ok(transactionDTO);

            return BadRequest(transactionDTO);
        }
    }
}