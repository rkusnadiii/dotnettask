using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using examplemvc.Models.Request;
using Microsoft.AspNetCore.Authorization;
using examplemvc.Models;
using System;
using System.Linq;
using examplemvc.Filters;

namespace examplemvc.Controllers
{
    // [TypeFilter(typeof(CustomAuthorizeFilter))]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public PostController(ILogger<PostController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("/Home/Create")]
        public IActionResult InsertForm()
        {
            return View("/Views/CRUD/Create.cshtml");
        }

        [HttpPost("/Home/Create")]
        public IActionResult ProcessInsert([FromForm] InsertPostRequest body)
        {
            try
            {
                var res = ModelState
                .Select(s => s.Value.Errors)
                .Where(w => w.Count > 0)
                .ToList();
                if (ModelState.IsValid == false || body == null)
                {
                    return BadRequest("Invalid request body");
                }

                var post = new Post()
                {
                    Title = body.Title,
                    Body = body.Body,
                    CreatedAt = DateTime.Now
                };

                _dbContext.Posts.Add(post);
                _dbContext.SaveChanges();

                DisplaySuccessMessage("Post created successfully!");
                return RedirectToAction("ReadPost");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Failed to create post: {ex.Message}");
                return RedirectToAction("InsertForm");
            }
        }

        [HttpGet("/Home/Read")]
        public IActionResult ReadPost()
        {
            var posts = _dbContext.Posts.ToList();
            return View("/Views/CRUD/AllPost.cshtml");
        }


        [HttpGet("/Home/Read/{id}")]
        public IActionResult ReadPost(int id)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return View("/Views/CRUD/Read.cshtml", post);
        }

        [HttpGet("/Home/Update/{id}")]
        public IActionResult UpdatePost()
        {
            return View("/Views/CRUD/Update.cshtml");
        }
        [HttpPost("/Home/Update/{id}")]
        public IActionResult UpdatePost(int id, [FromForm] InsertPostRequest body)
        {
            try
            {
                var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return NotFound("Post not found");
                }

                post.Title = body.Title;
                post.Body = body.Body;

                _dbContext.SaveChanges();

                DisplaySuccessMessage("Post updated successfully!");
                return RedirectToAction("ReadPost", new { id = post.Id });
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Failed to update post: {ex.Message}");
                return RedirectToAction("UpdatePost", new { id });
            }
        }

        [HttpGet("/Home/Delete/{id}")]
        public IActionResult DeletePost()
        {
            return View("/Views/CRUD/Delete.cshtml");
        }
        [HttpPost("/Home/Delete/{id}")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return NotFound("Post not found");
                }

                _dbContext.Posts.Remove(post);
                _dbContext.SaveChanges();

                DisplaySuccessMessage("Post deleted successfully!");
                return RedirectToAction("ReadPost");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Failed to delete post: {ex.Message}");
                return RedirectToAction("DeletePost", new { id });
            }
        }

        public void DisplaySuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        public void DisplayErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }

    }
}
