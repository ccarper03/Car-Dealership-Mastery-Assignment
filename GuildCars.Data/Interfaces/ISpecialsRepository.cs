using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface ISpecialsRepository
    {
        IEnumerable<Special> GetAll();
        void Insert(Special special);
        void Delete(int SpecialId);
    }
}
