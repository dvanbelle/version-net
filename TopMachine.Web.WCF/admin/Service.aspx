<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="TopMachine.Web.WCF.admin.Service" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblservice" runat="server" Text="Service"></asp:Label>
    
    </div>
    <p>
        <asp:Button ID="btnstart" runat="server" onclick="btnstart_Click" Text="start" 
            Width="74px" />
        <asp:Button ID="btnStop" runat="server" onclick="btnStop_Click" Text="Stop" 
            Width="93px" />
    </p>
    <p>
        <asp:Label ID="LblMsgError" runat="server" Text=""></asp:Label>
    </p>
    </form>
</body>
</html>
