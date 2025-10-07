﻿using FinanceDashboard.Application.DTOs.Transaction;
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
            var result = await _transactionService.CreateTransactionAsync(dto);
            if(result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
