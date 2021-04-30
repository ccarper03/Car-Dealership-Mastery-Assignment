using GuildCars.Data.Repositories.ADO;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.ColorRepositoryTests
{
    [TestFixture]
    public class ColorRepositoryTestsADO
    {
        [Test]
        public void CanGetAllColors()
        {
            ColorRepositoryADO repo = new ColorRepositoryADO();

            List<Color> Colors = repo.GetAll().ToList();

            Assert.AreEqual(5, Colors.Count);

            Assert.AreEqual(Colors[2].ColorId, 3);
            Assert.AreEqual(Colors[2].ColorName, "Gray");
        }

        [Test]
        public void CanGetColorById()
        {
            ColorRepositoryADO repo = new ColorRepositoryADO();

            Color Color = repo.GetAll().FirstOrDefault(c => c.ColorId == 3);

            Assert.AreEqual(Color.ColorId, 3);
            Assert.AreEqual(Color.ColorName, "Gray");
        }
    }
}
