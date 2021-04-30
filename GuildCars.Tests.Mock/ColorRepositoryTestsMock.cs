using GuildCars.Data.Repositories.Mock;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.ColorRepositoryTests
{
    [TestFixture]
    public class ColorRepositoryTestsMock
    {
        [Test]
        public void CanGetAllColors()
        {
            ColorRepositoryMock repo = new ColorRepositoryMock();

            List<Color> Colors = repo.GetAll().ToList();

            Assert.AreEqual(5, Colors.Count);

            Assert.AreEqual(Colors[2].ColorId, 3);
            Assert.AreEqual(Colors[2].ColorName, "Gray");
        }

        [Test]
        public void CanGetColorById()
        {
            ColorRepositoryMock repo = new ColorRepositoryMock();

            Color Color = repo.GetAll().FirstOrDefault(c => c.ColorId == 3);

            Assert.AreEqual(Color.ColorId, 3);
            Assert.AreEqual(Color.ColorName, "Gray");
        }
    }
}
