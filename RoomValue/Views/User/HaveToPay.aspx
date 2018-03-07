<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	HaveToPay
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="../../Scripts/jquery-1.11.2.js" type="text/javascript"></script>
<script src="../../Scripts/HaveToPay.js" type="text/javascript"></script>
    <h2>Have To Pay</h2>
    <% using (Html.BeginForm()) { %>
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
                Pay to</td>
            <td>
                &nbsp;
                <select id="dListTo" name="dListTo">
                    <option value="0">Select a Member</option>
                    <% 
                        RoomValue.Models.Member[] member = (RoomValue.Models.Member[])ViewData["Members"];
                        foreach (RoomValue.Models.Member tempMember in member)
                        {
                            Response.Write("<option value=\""+tempMember.ID+"\">"+tempMember.Name+"</option>");
                        }
            %>
                </select></td>
        </tr>
        <tr>
            <td>
                &nbsp;
                Pay For</td>
            <td>
                &nbsp;
                <select id="dListFor" name="dListFor" disabled="disabled">
                    <option value="0">Select a Member</option>
                </select></td>
        </tr>
        <tr>
            <td>
                &nbsp;
                Paying Now</td>
            <td>
                &nbsp;
                <input id="tbAmount" type="text" name="tbAmount" disabled="disabled" /></td>
        </tr>
        <tr>
            <td>
                <input type="submit" value="Submit" id="btnSubmit" disabled="disabled" /></td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <% } %>
</asp:Content>
