﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bomix_Force.Models;
using Microsoft.AspNetCore.Authorization;
using Bomix_Force.AppServices;

namespace Bomix_Force.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            try
            {
                Notify("AAAAAAAAAAAAAAAAAAA");
            }
            catch (Exception)
            {

            }
           
            return View();
        }

        public IActionResult Privacy()
        {
            try
            {
                Notify("AAAAAAAAAAAAAAAAAAA");
            }
            catch (Exception)
            {

            }
            return View();
        }

        public IActionResult Login()
        {
            try
            {
                Notify("AAAAAAAAAAAAAAAAAAA");
            }
            catch (Exception)
            {

            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
