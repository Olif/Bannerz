using Api.Controllers;
using Domain;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace Api.Tests
{
    public class BannerControllerTests
    {
        BannerController target;
        IBannerRepository repo;
        IHtmlValidator validator;
        IIdGenerator idGenerator;

        public BannerControllerTests()
        {
            repo = Substitute.For<IBannerRepository>();
            validator = Substitute.For<IHtmlValidator>();
            idGenerator = Substitute.For<IIdGenerator>();
            target = new BannerController(repo, validator, idGenerator);
        }

        [Fact]
        public void Get_WhenBannerExists_ReturnsTheBanner()
        {
            repo.Find(Arg.Is<int>(1)).Returns(new Banner("", 1));

            var response = target.Get(1);

            Assert.IsType<OkNegotiatedContentResult<Banner>>(response);
        }

        [Fact]
        public void Get_WhenBannerDoesNotExist_Returns404()
        {
            var response = target.Get(1);

            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Post_WithValidHtml_SavesAndReturnsBanner()
        {
            var bannerModel = new BannerDTO { Html = "html" };
            idGenerator.Next().Returns(1);
            repo.Add(Arg.Any<Banner>()).Returns(new Banner("html", 1));
            validator.ValidateAsync(Arg.Any<string>()).Returns(Task.FromResult(HtmlValidationResult.ValidResult));

            var response = await target.Post(bannerModel);

            Assert.IsType<OkNegotiatedContentResult<Banner>>(response);
            var banner = (response as OkNegotiatedContentResult<Banner>).Content;
            Assert.Equal(1, banner.Id);
        }

        [Fact]
        public async Task Post_WithInvalidHtml_Returns_BadRequest()
        {
            var bannerModel = new BannerDTO() { Html = "html" };
            validator.ValidateAsync(Arg.Any<string>()).Returns(Task.FromResult(HtmlValidationResult.ErrorResult(new List<string>())));

            var response = await target.Post(bannerModel);

            Assert.IsType<BadRequestErrorMessageResult>(response);
        }

        [Fact]
        public async Task Put_WhenBannerDoesNotExist_ReturnsNotFound()
        {
            var bannerModel = new BannerDTO() { Html = "html" };

            var response = await target.Put(1, bannerModel);

            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Put_WhenHtmlIsInvalid_ReturnsBadRequest()
        {
            var bannerModel = new BannerDTO() { Html = "html" };
            validator.ValidateAsync(Arg.Any<string>()).Returns(Task.FromResult(HtmlValidationResult.ErrorResult(new List<string>())));
            repo.Find(Arg.Any<int>()).Returns(new Banner("html", 1));

            var response = await target.Put(1, bannerModel);

            Assert.IsType<BadRequestErrorMessageResult>(response);
        }

        [Fact]
        public async Task Put_WhenValidHtml_ReturnsUpdatedBanner()
        {
            var bannerModel = new BannerDTO() { Html = "html" };
            validator.ValidateAsync(Arg.Any<string>()).Returns(Task.FromResult(HtmlValidationResult.ValidResult));
            repo.Find(Arg.Any<int>()).Returns(new Banner("lmth", 1));

            var response = await target.Put(1, bannerModel);

            Assert.IsType<OkNegotiatedContentResult<Banner>>(response);
            var updatedBanner = (response as OkNegotiatedContentResult<Banner>).Content;
            Assert.Equal("html", updatedBanner.Html);
        }

        [Fact]
        public void Delete_WhenBannerDoesNotExist_ReturnsNotFound()
        {
            var response = target.Delete(1);

            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void Delete_WhenBannerExist_DeletesAndReturnsDeletedBanner()
        {
            repo.Find(1).Returns(new Banner("html", 1));

            var response = target.Delete(1);

            Assert.IsType<OkNegotiatedContentResult<Banner>>(response);
            repo.Received().Delete(Arg.Any<Banner>());
        }

        [Fact]
        public void RenderHtml_WhenBannerDoesNotExist_ReturnsNotFound()
        {
            var response = target.RenderBannerHtml(1);

            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void RenderHtml_WhenBannerExist_ReturnsHtmlContentType()
        {
            repo.Find(Arg.Any<int>()).Returns(new Banner("hello", 1));

            var response = target.RenderBannerHtml(1);

            Assert.IsType<HtmlResult>(response);

            var html = (response as HtmlResult).ExecuteAsync(new System.Threading.CancellationToken()).Result;
            var contentType = html.Content.Headers.ContentType;
            Assert.True(contentType.ToString().Contains("text/html"));

        }
    }
}
