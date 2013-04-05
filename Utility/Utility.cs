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
    using System.Web.UI;

    using DotNetNuke.Framework;

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

        /// <summary>Adds the a reference to jQuery.</summary>
        public static void AddJQueryReference()
        {
            jQuery.RequestRegistration();
        }

        /// <summary>Gets the given setting as a <see cref="bool" />, or <paramref name="defaultValue" /> if the setting hasn't been set.</summary>
        /// <param name="settings">The settings collection.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given setting as a <see cref="bool" />, or <paramref name="defaultValue" /> if the setting hasn't been set.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="settings"/> or <paramref name="settingName"/> are <c>null</c></exception>
        public static bool GetBooleanSetting(IDictionary settings, string settingName, bool defaultValue)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (settingName == null)
            {
                throw new ArgumentNullException("settingName");
            }

            var settingValue = settings[settingName];
            if (settingValue != null)
            {
                bool value;
                if (bool.TryParse(settingValue.ToString(), out value))
                {
                    return value;
                }
            }

            return defaultValue;
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
