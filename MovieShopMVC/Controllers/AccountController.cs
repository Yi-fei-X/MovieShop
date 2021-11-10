using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        // will execute when user clicks on Register button in the view
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            // save the user registration information to the database
            // receive the model from view
            return View();
        }
        // use this action method to display empty view
        // No need to be async because we don't contact the database here
        [HttpGet]
        public IActionResult Register() //Why first go this method??
        {
            return View();
        }
    }
}
