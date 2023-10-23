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
using examplemvc.Data;


namespace examplemvc.Controllers
{
    [TypeFilter(typeof(CustomAuthorizeFilter))]
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
        public IActionResult ProcessInsert([FromForm] InsertPostRequest data)
        {
          var res = ModelState
            .Select(s => s.Value.Errors)
            .Where(w => w.Count > 0)
            .ToList();
        
        if (ModelState.IsValid == false)
        {
            return View("/Views/Errors.cshtml", res);
        }
        
        var d = new Post() {
            Title = data.Title,
            Body = data.Body,
            CreatedAt = DateTime.Now
        };

        _dbContext.Post.Add(d);
        _dbContext.SaveChanges();

        TempData.Add("msg", "Berhasil add data");

        // return Ok(new {body = request});
        return RedirectToAction("HomePost");
        }

        [HttpGet("/Home/Read")]
        public IActionResult HomePost()
        {
            var posts = _dbContext.Post.ToList();
            return View("/Views/CRUD/AllPost.cshtml", posts);
        }

        [HttpGet("/Home/Read/{id}")]
        public IActionResult ReadPost(int id)
        {
            var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return View("/Views/CRUD/Read.cshtml", post);
        }

        [HttpGet("/Home/Update/{id}")]
        public IActionResult UpdatePost(int id)
        {
            var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return View("/Views/CRUD/Update.cshtml", post);
        }

        [HttpPost("/Home/Update/{id}")]
        public IActionResult UpdatePost(int id, [FromForm] InsertPostRequest data)
        {
            try
            {
                var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return NotFound("Post not found");
                }

                post.Title = data.Title;
                post.Body = data.Body;

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
        public IActionResult DeletePost(int id)
        {
            var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return View("/Views/CRUD/Delete.cshtml", post);
        }

        [HttpPost("/Home/Delete/{id}")]
        public IActionResult DeletePostConfirmed(int id)
        {
            try
            {
                var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return NotFound("Post not found");
                }

                _dbContext.Post.Remove(post);
                _dbContext.SaveChanges();

                DisplaySuccessMessage("Post deleted successfully!");
                return RedirectToAction("HomePost");
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
