<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddMemberPost
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Member Added Successfully!</h2>
    New Member's ID is <em><% Response.Write(ViewData["ID"].ToString()); %></em> and Password is his / her EMail Address!

</asp:Content>
