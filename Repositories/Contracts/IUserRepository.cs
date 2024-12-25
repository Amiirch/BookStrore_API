using BookStoreApi.Models;

namespace BookStoreApi.Services.Contracts;

public interface IUserRepository
{
    
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(int id);
        Task CreateAsync(User user);
        Task<bool> ValidateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(User user);

}