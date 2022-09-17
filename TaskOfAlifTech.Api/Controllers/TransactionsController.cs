using Microsoft.AspNetCore.Mvc;
using TaskOfAlifTech.Domain.Configurations;
using TaskOfAlifTech.Service.DTOs.Transactions;
using TaskOfAlifTech.Service.Interfaces;

namespace TaskOfAlifTech.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService transactionService;
    public TransactionsController(ITransactionService transactionService)
    {
        this.transactionService = transactionService;
    }

    /// <summary>
    /// Report all kind of transactions
    /// </summary>
    /// <param name="paginationParams"></param>
    /// <returns></returns>
    [HttpGet("report-all")]
    public async Task<IActionResult> GetAllTransactions([FromQuery] PaginationParams paginationParams)
    {
        var thisMonth = DateTime.Now.Month;
        var thisYear = DateTime.Now.Year;

        return Ok(await transactionService.GetAllAsync(paginationParams, transaction =>
            transaction.CreatedAt.Month == thisMonth && transaction.CreatedAt.Year == thisYear));
    }

    /// <summary>
    /// Report only replenishment transactions
    /// </summary>
    /// <param name="paginationParams"></param>
    /// <returns></returns>
    [HttpGet("report-replenishments")]  
    public async Task<IActionResult> GetAllReplenishments([FromQuery] PaginationParams paginationParams)
    {
        var thisMonth = DateTime.Now.Month;
        var thisYear = DateTime.Now.Year;

        var replenishments = await transactionService.GetAllAsync(paginationParams, transaction =>
            transaction.FromWalletId == null &&
            transaction.CreatedAt.Month == thisMonth &&
            transaction.CreatedAt.Year == thisYear);

        return Ok(new TransactionReplenishmentDto
        {
            TotalAmount = replenishments.Sum(rep => rep.Amount),
            TotalCount = replenishments.Count(),
            Data = replenishments
        });
    }

    /// <summary>
    /// Report only person to person transactions
    /// </summary>
    /// <param name="paginationParams"></param>
    /// <returns></returns>
    [HttpGet("report-p2p")]
    public async Task<IActionResult> GetAllTransactionsP2p([FromQuery] PaginationParams paginationParams)
    {
        var thisMonth = DateTime.Now.Month;
        var thisYear = DateTime.Now.Year;

        var replenishments = await transactionService.GetAllAsync(paginationParams, transaction =>
            transaction.FromWalletId != null &&
            transaction.CreatedAt.Month == thisMonth &&
            transaction.CreatedAt.Year == thisYear);

        return Ok(new TransactionReplenishmentDto
        {
            TotalAmount = replenishments.Sum(rep => rep.Amount),
            TotalCount = replenishments.Count(),
            Data = replenishments
        });
    }

    /// <summary>
    /// Create replenishment transaction
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("replenishment")]
    public async Task<IActionResult> Replenishment(TransactionToAddMoneyDto dto) 
        => Ok(await transactionService.AddMoneyAsync(dto));

    /// <summary>
    /// Create person to person transaction
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("p2p")]
    public async Task<IActionResult> AddTransaction(TransactionForCreationDto dto) 
        => Ok(await transactionService.AddAsync(dto));

}

