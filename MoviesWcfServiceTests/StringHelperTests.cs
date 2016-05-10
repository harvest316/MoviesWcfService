using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesWcfService;

namespace MoviesWcfServiceTests
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void ContainsAnyCaseTest()
        {
            const string testString = "Test String";
            Assert.IsTrue(testString.ContainsAnyCase("TEsT"));
        }
    }
}