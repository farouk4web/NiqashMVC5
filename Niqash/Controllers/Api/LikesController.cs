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
    public class LikesController : ApiController
    {
        private ApplicationDbContext _context;
        public LikesController()
        {
            _context = new ApplicationDbContext();
        }
       
        // POST: api/Likse
        [HttpPost]
        public IHttpActionResult AddLike(LikeDto likeDto)
         {
            if (!ModelState.IsValid)
                return BadRequest();

            likeDto.UserId = User.Identity.GetUserId();

            var sameLike = _context.Likes.Where(
                m => m.UserId == likeDto.UserId && m.PostId == likeDto.PostId);

            if (sameLike.Count() != 0)
                return BadRequest();

            var like = Mapper.Map<LikeDto, Like>(likeDto);
            _context.Likes.Add(like);
            _context.SaveChanges();

            likeDto.Id = like.Id;

            var currentUser=_context.Users.Find(User.Identity.GetUserId());
            likeDto.User = Mapper.Map<ApplicationUser, UserDto>(currentUser);

            return Ok(likeDto);
        }

        // DELETE: api/Likse/5
        [HttpDelete]
        public IHttpActionResult DeleteLike(int id)
        {
            var like = _context.Likes.Find(id);
            if (like == null)
                return NotFound();

            if (like.UserId != User.Identity.GetUserId())
                return BadRequest();

            _context.Likes.Remove(like);
            _context.SaveChanges();

            return Ok();
        }

    }
}
