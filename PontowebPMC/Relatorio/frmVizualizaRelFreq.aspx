﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVizualizaRelFreq.aspx.cs" Inherits="Relatorio_frmVizualizaRelFreq" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
            function FecharJanela() {
                ww = window.open(window.location, "_self");
                ww.close();
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" 
            style="font-size: large; font-family: Arial, Helvetica, sans-serif" 
            Text="Sem dados para exibir"></asp:Label>
    
    </div>
    <dx:ASPxButton ID="btFechar" runat="server" 
        CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" 
        CssPostfix="Office2003Olive" 
        SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" 
        Text="Fechar" ClientInstanceName="btFechar">
        <ClientSideEvents Click="function(s, e) {
	FecharJanela();
}" />
    </dx:ASPxButton>
    </form>
</body>
</html>