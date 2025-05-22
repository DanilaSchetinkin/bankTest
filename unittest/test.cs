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

        [Test]
        public void TopUp_add()
        {
            testAccount.sum = 3200;
            Console.SetIn(new StringReader("500\n"));
            testAccount.top_up();
            Assert.AreEqual(3700, testAccount.sum);
        }

        [Test]
        public void TopUp_floatNumber()
        {
            testAccount.sum = 3200;
            Console.SetIn(new StringReader("1000.5\n"));
            testAccount.top_up();
            Assert.AreEqual(4200.5f, testAccount.sum);
        }

        [Test]
        public void umen_snyat_bolyshechemest()
        {
            testAccount.sum = 2000;
            Console.SetIn(new StringReader("2500\n"));

            testAccount.umen();
            Assert.AreEqual(0, testAccount.sum);
            Assert.That(consoleOutput.ToString(), Is.EqualTo("Недостаточно средств"));
        }
        [Test]
        public void umen_snyat_dvesti()
        {
            testAccount.sum = 1500;
            Console.SetIn(new StringReader("200\n"));

            testAccount.umen();
            Assert.AreEqual(1300, testAccount.sum);


        }

        [Test]
        public void obnul()
        {
            testAccount.sum = 2000;

            testAccount.obnul();
            Assert.AreEqual(0, testAccount.sum);
        }

        [Test]
        public void perevod_NormalTransfer()
        {
            testAccount.sum = 7000;
            var accounTwo = new account { sum = 2000, index = 1 };
            Console.SetIn(new StringReader("2000\n1\n"));

            testAccount.perevod();

            Assert.AreEqual(5000, testAccount.sum);
            Assert.AreEqual(4000, accounTwo.sum);
        }

        [Test]
        public void perevod_ZeroAmount_ShouldReject()
        {
            testAccount.sum = 2000;
            Console.SetIn(new StringReader("0\n1\n"));

            testAccount.perevod();

            Assert.AreEqual(2000, testAccount.sum);
            StringAssert.Contains("Сумма должна быть положительной", consoleOutput.ToString());
        }
        [Test]
        public void Withdraw_NegativeAmount_ShouldReject()
        {
            testAccount.sum = 2000;
            Console.SetIn(new StringReader("-100\n"));

            testAccount.umen();

            Assert.AreEqual(2000, testAccount.sum);
            StringAssert.Contains("Сумма не может быть отрицательной", consoleOutput.ToString());
        }

        [Test]
        public void Withdraw_ValidAmount_ShouldSubtractCorrectly()
        {
            testAccount.sum = 2000;
            Console.SetIn(new StringReader("500\n"));

            testAccount.umen();

            Assert.AreEqual(1500, testAccount.sum);
        }

        [Test]
        public void perevod_ToSameAccount_ShouldReject()
        {
            testAccount.sum = 5000;
            testAccount.index = 0;
            Console.SetIn(new StringReader("1000\n0\n"));
            testAccount.perevod();
            Assert.AreEqual(5000, testAccount.sum);
            StringAssert.Contains("Нельзя перевести на тот же счёт", consoleOutput.ToString());
        }
        [Test]
        public void otk_EmptyName_ShouldReject()
        {
            Console.SetIn(new StringReader("\n1000\nJohn Doe\n2000\n"));
            testAccount.otk();
            StringAssert.Contains("Пожалуйста, введите своё ФИО", consoleOutput.ToString());
        }
        [Test]
        public void otk_SumExactly1000_ShouldAccept()
        {
            Console.SetIn(new StringReader("John Doe\n1000\n"));
            testAccount.otk();
            Assert.AreEqual(1000, testAccount.sum);
        }
        [Test]
        public void num_gen_ShouldGenerateUniqueNumbers()
        {
            var account1 = new account();
            var account2 = new account();
            account1.num_gen();
            account2.num_gen();
            Assert.AreNotEqual(account1.num, account2.num);
        }
        [Test]
        public void num_gen_ShouldContainOnlyDigits()
        {
            testAccount.num_gen();
            Assert.IsTrue(testAccount.num.All(char.IsDigit));
        }
        [Test]
        public void top_up_ZeroAmount_ShouldReject()
        {
            testAccount.sum = 1000;
            Console.SetIn(new StringReader("0\n"));
            testAccount.top_up();
            Assert.AreEqual(1000, testAccount.sum);
            StringAssert.Contains("Сумма должна быть положительной", consoleOutput.ToString());
        }
        [Test]
        public void otk_WhitespaceInName_ShouldTrim()
        {
            Console.SetIn(new StringReader("  цфафцафц  цафцафа  \n2000\n"));
            testAccount.otk();
            Assert.AreEqual("John Doe", testAccount.name);
        }
    }
}
