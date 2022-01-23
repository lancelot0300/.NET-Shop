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
        private IObslugaBazyDanych ObslugaBazyDanych { get; set; }


        [TestMethod()]
        public void AccountAdminTest()
        {
            var login = new Login();

            login.UserName = "admin";
            login.Password = "1234";

            var test = new Login();

            test.UserName = "admin";
            test.Password = "1234";

            Assert.AreEqual(login.UserName, test.UserName);
            Assert.AreEqual(login.Password, test.Password);
        }
        [TestMethod()]
        public void AccountAdminTest2()
        {
            var login = new Login();

            login.UserName = "admin";
            login.Password = "1234";

            var test = new Login();

            test.UserName = "admin";
            test.Password = "1224";

            Assert.AreEqual(login.UserName, test.UserName);
            Assert.AreEqual(login.Password, test.Password);
        }

        [TestMethod()]
        public void AccountAdminTest3()
        {

            using (var mock = AutoMock.GetLoose())
            {
                Login expected = new Login
                {
                    UserName = "admin",
                    Password = "1234"
                };
                mock.Mock<IObslugaBazyDanych>().Setup(x => x.User(expected.UserName,expected.Password)).Returns(GetLogin);

              var cls =  mock.Create<IObslugaBazyDanych>();
                
                var actual = cls.User("admin", "1234");

               
               Assert.AreEqual(expected.UserName, actual.UserName);

               
            }



        }
        private Login GetLogin()
        {
           Login login = new Login
               {
                UserName = "admin",
                Password ="1234"
               };


            return login;
        }

    }
}
