// <copyright file="FeaturesController.cs" company="Engage Software">
// Engage: TellAFriend - http://www.EngageSoftware.com
// Copyright (c) 2004-2010
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.TellAFriend
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    using DotNetNuke.Common;
    using DotNetNuke.Entities.Users;

    using JetBrains.Annotations;

    /// <summary>
    /// Controls which DNN features are available for this module.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated through reflection by DNN")]
    {
        /// <summary>Stores the email regular expression for each portal</summary>
        private static readonly Dictionary<string, string> EmailsRegularExpressions = new Dictionary<string, string>(1);

        /// <summary>Gets a regular expression which will match a comma-delimited list of email addresses</summary>
        /// <param name="portalId">The portal ID.</param>
        /// <returns>A regular expression.</returns>
        public static string GetEmailListRegularExpression(int portalId)
        {
            var emailRegularExpression = GetEmailRegularExpression(portalId);

            string preCalulatedRegularExpression;
            if (EmailsRegularExpressions.TryGetValue(emailRegularExpression, out preCalulatedRegularExpression))
            {
                return preCalulatedRegularExpression;
            }

            return EmailsRegularExpressions[emailRegularExpression] = CreateListRegularExpression(emailRegularExpression);
        }

        /// <summary>Gets a regular expression which will match an email address</summary>
        /// <param name="portalId">The portal ID.</param>
        /// <returns>A regular expression.</returns>
        public static string GetEmailRegularExpression(int portalId)
        {
            return (UserController.GetUserSettings(portalId)["Security_EmailValidation"] as string) ?? Globals.glbEmailRegEx;
        }

        /// <summary>Creates a regular expression that represents a comma-delimited list of the given regular expression pattern.</summary>
        /// <param name="expression">The regular expression.</param>
        /// <returns>A new regular expression.</returns>
        private static string CreateListRegularExpression(string expression)
        {
            var emailRegularExpressionBuilder = new StringBuilder(expression);
            var prefixBuilder = new StringBuilder();
            if (emailRegularExpressionBuilder[0] == '^')
            {
                prefixBuilder.Append('^');
                emailRegularExpressionBuilder.Remove(0, 1);
            }

            if (emailRegularExpressionBuilder.ToString(0, 2).Equals(@"\b", StringComparison.Ordinal))
            {
                prefixBuilder.Append(@"\b");
                emailRegularExpressionBuilder.Remove(0, 2);
            }

            var suffixBuilder = new StringBuilder();
            if (emailRegularExpressionBuilder[emailRegularExpressionBuilder.Length - 1] == '$')
            {
                suffixBuilder.Append('$');
                emailRegularExpressionBuilder.Remove(emailRegularExpressionBuilder.Length - 1, 1);
            }

            if (emailRegularExpressionBuilder.ToString(emailRegularExpressionBuilder.Length - 2, 2)
                                             .Equals(@"\b", StringComparison.Ordinal))
            {
                suffixBuilder.Append(@"\b");
                emailRegularExpressionBuilder.Remove(emailRegularExpressionBuilder.Length - 2, 2);
            }

            return string.Format(@"{0}{1}(?:,\s*{1})*{2}", prefixBuilder, emailRegularExpressionBuilder, suffixBuilder);
        }
    }
}
