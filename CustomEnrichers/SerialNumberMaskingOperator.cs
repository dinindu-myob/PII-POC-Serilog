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
    /// This is a custom masking operator for serial numbers that extends the RegexMaskingOperator class.
    /// </summary>
    public class SerialNumberMaskingOperator : RegexMaskingOperator
    {
        private const string serialNumberRegex = @"\d{12}";

        public SerialNumberMaskingOperator() :
            base(serialNumberRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled)
        {
        }

        protected override string PreprocessMask(string mask, Match match)
        {
            char startDigit = match.Value[0];
            char endDigit = match.Value[^1];
            return $"{startDigit}{new string(mask[0], 10)}{endDigit}";
        }
    }
}
