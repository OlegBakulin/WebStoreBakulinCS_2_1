using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.TestApi;

namespace WebStoreCoreApplication.Controllers
{
    [Route("values")]
    public class WebAPIController : Controller
    {

        private readonly IValueService _ValueService;

        public WebAPIController(IValueService ValueService) => _ValueService = ValueService;
       
        [Route("Index")]
        public IActionResult Index()
        {
            var values = _ValueService.Get();
            return View(values);
        }
       
        [Route("{id}")]
        [HttpGet]
        public IActionResult ValueByID(int id)
        {
            var values = _ValueService.Get(id);
            return View(values);
        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _ValueService.Delete(id-1);
            return RedirectToAction(nameof(Index));
        }
    }
}
