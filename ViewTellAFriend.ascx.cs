// <copyright file="ViewTellAFriend.ascx.cs" company="Engage Software">
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
    using System;
    using System.Web.Script.Serialization;
    using System.Web.UI;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Framework;
    using DotNetNuke.Services.Exceptions;

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewTellAFriend class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class ViewTellAFriend : EngageModuleBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether the message textbox should be shown.
        /// </summary>
        /// <value><c>true</c> if [show message]; otherwise, <c>false</c>.</value>
        public bool ShowMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the module should be displayed as a modal popup or inline.
        /// </summary>
        /// <value><c>true</c> if [show in modal]; otherwise, <c>false</c>.</value>
        public bool ShowInModal { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL to be used.</value>
        public string Url { get; set; }
        
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.LoadSettings();
            this.LocalResourceFile = this.ResolveUrl("App_LocalResources/ViewTellAFriend.ascx.resx");
            this.InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            string url = this.ResolveUrl("~" + DesktopModuleFolderName + "/JavaScript/validators.js");
            string validatorOverrideScripts = "<script src=\"" + url + "\" type=\"text/javascript\"></script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
            base.Render(writer);
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            this.ShowInModal = Utility.GetBooleanSetting(Settings, "ShowModal", false);
            this.Url = Utility.GetStringSetting(Settings, "SiteUrl", string.Empty);
            this.ShowMessage = Utility.GetBooleanSetting(Settings, "ShowMessage", true);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += this.Page_Load;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Utility.AddJQueryReference(this.Page);
                this.AddCssFile();
                this.RegisterCurrentContext();
                this.PopulateUserInfo();
                this.MessageRow.Visible = this.ShowMessage;
                
                this.FormWrapDiv.Style["display"] = this.ShowInModal ? "none" : "block";
                this.ModalAnchorDiv.Style["display"] = this.ShowInModal ? "block" : "none";
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Adds the CSS file.
        /// </summary>
        private void AddCssFile()
        {
            var basePage = this.Page as CDefault;
            if (basePage != null)
            {
                basePage.AddStyleSheet("TellAFriend", this.ResolveUrl("TellAFriend.css"), true);
            }
        }

        /// <summary>
        /// Populates the "from" fields with the current DNN user's display name and email address.
        /// </summary>
        private void PopulateUserInfo()
        {
            if (!Null.IsNull(this.UserId))
            {
                this.SenderNameTextBox.Text = this.UserInfo.DisplayName;
                this.SenderEmailTextBox.Text = this.UserInfo.Email;
            }
        }

        /// <summary>
        /// Registers the JSON current context object on the client side.
        /// </summary>
        private void RegisterCurrentContext()
        {
            string siteUrl = Utility.GetStringSetting(this.Settings, "SiteUrl", string.Empty);
            var currentContextInfo = new CurrentContext(
                String.IsNullOrEmpty(siteUrl) ? this.GetCurrentUrl() : siteUrl,
                this.LocalResourceFile,
                this.PortalId,
                this.PortalSettings.PortalName,
                (this.ResolveUrl("~" + DesktopModuleFolderName + "WebMethods.asmx") + "/SendEmail"),
                Utility.GetBooleanSetting(Settings, "ShowModal", false));

            var serializer = new JavaScriptSerializer();
            string scriptBlock = "var CurrentContextInfo = " + serializer.Serialize(currentContextInfo);
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CurrentContext", scriptBlock, true);
        }

        /// <summary>
        /// Gets the current URL.
        /// </summary>
        /// <returns>The fully qualified current URL.</returns>
        private string GetCurrentUrl()
        {
            string currentUrl = Globals.NavigateURL(this.TabId);
            if (!Uri.IsWellFormedUriString(currentUrl, UriKind.Absolute))
            {
                currentUrl = this.Request.Url.Scheme + ":\\" + this.PortalSettings.PortalAlias.HTTPAlias + currentUrl;
            }

            return currentUrl;
        }
    }
}