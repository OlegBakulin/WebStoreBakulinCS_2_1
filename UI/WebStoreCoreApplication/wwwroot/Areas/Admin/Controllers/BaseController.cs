using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebStoreCoreApplication.wwwroot.Areas.Admin.Controllers
{
    [Area("Boss, Admin, Manager")]
    [Authorize(Roles = "Boss, Admin, Manager")]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
