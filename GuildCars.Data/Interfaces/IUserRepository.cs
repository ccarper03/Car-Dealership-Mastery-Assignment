using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersByRole(string Role);
        User GetUserById(string UserId);
        User GetUserByUserName(string UserName);
        void Insert(User user);
        void Update(User user);
    }
}
