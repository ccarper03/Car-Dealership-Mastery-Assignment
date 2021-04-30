using GuildCars.Data.Repositories.Mock;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.TransmissionRepositoryTests
{
    [TestFixture]
    public class TransmissionRepositoryTestsMock
    {
        [Test]
        public void CanGetAllTransmissions()
        {
            TransmissionRepositoryMock repo = new TransmissionRepositoryMock();

            List<Transmission> Transmissions = repo.GetAll().ToList();

            Assert.AreEqual(2, Transmissions.Count);

            Assert.AreEqual(Transmissions[1].TransmissionId, 2);
            Assert.AreEqual(Transmissions[1].TransmissionType, "Manual");
        }

        [Test]
        public void CanGetTransmissionById()
        {
            TransmissionRepositoryMock repo = new TransmissionRepositoryMock();

            Transmission Transmission = repo.GetAll().FirstOrDefault(c => c.TransmissionId == 2);

            Assert.AreEqual(Transmission.TransmissionId, 2);
            Assert.AreEqual(Transmission.TransmissionType, "Manual");
        }
    }
}
