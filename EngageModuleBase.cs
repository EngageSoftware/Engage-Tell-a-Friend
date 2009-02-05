// <copyright file="EngageModuleBase.cs" company="Engage Software">
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
    using System.Web.UI;

    /// <summary>
    /// Base class for user controls within the TellAFriend module
    /// </summary>
    public class EngageModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        /// <summary>
        /// The relative path of the root module folder
        /// </summary>
        protected const string DesktopModuleFolderName = "/DesktopModules/EngageTellAFriend/";

        /// <summary>
        /// Adds the a reference to jQuery 1.2.6.
        /// </summary>
        /// <param name="page">The page. Used to generate a key when registering the client script.</param>
        protected static void AddJQueryReference(Page page)
        {
            Utility.AddJQueryReference(page);
        }
    }
}
