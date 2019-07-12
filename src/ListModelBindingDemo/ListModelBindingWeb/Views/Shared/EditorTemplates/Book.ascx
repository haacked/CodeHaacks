<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ListModelBindingWeb.Models.Book>" %>

<tr>
    <td>
        <%: Html.TextBoxFor(m => m.Title) %>
        <%: Html.ValidationMessageFor(m => m.Title) %>
    </td>
    <td>
        <%: Html.TextBoxFor(m => m.Author) %>
        <%: Html.ValidationMessageFor(m => m.Author) %>
    </td>
    <td>
        <%: Html.TextBoxFor(m => m.DatePublished) %> 
        <%: Html.ValidationMessageFor(m => m.DatePublished) %>
    </td>

    <td>
        <%: Html.HiddenIndexerInputForModel() %>
        <a href="#" class="delete">Delete</a>
    </td>
</tr>

