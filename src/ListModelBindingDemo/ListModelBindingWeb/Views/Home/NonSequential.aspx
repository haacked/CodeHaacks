<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ListModelBindingWeb.Models.Book>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Binding to a list of complex objects</h2>
    <h3>Non-Sequential Arbitrary Index</h3>
    <p>
        This example shows the format where we include a special hidden input 
        which allows us to choose an arbitrary index and no longer requires 
        that the index be sequential.
    </p>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
        <table>
            <tr>
                <th>Title</th>
                <th>Author</th>
                <th>Date Published</th>
            </tr>
        
            <%: Html.Hidden("Index", "book-one") %>

            <tr>
                <td>
                    <%: Html.TextBox("[book-one].Title") %>
                </td>
                <td>
                    <%: Html.TextBox("[book-one].Author") %>
                </td>
                <td>
                    <%: Html.TextBox("[book-one].DatePublished") %>
                </td>
            </tr>

            <%: Html.Hidden("Index", "pear") %>

            <tr>
                <td>
                    <%: Html.TextBox("[pear].Title") %>
                </td>
                <td>
                    <%: Html.TextBox("[pear].Author") %>
                </td>
                <td>
                    <%: Html.TextBox("[pear].DatePublished") %>
                </td>
            </tr>

            <%: Html.Hidden("Index", "sibilance") %>

            <tr>
                <td>
                    <%: Html.TextBox("[sibilance].Title")%>
                </td>
                <td>
                    <%: Html.TextBox("[sibilance].Author")%>
                </td>
                <td>
                    <%: Html.TextBox("[sibilance].DatePublished")%>
                </td>
            </tr>
        
        </table>
        <p>
            <input type="submit" value="Create" />
        </p>
    <% } //endusing%>

    <h3>The Resulting Markup</h3>

<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">form</span> <span class="attr">action</span><span class="kwrd">="/home/nonsequential"</span> <span class="attr">method</span><span class="kwrd">="post"</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Index"</span> <span class="attr">name</span><span class="kwrd">="Index"</span> <span class="attr">type</span><span class="kwrd">="hidden"</span> <span class="attr">value</span><span class="kwrd">="book-one"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">="[book-one].Title"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">=""</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">="[book-one].Author"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">=""</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">="[book-one].DatePublished"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">=""</span> <span class="kwrd">/&gt;</span>
   
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Index"</span> <span class="attr">name</span><span class="kwrd">="Index"</span> <span class="attr">type</span><span class="kwrd">="hidden"</span> <span class="attr">value</span><span class="kwrd">="pear"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">="[pear].Title"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">=""</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">="[pear].DatePublished"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">=""</span> <span class="kwrd">/&gt;</span>
   
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Index"</span> <span class="attr">name</span><span class="kwrd">="Index"</span> <span class="attr">type</span><span class="kwrd">="hidden"</span> <span class="attr">value</span><span class="kwrd">="sibilance"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">="[sibilance].Title"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">=""</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">="[sibilance].Author"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">=""</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">="[sibilance].DatePublished"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">=""</span> <span class="kwrd">/&gt;</span>
  
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="submit"</span> <span class="attr">value</span><span class="kwrd">="Create"</span> <span class="kwrd">/&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">form</span><span class="kwrd">&gt;</span>
</pre>

    <% if(Model != null) { %>
    <h3>The Submitted Values</h3>
    <ul>
    
        <% foreach(var book in Model) { %>
            <li><%: book.Title %>, <%: book.Author %>, <%: book.DatePublished %></li>
        <% } %>
    </ul>
    <% } %>

</asp:Content>

