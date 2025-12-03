namespace ChristmasBackend.Domain.Entities;

public class Animation
{
    // Primary key
    public Guid Id { get; set; }
    // Animation file name
    public string FileName { get; set; } = string.Empty;
    // Path to the animation file
    public string FilePath { get; set; } = string.Empty;

    // Navigation property to User
    public Guid UserId { get; set; }
    public User? User { get; set; }
}

