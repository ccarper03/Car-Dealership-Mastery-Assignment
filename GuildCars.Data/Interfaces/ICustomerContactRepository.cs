using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface ICustomerContactRepository
    {
        IEnumerable<CustomerContact> GetAllContacts();
        CustomerContact GetContactById(int ContactId);
        void Insert(CustomerContact CustomerContact);
    }
}
