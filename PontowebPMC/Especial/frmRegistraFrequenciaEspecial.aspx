﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmRegistraFrequenciaEspecial.aspx.cs" Inherits="Especial_frmRegistraFrequenciaEspecial" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
    .style2
    {
        width: 47px;
    }
    .style3
    {
        width: 95px;
    }
    .style4
    {
        font-size: medium;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="style1">
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td class="style4">
            <strong>Registro de Frequência</strong></td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            Usuário:</td>
        <td>
            <dx:ASPxTextBox ID="tbLogin" runat="server" 
                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Width="170px">
                <ValidationSettings>
                    <ErrorFrameStyle ImageSpacing="4px">
                        <ErrorTextPaddings PaddingLeft="4px" />
                    </ErrorFrameStyle>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </dx:ASPxTextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            Senha:</td>
        <td>
            <dx:ASPxTextBox ID="tbSenha" runat="server" 
                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                Password="True" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                Width="170px">
                <ValidationSettings>
                    <ErrorFrameStyle ImageSpacing="4px">
                        <ErrorTextPaddings PaddingLeft="4px" />
                    </ErrorFrameStyle>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </dx:ASPxTextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
            <table class="style1">
                <tr>
                    <td class="style3">
                        <dx:ASPxButton ID="btSalvar" runat="server" 
                            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                            onclick="btSalvar_Click" 
                            SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Text="Lançar" 
                            Width="100px">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btCancelar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                            CssPostfix="PlasticBlue" onclick="btCancelar_Click" 
                            SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Text="Voltar" 
                            Width="100px">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>
