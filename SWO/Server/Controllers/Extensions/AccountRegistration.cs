using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWO.Server.Controllers.Extensions
{
    public class AccountRegistration : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
