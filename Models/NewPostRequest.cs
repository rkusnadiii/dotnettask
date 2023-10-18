using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace examplemvc.Models;

    public class Post
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Pengguna")]
        [Column("user_id")]
        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [InverseProperty(nameof(PostTag.Post))]
        public ICollection<PostTag>? PostTags { get; set; }

        public User? Pengguna { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PostTag
    {
        public int Id { get; set; }
        [ForeignKey("Post")]
        [Column("id_post", Order = 1)]
        public int IdPost { get; set; }

        [ForeignKey("Tags")]
        [Column("id_tag", Order = 2)]
        public int IdTags { get; set; }

        public Post Post { get; set; } = null!;

        public Tag Tag { get; set; } = null!;
    }
        public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

