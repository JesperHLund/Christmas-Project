namespace ChristmasBackend.Domain.Repositories;

using ChristmasBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAnimationRepository
{
    // Returns an animation by its ID
    Task<Animation?> GetByIdAsync(Guid id);
    
    // Returns animations for a user
    Task<IEnumerable<Animation>> GetByUserIdAsync(Guid userId);

    // Create, Update, Delete operations for Animation entity
    Task AddAsync(Animation animation);
    Task UpdateAsync(Animation animation);
    Task DeleteAsync(Animation animation);
}