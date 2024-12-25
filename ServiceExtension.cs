using BookStoreApi.Repositories;
using BookStoreApi.Repositories.Contracts;
using BookStoreApi.Services;
using BookStoreApi.Services.contracts;
using BookStoreApi.Services.Contracts;


namespace BookStoreApi
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookService, BookService>();
        }

        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IBookRepository,BookRepository>();
            
        }
    }
}