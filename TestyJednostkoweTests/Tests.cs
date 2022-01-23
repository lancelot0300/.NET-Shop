using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestyJednostkowe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appv1.DAL.Models;
using appv1.Controllers;
using Autofac.Extras.Moq;
using appv1.Interfaces;
using appv1.Services;
using appv1.DAL.Contexts;

namespace TestyJednostkowe.Tests
{
    [TestClass()]
    public class Tests
    {
        [TestMethod()]
        public void Test1()
        {

            using (var mock = AutoMock.GetLoose())
            {
                Login expected = new Login
                {
                    UserName = "admin",
                    Password = "1234"
                };
                mock.Mock<IObslugaBazyDanych>().Setup(x => x.User(expected.UserName, expected.Password)).Returns(GetLogin(expected.UserName, expected.Password));

                var cls = mock.Create<IObslugaBazyDanych>();

                var actual = cls.User(expected.UserName,expected.Password);


                Assert.AreEqual(expected.UserName, actual.UserName);
            }

        }
        [TestMethod()]
        public void Test2()
        {

            using (var mock = AutoMock.GetLoose())
            {
                Login expected = new Login
                {
                    UserName = "pawel",
                    Password = "1234"
                };
                mock.Mock<IObslugaBazyDanych>().Setup(x => x.User(expected.UserName, expected.Password)).Returns(GetLogin(expected.UserName, expected.Password));

                var cls = mock.Create<IObslugaBazyDanych>();

                var actual = cls.User(expected.UserName, expected.Password);


                Assert.AreEqual(expected.UserName, actual.UserName);
            }

        }
        [TestMethod()]
        public void Test3()
        {
            try
            {
                using (var mock = AutoMock.GetLoose())
                {
                    Login expected = new Login
                    {
                        UserName = "test",
                        Password = "1234"
                    };
                    mock.Mock<IObslugaBazyDanych>().Setup(x => x.User(expected.UserName, expected.Password)).Returns(GetLogin(expected.UserName, expected.Password));

                    var cls = mock.Create<IObslugaBazyDanych>();

                    var actual = cls.User(expected.UserName, expected.Password);



                    Assert.AreEqual(expected.UserName, actual.UserName);
                }
            }
           catch (Exception ex)
            {
                Console.WriteLine("Nie udało się");
            }

        }




        private Login GetLogin(string username, string password)
        {
            List<Login> list = new List<Login>
            {
                new Login
               {
                UserName = "admin",
                Password ="1234"
               },

                new Login
               {
                UserName = "pawel",
                Password ="1234"
               }

        };

            foreach (var user in list)
            {
                if (user.UserName == username && user.Password == password)
                {
                    return user;
                }
            }

            return null;
        }

    }
}
