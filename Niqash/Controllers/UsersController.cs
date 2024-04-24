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
    public class UsersController : Controller
    {

        private ApplicationDbContext _context;
        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        //[Route("{nickname}")]
        public ActionResult Account(string id)
        {
            if (id == null)
                return HttpNotFound();

            var viewModel = new UserAccountVm
            {
                UserId = id,
                NewPost = new Post(),
                NewComment = new Comment()
            };

            return View(viewModel);
        }

    }
}