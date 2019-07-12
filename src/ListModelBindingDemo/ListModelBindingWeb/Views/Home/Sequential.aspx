<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ListModelBindingWeb.Models.Book>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Binding to a list of complex objects</h2>
    <h3>Sequential Index</h3>
    <p>
        This example shows the format where the indices for the form elements 
        must form a sequential unbroken chain of indices starting from 0 and increasing 
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
        <% for (int i = 0; i < 3; i++) { %>
            
            <tr>
                <td>
                    <%: Html.TextBoxFor(m => m[i].Title) %>
                    <%: Html.ValidationMessageFor(m => m[i].Title) %>
                </td>
                <td>
                    <%: Html.TextBoxFor(m => m[i].Author) %>
                    <%: Html.ValidationMessageFor(m => m[i].Author) %>
                </td>
                <td>
                    <%: Html.TextBoxFor(m => m[i].DatePublished) %> 
                    <%: Html.ValidationMessageFor(m => m[i].DatePublished) %>
                </td>
            </tr>
        <% } %>
        </table>
        <p>
            <input type="submit" value="Create" />
        </p>
    <% } //endusing%>

    <h3>The Resulting Markup</h3>

<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">form</span> <span class="attr">action</span><span class="kwrd">=&quot;/home/sequential&quot;</span> <span class="attr">method</span><span class="kwrd">=&quot;post&quot;</span><span class="kwrd">&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[0].Title&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[0].Author&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[0].DatePublished&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span> 
  
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[1].Title&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[1].Author&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span>
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[1].DatePublished&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span> 
                    
   <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[2].Title&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span>
   <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[2].Author&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span>
   <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">name</span><span class="kwrd">=&quot;[2].DatePublished&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;&quot;</span> <span class="kwrd">/&gt;</span> 
                    
   <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">=&quot;submit&quot;</span> <span class="attr">value</span><span class="kwrd">=&quot;Create&quot;</span> <span class="kwrd">/&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">form</span><span class="kwrd">&gt;</span></pre>
    
    <% if(Model != null) { %>
    <h3>The Submitted Values</h3>
    <ul>
    
        <% foreach(var book in Model) { %>
            <li><%: book.Title %>, <%: book.Author %>, <%: book.DatePublished %></li>
        <% } %>
    </ul>
    <% } %>
</asp:Content>