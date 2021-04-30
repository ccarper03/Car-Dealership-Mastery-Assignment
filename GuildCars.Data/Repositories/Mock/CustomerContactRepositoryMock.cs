using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Data.Repositories.Mock
{
    public class CustomerContactRepositoryMock : ICustomerContactRepository
    {
        private static List<CustomerContact> _contacts = new List<CustomerContact>();

        private static CustomerContact ContactOne = new CustomerContact
        {
            ContactId = 1,
            ContactName = "Test Contact 1",
            Email = "test1@test.com",
            Phone = "555-555-5555",
            MessageBody = "Test Contact Message 1"
        };

        private static CustomerContact ContactTwo = new CustomerContact
        {
            ContactId = 2,
            ContactName = "Test Contact 2",
            Email = "test2@test.com",
            Phone = "777-777-7777",
            MessageBody = "Test Contact Message 2"
        };

        private static CustomerContact ContactThree = new CustomerContact
        {
            ContactId = 3,
            ContactName = "Test Contact 3",
            Email = "test3@test.com",
            Phone = "111-111-1111",
            MessageBody = "Test Contact Message 3"
        };

        public CustomerContactRepositoryMock()
        {
            if (_contacts.Count() == 0)
            {
                _contacts.Add(ContactOne);
                _contacts.Add(ContactTwo);
                _contacts.Add(ContactThree);
            }
        }

        public IEnumerable<CustomerContact> GetAllContacts()
        {
            return _contacts;
        }

        public CustomerContact GetContactById(int ContactId)
        {
            return _contacts.FirstOrDefault(c => c.ContactId == ContactId);
        }

        public void Insert(CustomerContact CustomerContact)
        {
            CustomerContact.ContactId = _contacts.Max(c => c.ContactId) + 1;

            _contacts.Add(CustomerContact);
        }

        public void ClearContactsList()
        {
            _contacts.Clear();
        }
    }
}
