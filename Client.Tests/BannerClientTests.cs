using Domain;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Client.Tests
{
    public class BannerClientTests
    {
        [Fact]
        public async Task CRUD_Scenario()
        {
            var bannerzClient = new BannerzApiClient("http://localhost:62833");
            var banner1 = new BannerDTO()
            {
                Html = @"<!DOCTYPE html>
                            <html>
                                <head>
                                    <title>Hello</title>
                                </head>
                                <body>
                                    <p>Hello Crille Lampa/p>
                                </body>
                            </html>"
            };

            var banner2 = new BannerDTO()
            {
                Html = @"<!DOCTYPE html>
                            <html>
                                <head>
                                    <title>Hello</title>
                                </head>
                                <body>
                                    <p>Hello Magnus Villson</p>
                                </body>
                            </html>"
            };

            // Create
            var createdBanner = await bannerzClient.CreateAsync(banner1);
            Assert.NotNull(createdBanner);

            // Get
            var gettedBanner = await bannerzClient.GetAsync(createdBanner.Id);
            Assert.Equal(createdBanner.Id, gettedBanner.Id);

            // Update
            var updatedBanner = await bannerzClient.UpdateAsync(gettedBanner.Id, banner2);
            Assert.Equal(banner2.Html, updatedBanner.Html);

            // Delete
            var deletedBanner = await bannerzClient.DeleteAsync(updatedBanner.Id);
            Assert.NotNull(deletedBanner);

            // Try find deleted banner
            Func<Task> action = () => bannerzClient.GetAsync(deletedBanner.Id);
            await Assert.ThrowsAsync<BannerzClientException>(action);
        }
    }
}
