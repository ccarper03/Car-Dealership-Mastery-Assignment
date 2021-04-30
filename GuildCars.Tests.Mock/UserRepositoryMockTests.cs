using GuildCars.Data.Repositories.Mock;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.UserRepositoryTests
{
    [TestFixture]
    public class UserRepositoryMockTests
    {
        UserRepositoryMock repo;

        [SetUp]
        public void Init()
        {
            repo = new UserRepositoryMock();
        }

        [TearDown]
        public void ResetRepo()
        {
            repo.ClearUsers();
        }

        [Test]
        public void CanGetALLUsers()
        {
            List<User> users = repo.GetUsers().ToList();

            Assert.AreEqual(4, users.Count);

            Assert.AreEqual("11111111-1111-1111-1111-111111111111", users[1].Id);
            Assert.AreEqual("sales2@test.com", users[1].Email);
            Assert.AreEqual("Sales Test User 2", users[1].UserName);
            Assert.AreEqual(0, users[1].AccessFailedCount);
            Assert.IsFalse(users[1].EmailConfirmed);
            Assert.IsFalse(users[1].PhoneNumberConfirmed);
            Assert.IsFalse(users[1].TwoFactorEnabled);
            Assert.IsFalse(users[1].LockoutEnabled);
        }

        [Test]
        public void CanGetUserById()
        {
            User user = repo.GetUserById("11111111-1111-1111-1111-111111111111");

            Assert.AreEqual("11111111-1111-1111-1111-111111111111", user.Id);
            Assert.AreEqual("sales2@test.com", user.Email);
            Assert.AreEqual("Sales Test User 2", user.UserName);
            Assert.AreEqual(0, user.AccessFailedCount);
            Assert.IsFalse(user.EmailConfirmed);
            Assert.IsFalse(user.PhoneNumberConfirmed);
            Assert.IsFalse(user.TwoFactorEnabled);
            Assert.IsFalse(user.LockoutEnabled);
        }

        [Test]
        public void CanAddUser()
        {
            User user = new User
            {
                Email = "AddedTestUser@test.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = new DateTime(2018, 1, 1),
                AccessFailedCount = 0,
                UserName = "Added Test User",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
            };

            repo.Insert(user);

            List<User> users = repo.GetUsers().ToList();

            Assert.AreEqual(5, users.Count);

            Assert.AreEqual("Added-Test-User", users[4].Id);
            Assert.AreEqual("AddedTestUser@test.com", users[4].Email);
            Assert.AreEqual("Added Test User", users[4].UserName);
            Assert.AreEqual(0, users[4].AccessFailedCount);
            Assert.IsFalse(users[4].EmailConfirmed);
            Assert.IsFalse(users[4].PhoneNumberConfirmed);
            Assert.IsFalse(users[4].TwoFactorEnabled);
            Assert.IsFalse(users[4].LockoutEnabled);
        }

        [Test]
        public void CanEditUser()
        {
            User user = new User
            {
                Email = "AddedTestUser@test.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = new DateTime(2018, 1, 1),
                AccessFailedCount = 0,
                UserName = "Added Test User",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
            };

            repo.Insert(user);

            user.Email = "updatedTestUser@test.com";
            user.EmailConfirmed = true;
            user.LockoutEndDateUtc = new DateTime(2018, 1, 2);
            user.AccessFailedCount = 1;
            user.UserName = "Updated Test User";
            user.PhoneNumberConfirmed = true;
            user.TwoFactorEnabled = true;

            repo.Update(user);

            List<User> users = repo.GetUsers().ToList();

            Assert.AreEqual(5, users.Count);

            Assert.AreEqual("Added-Test-User", users[4].Id);
            Assert.AreEqual("updatedTestUser@test.com", users[4].Email);
            Assert.AreEqual("Updated Test User", users[4].UserName);
            Assert.AreEqual(1, users[4].AccessFailedCount);
            Assert.IsTrue(users[4].EmailConfirmed);
            Assert.IsTrue(users[4].PhoneNumberConfirmed);
            Assert.IsTrue(users[4].TwoFactorEnabled);
            Assert.IsFalse(users[4].LockoutEnabled);
        }
    }
}

