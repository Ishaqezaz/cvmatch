using System.Threading.Tasks;
using api.Services;
using api.Dtos.User;
using api.Dtos.Auth;
using api.Common;


namespace api.Tests.Services
{
    public class AuthServiceTests : TestBase
    {
        private AuthService CreateService()
            => new AuthService(_context, _mapper, _config);

        [Fact]
        public async Task CreateUserPositive()
        {
            var service = CreateService();
            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var response = await service.CreateUserAsync(userDto);

            Assert.True(response.IsSuccess, "User should be created");
            Assert.NotNull(response.Data);
            Assert.Equal("Ronaldo", response.Data.FirstName);
            Assert.Equal("OverMessi", response.Data.LastName);
            Assert.Equal("halamadrid@gmail.com", response.Data.Email);
            Assert.Null(response.ErrorCode);
        }

        [Fact]
        public async Task CreateUserNegative()
        {
            var service = CreateService();
            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            await service.CreateUserAsync(userDto);
            var duplicateResponse = await service.CreateUserAsync(userDto);

            Assert.False(duplicateResponse.IsSuccess, "Creation should fail for duplicate email");
            Assert.Equal("User already exist", duplicateResponse.Message);
            Assert.Equal(ServiceErrorCode.Conflict, duplicateResponse.ErrorCode);
        }

        [Fact]
        public async Task LoginPositive()
        {
            var service = new AuthService(_context, _mapper, _config);

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            await service.CreateUserAsync(userDto);

            var loginDto = new LoginDto
            {
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            var response = await service.LoginUserAsync(loginDto);

            Assert.True(response.IsSuccess, "Login should succeed");
            Assert.NotNull(response.Data);
            Assert.Equal("Token created", response.Message);
            Assert.Null(response.ErrorCode);
        }

        [Fact]
        public async Task LoginNegative()
        {
            var service = new AuthService(_context, _mapper, _config);

            var userDto = new UserCreateDto
            {
                FirstName = "Ronaldo",
                LastName = "OverMessi",
                Email = "halamadrid@gmail.com",
                Password = "Ancelotti"
            };

            await service.CreateUserAsync(userDto);

            var loginDto = new LoginDto
            {
                Email = "halamadrid@gmail.com",
                Password = "klopp"
            };

            var response = await service.LoginUserAsync(loginDto);

            Assert.False(response.IsSuccess, "Login should fail");
            Assert.Null(response.Data);
            Assert.Equal("Wrong credentials", response.Message);
            Assert.Equal(ServiceErrorCode.Unauthorized, response.ErrorCode);
        }
    }
}
