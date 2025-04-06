using System;
using Microsoft.Extensions.DependencyInjection;
using api.Interfaces;
using api.Services.Models;
using api.Tests.api.Tests.Integration;


namespace api.Tests.Integration
{
    public class CvScanIntegrationTests : IClassFixture<TestBase>
    {
        private readonly ICVScannerService _scanner;
        private readonly Stream _stream;

        public CvScanIntegrationTests(TestBase testBase)
        {
            _scanner = testBase.Services.GetRequiredService<ICVScannerService>();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "CV.pdf");
            _stream = new MemoryStream(File.ReadAllBytes(filePath));
        }

        [Fact]
        public async Task ScanParseResponsePositive()
        {
            bool useRealApi = Environment.GetEnvironmentVariable("USE_REAL_API") == "true";

            CVScanResult result;
            if (useRealApi)
            {
                result = await _scanner.ScanSkillsAndLocationAsync(_stream);
                Console.WriteLine("REAL API WAS TESTED");
            }
            else
            {
                string fakeResponse = @"{
                ""choices"": [
                    {
                    ""message"": {
                    ""content"": ""{ \""Skills\"": [\""Rightfoot\"", \""Leftfoot\"", \""Headers\"", \""Penalties\""], \""Location\"": \""Stockholm\"" }""
                        }
                    }
                ]
            }";

                result = _scanner.ParseOpenAiResponse(fakeResponse);
                Console.WriteLine("FAKE API WAS TESTED");
            }

            //skills
            Assert.NotEmpty(result.Skills);
            Assert.Contains("Rightfoot", result.Skills);
            Assert.Contains("Leftfoot", result.Skills);
            Assert.Contains("Headers", result.Skills);
            Assert.DoesNotContain("C++", result.Skills);

            Assert.Contains("Stockholm", result.Location); // extracted city

            // counties within 100 km radius
            var nearby = _scanner.CalculateNearbyCounties(result.Location, 100);
            Assert.NotEmpty(nearby);
            Assert.Contains("Uppsala", nearby);
            Assert.DoesNotContain("Västernorrlands", nearby);
            Assert.DoesNotContain("Skåne", nearby);
        }

        [Fact]
        public void PdfReaderPositive()
        {
            var reader = new PdfReaderRepository();
            var text = reader.ReadText(_stream);

            Assert.NotNull(text);
            Assert.Contains("www.cristianoronaldo.com", text);
        }
    }
}