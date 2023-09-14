using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using atm_web.Models.Data;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ValidateAccount(int accountNumber, int pin)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber && a.PIN == pin);
        if (account == null)
        {
            ViewData["ErrorMessage"] = "Account not found";
            return View("Index");
        }

        return RedirectToAction("AccountDetails", new { accountNumber = account.AccountNumber });
    }

    public async Task<IActionResult> AccountDetails(int accountNumber)
    {
        var account = await _context.Accounts.FindAsync(accountNumber);
        if (account == null)
        {
            return NotFound();
        }

        return View(account);
    }


    [HttpPost]
    public async Task<IActionResult> Deposit(int accountNumber, decimal depositAmount)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            return NotFound();
        }

        
        account.Balance += depositAmount;
        await _context.SaveChangesAsync();

        return RedirectToAction("AccountDetails", new { accountNumber = account.AccountNumber });
    }


    [HttpPost]
    public async Task<IActionResult> Withdraw(int accountNumber, decimal withdrawAmount)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            return NotFound();
        }

        
        account.Balance -= withdrawAmount;
        await _context.SaveChangesAsync();

        return RedirectToAction("AccountDetails", new { accountNumber = account.AccountNumber });
    }


    public IActionResult Logout()
    {
        

        return RedirectToAction("Index", "Home"); 
    }


    [HttpPost]
    public async Task<IActionResult> ChangePIN(int accountNumber, int newPIN)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            return NotFound();
        }

        account.PIN = newPIN;
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "PIN changed successfully." });
    }
}
