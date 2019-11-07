<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCadastraFeriadoPontoFacultativo.aspx.cs" Inherits="Cadastro_frmCadastraFeriadoPontoFacultativo" %>

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
            width: 73px;
        }
        .style4
        {
            width: 81px;
        }

    .dxbButton_PlasticBlue 
{	
  	color: #000000;    
	font-weight:normal;  	
	font-size: 9pt;
	font-family: Tahoma;				    
	vertical-align: middle;	 		
	border: solid 1px #B8B8B8;
    padding: 1px 1px 1px 1px;
	cursor: pointer;
}
        .style5
        {
            width: 604px;
        }
        .auto-style3
        {
            height: 24px;
            width: 18px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">FERIADOS E PONTOS FACULTATIVOS</div> 

            </td>
        </tr>
        <tr>
            <td>
                 <strong style="font-size: 16px; font-weight: normal;"><img alt="" class="auto-style3" src="../Images/Imagem41.png" /> Lançar f<span style="font-size:Small;cursor:default;">eriados e pontos facultativos</span></strong></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="center">    <dx:ASPxPageControl ID="pgFeriadoPontoFacultativo" runat="server" 
    ActiveTabIndex="0" 
    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
    Width="600px" onactivetabchanged="pgFeriadoPontoFacultativo_ActiveTabChanged" 
        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
    <TabPages>
        <dx:TabPage Text="Cadastrar">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <table class="style1">
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientIDMode="AutoID" 
                                    Text="Descrição:">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="tbDescricaoFeriado" runat="server" 
                                    Width="400px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" 
                                    CssPostfix="SoftOrange" 
                                    SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css">
                                    <ValidationSettings>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="tbDescricaoFeriado" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" ClientIDMode="AutoID" Text="Data:">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="deFeriadoPontoFacultativo" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                    CssPostfix="DevEx" 
                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="200px" 
                                    Spacing="0">
                                    <CalendarProperties ClearButtonText="Apagar" TodayButtonText="Hoje">
                                        <HeaderStyle Spacing="1px" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="deFeriadoPontoFacultativo" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="style1">
                                    <tr>
                                        <td class="style4">
                                            <dx:ASPxButton ID="btSalvar" runat="server" 
                                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                OnClick="btSalvar_Click" 
                                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar" 
                                                Width="100px" Theme="iOS" ToolTip="Salvar">
                                            </dx:ASPxButton>
                                        </td>
                                        <td class="style2">
                                            <dx:ASPxButton ID="btLista" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                                CssPostfix="DevEx" OnClick="btLista_Click" 
                                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Lista" 
                                                Width="100px" Theme="iOS" ToolTip="Lista">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btVoltarCad" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                                CssPostfix="DevEx" OnClick="btVoltarCad_Click" 
                                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                                                Width="100px" Theme="iOS" ToolTip="Voltar">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Listar">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <table class="style5">
                        <tr>
                            <td>
                                <dx:ASPxCheckBox ID="cbFeriados" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="cbFeriados" 
                                    Text="Acrescentar Feriados e Pontos facultativos para todos os órgãos" 
                                    Theme="DevEx">
                                </dx:ASPxCheckBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btOkFeriado" runat="server" ClientInstanceName="btOkFeriado" 
                                    OnClick="btOkFeriado_Click" Text="Ok" Theme="iOS" Width="100px">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <dx:ASPxGridView ID="gridFeriadoPontoFacultativo" runat="server" 
                        AutoGenerateColumns="False" 
                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                        DataMember="TBDiasAno" KeyFieldName="IDDiasAno" 
                        Width="600px">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="IDDiasAno" 
                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Descrição" 
                                FieldName="OBS" ShowInCustomizationForm="True" 
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="DTDiasAno" 
                                ShowInCustomizationForm="True" VisibleIndex="2" Caption="Data">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <SettingsPager NumericButtonCount="20" PageSize="20">
                            <AllButton Text="All">
                            </AllButton>
                            <NextPageButton Text="Próx. &gt;">
                            </NextPageButton>
                            <PrevPageButton Text="&lt; Ant.">
                            </PrevPageButton>
                            <Summary Text="Página {0} de {1} ({2} itens)" />
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowVerticalScrollBar="True" />
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
                            <td class="style2">
                                <dx:ASPxButton ID="btCadastrar" runat="server" 
                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                    OnClick="btCadastrar_Click" 
                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cadastrar" 
                                    Width="100px" Theme="iOS" ToolTip="Cadastrar">
                                </dx:ASPxButton>
                            </td>
                            <td class="style2">
                                <dx:ASPxButton ID="btAlterar" runat="server" 
                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                    OnClick="btAlterar_Click" 
                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Alterar" 
                                    Width="100px" Theme="iOS" ToolTip="Alterar">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btVoltar" runat="server" 
                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                    OnClick="btVoltar_Click" 
                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                                    Width="100px" Theme="iOS" ToolTip="Voltar">
                                </dx:ASPxButton>
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

