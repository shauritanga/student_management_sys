using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using sms.Data;
using sms.Models;
namespace sms.Controllers;

public class StudentController:Controller
{
    public StudentController(){}

    public IActionResult Index(){
      if (User.Identity.IsAuthenticated)
        {
            if (HttpContext.Session.GetString("UserName") == null)
        {
            // Session has expired, redirect to the login page
            return RedirectToAction("Login", "Home");
        }
           
            return View();
        }
        else
        {
           
            return RedirectToAction("Login", "Home");
        }
    }
}