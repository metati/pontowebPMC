<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmfoto.aspx.cs" Inherits="Cadastro_frmfoto" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
    </script>
    <style type="text/css">
        .style1
        {
            width: 65%;
        }
        .style3
        {
            width: 169px;
            height: 63px;
        }
    </style>
    
    <script type="text/javascript">
        function ZeraTudo() {
        }
    </script>

</head>

<body style="width: 139px; height: 179px;">
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style3">
                    <dx:ASPxBinaryImage ID="ImgFotoUsuario" runat="server" 
                        ClientInstanceName="ImgFotoUsuario" Height="136px" Width="136px" 
                        Visible="False">
                    </dx:ASPxBinaryImage>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <dx:ASPxLabel ID="lbNome" runat="server" style="font-weight: 700" Text="Nome:" 
                        ClientInstanceName="lbNome">
                    </dx:ASPxLabel>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
