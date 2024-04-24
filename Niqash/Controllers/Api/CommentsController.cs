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
    public class CommentsController : ApiController
    {
        private ApplicationDbContext _context;
        public CommentsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetComments()
        {   
            var comments = _context.Comments.OrderByDescending(m => m.Id)
                          .ToList()
                          .Select(Mapper.Map<Comment, CommentDto>);

            return Ok(comments);
        }

        public IHttpActionResult GetComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
                return NotFound();

            var commentDto = Mapper.Map<Comment, CommentDto>(comment);

            return Ok(commentDto);
        }

        [HttpPost]
        public IHttpActionResult CreateComment(CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            commentDto.UserId = User.Identity.GetUserId();
            commentDto.PublishDate = DateTime.Now;

            var comment = Mapper.Map<CommentDto, Comment>(commentDto);

            _context.Comments.Add(comment);
            _context.SaveChanges();

            commentDto.Id = comment.Id;

            var currentUser = _context.Users.Find(User.Identity.GetUserId());
            commentDto.User = Mapper.Map<ApplicationUser, UserDto>(currentUser);

            return Ok(commentDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateComment(int id, CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var commentInDb = _context.Comments.Find(id);
            if (commentInDb == null)
                return NotFound();

            commentInDb.Content = commentDto.Content;
            _context.SaveChanges();

            commentDto = Mapper.Map<Comment, CommentDto>(commentInDb);

            return Ok(commentDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
                return NotFound();

            var userId = User.Identity.GetUserId();
            if (userId != comment.UserId)
                return BadRequest();

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return Ok();
        }
    }
}
