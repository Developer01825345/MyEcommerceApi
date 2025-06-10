using AutoMapper;
using MyECommerceApi.Domain.Interfaces;
using MyECommerceApi.Domain.Models.Domain;
using MyECommerceApi.Domain.Models.DTO;
using MyECommerceApi.Infrastructure.Helpers;

namespace MyECommerceApi.Infrastructure.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<RegisterRequest> RegisterUser(RegisterRequest registerRequest)
    {
        if (string.IsNullOrWhiteSpace(registerRequest.Email))
        {
            throw new ArgumentException("Email is required", nameof(registerRequest.Email));
        }

        if (string.IsNullOrWhiteSpace(registerRequest.Password))
        {
            throw new ArgumentException("Password is required", nameof(registerRequest.Password));
        }

        var checkUserExists = _userRepository.GetUserDetails(registerRequest.Email);
        if (checkUserExists is not null)
        {
            throw new Exception("User already exists.");
        }

        registerRequest.Password = UserHelper.HashPassword(registerRequest.Password);

        var mapToUser = _mapper.Map<User>(registerRequest);
        
        var newUser = await _userRepository.Create(mapToUser);

        return _mapper.Map<RegisterRequest>(newUser);
    }


    public string VerifyUser(LoginRequest loginRequest)
    {
        var users = _userRepository.GetUserDetails(loginRequest.Email);

        if (users == null || !UserHelper.VerifyPassword(loginRequest.Password, users.Password))
            throw new Exception("User not found.");

        var token = UserHelper.GenerateToken(loginRequest.Email, "Admin");

        return token;
    }

}
