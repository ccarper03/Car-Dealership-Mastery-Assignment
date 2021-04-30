using GuildCars.Data.Repositories.Mock;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.SpecialsRepositoryTests
{
    [TestFixture]
    public class SpecialsRepositoryTestsMock
    {
        SpecialsRepositoryMock repo;

        [SetUp]
        public void Init()
        {
            repo = new SpecialsRepositoryMock();
        }

        [TearDown]
        public void ResetRepo()
        {
            repo.SpecialsClearList();
        }

        [Test]
        public void CanGetAllSpecials()
        {
            SpecialsRepositoryMock repo = new SpecialsRepositoryMock();
            List<Special> specials = repo.GetAll().ToList();

            Assert.AreEqual(4, specials.Count);

            Assert.AreEqual(2, specials[1].SpecialId);
            Assert.AreEqual("Free tank of gas with every purchase!", specials[1].SpecialDetails);
            Assert.AreEqual("Free tank of gas!", specials[1].Title);
        }

        [Test]
        public void CanAddSpecial()
        {
            Special special = new Special
            {
                SpecialDetails = "Test Special",
                Title = "Test title"
            };

            SpecialsRepositoryMock repo = new SpecialsRepositoryMock();

            repo.Insert(special);

            List<Special> specials = repo.GetAll().ToList();

            Assert.AreEqual(5, specials.Count);

            Assert.AreEqual(5, specials[4].SpecialId);
            Assert.AreEqual("Test Special", specials[4].SpecialDetails);
            Assert.AreEqual("Test title", specials[4].Title);
        }

        [Test]
        public void CanDeleteSpecial()
        {
            Special special = new Special
            {
                SpecialDetails = "Test Special",
                Title = "Test title"
            };

            SpecialsRepositoryMock repo = new SpecialsRepositoryMock();

            repo.Insert(special);

            List<Special> specials = repo.GetAll().ToList();

            Assert.AreEqual(5, specials.Count);

            Assert.AreEqual(5, specials[4].SpecialId);
            Assert.AreEqual("Test Special", specials[4].SpecialDetails);
            Assert.AreEqual("Test title", specials[4].Title);

            repo.Delete(5);

            List<Special> updatedSpecials = repo.GetAll().ToList();

            Special deletedSpecial = updatedSpecials.FirstOrDefault(s => s.SpecialId == 5);

            Assert.AreEqual(4, updatedSpecials.Count);

            Assert.IsNull(deletedSpecial);

        }
    }
}
