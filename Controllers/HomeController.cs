using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bolt.Models;
using Bolt.Logic.Services;
using Bolt.Services;

namespace Bolt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _service;
        private readonly IEmailSender _sender;

        public HomeController(IHomeService service, IEmailSender sender)
        {
            _service = service;
            _sender = sender;
        }

        public async Task<IActionResult> Index()
        {
            var indexVM = await _service.GetIndexViewModel();

            return View(indexVM);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> SendEmail()
        {
           await _sender.SendEmailAsync("starodub.ilya12@gmail.com", "Test2", "1111test krokus message");

            return Ok();
        }
    }
}
