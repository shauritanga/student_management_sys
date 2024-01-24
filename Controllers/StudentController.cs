using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sms.Data;
using sms.Models;
namespace sms.Controllers;

public class StudentController:Controller
{
    public StudentController(){}

    public IActionResult Index(){
      if (User.Identity.IsAuthenticated)
        {
           
            return View();
        }
        else
        {
           
            return RedirectToAction("Login", "Home");
        }
    }
}