<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVizualizaRelatorio.aspx.cs" Inherits="Relatorio_frmVizualizaRelatorio" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gerenciador Ponto Frequência - Visualizar Folha de Frequencia</title>
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
    </div>
    <asp:Label ID="Label1" runat="server" 
        style="font-weight: 700; font-size: x-large" Text="Label"></asp:Label>
    <dx:ASPxButton ID="btFechar" runat="server" AutoPostBack="False" 
        ClientIDMode="AutoID" Text="Fechar Janela" 
        CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
        SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
        <ClientSideEvents Click="function(s, e) {
	FecharJanela();
}" />
    </dx:ASPxButton>
    </form>
</body>
</html>
