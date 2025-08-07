namespace E_shift.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsAvailable { get; set; }
        public string AvailabilityDisplay => IsAvailable ? "Yes" : "No";
    }
}