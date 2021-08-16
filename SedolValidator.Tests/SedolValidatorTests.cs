using NUnit.Framework;

namespace SedolValidator.Tests
{
    [TestFixture]
    public class SedolValidatorTests
    {
        [TestCase(null)]
        [TestCase("   ")]
        [TestCase("12")]
        [TestCase("123456789")]
        public void Sedol_Not_Seven_Characters(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.NOT_VALID_LENGTH);
            AssertValidationResult(expected, actual);
        }

        [TestCase("1234567")]
        public void Invalid_Checksum_Non_Userdefined_Sedol(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.CHECKSUM_NOT_VALID);
            AssertValidationResult(expected, actual);
        }
        
        [TestCase("0709954")]        
        [TestCase("B0YBKJ7")]        
        public void Valid_Non_Userdefined_Sedols(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, true, false, null);
            AssertValidationResult(expected, actual);
        }


        [TestCase("9123451")]
        [TestCase("9ABCDE8")]
        public void Invalid_Checksum_Userdefined_Sedol(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, true, Constants.CHECKSUM_NOT_VALID);
            AssertValidationResult(expected, actual);
        }

        [TestCase("9123_51")]
        [TestCase("VA.CDE8")]
        public void Sedols_With_Invalid_Characters(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.INVALID_CHARACTERS);
            AssertValidationResult(expected, actual);
        }

        [TestCase("9123458")]
        [TestCase("9ABCDE1")]
        public void Valid_Userdefined_Sedols(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, true, true, null);
            AssertValidationResult(expected, actual);
        }       
        

        private static void AssertValidationResult(ISedolValidationResult expected, ISedolValidationResult actual)
        {
            Assert.AreEqual(expected.InputString, actual.InputString, "Input String Failed");
            Assert.AreEqual(expected.IsValidSedol, actual.IsValidSedol, "Is Valid Failed");
            Assert.AreEqual(expected.IsUserDefined, actual.IsUserDefined, "Is User Defined Failed");
            Assert.AreEqual(expected.ValidationDetails, actual.ValidationDetails, "Validation Details Failed");
        }

    }
}
