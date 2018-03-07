<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RoomValue.Models.ExpenditureModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	My Expenditure
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/jquery-1.11.2.js" type="text/javascript"></script>
    <script src="../../Scripts/Expenditure.js" type="text/javascript"></script>
<% using (Html.BeginForm()) { %>
    <h2>My Expenditure</h2>
    <table style="width: 100%;">
        <tr>
            <td>
                Item
            </td>
            <td>
                <input id="tbItem" name="tbItem" type="text" />
            </td>
        </tr>
        <tr>
            <td>
                Total Amount
            </td>
            <td>
                <input id="tbTotalAmount" name="tbTotalAmount" type="text" />
            </td>
        </tr>
    </table> 
    <table style="width: 100%; border:0px">
        <tr>
            <th>
                Members
            </th>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <input value="All" type="radio" name="rbMemberSelection" class="rbMemberSelectionID" checked="checked" />All People<br />
                <input value="SomeWithMe" type="radio" name="rbMemberSelection" class="rbMemberSelectionID" />Some People(Including Me)<br />
                <input value="SomeWithoutMe" type="radio" name="rbMemberSelection" class="rbMemberSelectionID" />Some People(Excluding Me)
            </td>
            <td>
            <%
            {
                RoomValue.Models.Member[] x = (RoomValue.Models.Member[])ViewData["Members"];
                foreach (RoomValue.Models.Member tempMember in x)
                {
                    String tempCheck = "<input type=\"checkbox\" name=\"Check\" id=\"" + tempMember.ID + "\" value=\"" + tempMember.ID + "\" class=\"checkBoxMemberList\" disabled=disabled />" + tempMember.Name + "<br />";
                    Response.Write(tempCheck);
                } 
            }

             %>
            </td>
            <td>
                
            </td>
        </tr>
        <tr><td><input type="submit" value="Submit" id="btnSubmit" /></td><td></td><td></td></tr>
    </table>
    <% } %>
</asp:Content>
