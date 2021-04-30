using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using GuildCars.Models.Tables;

namespace GuildCars.Data.Repositories.Mock
{
    public class MakeRepositoryMock : IMakeRepository
    {
        private static List<Make> _makes = new List<Make>();

        private static Make Toyota = new Make
        {
            MakeId = "1",
            MakeName = "Toyota",
            DateAdded = new DateTime(2017, 7, 19),
            AddedBy = "admin3@test.com"
        };

        private static Make Acura = new Make
        {
            MakeId = "2",
            MakeName = "Acura",
            DateAdded = new DateTime(2017, 7, 2),
            AddedBy = "admin3@test.com"

        };

        private static Make Ford = new Make
        {
            MakeId = "3",
            MakeName = "Ford",
            DateAdded = new DateTime(2015, 6, 2),
            AddedBy = "admin3@test.com"

        };

        private static Make Dodge = new Make
        {
            MakeId = "4",
            MakeName = "Dodge",
            DateAdded = new DateTime(2009, 5, 1),
            AddedBy = "admin3@test.com"

        };

        private static Make Mock = new Make
        {
            MakeId = "5",
            MakeName = "Mock",
            DateAdded = new DateTime(2009, 5, 1),
            AddedBy = "admin3@test.com"

        };

        public MakeRepositoryMock()
        {
            if (_makes.Count() == 0)
            {
                _makes.Add(Toyota);
                _makes.Add(Acura);
                _makes.Add(Ford);
                _makes.Add(Dodge);
                _makes.Add(Mock);
            }
        }

        public void ClearMakesList()
        {
            _makes.Clear();
        }

        public IEnumerable<Make> GetAll()
        {
            return _makes;
        }

        public Make GetMakeById(string MakeId)
        {
            return _makes.FirstOrDefault(m => m.MakeId == MakeId);
        }

        public void Insert(Make make)
        {
            var id = _makes.Max(m => m.MakeId);

            if(int.TryParse(id, out int result)){
                make.MakeId = (result + 1).ToString();
            };

            _makes.Add(make);
        }
    }
}
