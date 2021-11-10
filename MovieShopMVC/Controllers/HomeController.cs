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
using ApplicationCore.Models;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller    //default controller
    {
        private readonly IMovieService _movieService;    //_ means an object of private variable
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // by default it's get method if you don't specify
        // but always specify to make it clear
        // URL https://localhost/home/index
        [HttpGet]
        public async Task<IActionResult> Index()    //default method 
        {
            // call movie service class to get list of movie card models
            
             var movieCards = await _movieService.GetTop30RevenueMovies();

            // To pass data from controller to view:
            // 1. use strongly type models (Prefered)
            // 2. ViewBag and 3. ViewData
            //ViewBag.PageTitle = "Top Revenue Movies";   //ViewBag, dynamic type (Not recommend)
            //ViewData["xyz"] = "test data";  //ViewData, object type, to pass object (Not recommend)
            return View(movieCards);    //strongly type models  (Prefered)
            // Cannot pass multiple models to a view. When view request multiple modes, create ViewModel.
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
