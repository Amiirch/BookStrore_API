using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreApi.Dtos.Auth;
using BookStoreApi.Dtos.User;
using BookStoreApi.Exceptions;
using BookStoreApi.Helpers;
using BookStoreApi.Models;
using BookStoreApi.Services.contracts;
using BookStoreApi.Services.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApi.Services;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly AuthSetting _authSetting;

    public AuthService(IUserRepository userRepository,
        IOptions<AuthSetting> authSetting)
    {
        _userRepository = userRepository;
        _authSetting = authSetting.Value;
    }

    public async Task Register(SignUpRequest signUpRequest)
    {
        var user = new User();
        user.UserName = signUpRequest.UserName;
        user.Email = signUpRequest.Email;
        user.FullName = signUpRequest.FullName;
        user.phoneNumber = signUpRequest.PhoneNumber;
            var existUser = await _userRepository.ValidateAsync(user);
            if (!existUser)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(signUpRequest.Password);
                user.Roles = [$"{UserRoles.User}"];
                await _userRepository.CreateAsync(user);
            }
            else throw new DuplicateException("user is exist with this email or username,please try again with another userName or Email", 409);
    }
    
    public async Task<string> SignIn(SignInRequest signInRequest)
    {
        var user = await _userRepository.GetByUserNameAsync(signInRequest.UserName);
        if (user == null)
        {
            throw new UserNotFoundException("username is incorrect",404);
        }
        var verified = BCrypt.Net.BCrypt.Verify(signInRequest.Password, user.Password);
        
        if (verified)
        {
            var token = GenerateToken(user);
            return token;
        }
        throw new InvalidPasswordException("password is incorrect", 401);
    }

    public string GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authSetting.PrivateKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(120),
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.PrimarySid, user.Id.ToString()));

        foreach (var role in user.Roles) claims.AddClaim(new Claim(ClaimTypes.Role, role));

        return claims;
    }
}