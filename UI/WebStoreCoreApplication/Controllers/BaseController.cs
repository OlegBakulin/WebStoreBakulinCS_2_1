using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStoreCoreApplication.Controllers.Infrastructure;

namespace WebStoreCoreApplication.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Page404()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Blog_Single()
        {
            return View();
        }
       
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Throw(string id) =>
            throw new ApplicationException($"Исключение: {id ?? "<null>"}");

        public IActionResult ErrorStatus(string Code) => Code switch
        {
            "404" => RedirectToAction(nameof(Page404)),
            _ => Content($"Error {Code}")
        };


    }
}