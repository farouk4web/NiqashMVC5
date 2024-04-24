using Microsoft.AspNet.Identity;
using Niqash.Models;
using Niqash.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Niqash.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext _context;
        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}