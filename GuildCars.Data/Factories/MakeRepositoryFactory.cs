using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.ADO;
using GuildCars.Data.Repositories.Mock;
using System;

namespace GuildCars.Data.Factories
{
    public class MakeRepositoryFactory
    {
        public static IMakeRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new MakeRepositoryADO();
                case "QA":
                    return new MakeRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
