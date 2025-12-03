using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChristmasBackend.Domain.Entities;
using ChristmasBackend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves a user by its unique identifier, including related animations.
        /// </summary>
        /// <param name="id">The user's unique identifier.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The user if found; otherwise null.</returns>
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                                 .Include(u => u.Animations)
                                 .FirstOrDefaultAsync(u => u.Id == id, cancellationToken)
                                 .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a user by email, including related animations.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The user if found; otherwise null.</returns>
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;

            return await _context.Users
                                 .Include(u => u.Animations)
                                 .FirstOrDefaultAsync(u => u.Email == email, cancellationToken)
                                 .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a user by name, including related animations.
        /// </summary>
        /// <param name="name">The user's name.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The user if found; otherwise null.</returns>
        public async Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            return await _context.Users
                                 .Include(u => u.Animations)
                                 .FirstOrDefaultAsync(u => u.Name == name, cancellationToken)
                                 .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a list of all users, including related animations.
        /// </summary>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>An enumerable of users.</returns>
        public async Task<IEnumerable<User>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users
                                 .Include(u => u.Animations)
                                 .ToListAsync(cancellationToken)
                                 .ConfigureAwait(false);
        }

        /// <summary>
        /// Adds a new user to the database and saves changes.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            await _context.Users.AddAsync(user, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates an existing user in the database and saves changes.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the specified user from the database and saves changes.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a user by its unique identifier if it exists.
        /// </summary>
        /// <param name="id">The user's unique identifier.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            if (user != null)
            {
                await DeleteAsync(user, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Checks whether a user with the specified identifier exists.
        /// </summary>
        /// <param name="id">The user's unique identifier.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>True if the user exists; otherwise false.</returns>
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(u => u.Id == id, cancellationToken)
                                       .ConfigureAwait(false);
        }

        /// <summary>
        /// Checks whether a user with the specified email exists.
        /// </summary>
        /// <param name="email">The email to check for existence.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>True if a user with the email exists; otherwise false.</returns>
        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken)
                                       .ConfigureAwait(false);
        }
    }
}
