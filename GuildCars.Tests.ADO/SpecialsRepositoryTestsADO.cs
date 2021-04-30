using GuildCars.Data.Repositories.ADO;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace GuildCars.Tests.SpecialsRepositoryTests
{
    [TestFixture]
    public class SpecialsRepositoryTestsADO
    {
        [SetUp]
        public void Init()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                using (dbConnection)
                {
                    var cmd = new SqlCommand
                    {
                        CommandText = "GuildCarsDBReset",
                        CommandType = System.Data.CommandType.StoredProcedure,

                        Connection = dbConnection
                    };
                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(CultureInfo.CurrentCulture,
                          "Exception Type: {0}, Message: {1}{2}",
                          ex.GetType(),
                          ex.Message,
                          ex.InnerException == null ? String.Empty :
                          String.Format(CultureInfo.CurrentCulture,
                                       " InnerException Type: {0}, Message: {1}",
                                       ex.InnerException.GetType(),
                                       ex.InnerException.Message));

                System.Diagnostics.Debug.WriteLine(errorMessage);

                dbConnection.Close();
            }
        }

        [Test]
        public void CanGetAllSpecials()
        {
            SpecialsRepositoryADO repo = new SpecialsRepositoryADO();
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

            SpecialsRepositoryADO repo = new SpecialsRepositoryADO();

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

            SpecialsRepositoryADO repo = new SpecialsRepositoryADO();

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

