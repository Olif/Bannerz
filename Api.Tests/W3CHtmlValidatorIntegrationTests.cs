using System.Threading.Tasks;
using Xunit;

namespace Api.Tests
{
    public class W3CHtmlValidatorIntegrationTests
    {
        [Fact]
        public async Task Validate_WhenValidHtml_ReturnsValid()
        {
            W3CHtmlValidator validator = new W3CHtmlValidator();

            var validationResult = await validator.ValidateAsync(@"<!DOCTYPE html><html><head><title>test</title></head><body>Hello world, 123</body></html>");

            Assert.True(validationResult.IsValid);
            Assert.Null(validationResult.ErrorMessages);
        }

        [Fact]
        public async Task Validate_WhenInvalidHtml_ReturnsInvalidAndErrorList()
        {
            W3CHtmlValidator validator = new W3CHtmlValidator();
            var validationResult = await validator.ValidateAsync(@"<!DOCTYPE html><html><head></head><body><a>Hello world/a>, 123</body></html>");
            Assert.False(validationResult.IsValid);
        }
    }
}
