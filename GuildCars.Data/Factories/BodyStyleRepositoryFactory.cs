using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.ADO;
using GuildCars.Data.Repositories.Mock;
using System;

namespace GuildCars.Data.Factories
{
    public class BodyStyleRepositoryFactory
    {
        public static IBodyStyleRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new BodyStyleRepositoryADO();
                case "QA":
                    return new BodyStyleRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
