using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface IMakeRepository
    {
        IEnumerable<Make> GetAll();
        Make GetMakeById(string MakeId);
        void Insert(Make make);
    }
}
