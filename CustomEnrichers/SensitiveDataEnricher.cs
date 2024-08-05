using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;

namespace test_serilog.Custom
{
    /// <summary>
    /// This is our own custom enricher to mask sensitive data in log messages.
    /// </summary>
    public class SensitiveDataEnricher : ILogEventEnricher
    {
        private static readonly Regex EmailRegex = new Regex(
            @"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}",
            RegexOptions.IgnoreCase);

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            string messageText = logEvent.MessageTemplate.Text;
            string maskedMessageText = MaskSensitiveData(messageText);

            if (messageText != maskedMessageText)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("MessageTemplate", new ScalarValue(maskedMessageText)));
            }
        }

        private string MaskSensitiveData(string input)
        {
            return EmailRegex.Replace(input, match => MaskMatch(match.Value));
        }

        private string MaskMatch(string match)
        {
            if (EmailRegex.IsMatch(match))
            {
                return MaskEmail(match);
            }

            return match;
        }

        private string MaskEmail(string email)
        {
            var atIndex = email.IndexOf('@');
            if (atIndex <= 1)
            {
                return "***" + email.Substring(atIndex);
            }

            var maskedLocalPart = email.Substring(0, 1) + new string('*', atIndex - 2) + email.Substring(atIndex - 1, 1);
            return maskedLocalPart + email.Substring(atIndex);
        }
    }
}
