using Niqash.Models;
using Niqash.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Niqash.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return RedirectToAction("About", "Home");
        }

        public ActionResult About()
        {
            var viewModel = new AboutVm
            {
                UsersNumebr = _context.Users.Count(),
                PostsNumebr = _context.Posts.Count(),
                CommentsNumebr = _context.Comments.Count(),
                ReactNumebr = _context.Likes.Count() + _context.Loves.Count()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Search(string query)
        {
            if (query == null || query == "" || query == " ")
                return HttpNotFound();

            var users = _context.Users.Where(m => m.FirstName.Contains(query) || m.LastName.Contains(query));
            return View(users);
        }

        public ActionResult ChangeLanguge(string lang, string returnUrl)
        {
            // change the languge and the culture of site 
            if (lang != null)
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            // assign new value on cookie with "Language" key
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = lang;
            Response.Cookies.Add(cookie);

            // return to home Page
            if (returnUrl == null)
                return RedirectToAction("Index");

            return Redirect(returnUrl);
        }
    }
}