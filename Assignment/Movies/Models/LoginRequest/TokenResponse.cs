namespace Movies.Models.LoginRequest
{
    public class TokenResponse
    {
        public class User
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public User user { get; set; }
        public string Token { get; set; }
    }
}
