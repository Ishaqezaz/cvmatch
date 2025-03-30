using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Data;
using api.Dtos.Auth;
using api.Dtos.User;
using api.interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;

// will fix custom exceptions later

namespace api.Services
{
    public class AuthService : IAuth
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _pwdHasher;
        private readonly IConfiguration _config;
        public AuthService(ApplicationDBContext context, IMapper mapper,
            IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _pwdHasher = new PasswordHasher<User>();
            _config = config;
        }

        public async Task<UserResponseDto> CreateUserAsync(UserCreateDto dto)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (exist != null)
                throw new Exception("User already exists");

            User user = _mapper.Map<User>(dto);
            user.HashPassword = _pwdHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<string> LoginUserAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user is null)
                throw new Exception("Wrong credentials");

            var match = _pwdHasher.VerifyHashedPassword(user, user.HashPassword, dto.Password);

            if (match == PasswordVerificationResult.Failed)
                throw new Exception("Wrong crendentials");

            var token = CreateJwtToken(user);
            return token;
        }

        public string CreateJwtToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secret = _config["JwtSettings:Secret"] ?? throw new ArgumentNullException();
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];
            var expireInHours = DateTime.UtcNow.AddHours(Convert.ToDouble(_config["JwtSettings:ExpiresInHours"]
                ?? throw new ArgumentNullException()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                issuer: issuer,
                audience: audience,
                expires: expireInHours,
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}