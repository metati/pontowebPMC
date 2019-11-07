<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmLancarFerias.aspx.cs" Inherits="Cadastro_frmLancarFerias" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            font-size: medium;
        }
        .style3
        {
            width: 69px;
        }
        .style4
        {
            width: 63px;
        }
        .style5
        {
            width: 87px;
        }
        .style6
        {
            width: 77px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="style1">
        <tr>
            <td class="style2">
                <strong>Lançar Férias</strong></td>
        </tr>
        <tr>
            <td>
                <dx:ASPxPageControl ID="tbFerias" runat="server" ActiveTabIndex="1" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" Width="600px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                    <TabPages>
                        <dx:TabPage Text="Lançar">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientIDMode="AutoID" 
                                                    Text="Setor:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSetor" runat="server" Width="300px" 
                                                    DataTextField="DSSetor" DataValueField="IDSetor" 
                                                    AppendDataBoundItems="True" AutoPostBack="True" DataMember="TBSetor" 
                                                    OnSelectedIndexChanged="ddlSetor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ControlToValidate="ddlSetor" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" ClientIDMode="AutoID" 
                                                    Text="Usuário:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUsuario" runat="server" Width="300px" 
                                                    AppendDataBoundItems="True" DataMember="vwNomeUsuario" 
                                                    DataTextField="Nome" DataValueField="IDUsuario">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                    ControlToValidate="ddlUsuario" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" ClientIDMode="AutoID" 
                                                    Text="Início das Férias:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="deInicioFerias" runat="server" 
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px" 
                                                    Spacing="0">
                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                        <HeaderStyle Spacing="1px" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                    ControlToValidate="deInicioFerias" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" ClientIDMode="AutoID" 
                                                    Text="Fim das Férias:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="deFimFerias" runat="server" 
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px" 
                                                    Spacing="0">
                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                        <HeaderStyle Spacing="1px" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                    ControlToValidate="deFimFerias" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="style1">
                                        <tr>
                                            <td class="style5">
                                                <dx:ASPxButton ID="btSalvar" runat="server" 
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar" 
                                                    Width="100px" OnClick="btSalvar_Click">
                                                </dx:ASPxButton>
                                            </td>
                                            <td class="style6">
                                                <dx:ASPxButton ID="btListar" runat="server" 
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Listar" 
                                                    Width="100px" OnClick="btListar_Click" CausesValidation="False">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btVoltar" runat="server" 
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                                                    Width="100px" CausesValidation="False">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Lista">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <table class="style1">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" ClientIDMode="AutoID" 
                                                                Text="Setor:">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSetorLista" runat="server" AppendDataBoundItems="True" 
                                                                AutoPostBack="True" DataMember="TBSetor" DataTextField="DSSetor" 
                                                                DataValueField="IDSetor" 
                                                                OnSelectedIndexChanged="ddlSetorLista_SelectedIndexChanged" Width="300px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <dx:ASPxGridView ID="gridFerias" runat="server" 
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                    Width="600px" AutoGenerateColumns="False" DataMember="vwFerias" 
                                                    KeyFieldName="IDFerias">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="IDFerias" ShowInCustomizationForm="True" 
                                                            Visible="False" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nome" FieldName="Nome" 
                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Início" FieldName="DTInicioFerias1" 
                                                            ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Final" FieldName="DTFimFerias1" 
                                                            ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="IDSetor" ShowInCustomizationForm="True" 
                                                            Visible="False" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="IDUsuario" ShowInCustomizationForm="True" 
                                                            Visible="False" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPager>
                                                        <AllButton Text="All">
                                                        </AllButton>
                                                        <NextPageButton Text="Next &gt;">
                                                        </NextPageButton>
                                                        <PrevPageButton Text="&lt; Prev">
                                                        </PrevPageButton>
                                                        <Summary Text="Página {0} de {1} ({2} )" />
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" />
                                                    <SettingsText EmptyDataRow="Sem dados para exibir" />
                                                    <SettingsLoadingPanel Text="Processando&amp;hellip;" />
                                                    <Images SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                                                        <LoadingPanelOnStatusBar Url="~/App_Themes/DevEx/GridView/StatusBarLoading.gif">
                                                        </LoadingPanelOnStatusBar>
                                                        <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                                                        </LoadingPanel>
                                                    </Images>
                                                    <ImagesFilterControl>
                                                        <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                                                        </LoadingPanel>
                                                    </ImagesFilterControl>
                                                    <Styles CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                                        CssPostfix="DevEx">
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                        <AlternatingRow Enabled="True">
                                                        </AlternatingRow>
                                                        <LoadingPanel ImageSpacing="5px">
                                                        </LoadingPanel>
                                                    </Styles>
                                                    <StylesEditors ButtonEditCellSpacing="0">
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>
                                                <br />
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style3">
                                                            <dx:ASPxButton ID="btCadastrar" runat="server" 
                                                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                                                                Text="Ok" Width="100px" OnClick="btCadastrar_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td class="style4">
                                                            <dx:ASPxButton ID="btAlterar" runat="server" 
                                                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Alterar" 
                                                                Width="100px" OnClick="btAlterar_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btVoltarS" runat="server" 
                                                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                                                                Width="100px" OnClick="btVoltarS_Click">
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
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <Paddings PaddingLeft="5px" PaddingRight="5px" Padding="2px" />
                    <ContentStyle>
                        <Paddings Padding="12px" />
                        <Border BorderWidth="1px" BorderColor="#9DA0AA" BorderStyle="Solid" />
                    </ContentStyle>
                </dx:ASPxPageControl>
            </td>
        </tr>
    </table>
</asp:Content>

