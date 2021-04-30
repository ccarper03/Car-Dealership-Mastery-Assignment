using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.ADO;
using GuildCars.Data.Repositories.Mock;
using System;

namespace GuildCars.Data.Factories
{
    public class ModelRepositoryFactory
    {
        public static IModelRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new ModelRepositoryADO();
                case "QA":
                    return new ModelRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
