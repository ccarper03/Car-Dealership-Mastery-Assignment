using GuildCars.Data.Repositories.ADO;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.TransmissionRepositoryTests
{
    [TestFixture]
    public class TransmissionRepositoryTestsADO
    {
        [Test]
        public void CanGetAllTransmissions()
        {
            TransmissionRepositoryADO repo = new TransmissionRepositoryADO();

            List<Transmission> Transmissions = repo.GetAll().ToList();

            Assert.AreEqual(2, Transmissions.Count);

            Assert.AreEqual(Transmissions[1].TransmissionId, 2);
            Assert.AreEqual(Transmissions[1].TransmissionType, "Manual");
        }

        [Test]
        public void CanGetTransmissionById()
        {
            TransmissionRepositoryADO repo = new TransmissionRepositoryADO();

            Transmission Transmission = repo.GetTransmissionById(2);

            Assert.AreEqual(Transmission.TransmissionId, 2);
            Assert.AreEqual(Transmission.TransmissionType, "Manual");
        }
    }
}
