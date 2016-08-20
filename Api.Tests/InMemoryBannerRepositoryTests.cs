using Api.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests
{
    public class InMemoryBannerRepositoryTests
    {
        InMemoryBannerRepository target;
        IDictionary<int, Banner> ratesTable;

        public InMemoryBannerRepositoryTests()
        {
            ratesTable = new Dictionary<int, Banner>();
            target = new InMemoryBannerRepository(ratesTable);
        }

        [Fact]
        public void Add_WhenNewBanner_AddsItToInternalListAndIncrementsSequence()
        {
            var banner = new Banner("", 1);

            target.Add(banner);
            var sequence = target.NextSequence();

            Assert.Equal(1, ratesTable.Count());
            Assert.Equal(1, sequence);
        }

        [Fact]
        public void Find_WhenBannerExists_ReturnsTheBanner()
        {
            var mockBanner = new Banner("", 1);
            ratesTable.Add(1, mockBanner);

            var banner = target.Find(1);

            Assert.NotNull(banner);
        }

        [Fact]
        public void Find_WhenBannerDoesNotExist_ReturnsNull()
        {
            var banner = target.Find(2);

            Assert.Null(banner);
        }

        [Fact]
        public void Delete_WhenBannerExist_DeletesTheBannerAndReturnsIt()
        {
            var mockBanner = new Banner("", 1);
            ratesTable.Add(1, mockBanner);

            var banner = target.Delete(mockBanner);

            Assert.NotNull(banner);
            Assert.Equal(0, ratesTable.Count());
        }

        [Fact]
        public void NextSequence_WhenEmptiedRepo_ReturnsTheAccumulated_Nr()
        {
            var mockBanner1 = new Banner("", 1);
            var mockBanner2 = new Banner("", 2);
            target.Add(mockBanner1);
            target.Add(mockBanner2);
            ratesTable.Clear();

            var sequence = target.NextSequence();

            Assert.Equal(2, sequence);
        }
    }
}
