using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using api.Data;
using api.Mappers;

namespace api.Tests.Services
{
    public abstract class TestBase : IDisposable
    {
        protected readonly ApplicationDBContext _context;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _config;

        public TestBase()
        {
            // 1) in memory db
            var dbOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDBContext(dbOptions);
            _context.Database.EnsureCreated();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
                cfg.AddProfile(new ProfileMapper());
                cfg.AddProfile(new ProfileSkillMapper());
                cfg.AddProfile(new ProfileLocationMapper());
            });
            _mapper = mapperConfig.CreateMapper();

            var inMemorySettings = new Dictionary<string, string?>
            {
                ["JwtSettings:Secret"] = "Alongstringthateventuallyendsat32",
                ["JwtSettings:Issuer"] = "TestIssuer",
                ["JwtSettings:Audience"] = "TestAudience",
                ["JwtSettings:ExpiresInHours"] = "24"
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
