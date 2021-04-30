using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Data.Repositories.Mock
{
    public class ModelRepositoryMock : IModelRepository
    {
        private static List<Model> _models = new List<Model>();

        private static Model TundraLX = new Model
        {
            ModelId = 1,
            MakeId = 1,
            ModelName = "Tundra LX",
            DateAdded = new DateTime(2017, 7, 19),
            Addedby = "admin3@test.com"
        };

        private static Model Escape = new Model
        {
            MakeId = 4,
            ModelId = 2,
            ModelName = "Escape",
            DateAdded = new DateTime(2015, 6, 2),
            Addedby = "admin3@test.com"
        };

        private static Model TLX = new Model
        {
            MakeId = 2,
            ModelId = 3,
            ModelName = "TLX",
            DateAdded = new DateTime(2017, 7, 2),
            Addedby = "admin3@test.com"
        };

        private static Model GrandCaravan = new Model
        {
            MakeId = 4,
            ModelId = 4,
            ModelName = "Grand Caravan",
            DateAdded = new DateTime(2009, 5, 1),
            Addedby = "admin3@test.com"
        };

        private static Model Vehicle = new Model
        {
            MakeId = 5,
            ModelId = 5,
            ModelName = "Vehicle",
            DateAdded = new DateTime(2009, 5, 1),
            Addedby = "admin3@test.com"
        };

        public ModelRepositoryMock()
        {
            if (_models.Count() == 0)
            {
                _models.Add(TundraLX);
                _models.Add(Escape);
                _models.Add(TLX);
                _models.Add(GrandCaravan);
                _models.Add(Vehicle);
            }
        }

        public void ClearModelsList()
        {
            _models.Clear();
        }

        public IEnumerable<Model> GetAll()
        {
            return _models;
        }

        public Model GetModelById(int ModelId)
        {
            return _models.FirstOrDefault(m => m.ModelId == ModelId);
        }

        public List<Model> GetModelsByMakeId(int MakeId)
        {
            return _models.FindAll(m => m.MakeId == MakeId);
        }

        public void Insert(Model Model)
        {
            Model.ModelId = _models.Max(m => m.ModelId) + 1;

            _models.Add(Model);
        }
    }
}
