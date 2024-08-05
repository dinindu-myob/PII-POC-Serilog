using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog.Enrichers.Sensitive;

namespace test_serilog.CustomEnrichers
{
    /// <summary>
    /// This is a custom masking operator for email addresses that extends the EmailAddressMaskingOperator class.
    /// </summary>
    public class CustomizedEmailAddressMaskingOperator : EmailAddressMaskingOperator
    {
        protected override string PreprocessMask(string mask, Match match)
        {
            var parts = match.Value.Split('@');
            char startChar = parts[0][0];
            char endChar = parts[0][^1];

            return $"{startChar}{new string(mask[0], 3)}{endChar}@{parts[1]}";
        }
    }
}
