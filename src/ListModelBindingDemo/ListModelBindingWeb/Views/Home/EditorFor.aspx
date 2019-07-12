<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ListModelBindingWeb.Models.Book>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Unbroken Index with EditorFor</h3>
    <p>
        This example shows the format where the indices for the form elements 
        must form an unbroken chain of indices starting from 0 and increasing 
        by 1.
    </p>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
        <table>
            <tr>
                <th>Title</th>
                <th>Author</th>
                <th>Date Published</th>
            </tr>
        <% for (int i = 0; i < 4; i++) { %>
            
            <%: Html.EditorFor(m = m[i]) %>

        <% } %>
        </table>
        <p>
            <input type="submit" value="Create" />
        </p>
    <% } //endusing%>

</asp:Content>

