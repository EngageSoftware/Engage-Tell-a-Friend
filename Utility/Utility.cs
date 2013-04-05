// <copyright file="Utility.cs" company="Engage Software">
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
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using DotNetNuke.Common;

    /// <summary>Utility Class for Engage: Tell-A-Friend</summary>
    public static class Utility
    {
        /// <summary>The resource namespace for JavaScript files in this module</summary>
        private const string JavaScriptNamespace = "Engage.Dnn.TellAFriend.JavaScript.";

        /// <summary>The extension to use for JavaScript files</summary>
        private const string JavaScriptExtension = ".js";

        /// <summary>Adds a reference to the  JavaScript (embedded) resource with the given <paramref name="scriptName" /> to the given <paramref name="page" />.</summary>
        /// <param name="page">The page to which the reference needs to be added.</param>
        /// <param name="scriptName">Name of the script (without file extension).</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="page"/> is <c>null</c></exception>
        public static void AddJavaScriptResource(Page page, string scriptName)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }
            
            page.ClientScript.RegisterClientScriptResource(typeof(ModuleBase), GetJavaScriptResourceName(scriptName));
        }

        /// <summary>Adds the a reference to jQuery 1.4.2.</summary>
        /// <param name="page">The page. Used to generate a key when registering the client script.</param>
        public static void AddJQueryReference(Page page)
        {
            InjectJQueryLibrary(page, true);
        }

        /// <summary>Gets the given setting as a <see cref="bool" />, or <paramref name="defaultValue" /> if the setting hasn't been set.</summary>
        /// <param name="settings">The settings collection.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given setting as a <see cref="bool" />, or <paramref name="defaultValue" /> if the setting hasn't been set.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="settings"/> or <paramref name="settingName"/> are <c>null</c></exception>
        public static bool GetBooleanSetting(IDictionary settings, string settingName, bool? defaultValue)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (settingName == null)
            {
                throw new ArgumentNullException("settingName");
            }

            object o = settings[settingName];
            if (o != null)
            {
                bool value;
                if (bool.TryParse(o.ToString(), out value))
                {
                    return value;
                }
            }

            return defaultValue.Value;
        }

        /// <summary>Gets the given setting as a <see cref="string" />, or <paramref name="defaultValue" /> if the setting is not set.</summary>
        /// <param name="settings">The settings collection.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The setting or default as a <see cref="string" /></returns>
        public static string GetStringSetting(Hashtable settings, string settingName, string defaultValue)
        {
            if (settings != null && settings.Contains(settingName))
            {
                return (settings[settingName] as string) ?? defaultValue;
            }

            return defaultValue;
        }

        /// <summary>Includes the jQuery libraries onto the page</summary>
        /// <param name="page">Page object from calling page/control</param>
        /// <param name="includeNoConflict">if <c>true</c>, includes the uncompressed libraries</param>
        /// <remarks>Based on <see href="http://www.ifinity.com.au/Blog/EntryId/75/Include-jQuery-in-a-DotNetNuke-Module-with-version-independent-code" /></remarks>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Page's Controls collection is responsible for disposing")]
        private static void InjectJQueryLibrary(Page page, bool includeNoConflict)
        {
            int major, minor, build, revision;
            bool injectLib = !SafeDNNVersion(out major, out minor, out revision, out build) || major < 5;

            if (injectLib)
            {
                // no in-built jQuery libraries into the framework, so include the google version
                string lib = page.ClientScript.GetWebResourceUrl(typeof(ModuleBase), GetJavaScriptResourceName("jquery-1.4.2"));

                if (page.Header.FindControl("jquery") == null)
                {
                    var jqueryReference = new HtmlGenericControl("script");
                    jqueryReference.Attributes.Add("src", lib);
                    jqueryReference.Attributes.Add("type", "text/javascript");
                    jqueryReference.ID = "jquery";
                    page.Header.Controls.Add(jqueryReference);
                    
                    if (includeNoConflict)
                    {
                        // use the noConflict (stops use of $) due to the use of prototype 
                        // with a standard DNN distro
                        var jqueryNoConflictScript = new HtmlGenericControl("script");
                        jqueryNoConflictScript.InnerText = " jQuery.noConflict(); ";
                        jqueryNoConflictScript.Attributes.Add("type", "text/javascript");
                        page.Header.Controls.Add(jqueryNoConflictScript);
                    }
                }
            }
            else
            {
                // call DotNetNuke.Framework.jQuery.RequestRegistration();
                var jqueryType = Type.GetType("DotNetNuke.Framework.jQuery, DotNetNuke");
                if (jqueryType != null)
                {
                    // run the DNN 5.0 specific jQuery registration code
                    jqueryType.InvokeMember("RequestRegistration", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, jqueryType, null, null);
                }
            }
        }

        /// <summary>Returns a version-safe set of version numbers for DNN</summary>
        /// <param name="major">out value of major version (i.e., 4,5 etc)</param>
        /// <param name="minor">out value of minor version (i.e., 1 in 5.1 etc)</param>
        /// <param name="revision">out value of revision (i.e., 3 in 5.1.3)</param>
        /// <param name="build">out value of build number</param>
        /// <returns><c>true</c> if successful, <c>false</c> if not</returns>
        /// <remarks>DNN moved the version number during about the 4.9 version,
        /// which to me was a bit frustrating and caused the need for this reflection method call</remarks>
        private static bool SafeDNNVersion(out int major, out int minor, out int revision, out int build)
        {
            var version = Assembly.GetAssembly(typeof(Globals)).GetName().Version;
            if (version != null)
            {
                major = version.Major;
                minor = version.Minor;
                build = version.Build;
                revision = version.Revision;

                return true;
            }

            major = 0;
            minor = 0;
            build = 0;
            revision = 0;

            return false;
        }

        /// <summary>Gets the full name of the JavaScript (embedded) resource with the given name, providing a debug version if compiled in Debug mode, and a minified version in Release mode.</summary>
        /// <param name="scriptName">Name of the script (without file extension).</param>
        /// <returns>The full name of the JavaScript embedded resource</returns>
        private static string GetJavaScriptResourceName(string scriptName)
        {
#if DEBUG
            scriptName += ".debug";
#endif
            return JavaScriptNamespace + scriptName + JavaScriptExtension;
        }
    }
}