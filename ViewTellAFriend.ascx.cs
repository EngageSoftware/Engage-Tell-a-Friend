// <copyright file="ViewTellAFriend.ascx.cs" company="Engage Software">
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
    using System.Web.Script.Serialization;
    using DotNetNuke.Services.Exceptions;
    
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewTellAFriend class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class ViewTellAFriend : EngageModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
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
                string siteUrl = Utility.GetStringSetting(this.Settings, "SiteUrl");

                var currentContextInfo = new CurrentContext(
                        String.IsNullOrEmpty(siteUrl) ? null : siteUrl,
                        this.TabId,
                        this.LocalResourceFile,
                        this.PortalId,
                        this.PortalSettings.PortalName,
                        (this.ResolveUrl("~" + DesktopModuleFolderName + "WebMethods.asmx") + "/SendEmail"));

                    var serializer = new JavaScriptSerializer();
                    string scriptBlock = "var CurrentContextInfo = " + serializer.Serialize(currentContextInfo);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CurrentContext", scriptBlock, true);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
    }
}