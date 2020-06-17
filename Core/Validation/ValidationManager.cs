using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace SoloContacts.Core.Validation
{
    public class ValidationManager
    {
        /// <summary>
        /// Validates that the <paramref name="Value"/> is not null or empty.
        /// </summary>
        /// <param name="Value">string to check</param>
        /// <returns>true if the string is not null or empty</returns>
        public static bool StringRequired(string Value)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the maximum length of the string value. Returns true if the string's length's is less than the MaxLength
        /// </summary>
        /// <param name="Value">The string to validate</param>
        /// <param name="MaxLength">The maximum length of the string</param>
        /// <returns>true if the string's length's is less than the MaxLength</returns>
        public static bool StringMaxLength(string Value, int MaxLength)
        {
          if (Value.Length > MaxLength)
          {
            return false;
          }
          return true;
        }

        public static bool StringMinLength(string Value, int MinLength)
        {
            if (Value.Length < MinLength)
            {
                return false;
            }
            return true;
        }

        public static bool IntegerMaxValue(int Value, int Max)
        {
            if (Value > Max)
            {
                return false;
            }
            return true;
        }

        public static bool IntegerMinValue(int Value, int Min)
        {
            if (Value < Min)
            {
                return false;
            }
            return true;
        }

            public static bool RegExMatch(string Value, string RegexPattern)
        {
            // regular expression to match links (not exhaustive)
            Regex _Regex;
            _Regex = new Regex(RegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
            
            return _Regex.IsMatch(Value);
        }

        public static bool IsNumeric(string str)
        {
            try
            {
                str = str.Trim();
                Int64 _ProofValue = Int64.Parse(str);
                return (true);
            }
            catch (FormatException)
            {
                // Not a numeric value
                return (false);
            }
        }

        public static bool IsDecimal(string str)
        {
            try
            {
                str = str.Trim();
                decimal _ProofValue = 0.00m;
                Decimal.TryParse(str, out _ProofValue);
                return (true);
            }
            catch (FormatException)
            {
                // Not a numeric value
                return (false);
            }
        }

        public static bool IsEmail(string Email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(Email))
                return (true);
            else
                return (false);
        } 

    }
}
