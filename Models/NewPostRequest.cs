using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examplemvc.Models;

public class Post
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}

public class PostTag
{
    [Key]
    public int PostId { get; set; }

    [Key]
    public int TagId { get; set; }

    [ForeignKey("PostId")]
    public Post? Post { get; set; }

    [ForeignKey("TagId")]
    public Tag? Tag { get; set; }
}

public class Tag
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}

public class Login
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}



