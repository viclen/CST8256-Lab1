<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookStore.aspx.cs" Inherits="BookStore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Store</title>
    <link href="App_Themes/SiteStyles.css" rel="stylesheet" />
</head>
<body>
    <h1>Online Book Store</h1>
    <form id="form1" runat="server">
        <a  href="ShoppingCartView.aspx">View Cart</a> (<asp:Label runat="server" ID="lblNumItems"></asp:Label>) <br /><br />
        <asp:DropDownList  ID="drpBookSelection" runat="server" CssClass="dropdown" 
            OnSelectedIndexChanged="drpBookSelection_SelectedIndexChanged" AutoPostBack="true" >
            <asp:ListItem Value="-1">Select a Book ... </asp:ListItem>
        </asp:DropDownList>
        
        <%-- todo: Add Required Field Validator --%>
        <asp:RangeValidator id="RangeValidator1"
           ControlToValidate="drpBookSelection"
           MinimumValue="0"
           MaximumValue="100"
           Type="Double"
           EnableClientScript="false"
           Text="The book must be selected."
           runat="server"
           class="error" />
        
        <div class="description">
            <asp:Label runat="server" ID="lblDescription"></asp:Label>
        </div>
        <br />
        <span class="emphsis">Price: </span><asp:Label runat="server" ID="lblPrice" CssClass="Price" ></asp:Label>                
        <span class="emphsis">Quantity: </span><asp:TextBox runat="server" ID="txtQuantity" cssclass="input"/>
        
        <div class="right error">
            <%-- todo: Add Required Field Validator --%>
            <asp:RequiredFieldValidator ID="quantityValidator" runat="server" ControlToValidate="txtQuantity"
                       CssClass="failureNotification" Text="Quantity is required." ToolTip="Quantity is required."
                       ValidationGroup="ValidationGroup" />

            <%-- todo: Add Range Validator --%>
            <asp:RangeValidator id="RangeValidator2"
               ControlToValidate="txtQuantity"
               MinimumValue="1"
               MaximumValue="3"
               Type="Double"
               EnableClientScript="false"
               Text="The quantity must between 1 and 3."
               runat="server"/>
        </div>

        <br /><br /><asp:Button runat="server" ID="btnAddToCart" Text="Add to Cart" cssclass="button" OnClick="btnAddToCart_Click"/>
    </form>  
</body>
</html>

