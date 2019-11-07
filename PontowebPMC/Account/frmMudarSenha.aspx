<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmMudarSenha.aspx.cs" Inherits="Account_frmMudarSenha" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style4
        {
            width: 93px;
        }
        .style5
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="style1">
        <tr>
            <td class="style5">
                <strong>MUDAR SENHA</strong></td>
        </tr>
        <tr>
            <td>
                Em seu primeiro acesso a troca da senha é obrigatória</td>
        </tr>
        <tr>
            <td>
                <table class="style1">
                    <tr>
                        <td>
                            ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientIDMode="AutoID" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                ForeColor="Red" Text="Senha Antiga Inválida. Tente novamente" Visible="False">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Senha Antiga:</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxTextBox ID="tbSenhaAntiga" runat="server" ClientIDMode="AutoID" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                Password="True" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                                Width="170px">
                                <ValidationSettings CausesValidation="True" ErrorText="">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField ErrorText="" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Senha Nova:</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxTextBox ID="tbSenhaNova" runat="server" ClientIDMode="AutoID" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                Password="True" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                                Width="170px">
                                <ValidationSettings CausesValidation="True" ErrorText="">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField ErrorText="" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Confirma Nova Senha:</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxTextBox ID="tbConfirmaSenhaNova" runat="server" ClientIDMode="AutoID" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                Password="True" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                                Width="170px">
                                <ValidationSettings CausesValidation="True" ErrorText="">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField ErrorText="" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table class="style1">
                                <tr>
                                    <td class="style4">
                                        <dx:ASPxButton ID="btConfirmar" runat="server" 
                                            CssFilePath="~/App_Themes/Youthful/{0}/styles.css" CssPostfix="Youthful" 
                                            onclick="btConfirmar_Click" 
                                            SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" Text="Mudar Senha" 
                                            Width="100px">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btCancelar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/Youthful/{0}/styles.css" 
                                            CssPostfix="Youthful" 
                                            SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" Text="Cancelar" 
                                            Width="100px" onclick="btCancelar_Click">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

