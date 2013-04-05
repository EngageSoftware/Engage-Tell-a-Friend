// <copyright file="Settings.ascx.cs" company="Engage Software">
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
    using System.Globalization;
    using System.Web.UI;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>The Settings class manages Module Settings</summary>
    public partial class Settings : ModuleSettingsBase
    {
        /// <summary>The local resources file for ViewTellAFriend so we can get the default email subject and body</summary>
        private const string LocalResourcesFile = "~/DesktopModules/EngageTellAFriend/App_LocalResources/ViewTellAFriend.ascx.resx";

        /// <summary>LoadSettings loads the settings from the Database and displays them</summary>
        public override void LoadSettings()
        {
            try
            {
                if (this.IsPostBack)
                {
                    return;
                }

                this.SiteUrlTextBox.Text = Utility.GetStringSetting(this.Settings, "SiteUrl", string.Empty);
                this.ShowMessageCheckBox.Checked = Utility.GetBooleanSetting(this.Settings, "ShowMessage", true);
                this.ShowModalCheckBox.Checked = Utility.GetBooleanSetting(this.Settings, "ShowModal", false);
                this.CarbonCopyTextBox.Text = Utility.GetStringSetting(this.Settings, "CarbonCopy", string.Empty);
                this.BlindCarbonCopyTextBox.Text = Utility.GetStringSetting(this.Settings, "BlindCarbonCopy", string.Empty);
                this.FromTextBox.Text = Utility.GetStringSetting(this.Settings, "From", string.Empty);
                this.SubjectTextBox.Text = Utility.GetStringSetting(this.Settings, "Subject", string.Empty);
                this.BodyTextBox.Text = Utility.GetStringSetting(this.Settings, "Body", string.Empty);
                this.InvisibleCaptchaCheckBox.Checked = Utility.GetBooleanSetting(this.Settings, "InvisibleCaptcha", true);
                this.TimedCaptchaCheckBox.Checked = Utility.GetBooleanSetting(this.Settings, "TimedCaptcha", true);
                this.StandardCaptchaCheckBox.Checked = Utility.GetBooleanSetting(this.Settings, "StandardCaptcha", false);

                this.SetEmailValidation();
            } 
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>UpdateSettings saves the modified settings to the Database</summary>
        public override void UpdateSettings()
        {
            try
            {
                if (!this.Page.IsValid)
                {
                    return;
                }

                var modules = new ModuleController();
                modules.UpdateModuleSetting(this.ModuleId, "SiteUrl", this.SiteUrlTextBox.Text);
                modules.UpdateModuleSetting(this.ModuleId, "ShowMessage", this.ShowMessageCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateModuleSetting(this.ModuleId, "ShowModal", this.ShowModalCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateModuleSetting(this.ModuleId, "CarbonCopy", this.CarbonCopyTextBox.Text);
                modules.UpdateModuleSetting(this.ModuleId, "BlindCarbonCopy", this.BlindCarbonCopyTextBox.Text);
                modules.UpdateModuleSetting(this.ModuleId, "From", this.FromTextBox.Text);
                modules.UpdateModuleSetting(this.ModuleId, "Subject", this.SubjectTextBox.Text);
                modules.UpdateModuleSetting(this.ModuleId, "Body", this.BodyTextBox.Text);
                modules.UpdateModuleSetting(this.ModuleId, "InvisibleCaptcha", this.InvisibleCaptchaCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateModuleSetting(this.ModuleId, "TimedCaptcha", this.TimedCaptchaCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateModuleSetting(this.ModuleId, "StandardCaptcha", this.StandardCaptchaCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>Raises the <see cref="Control.Init" /> event.</summary>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Utility.AddJQueryReference();
            this.RestoreButton.Click += this.RestoreButton_Click;
        }

        /// <summary>Handles the Click event of the RestoreButton control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void RestoreButton_Click(object sender, EventArgs e)
        {
            this.SubjectTextBox.Text = Localization.GetString("EmailAFriendSubject", LocalResourcesFile);
            this.BodyTextBox.Text = Localization.GetString("EmailAFriend", LocalResourcesFile);
        }

        /// <summary>Sets the validator expression for email fields.</summary>
        private void SetEmailValidation()
        {
            this.CarbonCopyValidator.ValidationExpression = FeaturesController.GetEmailListRegularExpression(this.PortalId);
            this.BlindCarbonCopyValidator.ValidationExpression = FeaturesController.GetEmailListRegularExpression(this.PortalId);
            this.FromValidator.ValidationExpression = FeaturesController.GetEmailRegularExpression(this.PortalId);
        }
    }
}
