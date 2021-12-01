using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    // all the action methods in User Controller should work only when user is Authenticated (login success)
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        public UserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            // purchase a movie when user clicks on Buy button on Movie Details Page
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Favorite()
        {
            // favorite a movie when user clicks on Favorite Button on Movie Details Page
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Review()
        {
            // add a new review done by the user for that movie
            return View();
        }
        [HttpGet]
        // Filters in ASP.NET 
        [Authorize]     // Filter(Authorize attribute)
        public async Task<IActionResult> Purchases()
        {
            // get the id from HttpCOntext.User.Claims
            // get all the movies that purchased by the user. Data type: List<MovieCard>
            // var userId = _currentUserService.UserId;
            // pass the user id to the UserService, that will pass to the UserRepository
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Favorites(int id)
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            // get all the reviews by the user
            return View();
        }
    }
}
