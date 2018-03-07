<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/i18n/grid.locale-en.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../Scripts/ExpenditureList.js" type="text/javascript"></script>
    <h2>Expenditure List</h2>
    <table id="list"></table>
<div id="pager"></div>
</asp:Content>
