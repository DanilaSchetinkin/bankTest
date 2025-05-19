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
            Assert.That(consoleOutput.ToString(), Is.EqualTo("Сумма слишком мала, попробуйте ещё раз!"));
        }

        [Test]
        public void otk_Valiadte_NumName()
        {
            Console.SetIn(new StringReader("dadwa123"));
            testAccount.otk();
            Assert.That(consoleOutput.ToString(), Is.EqualTo("Неверное имя пользователя"));
        }

        [Test]
        public void num_gen_GenerateLenght()
            {
            testAccount.num_gen();
            Assert.AreEqual(20, testAccount.num.Length);
            }

        public void TopUp_add()
        {
            testAccount.sum = 3200;
            Console.SetIn(new StringReader("500\n"));
            testAccount.top_up();
            Assert.AreEqual(3700,testAccount.sum);
        }

        public void TopUp_floatNumber()
        {
            testAccount.sum = 3200;
            Console.SetIn(new StringReader("1000.5\n"));
            testAccount.top_up();
            Assert.AreEqual(4200.5f, testAccount.sum);
        }

        public void umen()
        {
            testAccount.sum = 1500;
            Console.SetIn(new StringReader("200\n"));

            testAccount.umen();
            Assert.AreEqual(1300, testAccount.sum);
              
            
        }
    }
}
