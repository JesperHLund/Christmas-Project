namespace ChristmasBackend.Domain.Entities;

public class User
{
    // Properties
    // Unique identifier for the user
    public Guid Id { get; set; }
    // User's username
    public string Name { get; set; } = string.Empty;
    // User's email address
    public string Email { get; set; } = string.Empty;
    // Hashed password using bcrypt
    public string PasswordHash { get; set; } = string.Empty; // bcrypt hash

    // Navigation property
    public ICollection<Animation> Animations { get; set; } = new List<Animation>();
}
