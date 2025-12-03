using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChristmasBackend.Domain.Entities;

namespace ChristmasBackend.Domain.Interfaces
{
    public interface IUserRepository
    {
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    }
}