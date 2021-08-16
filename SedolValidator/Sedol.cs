using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SedolValidator
{
    public class Sedol
    {
        private readonly string _value;
        private const int SEDOL_LENGTH = 7;
        private const char USER_DEFINED_CHAR = '9';
        private readonly List<int> _weightFactorList = new List<int> { 1, 3, 1, 7, 3, 9 };
        private const int CHECKSUM_DIGIT_INDEX = 6;

        public Sedol(string input)
        {
            _value = input;
        }

        /// <summary>
        /// Letters have the value of 9 plus their alphabet position, such that B = 11 and Z = 35.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Code(char input)
        {
            if (Char.IsLetter(input))
            {
                return Char.ToUpper(input) - 55;
            }
            return input - 48;
        }

        public bool IsValidLength
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_value) && _value.Length == SEDOL_LENGTH)
                    return true;
                else
                    return false;
            }
        }

        public bool IsAlphaNumeric
        {
            get
            {
                return Regex.IsMatch(_value, "^[a-zA-Z0-9]*$");
            }
        }

        /// <summary>
        /// The first character of a user defined SEDOL is a 9
        /// </summary>
        public bool IsUserDefined
        {
            get
            {
                return _value[0] == USER_DEFINED_CHAR;
            }
        }

        /// <summary>
        /// Calculate Checksum Digit
        /// (10 − (weighted sum modulo 10)) modulo 10.
        /// </summary>
        /// <returns></returns>
        public char ChecksumDigit
        {
            get
            {
                var codeList = _value.Take(SEDOL_LENGTH - 1).Select(Code).ToList();
                var weightedSum = codeList.Zip(_weightFactorList, (code, weight) => code * weight).Sum();

                var checkSumDigit = Convert.ToChar(((10 - (weightedSum % 10)) % 10).ToString());
                return checkSumDigit;
            }
        }

        public bool HasValidChecksumDigit
        {
            get
            {
                return _value[CHECKSUM_DIGIT_INDEX] == ChecksumDigit;
            }
        }

    }
}
