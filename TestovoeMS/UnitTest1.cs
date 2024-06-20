using System.Windows;
using MusCassa.DB;

namespace TestovoeMS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void nulltest()
        {
            var regcheck = new MusCassa.RegPolz().registration("","","");
            string result = regcheck.Normalize();
            Assert.AreEqual(Assert.AreEqual(regcheck., "") false);

        }
    }

}