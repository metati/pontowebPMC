﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmInformacao.aspx.cs" Inherits="frmInformacao" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style4
        {
            font-size: small;
        }
        .style5
        {
            width: 87px;
        }
        .style6
        {
            width: 80px;
        }
    .style7
    {
        width: 99px;
    }
    .style8
    {
        width: 102px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="style1">
        <tr>
            <td class="style4">
                <strong>Informações Diárias</strong></td>
        </tr>
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" CssFilePath="~/App_Themes/Youthful/{0}/styles.css" 
                    CssPostfix="Youthful" 
                    Width="500px" SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" 
                    LoadingPanelImagePosition="Bottom">
                    <TabPages>
                        <dx:TabPage Text="Cadastrar">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <table class="style1">
                                                    <tr>
                                                        <td valign="top">
                                                            Informação:</td>
                                                        <td>
                                                            <dx:ASPxMemo ID="ASPxMemo1" runat="server" 
                                                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" 
                                                                Height="71px" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" 
                                                                Width="400px">
                                                                <ValidationSettings>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </dx:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style7">
                                                            <dx:ASPxButton ID="btLista" runat="server" 
                                                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" 
                                                                OnClick="btLista_Click1" 
                                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Text="Listar" 
                                                                Width="100px">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td class="style8">
                                                            <dx:ASPxButton ID="btSalvar" runat="server" 
                                                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" 
                                                                OnClick="btSalvar_Click1" 
                                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Text="Salvar" 
                                                                Width="100px">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btVoltarCad" runat="server" 
                                                                OnClick="btVoltarCad_Click" Text="Voltar" Width="100px" 
                                                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" 
                                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Listar">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="gridInformacaoDiaria" runat="server" 
                                                    AutoGenerateColumns="False" 
                                                    CssFilePath="~/App_Themes/Youthful/{0}/styles.css" CssPostfix="Youthful" 
                                                    DataMember="TBInformacaoDiaria" KeyFieldName="TBInformacaoDiaria" 
                                                    Width="500px">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TBInformacaoDiaria" 
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Conteúdo" FieldName="DSInformacaoDiaria" 
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Data" FieldName="DTInformacaoDiaria" 
                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPager>
                                                        <AllButton Text="All">
                                                        </AllButton>
                                                        <NextPageButton Text="Próximo &gt;">
                                                        </NextPageButton>
                                                        <PrevPageButton Text="&lt; Anterior">
                                                        </PrevPageButton>
                                                        <Summary Text="Página {0} de {1} ({2} itens)" />
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" />
                                                    <SettingsLoadingPanel Text="" />
                                                    <Images SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css">
                                                        <LoadingPanelOnStatusBar Url="~/App_Themes/Youthful/GridView/gvLoadingOnStatusBar.gif">
                                                        </LoadingPanelOnStatusBar>
                                                        <LoadingPanel Url="~/App_Themes/Youthful/GridView/Loading.gif">
                                                        </LoadingPanel>
                                                    </Images>
                                                    <ImagesFilterControl>
                                                        <LoadingPanel Url="~/App_Themes/Youthful/Editors/Loading.gif">
                                                        </LoadingPanel>
                                                    </ImagesFilterControl>
                                                    <Styles CssFilePath="~/App_Themes/Youthful/{0}/styles.css" 
                                                        CssPostfix="Youthful">
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                        <LoadingPanel ImageSpacing="10px">
                                                        </LoadingPanel>
                                                    </Styles>
                                                    <StylesEditors>
                                                        <CalendarHeader Spacing="8px">
                                                        </CalendarHeader>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style5">
                                                            <dx:ASPxButton ID="btCadastrar" runat="server" 
                                                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" 
                                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Text="Cadastrar" 
                                                                Width="100px" OnClick="btCadastrar_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td class="style6">
                                                            <dx:ASPxButton ID="btAlterar" runat="server" 
                                                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" 
                                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Text="Alterar" 
                                                                Width="100px" OnClick="btAlterar_Click1">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btVoltar" runat="server" 
                                                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" 
                                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Text="Voltar" 
                                                                Width="100px" OnClick="btVoltar_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
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
    </table>
</asp:Content>
