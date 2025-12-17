using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StoatApplication.Core.Utils;

public abstract class RegexUtilties
{
    /// <summary>
    ///     Validates whether the provided string is a valid email address format.
    /// </summary>
    /// <param name="email">The email address string to validate.</param>
    /// <returns>
    ///     <c>true</c> if the email address is valid; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     This method performs the following validations:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>Checks if the email is null or whitespace.</description>
    ///         </item>
    ///         <item>
    ///             <description>Normalizes internationalized domain names (IDN) to ASCII using punycode encoding.</description>
    ///         </item>
    ///         <item>
    ///             <description>Validates the email format using a regular expression pattern.</description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 Returns <c>false</c> if regex operations timeout (200ms for domain mapping, 250ms for pattern
    ///                 matching) or if argument exceptions occur.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        try
        {
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            string DomainMapper(Match match)
            {
                var idn = new IdnMapping();

                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}