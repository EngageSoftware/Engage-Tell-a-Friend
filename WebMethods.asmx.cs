// <copyright file="WebMethods.asmx.cs" company="Engage Software">
// Engage: Tell-A-Friend
// Copyright (c) 2004-2013
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.TellAFriend
{
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Script.Services;
    using System.Web.Services;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Mail;

    /// <summary>
    /// Web service method to handle client side calls
    /// </summary>
    [WebService(Namespace = "http://www.engagesoftware.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class WebMethods : WebService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="localResourceFile">The local resource file.</param>
        /// <param name="siteUrl">The site URL.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <param name="senderEmail">The sender email.</param>
        /// <param name="friendsEmail">The friends email.</param>
        /// <param name="senderName">Name of the sender.</param>
        /// <param name="friendName">Name of the friend.</param>
        /// <param name="message">The message.</param>
        /// <param name="portalEmail">The portal administrator's email.</param>
        /// <param name="currentCulture">The current culture.</param>
        /// <param name="moduleId">The ID of the module from which this email is being sent.</param>
        /// <param name="tabId">The ID of the tab from which this email is being sent.</param>
        /// <returns>The result of the SendEmail method.</returns>
        [WebMethod]
        public string SendEmail(string localResourceFile, string siteUrl, string portalName, string senderEmail, string friendsEmail, string senderName, string friendName, string message, string portalEmail, string currentCulture, int moduleId, int tabId)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);

            using (var settingsControl = new Settings())
            {
                settingsControl.ModuleConfiguration = new ModuleController().GetModule(moduleId, tabId);

                string from = Utility.GetStringSetting(settingsControl.Settings, "From", string.Empty);
                string carbonCopy = Utility.GetStringSetting(settingsControl.Settings, "CarbonCopy", string.Empty);
                string blindCarbonCopy = Utility.GetStringSetting(settingsControl.Settings, "BlindCarbonCopy", string.Empty);

                string body = Utility.GetStringSetting(settingsControl.Settings, "Body", Localization.GetString("EmailAFriend", localResourceFile));
                string subject = Utility.GetStringSetting(settingsControl.Settings, "Subject", Localization.GetString("EmailAFriendSubject", localResourceFile));

                if (string.IsNullOrEmpty(from))
                {
                    from = portalEmail;
                }

                body = ReplaceTokens(body, friendName, siteUrl, senderName, message, portalName, senderEmail, true);
                subject = ReplaceTokens(subject, friendName, siteUrl, senderName, message, portalName, senderEmail, false);

                return Mail.SendMail(from, friendsEmail, carbonCopy, blindCarbonCopy, MailPriority.Normal, subject, MailFormat.Html, Encoding.UTF8, body, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
        }

        /// <summary>
        /// Replaces the tokens in the given <paramref name="tokenizedText"/>.
        /// </summary>
        /// <param name="tokenizedText">Text with tokens to be replaced</param>
        /// <param name="friendName">Name of the friend, replaces <c>[Engage:Recipient]</c>.</param>
        /// <param name="siteUrl">The site URL, replaces <c>[Engage:Url]</c>.</param>
        /// <param name="senderName">Name of the sender, replaces <c>[Engage:From]</c>.</param>
        /// <param name="message">The message, replaces <c>[Engage:Message]</c>.</param>
        /// <param name="portalName">Name of the portal, replaces <c>[Engage:Portal]</c>.</param>
        /// <param name="senderEmail">The sender's email.</param>
        /// <param name="encode">Specifies if the replaced tokens should be encoded.</param>
        /// <returns>
        /// The string with the given tokens replaced
        /// </returns>
        private static string ReplaceTokens(string tokenizedText, string friendName, string siteUrl, string senderName, string message, string portalName, string senderEmail, bool encode)
        {
            var textBuilder = new StringBuilder(tokenizedText);
            textBuilder = textBuilder.Replace("[Engage:Recipient]", encode ? HttpUtility.HtmlEncode(friendName) : friendName);
            textBuilder = textBuilder.Replace("[Engage:Url]", encode ? HttpUtility.HtmlEncode(siteUrl) : siteUrl);
            textBuilder = textBuilder.Replace("[Engage:From]", encode ? HttpUtility.HtmlEncode(senderName) : senderName);
            textBuilder = textBuilder.Replace("[Engage:Message]", encode ? HttpUtility.HtmlEncode(message).Replace("\n", "<br />") : message);
            textBuilder = textBuilder.Replace("[Engage:Portal]", encode ? HttpUtility.HtmlEncode(portalName) : portalName);
            textBuilder = textBuilder.Replace("[Engage:SenderEmail]", encode ? HttpUtility.HtmlEncode(senderEmail) : senderEmail);
            return textBuilder.ToString();
        }
    }
}