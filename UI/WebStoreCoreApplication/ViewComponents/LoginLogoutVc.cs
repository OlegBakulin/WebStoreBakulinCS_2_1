﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStoreCoreApplication.ViewComponents
{
    public class LoginLogoutVc : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
