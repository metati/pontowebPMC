<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmTrocaSenha.aspx.cs" Inherits="Account_frmTrocaSenha" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .style5
        {
            font-size: small;
        }
        .style4
        {
            width: 93px;
        }
        .style6
        {
            height: 38px;
        }
        .auto-style3
        {
            width: 87px;
        }
        .auto-style4
        {
            width: 174px;
        }
        .auto-style5
        {
            width: 405px;
        }
        .auto-style6
        {
            width: 221px;
        }
        .auto-style8
        {
            width: 599px;
        }
        .auto-style9
        {
            width: 31px;
            height: 31px;
        }
        .auto-style10
        {
            width: 317px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">MÓDULO DE ALTERAR SENHA</div> 

            </td>
        </tr>
        <tr>
            <td>
                <img alt="" class="auto-style9" src="../Images/Imagem36.png" /> Mudar senha</td>
        </tr>
        <tr>
            <td align="center" style="background-color: #F5F5F5">&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientIDMode="AutoID" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                ForeColor="Red" Text="Senha Antiga Inválida. Tente novamente" Visible="False">
                            </dx:ASPxLabel>
                        </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="center">&nbsp;</td>
        </tr>
    </table>
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td align="right" class="auto-style5" style="color: #333333; font-size: 14px;">Senha Antiga:</td>
            <td class="auto-style3" style="color: #333333; font-size: 14px;">
                            <dx:ASPxTextBox ID="tbSenhaAntiga" runat="server" ClientIDMode="AutoID" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                Password="True" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                                Width="228px" Theme="iOS">
                                <ValidationSettings CausesValidation="True" ErrorText="">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField ErrorText="" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
            <td align="right" class="auto-style4" colspan="2" style="color: #333333; font-size: 14px;">&nbsp;</td>
            <td align="right" class="auto-style4" colspan="2" style="color: #333333; font-size: 14px;">Senha Nova:</td>
            <td class="auto-style6" style="color: #333333; font-size: 14px;">
                            <dx:ASPxTextBox ID="tbSenhaNova" runat="server" ClientIDMode="AutoID" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                Password="True" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                                Width="228px" Theme="iOS">
                                <ValidationSettings CausesValidation="True" ErrorText="">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField ErrorText="" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
            <td align="right" class="auto-style8" style="color: #333333; font-size: 14px;">Confirma Nova Senha:</td>
            <td class="auto-style10" style="color: #333333; font-size: 14px;">
                            <dx:ASPxTextBox ID="tbConfirmaSenhaNova" runat="server" ClientIDMode="AutoID" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                Password="True" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                                Width="228px" Theme="iOS">
                                <ValidationSettings CausesValidation="True" ErrorText="">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField ErrorText="" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td align="center" class="auto-style4" colspan="4">&nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style8">&nbsp;</td>
            <td class="auto-style10">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td align="center" class="auto-style4" colspan="4">&nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style8">&nbsp;</td>
            <td class="auto-style10">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td align="center">&nbsp;</td>
            <td align="center" class="auto-style4" colspan="2">&nbsp;</td>
            <td align="center">
                                        <dx:ASPxButton ID="btConfirmar" runat="server" 
                                            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                            onclick="btConfirmar_Click" 
                                            SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Text="Mudar Senha" 
                                            Width="154px" Theme="iOS" ToolTip="Mudar senha">
                                        </dx:ASPxButton>
                                    </td>
            <td align="center" class="auto-style6">
                                        <dx:ASPxButton ID="btCancelar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                                            CssPostfix="PlasticBlue" 
                                            SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Text="Cancelar" 
                                            Width="144px" onclick="btCancelar_Click" style="margin-left: 0px" Theme="iOS" ToolTip="Cancelar">
                                        </dx:ASPxButton>
                                    </td>
            <td class="auto-style8">&nbsp;</td>
            <td class="auto-style10">&nbsp;</td>
        </tr>
    </table>
    </asp:Content>

