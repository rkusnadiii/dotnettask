using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using examplemvc.Models.Request;
using Microsoft.AspNetCore.Authorization;
using examplemvc.Models;
using System;
using System.Linq;

namespace examplemvc.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly YourDbContext _dbContext;

        [HttpGet("/Home/Login")]
        public IActionResult Login()
        {
            return View("/Views/CRUD/Login.cshtml");
        }

        [HttpPost("/Home/Login")]
        public IActionResult Login(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                DisplaySuccessMessage("Login successful!");
                return RedirectToAction("ReadPost");
            }
            else
            {
                DisplayErrorMessage("Login failed. Invalid username or password.");
                return RedirectToAction("Login");
            }
        }

        [HttpGet("/Home/Register")]
        public IActionResult Register()
        {
            return View("/Views/CRUD/Register.cshtml");
        }
        [HttpPost("/Home/Register")]
        public IActionResult Register(string registerUsername, string registerPassword)
        {
            try
            {
                var newUser = new User
                {
                    Username = registerUsername,
                    Password = registerPassword
                };

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                DisplaySuccessMessage("Registration successful!");

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Registration failed: {ex.Message}");

                return RedirectToAction("Register");
            }
        }

        public PostController(ILogger<PostController> logger, YourDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("/Home/Create")]
        [Authorize]
        public IActionResult InsertForm()
        {
            return View("/Views/CRUD/Create.cshtml");
        }

        [HttpPost("/Home/Create")]
        [Authorize]
        public IActionResult ProcessInsert([FromForm] InsertPostRequest body)
        {
            try
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

                DisplaySuccessMessage("Post created successfully!");
                return RedirectToAction("ReadPost", new { id = post.Id });
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Failed to create post: {ex.Message}");
                return RedirectToAction("InsertForm");
            }
        }

        [HttpGet("/Home/Read")]
        [Authorize]
        public IActionResult ReadPost()
        {
            var posts = _dbContext.Posts.ToList();
            return View("/Views/CRUD/AllPosts.cshtml", posts);
        }


        [HttpGet("/Home/Read/{id}")]
        [Authorize]
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
        [Authorize]
        public IActionResult UpdatePost()
        {
            return View("/Views/CRUD/Update.cshtml");
        }
        [HttpPost("/Home/Update/{id}")]
        [Authorize]
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
            catch(Exception ex)
            {
                DisplayErrorMessage($"Failed to update post: {ex.Message}");
                return RedirectToAction("UpdatePost", new { id });
            }
        }

        [HttpGet("/Home/Delete/{id}")]
        [Authorize]
        public IActionResult DeletePost()
        {
            return View("/Views/CRUD/Delete.cshtml");
        }
        [HttpPost("/Home/Delete/{id}")]
        [Authorize]
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
