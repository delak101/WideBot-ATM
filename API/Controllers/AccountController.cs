using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly ITokenService tokenService;

    private readonly DataContext _context;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        this.tokenService = tokenService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUser()
    {
        var users = _context.Users.ToList();
        return users;
    }

    [HttpPost("register")] // Post: api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

        if (await CardExist(registerDto.CardNum)) return BadRequest("Card Already Exist");

        using var hmac = new HMACSHA512();

        byte[] pinBytes = BitConverter.GetBytes(registerDto.PIN);

        var user = new User
        {
            CardNum = registerDto.CardNum,
            PinSalt = hmac.Key,
            PinHash = hmac.ComputeHash(pinBytes)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        //instance of userDto to pass the username and the token
        return new UserDto
        {
            CardNum = user.CardNum,
            Token = tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")] //Post: api/account/login

    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync( 
            x => x.CardNum == loginDto.CardNum);    

            if (user == null) return Unauthorized("Invalid Card");

        using var hmac = new HMACSHA512(user.PinSalt);

        byte[] pinBytes = BitConverter.GetBytes(loginDto.PIN);

        var computedHash = hmac.ComputeHash(pinBytes);

        for (int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PinHash[i]) return Unauthorized("invalid PIN");
        }
        return new UserDto
        {
            CardNum = user.CardNum,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> CardExist(int CardNum)
    {
        return await _context.Users.AnyAsync(x => x.CardNum == CardNum);
    }
}
