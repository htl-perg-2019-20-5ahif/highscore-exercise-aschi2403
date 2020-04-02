using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ts_space_shooter_backend.Model;
using ts_space_shooter_backend.Services;
using Xunit;

namespace ts_space_shooter_backend_test
{
    public class HighScoreEntryServiceTest
    {

        [Fact]
        public async Task EmptyAtBeginning()
        {
            var options = new DbContextOptionsBuilder<HighScoreContext>()
                .UseInMemoryDatabase(databaseName: "HighScores")
                .Options;

            var databaseContext = new HighScoreContext(options);
            databaseContext.Database.EnsureCreated();

            var service = new HighScoreEntryService(databaseContext);

            var result = await service.GetAllItemsAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task AddFirstHighScoreEntry()
        {
            var options = new DbContextOptionsBuilder<HighScoreContext>()
                .UseInMemoryDatabase(databaseName: "HighScores")
                .Options;

            var databaseContext = new HighScoreContext(options);
            databaseContext.Database.EnsureCreated();

            var service = new HighScoreEntryService(databaseContext);

            await service.AddHighScoreEntryAsync(new HighScoreEntry
            {
                Points = 100,
                PlayerInitials = "JD"
            });

            var items = await service.GetAllItemsAsync();

            Assert.Single(items);
        }

        [Fact]
        public async Task RemoveEleventhElement()
        {
            var options = new DbContextOptionsBuilder<HighScoreContext>()
                .UseInMemoryDatabase(databaseName: "HighScores")
                .Options;

            var databaseContext = new HighScoreContext(options);
            databaseContext.Database.EnsureCreated();

            var service = new HighScoreEntryService(databaseContext);

            for (int i = 0; i < 10; i++)
            {
                await service.AddHighScoreEntryAsync(new HighScoreEntry
                {
                    Points = i * 10,
                    PlayerInitials = "JD"
                });

            }

            var lastScore = new HighScoreEntry
            {
                Points = 99999,
                PlayerInitials = "JD"
            };

            await service.AddHighScoreEntryAsync(lastScore);

            Assert.Contains(lastScore, service.GetAllItemsAsync().Result);
        }

        //sorted descending
        [Fact]
        public async Task SortedDescending()
        {
            var options = new DbContextOptionsBuilder<HighScoreContext>()
                .UseInMemoryDatabase(databaseName: "HighScores")
                .Options;

            var databaseContext = new HighScoreContext(options);
            databaseContext.Database.EnsureCreated();

            var service = new HighScoreEntryService(databaseContext);

            await service.AddHighScoreEntryAsync(new HighScoreEntry
            {
                Points = 10,
                PlayerInitials = "JD"
            });

            await service.AddHighScoreEntryAsync(new HighScoreEntry
            {
                Points = 5,
                PlayerInitials = "JD"
            });

            var highScores = await service.GetAllItemsAsync();
            var sortedDescending = true;

            foreach (var highScore in highScores)
            {
                if (highScore.Points < highScores[highScores.IndexOf(highScore) + 1].Points)
                {
                    sortedDescending = false;
                }
            }

            Assert.True(sortedDescending);
        }
        
        //lowest Score gets removed when adding 11th element
        [Fact]
        public async Task RemoveEleventh()
        {
            var options = new DbContextOptionsBuilder<HighScoreContext>()
                .UseInMemoryDatabase(databaseName: "HighScores")
                .Options;

            var databaseContext = new HighScoreContext(options);
            databaseContext.Database.EnsureCreated();

            var service = new HighScoreEntryService(databaseContext);

            for (int i = 0; i < 12; i++)
            {
                await service.AddHighScoreEntryAsync(new HighScoreEntry
                {
                    Points = i * 10,
                    PlayerInitials = "JD"
                });

            }

            Assert.Equal(10, service.GetAllItemsAsync().Result.Count);
        }
    }
}
