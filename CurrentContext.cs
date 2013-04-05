// <copyright file="CurrentContext.cs" company="Engage Software">
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
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Custom object to be serialized and made available in client code
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Following JSON naming convention")]
    public class CurrentContext
    {
        /// <summary>
        /// Initializes a new instance of the CurrentContext class.
        /// </summary>
        /// <param name="siteUrl">The site URL.</param>
        /// <param name="localResourceFile">The local resource file.</param>
        /// <param name="portalId">The portal id.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <param name="webMethodUrl">The web method URL.</param>
        /// <param name="portalEmail">The portal administrator's email.</param>
        /// <param name="currentCulture">The current culture.</param>
        /// <param name="validationGroup">The validation group.</param>
        /// <param name="tabId">The tab ID.</param>
        /// <param name="moduleId">The module ID.</param>
        public CurrentContext(
                string siteUrl,
                string localResourceFile,
                int portalId,
                string portalName,
                string webMethodUrl,
                string portalEmail,
                string currentCulture,
                string validationGroup,
                int tabId,
                int moduleId)
        {
            this.siteUrl = siteUrl;
            this.localResourceFile = localResourceFile;
            this.portalId = portalId;
            this.portalName = portalName;
            this.webMethodUrl = webMethodUrl;
            this.portalEmail = portalEmail;
            this.currentCulture = currentCulture;
            this.validationGroup = validationGroup;
            this.tabId = tabId;
            this.moduleId = moduleId;
        }

        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>The site URL.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "site", Justification = "Following JSON naming convention")]
        public string siteUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the local resource file.
        /// </summary>
        /// <value>The local resource file.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "local", Justification = "Following JSON naming convention")]
        public string localResourceFile
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the portal id.
        /// </summary>
        /// <value>The portal id.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "portal", Justification = "Following JSON naming convention")]
        public int portalId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the portal.
        /// </summary>
        /// <value>The name of the portal.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "portal", Justification = "Following JSON naming convention")]
        public string portalName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the portal email.
        /// </summary>
        /// <value>The email address of the portal administrator.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "portal", Justification = "Following JSON naming convention")]
        public string portalEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the web method URL.
        /// </summary>
        /// <value>The web method URL.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "web", Justification = "Following JSON naming convention")]
        public string webMethodUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current culture.
        /// </summary>
        /// <value>The current culture.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "current", Justification = "Following JSON naming convention")]
        public string currentCulture
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the validation group.
        /// </summary>
        /// <value>The validation group.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "validation", Justification = "Following JSON naming convention")]
        public string validationGroup
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tab ID.
        /// </summary>
        /// <value>The tab ID.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "tab", Justification = "Following JSON naming convention")]
        public int tabId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the module ID.
        /// </summary>
        /// <value>The module ID.</value>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "module", Justification = "Following JSON naming convention")]
        public int moduleId
        {
            get;
            set;
        }
    }
}