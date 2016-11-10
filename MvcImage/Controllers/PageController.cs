using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcImage.Controllers
{
    public class PageController : Controller
    {
        //
        // GET: /Page/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            ViewData["page"] = page;
            return View();
        }

        public ActionResult AddressListPartial(int? page)
        {
            if(page == null)
            {
                page = 1;
            }
            ViewData["page"] = page;
            return PartialView(page);
        }

    }
}
