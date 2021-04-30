using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.ADO;
using GuildCars.Data.Repositories.Mock;
using System;

namespace GuildCars.Data.Factories
{
    public class ReportsRepositoryFactory
    {
        public static IReportsRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new ReportsRepositoryADO();
                case "QA":
                    return new ReportsRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
