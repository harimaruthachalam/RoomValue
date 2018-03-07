<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <% using (Html.BeginForm("AddMember", "Admin", FormMethod.Get))
{ %>
    <input type="submit" value="Add Member" />
    <%
}
%>
<% using (Html.BeginForm("ViewMemberDetails", "Admin", FormMethod.Get))
{ %>
    <input type="submit" value="View Member Details" />
    <%
}
%>
<% using (Html.BeginForm("RemoveMember", "Admin", FormMethod.Get))
{ %>
    <input type="submit" value="Remove Member" />
    <%
}
%>
    <% using (Html.BeginForm("ChangePassword", "Account", FormMethod.Get))
{ %>
    <input type="submit" value="Change Password" />
    <%
}
%>
</asp:Content>
