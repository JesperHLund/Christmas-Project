using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristmasBackend.Domain.Entities;
using ChristmasBackend.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Infrastructure.Repositories;

public class AnimationRepository : IAnimationRepository
{
    private readonly DBConnection _context;

    public AnimationRepository(DBConnection context)
    {
        _context = context;
    }

    public async Task<Animation?> GetByIdAsync(Guid id)
    {
        return await _context.Animations
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Animation>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Animations
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(Animation animation)
    {
        await _context.Animations.AddAsync(animation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Animation animation)
    {
        _context.Animations.Update(animation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Animation animation)
    {
        _context.Animations.Remove(animation);
        await _context.SaveChangesAsync();
    }
}
