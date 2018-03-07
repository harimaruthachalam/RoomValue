<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if(HttpContext.Current.Session["UserName"]!=null) {
%>
        Welcome <b><%: HttpContext.Current.Session["Name"].ToString() %></b>!
        [ <%: Html.ActionLink("Home", "Index", "User") %> | <%: Html.ActionLink("Log Off", "LogOff", "Account") %> ]
<%
    }
    else {
%> 
        [ <%: Html.ActionLink("Home", "Index", "Home") %> | <%: Html.ActionLink("Log On", "LogOn", "Account") %> ]
<%
    }
%>
