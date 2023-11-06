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


[Route("User/CRUD")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "admin,author")]
    public ActionResult<User> CreateUser(User user)
    {
        if (user == null)
            return BadRequest();

        _context.Users.Add(user);
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
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _context.Users.ToList();
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
            data = users
        };
        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<User> GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();
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
            data = user
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult UpdateUser(int id, User user)
    {
        if (id != user.Id)
            return BadRequest();

        var existingUser = _context.Users.Find(id);
        if (existingUser == null)
            return NotFound();

        existingUser.Username = user.Username;
        existingUser.Password = user.Password;

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
    public ActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
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

