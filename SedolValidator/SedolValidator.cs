namespace SedolValidator
{
    public class SedolValidator : ISedolValidator
    {
        public ISedolValidationResult ValidateSedol(string input)
        {
            var sedol = new Sedol(input);

            var result = new SedolValidationResult
            {
                InputString = input,
                IsUserDefined = false,
                IsValidSedol = false,
                ValidationDetails = null
            };

            if (!sedol.IsValidLength)
            {
                result.ValidationDetails = Constants.NOT_VALID_LENGTH;
                return result;
            }
            if (!sedol.IsAlphaNumeric)
            {
                result.ValidationDetails = Constants.INVALID_CHARACTERS;
                return result;
            }
            if (sedol.IsUserDefined)
            {
                result.IsUserDefined = true;
                if (sedol.HasValidChecksumDigit)
                {
                    result.IsValidSedol = true;
                    return result;
                }
                result.ValidationDetails = Constants.CHECKSUM_NOT_VALID;
                return result;
            }

            if (sedol.HasValidChecksumDigit)
                result.IsValidSedol = true;
            else
                result.ValidationDetails = Constants.CHECKSUM_NOT_VALID;

            return result;

        }
    }
}
