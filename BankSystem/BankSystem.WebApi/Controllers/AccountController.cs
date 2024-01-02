using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Tüm işlemler için kimlik doğrulama gereklidir
[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly BankingDbContext _context;

    public AccountController(BankingDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "admin")] // Sadece admin rolüne sahip kullanıcılar tarafından erişilebilir
    [HttpPost("create-account")]
    public IActionResult CreateAccount([FromBody] AccountModel model)
    {
        // Hesap açılışında minimum bakiye kontrolü yapılabilir
        if (model.Balance < AccountConstants.MinimumBalance)
        {
            return BadRequest("Minimum bakiye miktarını karşılamıyor.");
        }

        var newAccount = new AccountModel
        {
            //AccountNumber = GenerateAccountNumber(), // Hesap numarası oluşturulması gerekiyor
            UserId = model.UserId, 
            Balance = model.Balance
        };

        _context.Accounts.Add(newAccount);
        _context.SaveChanges();

        return Ok(new { Message = "Hesap başarıyla oluşturuldu." });
    }

    [HttpGet("get-balance/{accountId}")]
    public IActionResult GetBalance(string accountId)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountId);

        if (account == null)
        {
            return NotFound("Hesap bulunamadı.");
        }

        // Hesap bakiyesini alma işlemi
        decimal balance = account.Balance;

        return Ok(new { Balance = balance });
    }

    [Authorize(Roles = "admin, user")] // Admin ve kullanıcı rollerine sahip herkes tarafından erişilebilir
    [HttpPost("update-balance")]
    public IActionResult UpdateBalance([FromBody] AccountModel model)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == model.AccountNumber);

        if (account == null)
        {
            return NotFound("Hesap bulunamadı.");
        }

        // Yalnızca admin rolüne sahip kullanıcılar herhangi bir hesabın bakiyesini güncelleyebilir
        if (!User.IsInRole("admin") && account.UserId != User.Identity.Name)
        {
            return Forbid("Bu işlemi gerçekleştirmek için yetkiniz yok.");
        }

        // Hesap bakiyesini güncelleme işlemi
        account.Balance = model.NewBalance;
        _context.SaveChanges();

        return Ok(new { Message = "Bakiye başarıyla güncellendi." });
    }

    public static class AccountConstants
    {
        public const decimal MinimumBalance = 100.0m; 
                                                      
    }
}
