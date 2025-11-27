namespace ChristmasBackend.Domain.Entities;

public class Animation
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User? User { get; set; }
}
