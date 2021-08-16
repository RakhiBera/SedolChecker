using NUnit.Framework;

namespace SedolValidator.Tests
{
    [TestFixture]
    public class SedolTests
    {
        [Test]
        public void Character_Code_For_0_Is_0()
        {
            var actual = Sedol.Code('0');
            Assert.AreEqual(0, actual);
        }
        [Test]
        public void Character_Code_For_9_Is_9()
        {
            var actual = Sedol.Code('9');
            Assert.AreEqual(9, actual);
        }

        [Test]
        public void Character_Code_For_A_Is_10()
        {
            var actual = Sedol.Code('a');
            Assert.AreEqual(10, actual);
        }

        [Test]
        public void Character_Code_For_Z_Is_35()
        {
            var actual = Sedol.Code('z');
            Assert.AreEqual(35, actual);
        }        

        [TestCase(null)]
        [TestCase("   ")]
        [TestCase("102030405")]
        [TestCase("123")]
        public void Check_If_IsValidLength(string input)
        {
            Sedol sedol = new Sedol(input);
            Assert.IsFalse(sedol.IsValidLength);
        }

        [TestCase("aa!#")]
        public void Check_If_IsAlphaNumeric(string input)
        {
            Sedol sedol = new Sedol(input);
            Assert.IsFalse(sedol.IsAlphaNumeric);
        }

        [TestCase("9asde01")]
        public void Check_If_UserDefined(string input)
        {
            Sedol sedol = new Sedol(input);
            Assert.IsTrue(sedol.IsUserDefined);
        }

        [TestCase("0709954")]
        [TestCase("B0YBKJ7")]
        [TestCase("9ABCDE1")]
        public void Check_If_Checksum_IsValid(string input)
        {
            Sedol sedol = new Sedol(input);
            var actual = sedol.ChecksumDigit;
            Assert.AreEqual(input[6], actual);
        }

        [TestCase("B0YBKJ7")]
        public void Sedol_Has_Valid_ChecksumDigit(string input)
        {
            Sedol sedol = new Sedol(input);
            Assert.IsTrue(sedol.HasValidChecksumDigit);
        }
    }
}
