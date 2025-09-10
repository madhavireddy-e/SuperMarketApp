namespace SuperMarketApp.Models
{
     public static class CurrentUser
    {
        // Change this to User to test user view
        public static UserRole Role { get; set; } = UserRole.Admin;
    }
}