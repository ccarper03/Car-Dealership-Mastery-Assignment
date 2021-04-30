using GuildCars.Data.Repositories.Mock;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.ModelRepositoryTests
{
    [TestFixture]
    public class ModelRepositoryMockTests
    {

        ModelRepositoryMock repo;

        [SetUp]
        public void Init()
        {
            repo = new ModelRepositoryMock();
        }

        [TearDown]
        public void ResetRepo()
        {
            repo.ClearModelsList();
        }

        [Test]
        public void CanGetAllModels()
        {
            ModelRepositoryMock repo = new ModelRepositoryMock();

            List<Model> Models = repo.GetAll().ToList();

            Assert.AreEqual(5, Models.Count);

            Assert.AreEqual(Models[2].ModelId, 3);
            Assert.AreEqual(Models[2].ModelName, "TLX");
            Assert.AreEqual(Models[2].DateAdded, new DateTime(2017, 7, 2));
        }

        [Test]
        public void CanGetModelById()
        {
            ModelRepositoryMock repo = new ModelRepositoryMock();

            Model Model = repo.GetAll().FirstOrDefault(c => c.ModelId == 3);

            Assert.AreEqual(Model.ModelId, 3);
            Assert.AreEqual(Model.ModelName, "TLX");
            Assert.AreEqual(Model.DateAdded, new DateTime(2017, 7, 2));
        }

        [Test]
        public void CanAddModel()
        {
            Model model = new Model
            {
                MakeId = 2,
                ModelName = "TestModel",
                DateAdded = DateTime.Now.Date,
                Addedby = "admin3@test.com"
            };

            ModelRepositoryMock repo = new ModelRepositoryMock();
            repo.Insert(model);

            List<Model> Models = repo.GetAll().ToList();
            Assert.AreEqual(6, Models.Count);

            Assert.AreEqual(6, Models[5].ModelId);
            Assert.AreEqual(2, Models[5].MakeId);
            Assert.AreEqual(model.ModelName, Models[5].ModelName);
            Assert.AreEqual(model.DateAdded, Models[5].DateAdded);
            Assert.AreEqual(model.Addedby, Models[5].Addedby);
        }
    }
}
