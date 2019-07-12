<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ListModelBindingWeb.Models.BookEditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Using EditorTemplate And ViewModel</h2>
    <p>
        In this case, we're binding a list which is a property of our 
        model. And we're combining it with an editor template while also 
        allowing the user to delete items in the middle of the list.
    </p>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        <table>
            <tr>
                <th>Title</th>
                <th>Author</th>
                <th>Date Published</th>
            </tr>
            
            <% for (int i = 0; i < Model.Books.Count; i++) { %>
            
                <%: Html.EditorFor(model => model.Books[i]) %>

            <% } %>
            </table>

            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <h3>Resulting Markup</h3>
<pre class="csharpcode">
<span class="kwrd">&lt;</span><span class="html">form</span> <span class="attr">action</span><span class="kwrd">="/home/editortemplate"</span> <span class="attr">method</span><span class="kwrd">="post"</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_0__Title"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[0].Title</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="aoeu"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_0__Author"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[0].Author</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="aoeu"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_0__DatePublished"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[0].DatePublished</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="1/23/2005"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="hidden"</span> <span class="attr">name</span><span class="kwrd">="Books.Index"</span> <span class="attr">value</span><span class="kwrd">="0"</span> <span class="kwrd">/&gt;</span>

  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_1__Title"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[1].Title</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="aoeu"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_1__Author"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[1].Author</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="aoeu"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_1__DatePublished"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[1].DatePublished</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="1/23/2006"</span> <span class="kwrd">/&gt;</span> 
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="hidden"</span> <span class="attr">name</span><span class="kwrd">="Books.Index"</span> <span class="attr">value</span><span class="kwrd">="1"</span> <span class="kwrd">/&gt;</span>

  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_2__Title"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[2].Title</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="aoeu"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_2__Author"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[2].Author</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="aoeu"</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">id</span><span class="kwrd">="Books_2__DatePublished"</span> <span class="attr">name</span><span class="kwrd">="<strong>Books[2].DatePublished</strong>"</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">value</span><span class="kwrd">="1/23/2005"</span> <span class="kwrd">/&gt;</span> 
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="hidden"</span> <span class="attr">name</span><span class="kwrd">="Books.Index"</span> <span class="attr">value</span><span class="kwrd">="2"</span> <span class="kwrd">/&gt;</span>
        
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="submit"</span> <span class="attr">value</span><span class="kwrd">="Create"</span> <span class="kwrd">/&gt;</span>
  
<span class="kwrd">&lt;/</span><span class="html">form</span><span class="kwrd">&gt;</span>
</pre>


    <% if(Model != null) { %>
    <h3>The Submitted Values</h3>
    <ul>
    
        <% foreach(var book in Model.Books) { %>
            <li><%: book.Title %>, <%: book.Author %>, <%: book.DatePublished %></li>
        <% } %>
    </ul>
    <% } %>

</asp:Content>

