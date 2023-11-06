using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using examplemvc.Models;
using examplemvc.Data;

[Route("Post/CRUD")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "admin,author")]
    public ActionResult<Post> CreatePost(Post post)
    {
        _context.Post.Add(post);
        _context.SaveChanges();
        string _message;
        int statusCode = HttpContext.Response.StatusCode;
        switch (statusCode)
        {
            case 200:
                _message = "Success (OK)";
                break;
            case 400:
                _message = "(Bad Request)";
                break;
            case 404:
                _message = "(Not Found)";
                break;
            case 201:
                _message = "Success (OK)";
                break;
            default:
                _message = "Pesan default untuk status code tidak dikenal";
                break;
        }
        var response = new {
            message = _message,
            status = statusCode,
        };
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public ActionResult<IEnumerable<Post>> GetPosts()
    {
        var posts = _context.Post.ToList();
        string _message;
        int statusCode = HttpContext.Response.StatusCode;
        switch (statusCode)
        {
            case 200:
                _message = "Permintaan berhasil (OK)";
                break;
            case 400:
                _message = "Permintaan tidak valid (Bad Request)";
                break;
            case 404:
                _message = "Data tidak ditemukan (Not Found)";
                break;
            case 201:
                _message = "Pembuatan berhasil";
                break;
            default:
                _message = "Pesan default untuk status code tidak dikenal";
                break;
        }
        var response = new {
            message = _message,
            status = statusCode,
            data = posts
        };
        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<Post> GetPost(int id)
    {
        var post = _context.Post.Find(id);
        if (post == null)
        {
            return NotFound();
        }
        string _message;
        int statusCode = HttpContext.Response.StatusCode;
        switch (statusCode)
        {
            case 200:
                _message = "Permintaan berhasil (OK)";
                break;
            case 400:
                _message = "Permintaan tidak valid (Bad Request)";
                break;
            case 404:
                _message = "Data tidak ditemukan (Not Found)";
                break;
            case 201:
                _message = "Pembuatan berhasil";
                break;
            default:
                _message = "Pesan default untuk status code tidak dikenal";
                break;
        }
        var response = new {
            message = _message,
            status = statusCode,
            data = post
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult UpdatePost(int id, Post post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }

        var existingPost = _context.Post.Find(id);
        if (existingPost == null)
        {
            return NotFound();
        }

        existingPost.Title = post.Title;
        existingPost.Body = post.Body;

        _context.SaveChanges();
        string _message;
        int statusCode = HttpContext.Response.StatusCode;
        switch (statusCode)
        {
            case 200:
                _message = "Permintaan berhasil (OK)";
                break;
            case 400:
                _message = "Permintaan tidak valid (Bad Request)";
                break;
            case 404:
                _message = "Data tidak ditemukan (Not Found)";
                break;
            case 201:
                _message = "Pembuatan berhasil";
                break;
            default:
                _message = "Pesan default untuk status code tidak dikenal";
                break;
        }
        var response = new {
            message = _message,
            status = statusCode,
        };
        NoContent();
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<Post> DeletePost(int id)
    {
        var post = _context.Post.Find(id);

        if (post == null)
        {
            return NotFound();
        }

        _context.Post.Remove(post);
        _context.SaveChanges();

        string _message;
        int statusCode = HttpContext.Response.StatusCode;
        switch (statusCode)
        {
            case 200:
                _message = "Permintaan berhasil (OK)";
                break;
            case 400:
                _message = "Permintaan tidak valid (Bad Request)";
                break;
            case 404:
                _message = "Data tidak ditemukan (Not Found)";
                break;
            case 201:
                _message = "Pembuatan berhasil";
                break;
            default:
                _message = "Pesan default untuk status code tidak dikenal";
                break;
        }
        var response = new {
            message = _message,
            status = statusCode,
        };
        return Ok(response);
    }
}
