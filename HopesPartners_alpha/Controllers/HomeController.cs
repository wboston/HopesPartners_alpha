﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HopesPartners_alpha.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "SuperAdmin, Admin, Content-Manager, Account-Manager, Partner")]
        public ActionResult Index()
        {
            return View();
        }

        
    }
}