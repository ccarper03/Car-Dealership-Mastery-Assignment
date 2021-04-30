using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface ITransmissionRepository
    {
        IEnumerable<Transmission> GetAll();
        Transmission GetTransmissionById(int TransmissionId);
    }
}
