using System;

namespace examplemvc.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Body { get; set; }
		public DateTime CreatedAt { get; set; }
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
}
