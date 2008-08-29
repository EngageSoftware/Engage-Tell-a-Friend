// <copyright file="WebMethods.asmx.cs" company="Engage Software">
// Engage: TellAFriend - http://www.engagemodules.com
// Copyright (c) 2004-2008
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
    using System.Web.Services;
    using DotNetNuke.Common;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Mail;

    /// <summary>
    /// Web service method to handle client side calls
    /// </summary>
    [WebService(Namespace = "http://www.engagesoftware.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService()]
    public class WebMethods : WebService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="localResourceFile">The local resource file.</param>
        /// <param name="siteUrl">The site URL.</param>
        /// <param name="tabId">The tab id.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <param name="senderEmail">The sender email.</param>
        /// <param name="friendsEmail">The friends email.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="message">The message.</param>
        /// <returns>The result of the SendEmail method.</returns>
        [WebMethod]
        public string SendEmail(string localResourceFile, string siteUrl, int tabId, string portalName, string senderEmail, string friendsEmail, string firstName, string lastName, string message)
        {
            string localizedMessage = Localization.GetString("EmailAFriend", localResourceFile);
            localizedMessage = localizedMessage.Replace("[Engage:Recipient]", friendsEmail);

            if (!String.IsNullOrEmpty(siteUrl) && siteUrl != "null")
            {
                localizedMessage = localizedMessage.Replace("[Engage:Url]", siteUrl);
            }
            else
            {
                localizedMessage = localizedMessage.Replace("[Engage:Url]", Globals.NavigateURL(tabId));
            }

            localizedMessage = localizedMessage.Replace("[Engage:From]", firstName + " " + lastName);
            localizedMessage = localizedMessage.Replace("[Engage:Message]", message);

            string subject = Localization.GetString("EmailAFriendSubject", localResourceFile);
            subject = subject.Replace("[Engage:Portal]", portalName);

            return Mail.SendMail(senderEmail, friendsEmail, String.Empty, subject, localizedMessage, String.Empty, "HTML", String.Empty, String.Empty, String.Empty, String.Empty);
        }
    }
}