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
    [Authorize(Policy = "AdminOnly, AuthorOnly")]
    public ActionResult<User> CreateUser(User user)
    {
        if (user == null)
            return BadRequest();

        _context.Users.Add(user);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<User> GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }
}

