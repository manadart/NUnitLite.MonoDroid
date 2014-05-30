using Java.Lang;
using NUnit.Framework;

namespace NUnitLite.MonoDroid.Example
{
    [TestFixture]
    public class TestFixture2
    {
        [SetUp]
        public void SetUp()
        {
             /* ... */
        }

        [TearDown]
        public void TearDown()
        {
             /* ... */
        }

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

        [Test]
        public void TestFail()
        {
            Assert.That(false, "This test failed.");
        }
    
        [Test]
        public void TestException()
        {
            throw new Exception("Test exception");
        }

        [Test]
        public void LongTest()
        {
            Thread.Sleep(10000);
            Assert.That(true);
        }
    }
}