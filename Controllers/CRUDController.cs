using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using examplemvc.Models.Request;
using examplemvc.Models;
using System;
using System.Linq;

namespace examplemvc.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly YourDbContext _dbContext;

        public PostController(ILogger<PostController> logger, YourDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("/Create")]
        public IActionResult InsertForm()
        {
            return View("/Views/CRUD/Create.cshtml");
        }

        [HttpPost("/Create")]
        public IActionResult ProcessInsert([FromBody] InsertPostRequest body)
        {
            if (body == null)
            {
                return BadRequest("Invalid request body");
            }

            var post = new Post
            {
                Title = body.Title,
                Body = body.Body,
                CreatedAt = DateTime.Now
            };

            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();

            return Ok(new { message = "Post created successfully", post });
        }

        [HttpGet("/Read/{id}")]
        public IActionResult ReadPost(int id)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return Ok(post);
        }

        [HttpPost("/Update/{id}")]
        public IActionResult UpdatePost(int id, [FromBody] InsertPostRequest body)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            post.Title = body.Title;
            post.Body = body.Body;

            _dbContext.SaveChanges();

            return Ok(new { message = "Post updated successfully", post });
        }

        [HttpPost("/Delete/{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();

            return Ok("Post deleted successfully");
        }
    }
}
