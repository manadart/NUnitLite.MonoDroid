using NUnit.Framework;

namespace NUnitLite.MonoDroid.Example
{
    [TestFixture]
    public class TestFixture3
    {
        [Test]
        public void TestPass()
        {
            Assert.That(true);
        }

        [Test, Ignore]
        public void TestIgnore()
        {
            Assert.That(true);
        }
    }
}