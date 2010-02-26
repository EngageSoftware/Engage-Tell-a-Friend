// <copyright file="ModuleBase.cs" company="Engage Software">
// Engage: TellAFriend - http://www.engagesoftware.com
// Copyright (c) 2004-2010
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.TellAFriend
{
    using DotNetNuke.Entities.Modules;

    /// <summary>
    /// Base class for user controls within the TellAFriend module
    /// </summary>
    public class ModuleBase : PortalModuleBase
    {
        /// <summary>
        /// The relative path of the root module folder
        /// </summary>
        protected const string DesktopModuleFolderName = "/DesktopModules/EngageTellAFriend/";
    }
}
