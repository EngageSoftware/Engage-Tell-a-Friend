// <copyright file="CurrentContext.cs" company="Engage Software">
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
    using System.Diagnostics;

    /// <summary>
    /// Custom object to be serialized and made available in client code
    /// </summary>
    public class CurrentContext
    {
        /// <summary>
        /// Backing field for <see cref="SiteUrl" />
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string siteUrl;

        /// <summary>
        /// Backing field for <see cref="LocalResourceFile" />
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string localResourceFile;

        /// <summary>
        /// Backing field for <see cref="PortalId" />
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int portalId;

        /// <summary>
        /// Backing field for <see cref="PortalName" />
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string portalName;

        /// <summary>
        /// Backing field for <see cref="PortalEmail" />
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string portalEmail;

        /// <summary>
        /// Backing field for <see cref="WebMethodUrl" />
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string webMethodUrl;

        /// <summary>
        /// Backing field for <see cref="ShowModal" />
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool showModal;

        /// <summary>
        /// Backing field for <see cref="CurrentCulture" />
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string currentCulture;

        /// <summary>
        /// Initializes a new instance of the CurrentContext class.
        /// </summary>
        /// <param name="siteUrl">The site URL.</param>
        /// <param name="localResourceFile">The local resource file.</param>
        /// <param name="portalId">The portal id.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <param name="webMethodUrl">The web method URL.</param>
        /// <param name="showModalPopup">if set to <c>true</c> the form is shown in a modal popup.</param>
        /// <param name="portalEmail">The portal administrator's email.</param>
        /// <param name="currentCulture">The current culture.</param>
        public CurrentContext(string siteUrl, string localResourceFile, int portalId, string portalName, string webMethodUrl, bool showModalPopup, string portalEmail, string currentCulture)
        {
            this.siteUrl = siteUrl;
            this.localResourceFile = localResourceFile;
            this.portalId = portalId;
            this.portalName = portalName;
            this.webMethodUrl = webMethodUrl;
            this.showModal = showModalPopup;
            this.portalEmail = portalEmail;
            this.currentCulture = currentCulture;
        }

        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>The site URL.</value>
        public string SiteUrl
        {
            [DebuggerStepThrough]
            get { return this.siteUrl; }
            [DebuggerStepThrough]
            set { this.siteUrl = value; }
        }

        /// <summary>
        /// Gets or sets the local resource file.
        /// </summary>
        /// <value>The local resource file.</value>
        public string LocalResourceFile
        {
            [DebuggerStepThrough]
            get { return this.localResourceFile; }
            [DebuggerStepThrough]
            set { this.localResourceFile = value; }
        }

        /// <summary>
        /// Gets or sets the portal id.
        /// </summary>
        /// <value>The portal id.</value>
        public int PortalId
        {
            [DebuggerStepThrough]
            get { return this.portalId; }
            [DebuggerStepThrough]
            set { this.portalId = value; }
        }

        /// <summary>
        /// Gets or sets the name of the portal.
        /// </summary>
        /// <value>The name of the portal.</value>
        public string PortalName
        {
            [DebuggerStepThrough]
            get { return this.portalName; }
            [DebuggerStepThrough]
            set { this.portalName = value; }
        }

        /// <summary>
        /// Gets or sets the portal email.
        /// </summary>
        /// <value>The email address of the portal administrator.</value>
        public string PortalEmail
        {
            [DebuggerStepThrough]
            get { return this.portalEmail; }
            [DebuggerStepThrough]
            set { this.portalEmail = value; }
        }

        /// <summary>
        /// Gets or sets the web method URL.
        /// </summary>
        /// <value>The web method URL.</value>
        public string WebMethodUrl
        {
            [DebuggerStepThrough]
            get { return this.webMethodUrl; }
            [DebuggerStepThrough]
            set { this.webMethodUrl = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to [show modal].
        /// </summary>
        /// <value><c>true</c> if [show modal]; otherwise, <c>false</c>.</value>
        public bool ShowModal
        {
            [DebuggerStepThrough]
            get { return this.showModal; }
            [DebuggerStepThrough]
            set { this.showModal = value; }
        }

        /// <summary>
        /// Gets or sets the current context.
        /// </summary>
        /// <value>The current context.</value>
        public string CurrentCulture
        {
            [DebuggerStepThrough]   
            get { return this.currentCulture; }
            [DebuggerStepThrough]
            set { this.currentCulture = value; }
        }
    }
}