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
    public class LovesController : ApiController
    {
        private ApplicationDbContext _context;
        public LovesController()
        {
            _context = new ApplicationDbContext();
        }

        // POST: api/Likse
        [HttpPost]
        public IHttpActionResult AddLove(LoveDto loveDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            loveDto.UserId = User.Identity.GetUserId();

            var sameLove = _context.Loves.Where(
                m => m.UserId == loveDto.UserId && m.PostId == loveDto.PostId);

            if (sameLove.Count() != 0)
                return BadRequest();

            var love = Mapper.Map<LoveDto, Love>(loveDto);
            _context.Loves.Add(love);
            _context.SaveChanges();

            loveDto.Id = love.Id;

            var currentUser = _context.Users.Find(User.Identity.GetUserId());
            loveDto.User = Mapper.Map<ApplicationUser, UserDto>(currentUser);

            return Ok(loveDto);
        }

        // DELETE: api/Likse/5
        [HttpDelete]
        public IHttpActionResult DeleteLove(int id)
        {
            var love = _context.Loves.Find(id);
            if (love == null)
                return NotFound();

            if (love.UserId != User.Identity.GetUserId())
                return BadRequest();

            _context.Loves.Remove(love);
            _context.SaveChanges();

            return Ok();
        }
    }
}
