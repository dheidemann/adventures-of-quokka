using NUnit.Framework;


namespace Tests.Editor
{
    [TestFixture]
    public class SampleTest
    {
        [Test]
        public void AdditionTest()
        {
            int result = 2 + 2;
            Assert.AreEqual(4, result);
        }

        [Test]
        public void testWolfEnemy(){
            WolfEnemy wolfi = new WolfEnemy(); 
        }
    }
}
