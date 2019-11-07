<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCadastraSetor.aspx.cs" Inherits="Cadastro_frmCadastraSetor" %>

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
        .style3
        {
            width: 96px;
        }
        .style4
        {
            width: 84px;
        }
        .auto-style5
        {
            width: 482px;
        }
        .style5
        {
            width: 100%;
        }
        .auto-style3
        {
            height: 24px;
            width: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">CADASTRO DE SETOR</div> 

            </td>
        </tr>
        <tr>
            <td align="left" class="auto-style5">
                 <strong style="font-size: 16px; font-weight: normal;"><img alt="" class="auto-style3" src="../Images/Imagem43.png" /> Cadastro de setor </strong></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
    CssPostfix="DevEx" Width="500px" 
        onactivetabchanged="ASPxPageControl1_ActiveTabChanged1" 
        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
    <TabPages>
        <dx:TabPage Text="Cadastro">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <table class="style1">
                        <tr>
                            <td width="30">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientIDMode="AutoID" 
                                    Text="Descrição:">
                                </dx:ASPxLabel>
                            </td>
                            <td colspan="2">
                                <dx:ASPxTextBox ID="tbDescricaoSetor" runat="server" 
                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="tbDescricaoSetor" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="30">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" ClientIDMode="AutoID" 
                                    Text="SIGLA:">
                                </dx:ASPxLabel>
                            </td>
                            <td colspan="2">
                                <dx:ASPxTextBox ID="tbSiglaSetor" runat="server" 
                                    Width="400px" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                    CssPostfix="DevEx" 
                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="tbSiglaSetor" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="30">
                                Situação:</td>
                            <td colspan="2">
                                <dx:ASPxComboBox ID="cbStatus" runat="server" 
                                    ClientInstanceName="cbStatusMotivoFalta" 
                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                    DataMember="TBStatus" EnableIncrementalFiltering="True" 
                                    IncrementalFilteringMode="StartsWith" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSStatus" 
                                    ValueField="IDStatus" Width="200px">
                                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ValidationSettings ValidationGroup="ValidaSetor">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td width="100">
                                &nbsp;</td>
                            <td width="300">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btSalvar" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btSalvar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar" Theme="iOS" ToolTip="Salvar" ValidationGroup="ValidaSetor" Width="100px">
                                </dx:ASPxButton>
                            </td>
                            <td width="100">
                                <dx:ASPxButton ID="btLista" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btLista_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Lista" Theme="iOS" ToolTip="Lista" Width="100px">
                                </dx:ASPxButton>
                            </td>
                            <td width="300">
                                <dx:ASPxButton ID="btVoltarCad" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btVoltarCad_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" Theme="iOS" ToolTip="Voltar" Width="100px">
                                </dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
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
                            <td style="margin-left: 40px">
                                <dx:ASPxGridView ID="gridSetor" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                    CssPostfix="DevEx" DataMember="TBSetor" Width="500px" 
                                    KeyFieldName="IDSetor">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Setor" FieldName="DSSetor" 
                                            ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Sigla" FieldName="Sigla" 
                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Identificacao" FieldName="IDSetor" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Status" FieldName="IDStatus" 
                                            ShowInCustomizationForm="True" VisibleIndex="2" Width="20px">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" SortMode="Value" />
                                    <SettingsPager PageSize="20">
                                        <AllButton Text="All">
                                        </AllButton>
                                        <NextPageButton Text="Próx. &gt;">
                                        </NextPageButton>
                                        <PrevPageButton Text="&lt;Ant.">
                                        </PrevPageButton>
                                        <Summary AllPagesText="Page: {0} - {1} ({2} items)" 
                                            Text="Página {0} of {1} ({2} items)" />
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" />
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
                                <table class="style1">
                                    <tr>
                                        <td class="style3" style="text-align: right">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style3" style="text-align: right">
                                            <dx:ASPxButton ID="btCadastrar" runat="server" AutoPostBack="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btCadastrar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cadastrar" Theme="iOS" ToolTip="Cadastrar" Width="100px">
                                            </dx:ASPxButton>
                                        </td>
                                        <td class="style4">
                                            <dx:ASPxButton ID="btAlterar2" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btAlterar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Alterar" Theme="iOS" ToolTip="Alterar" Width="100px">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btVoltar0" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btVoltar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" Theme="iOS" ToolTip="Voltar" Width="100px">
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

