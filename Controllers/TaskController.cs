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
    [Authorize]
    public ActionResult<User> CreateUser(User user)
    {
        if (user == null)
            return BadRequest();

        _context.Users.Add(user);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<User> GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
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
    [Authorize]
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

[Route("Tag/CRUD")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Tags> CreateTag(Tags tag)
    {
        _context.Tags.Add(tag);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Tags>> GetTags()
    {
        var tags = _context.Tags.ToList();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<Tags> GetTag(int id)
    {
        var tag = _context.Tags.Find(id);
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdateTag(int id, Tags tag)
    {
        if (id != tag.Id)
        {
            return BadRequest();
        }

        var existingTag = _context.Tags.Find(id);
        if (existingTag == null)
        {
            return NotFound();
        }

        existingTag.Name = tag.Name;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Tags> DeleteTag(int id)
    {
        var tag = _context.Tags.Find(id);

        if (tag == null)
        {
            return NotFound();
        }

        _context.Tags.Remove(tag);
        _context.SaveChanges();

        return tag;
    }
}

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
    [Authorize]
    public ActionResult<PostTag> CreatePostTag(PostTag postTag)
    {
        _context.PostTags.Add(postTag);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetPostTag), new { id = postTag}, postTag);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<PostTag>> GetPostTags()
    {
        var postTags = _context.PostTags.ToList();
        return Ok(postTags);
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<PostTag> GetPostTag(int id)
    {
        var postTag = _context.PostTags.Find(id);
        if (postTag == null)
        {
            return NotFound();
        }
        return Ok(postTag);
    }

    [HttpPut("{id}")]
    [Authorize]
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<PostTag> DeletePostTag(int id)
    {
        var postTag = _context.PostTags.Find(id);

        if (postTag == null)
        {
            return NotFound();
        }

        _context.PostTags.Remove(postTag);
        _context.SaveChanges();

        return postTag;
    }
}

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
    [Authorize]
    public ActionResult<Post> CreatePost(Post post)
    {
        _context.Post.Add(post);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Post>> GetPosts()
    {
        var posts = _context.Post.ToList();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<Post> GetPost(int id)
    {
        var post = _context.Post.Find(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPut("{id}")]
    [Authorize]
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Post> DeletePost(int id)
    {
        var post = _context.Post.Find(id);

        if (post == null)
        {
            return NotFound();
        }

        _context.Post.Remove(post);
        _context.SaveChanges();

        return post;
    }
}
