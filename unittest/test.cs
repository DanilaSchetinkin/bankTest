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

            Assert.AreEqual("Marcus Gray", testAccount.name);
            Assert.AreEqual(1000000, testAccount.sum);
            
        }

        [Test]
        public void otk_Validate_sum_zero()
        {
            Console.SetIn(new StringReader("Marcus Gay\n0\n"));
            testAccount.otk();
            Assert.IsNotNull(testAccount.num);
        }

        [Test]
        public void otk_Validate_NumError()
        {
            Console.SetIn(new StringReader("Marcus Gay\n400\n"));
            testAccount.otk();
            StringAssert.Contains("Сумма слишком мала, попробуйте ещё раз!", consoleOutput.ToString());
        }

        [Test]
        public void num_gen_GenerateLenght()
            {
            testAccount.num_gen();
            Assert.AreEqual(20, testAccount.num.Length);
            }
    }
}
