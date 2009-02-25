// <copyright file="Settings.ascx.cs" company="Engage Software">
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
    using System.Globalization;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : ModuleSettingsBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.SiteUrlTextBox.Text = Utility.GetStringSetting(this.Settings, "SiteUrl", string.Empty);
                    this.ShowMessageCheckBox.Checked = Utility.GetBooleanSetting(this.Settings, "ShowMessage", true);
                    this.ShowModalCheckBox.Checked = Utility.GetBooleanSetting(this.Settings, "ShowModal", false);
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                if (this.Page.IsValid)
                {
                    var modules = new ModuleController();
                    modules.UpdateModuleSetting(this.ModuleId, "SiteUrl", this.SiteUrlTextBox.Text);
                    modules.UpdateModuleSetting(this.ModuleId, "ShowMessage", this.ShowMessageCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                    modules.UpdateModuleSetting(this.ModuleId, "ShowModal", this.ShowModalCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
    }
}