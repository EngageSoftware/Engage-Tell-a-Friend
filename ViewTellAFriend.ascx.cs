// <copyright file="ViewTellAFriend.ascx.cs" company="Engage Software">
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
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Framework;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    using JetBrains.Annotations;

    /// <summary>The ViewTellAFriend class displays the content</summary>
    public partial class ViewTellAFriend : ModuleBase
    {
        /// <summary>Initializes a new instance of the <see cref="ViewTellAFriend" /> class.</summary>
        protected ViewTellAFriend()
        {
            this.ShowMessage = true;
            this.ShowInModal = false;
            this.Url = string.Empty;
            this.UseInvisibleCaptcha = true;
            this.UseTimedCaptcha = true;
            this.UseStandardCaptcha = false;
        }

        /// <summary>Gets or sets a value indicating whether the message textbox should be shown.</summary>
        /// <value><c>true</c> if the message textbox should be shown; otherwise, <c>false</c>.</value>
        [PublicAPI]
        public bool ShowMessage { get; set; }

        /// <summary>Gets or sets a value indicating whether the module should be displayed as a modal popup or inline.</summary>
        /// <value><c>true</c> if the module should be displayed as a modal popup; otherwise, <c>false</c>.</value>
        [PublicAPI]
        public bool ShowInModal { get; set; }

        /// <summary>Gets or sets the URL to use in the email, or <see cref="string.Empty" /> to use the current URL.</summary>
        /// <value>The URL to be used, or <see cref="string.Empty" /> to use the current URL.</value>
        [PublicAPI]
        public string Url { get; set; }

        /// <summary>Gets or sets a value indicating whether the module should use an invisible CAPTCHA.</summary>
        /// <value><c>true</c> if the module should use an invisible CAPTCHA; otherwise, <c>false</c>.</value>
        public bool UseInvisibleCaptcha { get; set; }

        /// <summary>Gets or sets a value indicating whether the module should use a timed CAPTCHA.</summary>
        /// <value><c>true</c> if the module should use a timed CAPTCHA; otherwise, <c>false</c>.</value>
        public bool UseTimedCaptcha { get; set; }

        /// <summary>Gets or sets a value indicating whether the module should use a standard CAPTCHA.</summary>
        /// <value><c>true</c> if the module should use a standard CAPTCHA; otherwise, <c>false</c>.</value>
        public bool UseStandardCaptcha { get; set; }

        /// <summary>Gets the validation group for this instance of the module.</summary>
        /// <value>The module's validation group.</value>
        [PublicAPI]
        public string ValidationGroup
        {
            get { return "EngageTellAFriend" + this.ModuleId.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>Raises the <see cref="Control.Init" /> event.</summary>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            // If this control is loaded as a Skin Object, we need to set the LocalResourceFile manually
            this.LocalResourceFile = this.ResolveUrl("App_LocalResources/ViewTellAFriend.ascx.resx");

            this.LoadSettings();
            this.Load += this.Page_Load;
            this.SubmitButton.Click += this.SubmitButton_Click;
            base.OnInit(e);
        }

        /// <summary>Loads the settings.</summary>
        private void LoadSettings()
        {
            this.ShowInModal = Utility.GetBooleanSetting(this.Settings, "ShowModal", this.ShowInModal);
            this.Url = Utility.GetStringSetting(this.Settings, "SiteUrl", this.Url);
            this.ShowMessage = Utility.GetBooleanSetting(this.Settings, "ShowMessage", this.ShowMessage);
            this.UseInvisibleCaptcha = Utility.GetBooleanSetting(this.Settings, "InvisibleCaptcha", this.UseInvisibleCaptcha);
            this.UseTimedCaptcha = Utility.GetBooleanSetting(this.Settings, "TimedCaptcha", this.UseTimedCaptcha);
            this.UseStandardCaptcha = Utility.GetBooleanSetting(this.Settings, "StandardCaptcha", this.UseStandardCaptcha);
        }

        /// <summary>Handles the Load event of the Page control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Utility.AddJQueryReference();
#if DEBUG
                Utility.AddJavaScriptResource(this.Page, "jquery.simplemodal");
                Utility.AddJavaScriptResource(this.Page, "taf");
#else
                Utility.AddJavaScriptResource(this.Page, "taf.bundle");
#endif

                this.AddCssFile();
                this.SetEmailValidation();
                this.SetValidationGroupOnChildControls();
                if (!this.IsPostBack)
                {
                    this.PopulateUserInfo();
                }

                this.SetupCaptchas();
                this.ModuleWrap.Attributes["data-close-button-html"] = Localization.GetString("ModalCloseButton.Text", this.LocalResourceFile);
                this.SubmitButton.ToolTip = Localization.GetString("SubmitButtonToolTip.Text", this.LocalResourceFile);
                this.MessageRow.Visible = this.ShowMessage;
                this.ModalAnchorPanel.Visible = this.ShowInModal;
                this.FormWrapPanel.Style[HtmlTextWriterStyle.Display] = this.ModalAnchorPanel.Visible ? "none" : "block";
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>Handles the <see cref="Button.Click" /> event of the <see cref="SubmitButton" /> control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsValid)
                {
                    return;
                }

                var siteUrl = Utility.GetStringSetting(this.Settings, "SiteUrl", string.Empty);
                var response = EmailService.SendEmail(
                    this.LocalResourceFile,
                    string.IsNullOrEmpty(siteUrl) ? this.GetCurrentUrl() : siteUrl,
                    this.PortalSettings.PortalName,
                    this.SenderEmailTextBox.Text,
                    this.FriendsEmailTextBox.Text,
                    this.SenderNameTextBox.Text,
                    this.FriendNameTextBox.Text,
                    this.MessageTextBox.Text,
                    this.PortalSettings.Email,
                    this.ModuleId,
                    this.TabId);

                var successfullySent = string.IsNullOrEmpty(response);
                this.SuccessPanel.Visible = successfullySent;
                this.ErrorPanel.Visible = !successfullySent;

                if (!successfullySent)
                {
                    return;
                }

                this.FriendNameTextBox.Text = string.Empty;
                this.FriendsEmailTextBox.Text = string.Empty;
                this.MessageTextBox.Text = string.Empty;
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>Sets the validator expression for email fields.</summary>
        private void SetEmailValidation()
        {
            this.FriendEmailPatternValidator.ValidationExpression = FeaturesController.GetEmailRegularExpression(this.PortalId);
            this.SenderEmailPatternValidator.ValidationExpression = FeaturesController.GetEmailRegularExpression(this.PortalId);
        }

        /// <summary>Adds the CSS file if this is loaded as a skin object rather than a regular module.</summary>
        private void AddCssFile()
        {
            PageBase.RegisterStyleSheet(this.Page, this.ResolveUrl("TellAFriend.css"), true);
        }

        /// <summary>Sets the validation group on child controls.</summary>
        private void SetValidationGroupOnChildControls()
        {
            this.FriendNameRequiredValidator.ValidationGroup = this.ValidationGroup;
            this.FriendEmailRequiredValidator.ValidationGroup = this.ValidationGroup;
            this.FriendEmailPatternValidator.ValidationGroup = this.ValidationGroup;
            this.SenderNameRequiredValidator.ValidationGroup = this.ValidationGroup;
            this.SenderEmailRequiredValidator.ValidationGroup = this.ValidationGroup;
            this.SenderEmailPatternValidator.ValidationGroup = this.ValidationGroup;
            this.InvisibleCaptcha.ValidationGroup = this.ValidationGroup;
            this.TimedCaptcha.ValidationGroup = this.ValidationGroup;
            this.StandardCaptcha.ValidationGroup = this.ValidationGroup;
            this.SubmitButton.ValidationGroup = this.ValidationGroup;
        }

        /// <summary>Populates the "from" fields with the current DNN user's display name and email address.</summary>
        private void PopulateUserInfo()
        {
            if (Null.IsNull(this.UserId))
            {
                return;
            }

            this.SenderNameTextBox.Text = this.UserInfo.DisplayName;
            this.SenderEmailTextBox.Text = this.UserInfo.Email;
        }

        /// <summary>Sets up the CAPTCHA controls.</summary>
        private void SetupCaptchas()
        {
            this.InvisibleCaptcha.Visible = this.InvisibleCaptcha.Enabled = this.UseInvisibleCaptcha;
            this.TimedCaptcha.Visible = this.TimedCaptcha.Enabled = this.UseTimedCaptcha;
            this.StandardCaptcha.Visible = this.StandardCaptcha.Enabled = this.UseStandardCaptcha;

            this.InvisibleCaptcha.ErrorMessage = Localization.GetString("InvisibleCaptchaFailed", this.LocalResourceFile);
            this.InvisibleCaptcha.InvisibleTextBoxLabel = Localization.GetString("InvisibleCaptchaLabel", this.LocalResourceFile);
            this.TimedCaptcha.ErrorMessage = Localization.GetString("TimedCaptchaFailed", this.LocalResourceFile);
            this.StandardCaptcha.ErrorMessage = Localization.GetString("StandardCaptchaFailed", this.LocalResourceFile);
            this.StandardCaptcha.CaptchaLinkButtonText = Localization.GetString("StandardCaptchaLink", this.LocalResourceFile);
            this.StandardCaptcha.CaptchaTextBoxLabel = Localization.GetString("StandardCaptchaLabel", this.LocalResourceFile);
        }

        /// <summary>Gets the current URL.</summary>
        /// <returns>The fully qualified current URL.</returns>
        private string GetCurrentUrl()
        {
            if (HttpContext.Current != null && HttpContext.Current.Items.Contains("UrlRewrite:OriginalUrl"))
            {
                return (string)HttpContext.Current.Items["UrlRewrite:OriginalUrl"];
            }

            var currentUrl = Globals.NavigateURL(this.TabId, string.Empty, this.GetCurrentQueryString());
            if (!Uri.IsWellFormedUriString(currentUrl, UriKind.Absolute))
            {
                currentUrl = this.Request.Url.Scheme + Uri.SchemeDelimiter + this.PortalSettings.PortalAlias.HTTPAlias + currentUrl;
            }

            return currentUrl;
        }

        /// <summary>Gets the current query string.</summary>
        /// <returns>The full and current query string parameters.</returns>
        private string[] GetCurrentQueryString()
        {
            var parameters = new List<string>(this.Request.QueryString.Count);
            foreach (var key in this.Request.QueryString.AllKeys)
            {
                if (string.Equals(key, "TABID", StringComparison.OrdinalIgnoreCase) || string.Equals(key, "LANGUAGE", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var values = this.Request.QueryString[key].Split(',');
                foreach (var value in values)
                {
                    parameters.Add(key == null ? value + '=' : key + '=' + value);
                }
            }

            return parameters.ToArray();
        }
    }
}
