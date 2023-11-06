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


[Route("Posttag/CRUD")]
[ApiController]
public class PostTagsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostTagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "admin,author")]
    public ActionResult<PostTag> CreatePostTag(PostTag postTag)
    {
        _context.PostTags.Add(postTag);
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
    public ActionResult<IEnumerable<PostTag>> GetPostTags()
    {
        var postTags = _context.PostTags.ToList();
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
            data = postTags
        };
        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<PostTag> GetPostTag(int id)
    {
        var postTag = _context.PostTags.Find(id);
        if (postTag == null)
        {
            return NotFound();
        }
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
            data = postTag
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult UpdatePostTag(int id, PostTag postTag)
    {
        if (id != postTag.PostId || id !=postTag.TagId)
        {
            return BadRequest();
        }

        var existingPostTag = _context.PostTags.Find(id);
        if (existingPostTag == null)
        {
            return NotFound();
        }

        existingPostTag.PostId = postTag.PostId;
        existingPostTag.TagId = postTag.TagId;

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
        NoContent();
        var response = new {
            message = _message,
            status = statusCode,
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<PostTag> DeletePostTag(int id)
    {
        var postTag = _context.PostTags.Find(id);

        if (postTag == null)
        {
            return NotFound();
        }

        _context.PostTags.Remove(postTag);
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
}

