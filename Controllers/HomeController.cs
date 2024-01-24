using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sms.Data;
using sms.Models;
using Microsoft.AspNetCore.Identity;

namespace sms.Controllers;

public class HomeController:Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _db;
    public HomeController( ApplicationDbContext db,SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager){
    _db=db;
      _signInManager = signInManager;
    _userManager = userManager;
    }

    public IActionResult Index(){

        if (User.Identity.IsAuthenticated)
        {
           
            return View();
        }
        else
        {
           
            return RedirectToAction("Login");
        }
    }

    public IActionResult Login(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.SignInAsync(user, model.RememberMe, "false");
                HttpContext.Session.SetString("UserName", model.UserName);
                return RedirectToAction("Index", "Student"); // Redirect to a successful login page
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
        }

        return View(model); // Return to the login page with errors
    }

    public IActionResult Register(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Optionally sign in the user after registration
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Student"); // Redirect to a successful registration page
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model); // Return to the registration page with errors
    }

    public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Student"); // Redirect to a successful logout page
}
}