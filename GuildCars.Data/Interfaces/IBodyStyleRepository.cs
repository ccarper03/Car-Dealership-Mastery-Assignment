using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface IBodyStyleRepository
    {
        IEnumerable<BodyStyle> GetAll();
        BodyStyle GetBodyStyleById(int BodyStyleId);
    }
}
