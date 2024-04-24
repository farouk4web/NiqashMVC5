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
    public class PostsController : ApiController
    {
        private ApplicationDbContext _context;
        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetPosts()
        {
            var posts = _context.Posts
                                .OrderByDescending(m => m.Loves.Count())
                                .ThenByDescending(m => m.Id)
                                .ToList()
                                .Select(Mapper.Map<Post, PostDto>);

            return Ok(posts);
        }

        public IHttpActionResult GetPost(int id)
        {
            var post = _context.Posts.Find(id);

            if (post == null)
                return NotFound();

            var postDto = Mapper.Map<Post, PostDto>(post);

            postDto.Comments = _context.Comments.Where(m => m.PostId == id)
                                                .OrderByDescending(m => m.Id).ToList()
                                                .Select(Mapper.Map<Comment, CommentDto>);

            return Ok(postDto);
        }

        [HttpPost]
        public IHttpActionResult CreatePost(PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            postDto.UserId = User.Identity.GetUserId();
            postDto.PublishDate = DateTime.Now;

            var post = Mapper.Map<PostDto, Post>(postDto);

            _context.Posts.Add(post);
            _context.SaveChanges();

            postDto.Id = post.Id;

            var currentUser = _context.Users.Find(User.Identity.GetUserId());
            postDto.User = Mapper.Map<ApplicationUser, UserDto>(currentUser);

            postDto.Comments = post.Comments.ToList().Select(Mapper.Map<Comment, CommentDto>);
            postDto.Likes = post.Likes.ToList().Select(Mapper.Map<Like, LikeDto>);
            postDto.Loves = post.Loves.ToList().Select(Mapper.Map<Love, LoveDto>);

            return Ok(postDto);
        }

        [HttpPut]
        public IHttpActionResult UpdatePost(int id, PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var postInDb = _context.Posts.Find(id);

            if (postInDb == null)
                return NotFound();

            var userId = User.Identity.GetUserId();
            if (userId != postInDb.UserId)
                return BadRequest();

            postInDb.Content = postDto.Content;
            _context.SaveChanges();

            postDto = Mapper.Map<Post, PostDto>(postInDb);

            return Ok(postDto);
        }

        [HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);

            if (post == null)
                return NotFound();

            var userId = User.Identity.GetUserId();
            if (userId != post.UserId)
                return BadRequest();

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return Ok();
        }

    }
}
