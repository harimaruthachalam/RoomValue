<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddMember
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/jquery-1.11.2.js" type="text/javascript"></script>
    <script src="../../Scripts/AddMember.js" type="text/javascript"></script>
    <h2>Add Member</h2>
    <% using (Html.BeginForm()) { %>
    <table style="width: 100%;">
        <tr>
            <td>
                Name</td>
            <td>
                &nbsp;
                <input id="tbName" name="tbName" type="text" /></td>
        </tr>
        <tr>
            <td>
                EMail Address</td>
            <td>
                &nbsp;
                <input id="tbMail" name="tbMail" type="text" /></td>
        </tr>
        <tr>
            <td>
                Phone</td>
            <td>
                &nbsp;
                <input id="tbPhone" name="tbPhone" type="text" /></td>
        </tr>
        <tr>
            <td>
                <input id="btnSubmit" type="submit" value="Submit" /></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <% } %>
</asp:Content>
