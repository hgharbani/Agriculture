using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agriculture.Areas.Admin.Controllers
{
    [Authorize]
    public partial class DashboardController : Controller
    {
        // GET: Admin/Dashboard
       [Authorize] public virtual ActionResult Index()
        {
            return View();
        }
    }
}