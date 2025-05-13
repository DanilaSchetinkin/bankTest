using System.Security.Principal;
using bank;
using NUnit.Framework;

namespace unittest
{
    [TestFixture]
    public class test
    {
        private account testAccount;
        private StringWriter consoleOutput;


        [SetUp]
        public void SetUp()
        {
            testAccount = new account();
            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
        }

        [TearDown]
        public void CleanUp()
            { 
            consoleOutput?.Dispose(); 
        }

        [Test]
        public void otk_Validate_createAccount()
        {
            Console.SetIn(new StringReader("Marcus Gray\n1000000\n"));

            testAccount.otk();

            Assert.AreEqual("Marcus Gray", testAccount);
        }
    }
}
