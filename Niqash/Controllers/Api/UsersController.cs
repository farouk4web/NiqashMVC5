using AutoMapper;
using Niqash.Dtos;
using Niqash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Niqash.Controllers.Api
{
    [Authorize]
    public class UsersController : ApiController
    {
        private ApplicationDbContext _context;
        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetUsers()
        {
            var users = _context.Users.ToList()
                .Select(Mapper.Map<ApplicationUser, UserDto>);

            return Ok(users);
        }

        [HttpGet]
        public IHttpActionResult GetUser(string id)
        {
            var account = _context.Users.Find(id);
            if (account == null)
                return NotFound();

            var accountDto = Mapper.Map<ApplicationUser, AccountDto>(account);

            return Ok(accountDto);
        }

        [HttpPut]
        public IHttpActionResult GetFollowers(string id)
        {
            var follows = _context.Follows.Where(m => m.AccountId == id).ToList();

            var followers = new List<ApplicationUser>();

            foreach (var follow in follows)
            {
                followers.Add(follow.User);
            }

            return Ok(followers);
        }

        [HttpDelete]
        public IHttpActionResult GetFollowing(string id)
        {
            var follows = _context.Follows.Where(m => m.UserId == id).ToList();

            var Following = new List<ApplicationUser>();

            foreach (var follow in follows)
            {
                Following.Add(follow.Account);
            }

            return Ok(Following);
        }

    }
}
