using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using examplemvc.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;  
using Microsoft.IdentityModel.Tokens; 

[Route("User/CRUD")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public ActionResult<User> CreateUser(User user)
    {
    if (user == null)
        return BadRequest();

    Repository.AddUser(user);
    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = Repository.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<User> GetUserById(int id)
    {
        var user = Repository.GetUserById(id);
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

        var existingUser = Repository.GetUserById(id);
        if (existingUser == null)
            return NotFound();

        Repository.UpdateUser(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult DeleteUser(int id)
    {
        var user = Repository.GetUserById(id);
        if (user == null)
            return NotFound();

        Repository.DeleteUser(id);
        return NoContent();
    }


}

[Route("Tag/CRUD")]
[ApiController]
public class TagsController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public ActionResult<Tag> CreateTag(Tag tag)
    {
        Repository.AddTag(tag);
        return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Tag>> GetTags()
    {
        var tags = Repository.GetTags();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<Tag> GetTag(int id)
    {
        var tag = Repository.GetTagById(id);
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdateTag(int id, Tag tag)
    {
        if (id != tag.Id)
        {
            return BadRequest();
        }

        var existingTag = Repository.GetTagById(id);
        if (existingTag == null)
        {
            return NotFound();
        }

        Repository.UpdateTag(tag);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Tag> DeleteTag(int id)
    {
        var tag = Repository.GetTagById(id);

        if (tag == null)
        {
            return NotFound();
        }

        Repository.DeleteTag(tag);

        return tag;
    }
}

[Route("Posttag/CRUD")]
[ApiController]
public class PostTagsController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public ActionResult<PostTag> CreatePostTag(PostTag postTag)
    {
        Repository.AddPostTag(postTag);
        return CreatedAtAction("GetPostTag", new { id = postTag.Id }, postTag);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<PostTag>> GetPostTags()
    {
        var postTags = Repository.GetPostTags();
        return Ok(postTags);
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<PostTag> GetPostTag(int id)
    {
        var postTag = Repository.GetPostTagById(id);
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
        if (id != postTag.Id)
        {
            return BadRequest();
        }

        var existingPostTag = Repository.GetPostTagById(id);
        if (existingPostTag == null)
        {
            return NotFound();
        }

        Repository.UpdatePostTag(postTag);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<PostTag> DeletePostTag(int id)
    {
        var postTag = Repository.GetPostTagById(id);

        if (postTag == null)
        {
            return NotFound();
        }

        Repository.DeletePostTag(postTag);

        return postTag;
    }
}

[Route("Post/CRUD")]
[ApiController]
public class PostsController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public ActionResult<Post> CreatePost(Post post)
    {
        Repository.AddPost(post);
        return CreatedAtAction("GetPost", new { id = post.Id }, post);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Post>> GetPosts()
    {
        var posts = Repository.GetPosts();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<Post> GetPost(int id)
    {
        var post = Repository.GetPostById(id);
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

        var existingPost = Repository.GetPostById(id);
        if (existingPost == null)
        {
            return NotFound();
        }

        Repository.UpdatePost(post);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Post> DeletePost(int id)
    {
        var post = Repository.GetPostById(id);

        if (post == null)
        {
            return NotFound();
        }

        Repository.DeletePost(post);

        return post;
    }
}