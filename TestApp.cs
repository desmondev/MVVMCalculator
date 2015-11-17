using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calc.ViewModel;


namespace Calc
{
    using NUnit.Framework;

    [TestFixture]
    public class TestApp
    {
        private MainWindowViewModel vm;

        [TestFixtureSetUp]
        public void Initialize()
        {
            vm = new MainWindowViewModel();
        }

        [Test]
        public void addTwoNumbers()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("2");
            vm.OperationCommandFunc("+");
            vm.ButtonCommandFunc("2");
            vm.OperationCommandFunc("=");
            Assert.AreEqual("4",vm.Display);
        }

        [Test]
        public void subtractTwoNumbers()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("2");
            vm.OperationCommandFunc("-");
            vm.ButtonCommandFunc("2");
            vm.OperationCommandFunc("=");
            Assert.AreEqual("0", vm.Display);
        }

        [Test]
        public void multiplyByZero()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("2");
            vm.OperationCommandFunc("*");
            vm.ButtonCommandFunc("0");
            vm.OperationCommandFunc("=");
            Assert.AreEqual("0", vm.Display);
        }

        [Test]
        public void multiplyByOne()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("5");
            vm.OperationCommandFunc("*");
            vm.ButtonCommandFunc("1");
            vm.OperationCommandFunc("=");
            Assert.AreEqual("5", vm.Display);
        }

        [Test]
        public void devideByZero()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("2");
            vm.OperationCommandFunc("/");
            vm.ButtonCommandFunc("0");
            vm.OperationCommandFunc("=");
            Assert.AreEqual("Err", vm.Display);
        }

        [Test]
        public void devideByOne()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("2");
            vm.OperationCommandFunc("/");
            vm.ButtonCommandFunc("1");
            vm.OperationCommandFunc("=");
            Assert.AreEqual("2", vm.Display);
        }

       [Test]
        public void addTwoDoubles()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("2");
            vm.ButtonCommandFunc(".");
            vm.ButtonCommandFunc("5");
            vm.OperationCommandFunc("+");
            vm.ButtonCommandFunc("2");
            vm.ButtonCommandFunc(".");
            vm.ButtonCommandFunc("5");
            vm.OperationCommandFunc("=");
            Assert.AreEqual("5", vm.Display);
        }
        
        [Test]
        public void squareRootOfPossitiveNumber()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("4");
            vm.SingularOperationCommandFunc("sqrt");
            Assert.AreEqual("2", vm.Display);
        }

        [Test]
        public void squareRootOfNegativeNumber()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("-4");
            vm.SingularOperationCommandFunc("sqrt");
            Assert.AreEqual("Err", vm.Display);
        }

        [Test]
        public void percentageOfNegative()
        {
            vm.ClearCommandFunction();
            vm.ButtonCommandFunc("4");
            vm.SingularOperationCommandFunc("-");
            vm.ButtonCommandFunc("-4");
            vm.PercentageOperationCommandFunc("%");

            Assert.AreEqual("Err", vm.Display);
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
        }
    }
}
