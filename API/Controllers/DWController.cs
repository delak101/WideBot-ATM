using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class DWController : BaseApiController
{
    private readonly DataContext _context;
    public DWController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("Balance/{cardnum}")] // Get: api/dw/balance/{cardnum}
    public async Task<ActionResult<decimal>> GetBalance(long cardNum)
    {
        var user = await GetUserByCardNum(cardNum);

        if (user == null)
            return BadRequest("Invalid Card Number");
        return Ok(user.Balance);
    }
    [HttpPost("Deposit")] // Post: api/dw/Deposit
    public async Task<ActionResult<DWDto>> Deposit(DWRequest request)
    {
        var user = await GetUserByCardNum(request.CardNum);

        if (user == null)
            return NotFound("User not found");

        user.Balance += request.Amount;
        await _context.SaveChangesAsync();

        var result = new DWDto
        {
            CardNum = user.CardNum,
            Amount = request.Amount,
            Balance = user.Balance
        };

        return Ok(result);

    }
    [HttpPost("Withdraw")] // Post: api/dw/Withdraw
    public async Task<ActionResult<DWDto>> Withdraw(DWRequest request)
    {
        var user = await GetUserByCardNum(request.CardNum);

        if (user == null)
            return NotFound("User not found");

        if (user.Balance < request.Amount)
            return BadRequest("Insufficient funds");

        user.Balance -= request.Amount;
        await _context.SaveChangesAsync();

        var result = new DWDto
        {
            CardNum = user.CardNum,
            Amount = request.Amount,
            Balance = user.Balance
        };

        return Ok(result);
    }
    public class DWRequest
    {
        public long CardNum { get; set; }
        public decimal Amount { get; set; }
    }

    private async Task<User> GetUserByCardNum(long cardNum)
    {
            return await _context.Users.FirstOrDefaultAsync(u => u.CardNum == cardNum);
    }
}
