<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <form id="Form1" runat="server" action="User/List">
    <asp:Button ID="btnExpenditureList" runat="server" Text="Expenditure List"  />
    </form>
   <% using (Html.BeginForm("Expenditure", "User", FormMethod.Get))
{ %>
    <input type="submit" value="My Expenditures" />
    <%
}
%>
   <% using (Html.BeginForm("HaveToPay", "User", FormMethod.Get))
{ %>
    <input type="submit" value="I Have To Pay" />
    <%
}
%>
   <% using (Html.BeginForm("GotPaid", "User", FormMethod.Get))
{ %>
    <input type="submit" value="I Got Paid" />
    <%
}
%>
   <% using (Html.BeginForm("UpdateDetails", "User", FormMethod.Get))
{ %>
    <input type="submit" value="Update Details" />
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
