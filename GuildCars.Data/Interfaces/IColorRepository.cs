using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface IColorRepository
    {
        IEnumerable<Color> GetAll();
        Color GetColorById(int ColorId);
    }
}
