using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.TestApi;

namespace WebStoreCoreApplication.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValueService _ValueService;

        public WebAPIController(IValueService ValueService) => _ValueService = ValueService;

        public IActionResult Index()
        {
            var values = _ValueService.Get();
            return View(values);
        }
    }
}
