<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamplePlugin.aspx.cs" Inherits="eShop.web.Templates.Plugins.ExamplePlugin" %>

<%@ Register Src="~/Templates/UserControl/AdminImagePicker.ascx" TagPrefix="uc1" TagName="AdminImagePicker" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        The time is <%= GetTheTime() %>

        <asp:Button ID="Button1" runat="server" Text="Update" OnClick="UpdateTheTime" />

        <uc1:AdminImagePicker runat="server" id="AdminImagePicker" />
    </div>
    </form>
</body>
</html>

