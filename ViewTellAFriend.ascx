<%@ Control language="C#" Inherits="Engage.Dnn.TellAFriend.ViewTellAFriend" AutoEventWireup="false" Codebehind="ViewTellAFriend.ascx.cs" %>
<%@ Import Namespace="Engage.Dnn.TellAFriend"%>
<%@ Import Namespace="DotNetNuke.Entities.Modules"%>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>

<div class="taf-wrap">
    <div id="ModalAnchorDiv" class="taf-anchor" runat="server">
        <a href="#" class="CommandButton"><%= Localization.GetString("MainLinkText", LocalResourceFile) %></a>
    </div>
    <div id="FormWrapDiv" runat="server" class="taf-form-wrap Normal">
        <div class="taf-intro"><%= Localization.GetString("Introduction", LocalResourceFile) %></div>
        <div class="taf-form">
	        <div>
	            <p><%= Localization.GetString("FriendName", LocalResourceFile) %></p>
                <asp:TextBox ID="FriendNameTextBox" runat="server" CssClass="NormalTextBox"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FriendNameTextBox" Display="Dynamic" ResourceKey="FriendNameRequired" ValidationGroup="EngageTellAFriend" CssClass="error-msg-rt NormalRed"/>
            </div>
            <div>
                <p><%= Localization.GetString("RecipientEmailAddress", LocalResourceFile) %></p>
                <asp:TextBox ID="FriendsEmailTextBox" runat="server" CssClass="NormalTextBox"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="FriendEmailRequired" ValidationGroup="EngageTellAFriend" CssClass="error-msg-email NormalRed"/>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="FriendsEmailTextBox" Display="Dynamic" ResourceKey="FriendEmailInvalid" ValidationGroup="EngageTellAFriend"  CssClass="error-msg-rt NormalRed" ValidationExpression="^[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Mm][Ii][Ll]|[Mm][Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt])$"/>
            </div>
            <div>
                <p><%= Localization.GetString("SenderName", LocalResourceFile) %></p>
	            <asp:TextBox ID="SenderNameTextBox" runat="server" CssClass="NormalTextBox"/>
	            <asp:RequiredFieldValidator runat="server" ControlToValidate="SenderNameTextBox" Display="Dynamic" ResourceKey="SenderNameRequired" ValidationGroup="EngageTellAFriend" CssClass="error-msg-rt NormalRed"/>
	        </div>
            <div>
                <p><%= Localization.GetString("EmailAddress", LocalResourceFile) %></p>
	            <asp:TextBox ID="SenderEmailTextBox" runat="server" CssClass="NormalTextBox"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="SenderEmailRequired" ValidationGroup="EngageTellAFriend" CssClass="error-msg-rt NormalRed"/>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="SenderEmailTextBox" Display="Dynamic" ResourceKey="SenderEmailInvalid" ValidationGroup="EngageTellAFriend"  CssClass="error-msg-last NormalRed" ValidationExpression="^[a-zA-Z0-9._%\-+']+@(?:[a-zA-Z0-9\-]+\.)+(?:[a-zA-Z]{2}|[Aa][Ee][Rr][Oo]|[Aa][Rr][Pp][Aa]|[Aa][Ss][Ii][Aa]|[Bb][Ii][Zz]|[Cc][Aa][Tt]|[Cc][Oo][Mm]|[Cc][Oo][Oo][Pp]|[Ee][dD][Uu]|[Gg][Oo][Vv]|[Ii][Nn][Ff][Oo]|[Ii][Nn][Tt]|[Jj][Oo][Bb][Ss]|[Mm][Mm][Ii][Ll]|[Mm][Mm][Oo][Bb][Ii]|[Mm][Uu][Ss][Ee][Uu][Mm]|[Nn][Aa][Mm][Ee]|[Nn][Ee][Tt]|[Oo][Rr][Gg]|[Pp][Rr][Oo]|[Rr][Oo][Oo][Tt]|[Tt][Ee][Ll]|[Tt][Rr][Aa][Vv][Ee][Ll]|[Cc][Yy][Mm]|[Gg][Ee][Oo]|[Pp][Oo][Ss][Tt])$"/>
            </div>
            <div runat="server" id="MessageRow">
                <p><%= Localization.GetString("Message", LocalResourceFile) %></p>
                <asp:TextBox runat="server" TextMode="MultiLine" Rows="6" CssClass="NormalTextBox" />
            </div>
            
            <div class="taf-submit">
                <a href="#" class="CommandButton"><%= Localization.GetString("SubmitButton.Text", LocalResourceFile) %></a>
            </div>
            <span class="taf-progress-icon"></span>
        </div>
        <div class="taf-error" style="display:none;">
	        <%= Localization.GetString("EmailError.Text", LocalResourceFile)%>
        </div>
        <div class="taf-success" style="display:none;">
            <%= Localization.GetString("EmailSuccess.Text", LocalResourceFile)%>
        </div>
        
    </div>
</div>