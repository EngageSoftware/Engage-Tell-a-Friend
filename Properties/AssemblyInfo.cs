// <copyright file="AssemblyInfo.cs" company="Engage Software">
// Engage: Tell-A-Friend
// Copyright (c) 2004-2013
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.UI;

[assembly: AssemblyTitle("EngageTellAFriend")]
[assembly: AssemblyDescription("A simple and flexible module for sharing DNN websites. Brought to you by Engage Software.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("2011 Engage Software")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: AssemblyVersion("01.02.01.*")]
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]

#if DEBUG
[assembly: WebResource("Engage.Dnn.TellAFriend.JavaScript.jquery-1.4.2.debug.js", "text/javascript")]
[assembly: WebResource("Engage.Dnn.TellAFriend.JavaScript.jquery.simplemodal.debug.js", "text/javascript")]
[assembly: WebResource("Engage.Dnn.TellAFriend.JavaScript.taf.debug.js", "text/javascript")]
[assembly: WebResource("Engage.Dnn.TellAFriend.JavaScript.json2.debug.js", "text/javascript")]
#else
[assembly: WebResource("Engage.Dnn.TellAFriend.JavaScript.jquery-1.4.2.js", "text/javascript")]
[assembly: WebResource("Engage.Dnn.TellAFriend.JavaScript.taf.bundle.js", "text/javascript")]
#endif