using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Api.Controllers
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

        [HttpGet("CreateTransaction")]
        public IActionResult Get()
        {
            return Ok("TransactionController is working!");
        }
        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO dto)
        {
            var result = await _transactionService.CreateAsync(dto);
            if(result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
        [HttpPost("GetTransactionById")]
        public async Task<IActionResult> GetTransactionById([FromBody] Guid transactionGuid)
        {
            var result = await _transactionService.GetByGuidAsync(transactionGuid);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
        [HttpPost("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransactionAsync([FromBody] Guid guid)
        {
            var result = await _transactionService.DeleteAsync(guid);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}
