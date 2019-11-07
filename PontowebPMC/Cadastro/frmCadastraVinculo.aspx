﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCadastraVinculo.aspx.cs" Inherits="Cadastro_frmCadastraVinculo" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

.dxeTextBox_PlasticBlue
{
    background-color: White;
    border: solid 1px #B8B8B8;
}
.dxeTextBox_PlasticBlue .dxeEditArea_PlasticBlue {
    background-color: White;
}
.dxeEditArea_PlasticBlue 
{
	font-family: Tahoma;
	font-size: 9pt;
	border: 1px solid #A0A0A0;
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
        .dxpControl_PlasticBlue
{
	/*padding: 5px 2px 5px 2px;*/
	padding: 0px;	
}
.dxpControl_PlasticBlue
{
	font: 9pt Tahoma;
	color: #5A83D0;
}
.dxpSummary_PlasticBlue
{
	font: 9pt Tahoma;
	color: #909090;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
	padding: 0px 4px 0px 4px;
}
.dxpDisabled_PlasticBlue
{
	color: #B8B8B8;
	border-color: #B8B8B8;
	cursor: default;
}

.dxpDisabledButton_PlasticBlue
{
	font: 9pt Tahoma;
	color: #5A83D0;
	text-decoration: none;
}
.dxpButton_PlasticBlue
{
	font: 9pt Tahoma;
	color: #5A83D0;
	text-decoration: none;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
}
.dxpCurrentPageNumber_PlasticBlue
{
	font: 9pt Tahoma;
	color: #FFFFFF;
	background-color: #5066AC;
	font-weight: normal;
	text-decoration: none;
	padding: 2px 3px 3px 3px;
}
.dxpPageNumber_PlasticBlue
{
	font: 9pt Tahoma;
	color: #5A83D0;
	text-decoration: none;
	text-align: center;
	vertical-align: middle;
	padding: 0px 5px 0px 5px;
}
    .style1
    {
        width: 100%;
    }

        .style2
        {
            width: 73px;
            height: 27px;
        }
        .style4
        {
            width: 81px;
            height: 27px;
        }

        .style5
        {
            height: 27px;
        }

        .auto-style3
        {
            width: 2%;
            height: 22px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="auto-style3">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">MANUTENÇÃO DE VÍNCULOS</div> 

            </td>
        </tr>
        <tr>
            <td><strong style="font-size: 16px; font-weight: normal;">
                <img alt="" class="auto-style3" src="../Images/Imagem13.png" /> Listar vinculos&nbsp; </strong></td>
        </tr>
        <tr>
            <td align="center">    <dx:ASPxPageControl ID="pgVinculo" runat="server" ActiveTabIndex="1" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
    CssPostfix="DevEx" Width="500px" 
        onactivetabchanged="pgVinculo_ActiveTabChanged" 
        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
    <tabpages>
        <dx:TabPage Text="Casdastrar">
            <contentcollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <table class="style1">
                        <tr>
                            <td>
                                <table class="style1">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientIDMode="AutoID" 
                                                Text="Tipo de Vínculo:">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="tbDescricaoCargo" runat="server" 
                                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" 
                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Width="300px">
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="tbDescricaoCargo" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                                                    <td class="style5">
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
                            </td>
                        </tr>
                    </table>
                </dx:ContentControl>
            </contentcollection>
        </dx:TabPage>
        <dx:TabPage Text="Listar">
            <contentcollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <table class="style1">
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="gridEntidade" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                    CssPostfix="DevEx" DataMember="TBEntidade" KeyFieldName="IDEntidade" 
                                    Width="500px">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="IDEntidade" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Descrição" FieldName="DSEntidade" 
                                            ShowInCustomizationForm="True" VisibleIndex="2">
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
                            </td>
                        </tr>
                    </table>
                </dx:ContentControl>
            </contentcollection>
        </dx:TabPage>
    </tabpages>
    <loadingpanelimage url="~/App_Themes/DevEx/Web/Loading.gif">
    </loadingpanelimage>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
    <paddings PaddingLeft="5px" PaddingRight="5px" Padding="2px" />
    <contentstyle>
        <Paddings Padding="12px" />
        <border BorderWidth="1px" BorderColor="#9DA0AA" BorderStyle="Solid"></border>
    </contentstyle>
</dx:ASPxPageControl>
            </td>
        </tr>
    </table>
</asp:Content>
