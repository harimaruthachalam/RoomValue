<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GotPaid
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>I Got Paid</h2>
    <table style="width: 100%;" border="1" cellpadding="2" cellspacing="2">
        <tr>
            <th>
                S. No.
                </th>
                <th>
                Payer</th>
            <th>
                Item
            </th>
            <th>
                Have To Pay You</th>
            
            <th>
                Paid</th>
            <th>
                Accept</th>
            <th>
                Reject</th>
        </tr>
        <% RoomValue.Models.GotPaidModel[] gotPaidList = (RoomValue.Models.GotPaidModel[])ViewData["List"];
           int count = 1;
           foreach (RoomValue.Models.GotPaidModel tempGotPaid in gotPaidList)
           {
               Response.Write("<tr><td>" + count.ToString() + "</td>" + "<td>"+tempGotPaid.Payer+"</td><td>"+tempGotPaid.Item
                   + "</td><td>" + tempGotPaid.TotalAmount + "</td><td>" + tempGotPaid.PaidAmount 
                   + "</td><td><a href=\"Accept/"+tempGotPaid.ExpenditureID+"\" class=\"linkAccept\">Accept</a></td>"+
                   "<td><a href=\"Reject/" + tempGotPaid.ExpenditureID + "\" class=\"linkReject\">Reject</a></td></tr>");
               count++;
           }
             %>
    </table>
</asp:Content>
