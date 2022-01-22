using Xunit;
using appv1;
using appv1.DAL.Models;

namespace TestyJednostkowe
{
    public class AccountTest
    {
        [Fact]
        public void AccountAdmin()
        {
            var login = new Login();

            login.UserName = "admin";
            login.Password = "1234";

            Assert.Equal("admin", login.UserName);
            Assert.Equal("1234", login.Password);
        }
    }
}