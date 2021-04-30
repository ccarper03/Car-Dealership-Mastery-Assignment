using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.Mock;
using GuildCustomerContacts.Data.Repositories.ADO;
using System;

namespace GuildCars.Data.Factories
{
    public class CustomerContactRepositoryFactory
    {
        public static ICustomerContactRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new CustomerContactRepositoryADO();
                case "QA":
                    return new CustomerContactRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
