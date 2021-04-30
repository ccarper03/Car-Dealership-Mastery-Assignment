using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface IModelRepository
    {
        IEnumerable<Model> GetAll();
        Model GetModelById(int ModelId);
        List<Model> GetModelsByMakeId(int MakeId);
        void Insert(Model model);
    }
}
