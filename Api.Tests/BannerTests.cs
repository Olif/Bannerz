using Domain;
using NSubstitute;
using System.Threading;
using Xunit;

namespace Api.Tests
{
    public class BannerTests
    {
        IIdGenerator idGenerator;

        public BannerTests()
        {
            idGenerator = Substitute.For<IIdGenerator>();
            idGenerator.Next().Returns(1);
        }

        [Fact]
        public void Create_ReturnsNewBanner()
        {

            var banner = Banner.Create("html", idGenerator);

            Assert.NotNull(banner);
            Assert.Equal("html", banner.Html);
        }

        [Fact]
        public void UpdateHtml_UpdatesHtmlAndModified()
        {
            var banner = Banner.Create("html", idGenerator);
            Thread.Sleep(100);

            banner.UpdateHtml("updatedHtml");

            Assert.Equal("updatedHtml", banner.Html);
            Assert.NotEqual(banner.Created, banner.Modified);
        }

        private Banner GetBanner(string html)
        {
            var banner = Banner.Create("html", idGenerator);
            return banner;
        }
    }
}
