using AutoMapper;
using Microsoft.AspNet.Identity;
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
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetUserPosts()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUserPosts = _context.Posts.Where(m => m.UserId == currentUserId)
                        .OrderByDescending(m => m.Id)
                        .ToList()
                        .Select(Mapper.Map<Post, PostDto>);

            var allPosts = new List<PostDto>();

            var follows = _context.Follows.Where(m => m.UserId == currentUserId).ToList();
            if (follows.Count() != 0)
            {
                foreach (var follow in follows)
                {
                    var accountPosts = _context.Posts.Where(m => m.UserId == follow.AccountId)
                            .OrderByDescending(m => m.Id)
                            .ToList()
                            .Select(Mapper.Map<Post, PostDto>);

                    allPosts = currentUserPosts.Concat(accountPosts)
                                .OrderByDescending(m => m.Loves.Count())
                                .ThenByDescending(m => m.Id)
                                .ToList();
                }
            }
            else
            {
                allPosts = currentUserPosts.ToList();
            }

            return Ok(allPosts);
        }

        [HttpGet]
        public IHttpActionResult GetFollowersAndFollowingNumber(string id)
        {
            // for current account
            var followers = _context.Follows.Where(m => m.AccountId == id);

            // for current user
            var following = _context.Follows.Where(m => m.UserId == id);

            return Ok(new { followersCount = followers.Count(), followingCount = following.Count() });
        }

        [HttpPost]
        public IHttpActionResult Follow(string idOfAccount)
        {
            if (!ModelState.IsValid)
                return BadRequest("ModelState Is Not Valid.......");

            var follow = new Follow();
            follow.AccountId = idOfAccount;
            follow.UserId = User.Identity.GetUserId();

            var followsInDb = _context.Follows.Where(m => m.AccountId == follow.AccountId);

            foreach (var f in followsInDb)
            {
                if (f.UserId == follow.UserId)
                    return BadRequest("You are stupid to try this <0_0>");
                break;
            }

            _context.Follows.Add(follow);
            _context.SaveChanges();

            return Ok(follow);
        }

        [HttpPut]
        public IHttpActionResult CheckFollow(string accountId)
        {
            var followsInDb = _context.Follows.Where(m => m.AccountId == accountId);
            var userId = User.Identity.GetUserId();

            foreach (var f in followsInDb)
            {
                if (f.UserId == userId)
                    return Ok(new { currentUserIsFollower = true });
                break;
            }

            return Ok(new { currentUserIsFollower = false });
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string idForAccount)
        {
            var currentUserId = User.Identity.GetUserId();

            var followInDb = _context.Follows.SingleOrDefault(m =>
                m.UserId == currentUserId
                &&
                m.AccountId == idForAccount
                );

            if (followInDb == null)
                return NotFound();

            _context.Follows.Remove(followInDb);
            _context.SaveChanges();

            return Ok();
        }


    }
}
