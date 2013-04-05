// <copyright file="FeaturesController.cs" company="Engage Software">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Controls which DNN features are available for this module.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated through reflection by DNN")]
    internal class FeaturesController
    {
        /// <summary>
        /// A regular expression which will match an email address
        /// </summary>
        /// <remarks>
        /// Because this needs to work in JavaScript and .NET, we can't just set <see cref="RegexOptions.IgnoreCase"/>, we have to manually include both cases for each letter
        /// <para>Basic Explanation: Any valid characters, @ symbol, any valid characters, . (period), then either two letters or one of the other 25 top-level-domains</para>
        /// <para>
        ///  Beginning of line or string
        ///  Any character in this class: <c>[a-zA-Z0-9._%\-+']</c>, one or more repetitions
        ///  <c>@</c>
        ///  Match expression but don't capture it. <c>[a-zA-Z0-9\-]+\.</c>, one or more repetitions
        ///      <c>[a-zA-Z0-9\-]+\.</c>
        ///          Any character in this class: <c>[a-zA-Z0-9\-]</c>, one or more repetitions
        ///          Literal <c>.</c>
        ///  Match expression but don't capture it. <c>[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Ii][Ll]|[Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt]</c>
        ///      Select from 26 alternatives
        ///          Any character in this class: <c>[a-zA-Z]</c>, exactly 2 repetitions
        ///          <c>[Aa][Ee][Rr][Oo]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///          <c>[Aa][Rr][Pp][Aa]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Pp]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///          <c>[Aa][Ss][Ii][Aa]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Ss]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///          <c>[Bb][Ii][Zz]</c>
        ///              Any character in this class: <c>[Bb]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Zz]</c>
        ///          <c>[Cc][Aa][Tt]</c>
        ///              Any character in this class: <c>[Cc]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///          <c>[Cc][Oo][Mm]</c>
        ///              Any character in this class: <c>[Cc]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///          <c>[Cc][Oo][Oo][Pp]</c>
        ///              Any character in this class: <c>[Cc]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Pp]</c>
        ///          <c>[Ee][dD][Uu]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[dD]</c>
        ///              Any character in this class: <c>[Uu]</c>
        ///          <c>[Gg][Oo][Vv]</c>
        ///              Any character in this class: <c>[Gg]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Vv]</c>
        ///          <c>[Ii][Nn][Ff][Oo]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Nn]</c>
        ///              Any character in this class: <c>[Ff]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///          <c>[Ii][Nn][Tt]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Nn]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///          <c>[Jj][Oo][Bb][Ss]</c>
        ///              Any character in this class: <c>[Jj]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Bb]</c>
        ///              Any character in this class: <c>[Ss]</c>
        ///          <c>[Mm][Ii][Ll]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Ll]</c>
        ///          <c>[Mm][Oo][Bb][Ii]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Bb]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///          <c>[Mm][Uu][Ss][Ee][Uu][Mm]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///              Any character in this class: <c>[Uu]</c>
        ///              Any character in this class: <c>[Ss]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Uu]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///          <c>[Nn][Aa][Mm][Ee]</c>
        ///              Any character in this class: <c>[Nn]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///          <c>[Nn][Ee][Tt]</c>
        ///              Any character in this class: <c>[Nn]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///          <c>[Oo][Rr][Gg]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Gg]</c>
        ///          <c>[Pp][Rr][Oo]</c>
        ///              Any character in this class: <c>[Pp]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///          <c>[Rr][Oo][Oo][Tt]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///          <c>[Tt][Ee][Ll]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Ll]</c>
        ///          <c>[Tt][Rr][Aa][Vv][Ee][Ll]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Vv]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Ll]</c>
        ///          <c>[Cc][Yy][Mm]</c>
        ///              Any character in this class: <c>[Cc]</c>
        ///              Any character in this class: <c>[Yy]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///          <c>[Gg][Ee][Oo]</c>
        ///              Any character in this class: <c>[Gg]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///          <c>[Pp][Oo][Ss][Tt]</c>
        ///              Any character in this class: <c>[Pp]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Ss]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///  End of line or string</para>
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "Ex is not an acronym.")]
        public const string EmailRegEx = @"^[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Ii][Ll]|[Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt])$";

        /// <summary>
        /// A regular expression which will match a comma-delimited list of email addresses
        /// </summary>
        /// <remarks>
        /// Because this needs to work in JavaScript and .NET, we can't just set <see cref="RegexOptions.IgnoreCase"/>, we have to manually include both cases for each letter
        /// <para>
        /// Basic Explanation: Any valid characters, @ symbol, any valid characters, . (period), then either two letters or one of the other 25 top-level-domains.
        /// Then, any number of times, an optional additional comma, whitespace, and the same pattern as above (that is, another email address)
        /// </para>
        /// <para>
        ///  Beginning of line or string
        ///  Any character in this class: <c>[a-zA-Z0-9._%\-+']</c>, one or more repetitions
        ///  @
        ///  Match expression but don't capture it. <c>[a-zA-Z0-9\-]+\.</c>, one or more repetitions
        ///      <c>[a-zA-Z0-9\-]+\.</c>
        ///          Any character in this class: <c>[a-zA-Z0-9\-]</c>, one or more repetitions
        ///          Literal .
        ///  Match expression but don't capture it. <c>[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Ii][Ll]|[Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt]</c>
        ///      Select from 26 alternatives
        ///          Any character in this class: <c>[a-zA-Z]</c>, exactly 2 repetitions
        ///          <c>[Aa][Ee][Rr][Oo]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///          <c>[Aa][Rr][Pp][Aa]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Pp]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///          <c>[Aa][Ss][Ii][Aa]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Ss]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///          <c>[Bb][Ii][Zz]</c>
        ///              Any character in this class: <c>[Bb]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Zz]</c>
        ///          <c>[Cc][Aa][Tt]</c>
        ///              Any character in this class: <c>[Cc]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///          <c>[Cc][Oo][Mm]</c>
        ///              Any character in this class: <c>[Cc]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///          <c>[Cc][Oo][Oo][Pp]</c>
        ///              Any character in this class: <c>[Cc]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Pp]</c>
        ///          <c>[Ee][dD][Uu]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[dD]</c>
        ///              Any character in this class: <c>[Uu]</c>
        ///          <c>[Gg][Oo][Vv]</c>
        ///              Any character in this class: <c>[Gg]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Vv]</c>
        ///          <c>[Ii][Nn][Ff][Oo]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Nn]</c>
        ///              Any character in this class: <c>[Ff]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///          <c>[Ii][Nn][Tt]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Nn]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///          <c>[Jj][Oo][Bb][Ss]</c>
        ///              Any character in this class: <c>[Jj]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Bb]</c>
        ///              Any character in this class: <c>[Ss]</c>
        ///          <c>[Mm][Ii][Ll]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///              Any character in this class: <c>[Ll]</c>
        ///          <c>[Mm][Oo][Bb][Ii]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Bb]</c>
        ///              Any character in this class: <c>[Ii]</c>
        ///          <c>[Mm][Uu][Ss][Ee][Uu][Mm]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///              Any character in this class: <c>[Uu]</c>
        ///              Any character in this class: <c>[Ss]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Uu]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///          <c>[Nn][Aa][Mm][Ee]</c>
        ///              Any character in this class: <c>[Nn]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///          <c>[Nn][Ee][Tt]</c>
        ///              Any character in this class: <c>[Nn]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///          <c>[Oo][Rr][Gg]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Gg]</c>
        ///          <c>[Pp][Rr][Oo]</c>
        ///              Any character in this class: <c>[Pp]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///          <c>[Rr][Oo][Oo][Tt]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///          <c>[Tt][Ee][Ll]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Ll]</c>
        ///          <c>[Tt][Rr][Aa][Vv][Ee][Ll]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///              Any character in this class: <c>[Rr]</c>
        ///              Any character in this class: <c>[Aa]</c>
        ///              Any character in this class: <c>[Vv]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Ll]</c>
        ///          <c>[Cc][Yy][Mm]</c>
        ///              Any character in this class: <c>[Cc]</c>
        ///              Any character in this class: <c>[Yy]</c>
        ///              Any character in this class: <c>[Mm]</c>
        ///          <c>[Gg][Ee][Oo]</c>
        ///              Any character in this class: <c>[Gg]</c>
        ///              Any character in this class: <c>[Ee]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///          <c>[Pp][Oo][Ss][Tt]</c>
        ///              Any character in this class: <c>[Pp]</c>
        ///              Any character in this class: <c>[Oo]</c>
        ///              Any character in this class: <c>[Ss]</c>
        ///              Any character in this class: <c>[Tt]</c>
        ///  Match expression but don't capture it. <c>,\s?[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Ii][Ll]|[Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt])</c>, any number of repetitions
        ///      <c>,\s?[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Ii][Ll]|[Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt])</c>
        ///          <c>,</c>
        ///          Whitespace, zero or one repetitions
        ///          Any character in this class: <c>[a-zA-Z0-9._%\-+']</c>, one or more repetitions
        ///          <c>@</c>
        ///          Match expression but don't capture it. <c>[a-zA-Z0-9\-]+\.</c>, one or more repetitions
        ///              <c>[a-zA-Z0-9\-]+\.</c>
        ///                  Any character in this class: <c>[a-zA-Z0-9\-]</c>, one or more repetitions
        ///                  Literal .
        ///          Match expression but don't capture it. <c>[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Ii][Ll]|[Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt]</c>
        ///              Select from 26 alternatives
        ///                  Any character in this class: <c>[a-zA-Z]</c>, exactly 2 repetitions
        ///                  <c>[Aa][Ee][Rr][Oo]</c>
        ///                      Any character in this class: <c>[Aa]</c>
        ///                      Any character in this class: <c>[Ee]</c>
        ///                      Any character in this class: <c>[Rr]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                  <c>[Aa][Rr][Pp][Aa]</c>
        ///                      Any character in this class: <c>[Aa]</c>
        ///                      Any character in this class: <c>[Rr]</c>
        ///                      Any character in this class: <c>[Pp]</c>
        ///                      Any character in this class: <c>[Aa]</c>
        ///                  <c>[Aa][Ss][Ii][Aa]</c>
        ///                      Any character in this class: <c>[Aa]</c>
        ///                      Any character in this class: <c>[Ss]</c>
        ///                      Any character in this class: <c>[Ii]</c>
        ///                      Any character in this class: <c>[Aa]</c>
        ///                  <c>[Bb][Ii][Zz]</c>
        ///                      Any character in this class: <c>[Bb]</c>
        ///                      Any character in this class: <c>[Ii]</c>
        ///                      Any character in this class: <c>[Zz]</c>
        ///                  <c>[Cc][Aa][Tt]</c>
        ///                      Any character in this class: <c>[Cc]</c>
        ///                      Any character in this class: <c>[Aa]</c>
        ///                      Any character in this class: <c>[Tt]</c>
        ///                  <c>[Cc][Oo][Mm]</c>
        ///                      Any character in this class: <c>[Cc]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Mm]</c>
        ///                  <c>[Cc][Oo][Oo][Pp]</c>
        ///                      Any character in this class: <c>[Cc]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Pp]</c>
        ///                  <c>[Ee][dD][Uu]</c>
        ///                      Any character in this class: <c>[Ee]</c>
        ///                      Any character in this class: <c>[dD]</c>
        ///                      Any character in this class: <c>[Uu]</c>
        ///                  <c>[Gg][Oo][Vv]</c>
        ///                      Any character in this class: <c>[Gg]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Vv]</c>
        ///                  <c>[Ii][Nn][Ff][Oo]</c>
        ///                      Any character in this class: <c>[Ii]</c>
        ///                      Any character in this class: <c>[Nn]</c>
        ///                      Any character in this class: <c>[Ff]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                  <c>[Ii][Nn][Tt]</c>
        ///                      Any character in this class: <c>[Ii]</c>
        ///                      Any character in this class: <c>[Nn]</c>
        ///                      Any character in this class: <c>[Tt]</c>
        ///                  <c>[Jj][Oo][Bb][Ss]</c>
        ///                      Any character in this class: <c>[Jj]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Bb]</c>
        ///                      Any character in this class: <c>[Ss]</c>
        ///                  <c>[Mm][Ii][Ll]</c>
        ///                      Any character in this class: <c>[Mm]</c>
        ///                      Any character in this class: <c>[Ii]</c>
        ///                      Any character in this class: <c>[Ll]</c>
        ///                  <c>[Mm][Oo][Bb][Ii]</c>
        ///                      Any character in this class: <c>[Mm]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Bb]</c>
        ///                      Any character in this class: <c>[Ii]</c>
        ///                  <c>[Mm][Uu][Ss][Ee][Uu][Mm]</c>
        ///                      Any character in this class: <c>[Mm]</c>
        ///                      Any character in this class: <c>[Uu]</c>
        ///                      Any character in this class: <c>[Ss]</c>
        ///                      Any character in this class: <c>[Ee]</c>
        ///                      Any character in this class: <c>[Uu]</c>
        ///                      Any character in this class: <c>[Mm]</c>
        ///                  <c>[Nn][Aa][Mm][Ee]</c>
        ///                      Any character in this class: <c>[Nn]</c>
        ///                      Any character in this class: <c>[Aa]</c>
        ///                      Any character in this class: <c>[Mm]</c>
        ///                      Any character in this class: <c>[Ee]</c>
        ///                  <c>[Nn][Ee][Tt]</c>
        ///                      Any character in this class: <c>[Nn]</c>
        ///                      Any character in this class: <c>[Ee]</c>
        ///                      Any character in this class: <c>[Tt]</c>
        ///                  <c>[Oo][Rr][Gg]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Rr]</c>
        ///                      Any character in this class: <c>[Gg]</c>
        ///                  <c>[Pp][Rr][Oo]</c>
        ///                      Any character in this class: <c>[Pp]</c>
        ///                      Any character in this class: <c>[Rr]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                  <c>[Rr][Oo][Oo][Tt]</c>
        ///                      Any character in this class: <c>[Rr]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Tt]</c>
        ///                  <c>[Tt][Ee][Ll]</c>
        ///                      Any character in this class: <c>[Tt]</c>
        ///                      Any character in this class: <c>[Ee]</c>
        ///                      Any character in this class: <c>[Ll]</c>
        ///                  <c>[Tt][Rr][Aa][Vv][Ee][Ll]</c>
        ///                      Any character in this class: <c>[Tt]</c>
        ///                      Any character in this class: <c>[Rr]</c>
        ///                      Any character in this class: <c>[Aa]</c>
        ///                      Any character in this class: <c>[Vv]</c>
        ///                      Any character in this class: <c>[Ee]</c>
        ///                      Any character in this class: <c>[Ll]</c>
        ///                  <c>[Cc][Yy][Mm]</c>
        ///                      Any character in this class: <c>[Cc]</c>
        ///                      Any character in this class: <c>[Yy]</c>
        ///                      Any character in this class: <c>[Mm]</c>
        ///                  <c>[Gg][Ee][Oo]</c>
        ///                      Any character in this class: <c>[Gg]</c>
        ///                      Any character in this class: <c>[Ee]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                  <c>[Pp][Oo][Ss][Tt]</c>
        ///                      Any character in this class: <c>[Pp]</c>
        ///                      Any character in this class: <c>[Oo]</c>
        ///                      Any character in this class: <c>[Ss]</c>
        ///                      Any character in this class: <c>[Tt]</c>
        ///  End of line or string</para>
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "Ex is not an acronym.")]
        public const string EmailsRegEx = @"^[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Ii][Ll]|[Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt])(?:,\s?[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Ii][Ll]|[Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt]))*$";
    }
}