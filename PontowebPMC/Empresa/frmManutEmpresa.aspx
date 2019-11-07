<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmManutEmpresa.aspx.cs" Inherits="Empresa_frmManutEmpresa" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            font-size: large;
        }
        .style3
        {
            width: 31px;
        }
        .style7
        {
            width: 545px;
        }
        .style8
        {
            width: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="style1">
        <tr>
            <td class="style2" colspan="3">
                Manutenção de Empresa<dx:ASPxUploadControl ID="ucLogoEmpresa" runat="server" ClientInstanceName="ucLogoEmpresa" ShowProgressPanel="True" Theme="Default" UploadMode="Auto" Width="280px" ShowUploadButton="True" OnFileUploadComplete="ucLogoEmpresa_FileUploadComplete">
                    <BrowseButton Text="Procurar...">
                    </BrowseButton>
                </dx:ASPxUploadControl>
                <dx:ASPxImage ID="img" runat="server" ClientInstanceName="img">
                </dx:ASPxImage>
                <br />
                <dx:ASPxBinaryImage ID="biImagem" runat="server" ClientInstanceName="biImagem">
                </dx:ASPxBinaryImage>
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="3">
                <dx:ASPxHyperLink ID="hlImagem" runat="server" 
                    CssFilePath="~/App_Themes/Youthful/{0}/styles.css" CssPostfix="Youthful" 
                    Text="Adicionar ou trocar imagem da empresa" />
            </td>
        </tr>
        <tr>
            <td class="style8">
            </td>
            <td class="style3">
                Empresa:</td>
            <td class="style7">
                <dx:ASPxTextBox ID="tbEmpresa" runat="server" ClientInstanceName="tbEmpresa" 
                    MaxLength="150" ToolTip="Nome da Empresa" Width="400px">
                    <ValidationSettings CausesValidation="True" ValidationGroup="GrupoEmpresa">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td class="style3">
                Endereço:</td>
            <td class="style7">
                <dx:ASPxTextBox ID="tbEndereco" runat="server" ClientInstanceName="tbEndereco" 
                    MaxLength="150" ToolTip="Nome da Empresa" Width="400px">
                    <ValidationSettings CausesValidation="True" ValidationGroup="GrupoEmpresa">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td class="style3">
                CNPJ:</td>
            <td class="style7">
                <dx:ASPxTextBox ID="tbCNPJ" runat="server" ClientInstanceName="tbCNPJ" 
                    MaxLength="150" ToolTip="Nome da Empresa" Width="400px">
                    <ValidationSettings CausesValidation="True" ValidationGroup="GrupoEmpresa">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td class="style3">
                Status:</td>
            <td class="style7">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

