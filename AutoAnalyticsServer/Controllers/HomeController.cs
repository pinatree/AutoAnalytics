/*
 * Common html page controller
 * 
 * Main page of our service
 * ROUTE: /
 *        /home
 * INPUT: -
 * OUTPUT: html page
 */

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AutoAnalyticsServer.EFModel;
using AutoAnalyticsServer.Models;

namespace AutoAnalyticsServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Instruction()
        {
            return View();
        }

        public IActionResult Privacy()
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
