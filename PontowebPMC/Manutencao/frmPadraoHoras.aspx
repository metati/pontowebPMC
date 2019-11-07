<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmPadraoHoras.aspx.cs" Inherits="Manutencao_frmPadraoHoras" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
    .style2
    {
        width: 140px;
    }
        .style4
    {
        width: 100%;
    }
    .style5
    {
        width: 81px;
    }
        .style11
        {
            width: 372px;
        }
        .style12
        {
            width: 413px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="style1">
    <tr>
        <td colspan="3">
            Definição de Padrões de Horas:</td>
    </tr>
    <tr>
        <td class="style2">
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True" 
                onselectedindexchanged="CheckBoxList1_SelectedIndexChanged">
                <asp:ListItem Selected="True">Definir por Usuário</asp:ListItem>
            </asp:CheckBoxList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<table class="style1">
    <tr>
        <td>
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" CssFilePath="~/App_Themes/Youthful/{0}/styles.css" 
                CssPostfix="Youthful" Width="600px" 
                SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" 
                LoadingPanelImagePosition="Bottom">
                <TabPages>
                    <dx:TabPage Text="Definir por Setor">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <table class="style4">
                                    <tr>
                                        <td>
                                            Setor:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlSetorLista" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" CausesValidation="True" DataMember="TBSetor" 
                                                DataTextField="DSSetor" DataValueField="IDSetor" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                ControlToValidate="ddlSetorLista" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table class="style1">
                                    <tr>
                                        <td class="style12">
                                            Entrada da Manhã:</td>
                                        <td width="600">
                                            <dx:ASPxTextBox ID="tbEntradaManhaSetor" runat="server" ClientIDMode="AutoID" 
                                                Width="100px">
                                                <MaskSettings Mask="HH:mm" />
                                                <ValidationSettings ErrorText="Valor Inválido">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="style11">
                                            Saída da Manhã:</td>
                                        <td style="text-align: right" width="600">
                                            <dx:ASPxTextBox ID="tbSaidaManhaSetor" runat="server" ClientIDMode="AutoID" 
                                                Width="100px">
                                                <MaskSettings Mask="HH:mm" />
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style12">
                                            Entrada da Tarde:</td>
                                        <td width="600">
                                            <dx:ASPxTextBox ID="tbEntradaTardeSetor" runat="server" ClientIDMode="AutoID" 
                                                Width="100px">
                                                <MaskSettings Mask="HH:mm" />
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="style11">
                                            Saída da Tarde:</td>
                                        <td style="text-align: right" width="600">
                                            <dx:ASPxTextBox ID="tbSaidaTardeSetor" runat="server" ClientIDMode="AutoID" 
                                                Width="100px">
                                                <MaskSettings Mask="HH:mm" />
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Definir por Usuário">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <table class="style1">
                                    <tr>
                                        <td>
                                            Setor:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlSetor" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" DataMember="TBSetor" 
                                                DataTextField="DSSetor" DataValueField="IDSetor" Width="300px" 
                                                OnSelectedIndexChanged="ddlSetor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="ddlSetor" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Usuário:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlUsuario" runat="server" AppendDataBoundItems="True" 
                                                CausesValidation="True" DataMember="vwNomeUsuario" 
                                                DataTextField="Nome" DataValueField="IDUsuario" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                ControlToValidate="ddlUsuario" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table class="style1">
                                    <tr>
                                        <td class="style12">
                                            Entrada da Manhã:</td>
                                        <td width="600">
                                            <dx:ASPxTextBox ID="tbEntradaManha" runat="server" 
                                                Width="100px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" 
                                                CssPostfix="SoftOrange" 
                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css">
                                                <MaskSettings Mask="HH:mm" />
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="style11">
                                            Saída da Manhã:</td>
                                        <td style="text-align: right" width="600">
                                            <dx:ASPxTextBox ID="tbSaidaManha" runat="server" ClientIDMode="AutoID" 
                                                Width="100px">
                                                <MaskSettings Mask="HH:mm" />
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style12">
                                            Entrada da Tarde:</td>
                                        <td width="600">
                                            <dx:ASPxTextBox ID="tbEntradaTarde" runat="server" ClientIDMode="AutoID" 
                                                Width="100px">
                                                <MaskSettings Mask="HH:mm" />
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="style11">
                                            Saída da Tarde:</td>
                                        <td style="text-align: right" width="600">
                                            <dx:ASPxTextBox ID="tbSaidaTarde" runat="server" ClientIDMode="AutoID" 
                                                Width="100px">
                                                <MaskSettings Mask="HH:mm" />
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <LoadingPanelImage Url="~/App_Themes/Youthful/Web/tcLoading.gif">
                </LoadingPanelImage>
                <LoadingPanelStyle ImageSpacing="3px">
                </LoadingPanelStyle>
                <Paddings PaddingLeft="5px" PaddingRight="5px" />
                <ActiveTabStyle>
                    <HoverStyle>
                        <BorderTop BorderColor="#F4921B" />
                    </HoverStyle>
                </ActiveTabStyle>
                <TabStyle>
                    <HoverStyle>
                        <BorderTop BorderColor="#F4BD17" />
                    </HoverStyle>
                </TabStyle>
                <ContentStyle>
                    <Border BorderColor="White" />
                </ContentStyle>
            </dx:ASPxPageControl>
        </td>
    </tr>
    <tr>
        <td>
            <table class="style4">
                <tr>
                    <td class="style5">
                        <dx:ASPxButton ID="btSalvar" runat="server" 
                            CssFilePath="~/App_Themes/Youthful/{0}/styles.css" CssPostfix="Youthful" 
                            SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" Text="Salvar" 
                            Width="100px" onclick="btSalvar_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btVoltar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/Youthful/{0}/styles.css" 
                            CssPostfix="Youthful" 
                            SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" Text="Voltar" 
                            Width="100px" onclick="btVoltar_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
&nbsp;&nbsp;&nbsp; 
</asp:Content>

