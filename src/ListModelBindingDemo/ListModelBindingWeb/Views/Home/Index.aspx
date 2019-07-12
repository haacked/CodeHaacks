<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<int>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Binding to a list of numbers</h2>

    <p>
        This shows the simple example of binding to a list of numbers. 
        Notice that each form input simply has the same name.
    </p>
       

    <% using (Html.BeginForm()) { %>
        
        <input type="text" name="numbers" value="1" /><br />
        <input type="text" name="numbers" value="4" /><br />
        <input type="text" name="numbers" value="2" /><br />
        <input type="text" name="numbers" value="8" /><br />
        
        <input type="submit" />

    <% } %>

    <h3>The resulting markup</h3>

<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">form</span> <span class="attr">method</span><span class="kwrd">="post"</span> <span class="attr">action</span><span class="kwrd">="/Home/Index"</span><span class="kwrd">&gt;</span> 

  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">name</span><span class="kwrd">="ints"</span> <span class="attr">value</span><span class="kwrd">="1"</span> <span class="kwrd">/&gt;</span> 
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">name</span><span class="kwrd">="ints"</span> <span class="attr">value</span><span class="kwrd">="4"</span> <span class="kwrd">/&gt;</span> 
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">name</span><span class="kwrd">="ints"</span> <span class="attr">value</span><span class="kwrd">="2"</span> <span class="kwrd">/&gt;</span> 
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="text"</span> <span class="attr">name</span><span class="kwrd">="ints"</span> <span class="attr">value</span><span class="kwrd">="8"</span> <span class="kwrd">/&gt;</span> 
  
  <span class="kwrd">&lt;</span><span class="html">input</span> <span class="attr">type</span><span class="kwrd">="submit"</span> <span class="kwrd">/&gt;</span> 

<span class="kwrd">&lt;/</span><span class="html">form</span><span class="kwrd">&gt;</span></pre>

    <% if (Model != null) { %>
      <hr />
      <h3>The submitted values.</h3>
      <ul>
      <% foreach(var number in Model) { %>
        <li><%: number %></li>
      <% } %>
      </ul>
    <% } %>
</asp:Content>
