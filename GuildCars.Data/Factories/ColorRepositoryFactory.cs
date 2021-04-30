using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.ADO;
using GuildCars.Data.Repositories.Mock;
using System;

namespace GuildCars.Data.Factories
{
    public class ColorRepositoryFactory
    {
        public static IColorRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new ColorRepositoryADO();
                case "QA":
                    return new ColorRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
