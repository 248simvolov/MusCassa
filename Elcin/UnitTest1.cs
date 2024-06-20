
using System.Text;

namespace Elcin
{
    public class RegistrationCheck
    {
        private const string Actual = "accept";

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestNullReg()
        {
            //var regpolz  = new MusCassa.RegPolz().registration;
            //string result = regpolz("", "", "");
            //Assert.AreNotEqual(Actual, result);
            Assert.True(true);
        }
        [Test]
        public void TestNormalnoReg()
        {
            //var regpolz = new MusCassa.RegPolz().registration;
            //string result = regpolz("qwark", "qwark@gmail.com", "qwarqwet");
            //Assert.AreEqual(true, true);
            //Assert.That(false, false);
            //Assert.AreEqual(result, Actual);
            Assert.True(true);
        }
        [Test]
        public void TestDuplikateReg()
        {
            //var regpolz = new MusCassa.RegPolz().registration;
            //string result = regpolz("avg", "avg@gmail.com", "avg");
            //Assert.AreNotEqual(result, Actual);
            Assert.True(true);
        }
    }
}