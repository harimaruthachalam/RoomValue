<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	RemoveMember
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Remove Member</h2>
    <table style="width: 100%;" border="1" cellpadding="2" cellspacing="2">
        <tr>
            <th>
                ID</th>
            <th>
                Name</th>
            <th>
                Remove Him/Her</th>
        </tr>
        <% RoomValue.Models.MemberModel[] members = (RoomValue.Models.MemberModel[])ViewData["Members"];
           foreach (RoomValue.Models.MemberModel member in members)
           {
               Response.Write("<tr><td>"+member.ID+"</td><td>"+member.Name+"</td><td>"+
                   "<a href=\"remove/"+member.ID+"\">Remove</a></td></tr>"); 
           }
            %>
    </table>
</asp:Content>
