using System;
using System.Collections.Generic;
using System.Linq;
using examplemvc.Models;

public static class Repository
{
    private static List<User> users = new List<User>();
    private static List<Tag> tags = new List<Tag>();
    private static List<PostTag> postTags = new List<PostTag>();
    private static List<Post> posts = new List<Post>();

    public static void SeedData()
    {
        users.Add(new User { Id = 1, Username = "user1", Password = "password1" });
        tags.Add(new Tag { Id = 1, Name = "Tag1" });
        postTags.Add(new PostTag { IdPost = 1, IdTags = 1 });
        posts.Add(new Post { Id = 1, UserId = 1, Title = "Post Title", Body = "Post Content", CreatedAt = DateTime.Now });
    }

    public static List<User> GetUsers() => users;
    public static List<Tag> GetTags() => tags;
    public static List<PostTag> GetPostTags() => postTags;
    public static List<Post> GetPosts() => posts;

    public static void AddUser(User user) => users.Add(user);
    public static void AddTag(Tag tag) => tags.Add(tag);
    public static void AddPostTag(PostTag postTag) => postTags.Add(postTag);
    public static void AddPost(Post post) => posts.Add(post);

    public static User GetUserById(int id) => users.FirstOrDefault(u => u.Id == id);
    public static void UpdateUser(User updatedUser)
    {
        var existingUser = users.FirstOrDefault(u => u.Id == updatedUser.Id);
        if (existingUser != null)
        {
            existingUser.Username = updatedUser.Username;
            existingUser.Password = updatedUser.Password;
        }
    }
    
    public static void DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user != null)
            users.Remove(user);
    }

    public static Tag GetTagById(int id) => tags.FirstOrDefault(t => t.Id == id);
    public static void UpdateTag(Tag updatedTag)
    {
        var existingTag = tags.FirstOrDefault(t => t.Id == updatedTag.Id);
        if (existingTag != null)
        {
            existingTag.Name = updatedTag.Name;
        }
    }

    public static void DeleteTag(Tag tag)
    {
        tags.Remove(tag);
    }

    public static PostTag GetPostTagById(int id) => postTags.FirstOrDefault(pt => pt.Id == id);
    public static bool PostTagExists(int id) => postTags.Any(pt => pt.Id == id);

    public static void UpdatePostTag(PostTag postTag)
    {
    var existingPostTag = postTags.FirstOrDefault(pt => pt.Id == postTag.Id);
    if (existingPostTag != null)
    {
        existingPostTag.IdPost = postTag.IdPost;
        existingPostTag.IdTags = postTag.IdTags;
    }
    }


    public static void DeletePostTag(PostTag postTag)
    {
        postTags.Remove(postTag);
    }

    public static Post GetPostById(int id) => posts.FirstOrDefault(p => p.Id == id);
    public static void UpdatePost(Post updatedPost)
    {
        var existingPost = posts.FirstOrDefault(p => p.Id == updatedPost.Id);
        if (existingPost != null)
        {
            existingPost.Title = updatedPost.Title;
            existingPost.Body = updatedPost.Body;
        }
    }

    public static void DeletePost(Post post)
    {
        posts.Remove(post);
    }
}
