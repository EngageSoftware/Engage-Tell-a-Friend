// <copyright file="WebMethods.asmx.cs" company="Engage Software">
// Engage: TellAFriend - http://www.engagesoftware.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.TellAFriend
{
    using System.Text;
    using System.Web.Script.Services;
    using System.Web.Services;
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
        /// <returns>The result of the SendEmail method.</returns>
        [WebMethod]
        public string SendEmail(string localResourceFile, string siteUrl, string portalName, string senderEmail, string friendsEmail, string senderName, string friendName, string message)
        {
            string body = ReplaceTokens(Localization.GetString("EmailAFriend", localResourceFile), friendName, siteUrl, senderName, message, portalName);
            string subject = ReplaceTokens(Localization.GetString("EmailAFriendSubject", localResourceFile), friendName, siteUrl, senderName, message, portalName);

            return Mail.SendMail(senderEmail, friendsEmail, string.Empty, subject, body, string.Empty, "HTML", string.Empty, string.Empty, string.Empty, string.Empty);
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
        /// <returns>The string with the given tokens replaced</returns>
        private static string ReplaceTokens(string tokenizedText, string friendName, string siteUrl, string senderName, string message, string portalName)
        {
            var textBuilder = new StringBuilder(tokenizedText);
            textBuilder = textBuilder.Replace("[Engage:Recipient]", friendName);
            textBuilder = textBuilder.Replace("[Engage:Url]", siteUrl);
            textBuilder = textBuilder.Replace("[Engage:From]", senderName);
            textBuilder = textBuilder.Replace("[Engage:Message]", message);
            textBuilder = textBuilder.Replace("[Engage:Portal]", portalName);
            return textBuilder.ToString();
        }
    }
}