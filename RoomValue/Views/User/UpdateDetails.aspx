<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	UpdateDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/jquery-1.11.2.js" type="text/javascript"></script>
    <script src="../../Scripts/UpdateDetails.js" type="text/javascript"></script>
    <h2>Update My Details</h2>
    <% using (Html.BeginForm())
       {
           RoomValue.Models.DetailsModel details = (RoomValue.Models.DetailsModel)ViewData["details"];
           %>
    <table style="width: 100%;">
        <tr>
            <td>
                Phone Number</td>
            <td>
            <% Response.Write("<input id=\"tbPhone\" name=\"tbPhone\" type=\"text\" value=\"" + details.Phone + "\" />"); %>
                </td>
        </tr>
        <tr>
            <td>
                Email Address</td>
            <td>
            <% Response.Write("<input id=\"tbEmail\" name=\"tbEmail\" type=\"email\" value=\"" + details.EMail + "\" />"); %></td>
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
