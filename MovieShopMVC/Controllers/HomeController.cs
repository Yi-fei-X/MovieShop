using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller    //default controller
    {
        /*private IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }*/
        // by default it's get method if you don't specify
        // but always specify to make it clear
        // URL https://localhost/home/index
        [HttpGet]
        public IActionResult Index()    //default method 
        {
            var movieCards = _movieService.GetTop30RevenueMovies();
            return View(movieCards);
            /*By default if you don't specify the view name, it will go to Views Folder 
             * => Home Folder => And then look for Index file (same name with action method)
             To go to specific view, write return View("view_name")*/
        }
        // URL https://localhost/home/privacy
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TopMovies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
