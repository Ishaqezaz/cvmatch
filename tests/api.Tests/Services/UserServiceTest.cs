using System;
using api.Services;
using api.Dtos.User;
using api.Common;
using api.Models;
using Microsoft.AspNetCore.Identity;


namespace api.Tests.Services
{
    public class UserServiceTest : TestBase
    {
        private UserService createUserService()
            => new UserService(_context, _mapper);

        private PasswordHasher<User> _pwd;

        [Fact]
        public async Task UpdateUserPositive()
        {
            var service = createUserService();

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // patch update
            var updateDto = new UserUpdateDto
            {
                FirstName = "MrChampionsLeague"
            };

            var response = await service.UpdateUserAsync(1, updateDto);

            Assert.True(response.IsSuccess, "Should be able to update");
            Assert.Null(response.ErrorCode);
            Assert.NotNull(response.Data);
            Assert.Equal("MrChampionsLeague", response.Data.FirstName);
        }

        [Fact]
        public async Task UpdateUserNegative()
        {
            var service = createUserService();

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // patch update
            var updateDto = new UserUpdateDto
            {
                FirstName = "MrChampionsLeague"
            };

            //wrong id -> not found
            var response = await service.UpdateUserAsync(2, updateDto);

            Assert.False(response.IsSuccess, "Unathourized update should fail");
            Assert.Equal(ServiceErrorCode.NotFound, response.ErrorCode);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task GetUserPositive()
        {
            var service = createUserService();

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = await service.GetUserAsync(1);

            Assert.True(response.IsSuccess, "Should fetch user");
            Assert.Null(response.ErrorCode);
            Assert.NotNull(response.Data);
            Assert.Equal("Ronaldo", response.Data.FirstName);
        }

        [Fact]
        public async Task GetUserNegative()
        {
            var service = createUserService();

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = await service.GetUserAsync(2);

            Assert.False(response.IsSuccess, "Should not found user");
            Assert.Equal(ServiceErrorCode.NotFound, response.ErrorCode);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task DeleteUserPositive()
        {
            var service = createUserService();
            this._pwd = new PasswordHasher<User>();

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = await service.DeleteUserAsync(1);

            Assert.True(response.IsSuccess, "Should delete user");
            Assert.Null(response.ErrorCode);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task DeleteUserNegative()
        {
            var service = createUserService();

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = await service.DeleteUserAsync(2);

            Assert.False(response.IsSuccess, "Should not delete user");
            Assert.Equal(ServiceErrorCode.NotFound, response.ErrorCode);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task UpdateUserPasswordPositive()
        {
            var service = createUserService();
            _pwd = new PasswordHasher<User>();

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var user = _mapper.Map<User>(userDto);
            user.HashPassword = _pwd.HashPassword(user, userDto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var changePassword = new UserPwdUpdateDto
            {
                Password = "Ancelotti",
                NewPassword = "Ronaldo"
            };

            var response = await service.UpdateUserPasswordAsync(1, changePassword);

            Assert.True(response.IsSuccess, "Should be able to change password");
            Assert.Null(response.ErrorCode);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task UpdateUserPasswordNegative()
        {
            var service = createUserService();
            _pwd = new PasswordHasher<User>();

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var changePassword = new UserPwdUpdateDto
            {
                Password = "Ancelotti",
                NewPassword = "Ronaldo"
            };

            var response = await service.UpdateUserPasswordAsync(1, changePassword);

            Assert.False(response.IsSuccess, "Should not be able to update, invalid hash");
            Assert.Equal(ServiceErrorCode.Unauthorized, response.ErrorCode);
            Assert.Null(response.Data);
        }
    }
}