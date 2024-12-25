using BookStoreApi.Data;
using BookStoreApi.Models;
using BookStoreApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _dataContext;

    public UserRepository(ApplicationDbContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(User user)
    {
        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _dataContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
    }
   
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }
    public async Task<bool> ValidateAsync(User user)
    {
        bool combinationExists = await _dataContext.Users
            .AnyAsync(x => x.UserName == user.UserName
                           && x.Email == user.Email);

        if (!combinationExists) return false;
        
        return true;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _dataContext.Users.Update(user);
        await _dataContext.SaveChangesAsync();
        return user;
    }
    public async Task DeleteAsync(User user)
    {
        _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync();
    }
}