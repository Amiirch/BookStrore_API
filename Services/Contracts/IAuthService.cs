using BookStoreApi.Dtos.Auth;

namespace BookStoreApi.Services.contracts;

public interface IAuthService
{
    Task Register(SignUpRequest signUpRequest);

    Task<string> SignIn(SignInRequest signInRequest);
}