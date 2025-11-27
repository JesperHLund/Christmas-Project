using BCrypt.Net;
using ChristmasBackend.Domain.Entities;
using ChristmasBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Application.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Validate login
    public async Task<User?> ValidateUserAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;

        bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        return isValid ? user : null;
    }

    // Create a new user
    public async Task<User> CreateUserAsync(string name, string email, string password)
    {
        var user = new User
        {
            Name = name,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<Animation>> GetUserAnimationsAsync(int userId)
    {
        return await _context.Animations.Where(a => a.UserId == userId).ToListAsync();
    }
    
    public async Task<UserVisualsDto> GetVisualSettingsForUser(int userId)
    {
        var defaultVisuals = new UserVisualsDto
        {
            BackgroundColor = "#4c29e9dd", // default blue
            Animations = new List<string>() // empty for default users
        };

        var visuals = await _context.UserVisuals
            .Where(v => v.UserId == userId)
            .ToListAsync();

        if (!visuals.Any()) return defaultVisuals;

        return new UserVisualsDto
        {
            BackgroundColor = visuals.First().BackgroundColor,
            Animations = visuals.Select(v => v.AnimationFileName).ToList()
        };
    }

    public class UserVisualsDto
    {
        public string BackgroundColor { get; set; } = "#4c29e9dd";
        public List<string> Animations { get; set; } = new();
    }

}
