// <copyright file="Utility.cs" company="Engage Software">
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
    using System.Collections;
    using System.Globalization;
    using System.Web.UI;

    /// <summary>
    /// Utility Class for Engage Tell A Friend
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Adds the a reference to jQuery 1.2.6.
        /// </summary>
        /// <param name="page">The page. Used to generate a key when registering the client script.</param>
        public static void AddJQueryReference(Page page)
        {
            const string JavaScriptReferenceFormat = @"<script src='{0}' type='text/javascript'></script>";
            const string JQueryRegistrationKey = "jQueryTAF";
            if (!page.ClientScript.IsClientScriptBlockRegistered(typeof(ModuleBase), JQueryRegistrationKey))
            {
                string scriptReference = string.Format(CultureInfo.InvariantCulture, JavaScriptReferenceFormat, page.ClientScript.GetWebResourceUrl(typeof(ModuleBase), "Engage.Dnn.TellAFriend.JavaScript.jquery-1.2.6.min.js"));
                page.Header.Controls.Add(new LiteralControl(scriptReference));
                page.ClientScript.RegisterClientScriptBlock(typeof(ModuleBase), JQueryRegistrationKey, "jQuery(function($){$.noConflict();});", true);
            }
        }

        /// <summary>
        /// Gets the given setting as a <see cref="bool"/>, or <paramref name="defaultValue"/> if the setting hasn't been set.
        /// </summary>
        /// <param name="settings">The settings collection.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The given setting as a <see cref="bool"/>, or <paramref name="defaultValue"/> if the setting hasn't been set.</returns>
        public static bool GetBooleanSetting(IDictionary settings, string settingName, bool? defaultValue)
        {
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

        /// <summary>
        /// Gets the given setting as a <see cref="string"/>, or <paramref name="defaultValue"/> if the setting is not set.
        /// </summary>
        /// <param name="settings">The settings collection.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The setting or default as a <see cref="string"/></returns>
        public static string GetStringSetting(Hashtable settings, string settingName, string defaultValue)
        {
            if (settings != null && settings.Contains(settingName))
            {
                return (settings[settingName] as string) ?? defaultValue;
            }

            return defaultValue;
        }
    }
}