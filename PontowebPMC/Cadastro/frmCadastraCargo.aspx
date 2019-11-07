﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCadastraCargo.aspx.cs" Inherits="Cadastro_frmCadastraCargo" %>

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
.dxeTextBox_PlasticBlue, .dxeMemo_PlasticBlue
{
    background-color: White;
    border: solid 1px #B8B8B8;
}
.dxeTextBox_PlasticBlue .dxeEditArea_PlasticBlue, .dxeMemo_PlasticBlue .dxeMemoEditArea_PlasticBlue {
    background-color: White;
}
.dxeEditArea_PlasticBlue 
{
	font-family: Tahoma;
	font-size: 9pt;
	border: 1px solid #A0A0A0;
}

        .style2
        {
            width: 73px;
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
        .style4
        {
            width: 81px;
        }

        .auto-style3
        {
            width: 2%;
            height: 25px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="auto-style3">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">MANUTENÇÃO DE CARGOS</div> 

            </td>
        </tr>
        <tr>
            <td><strong style="font-size: 16px; font-weight: normal;">
                <img alt="" class="auto-style3" src="../Images/Imagem13.png" /> Listar cargos&nbsp; </strong></td>
        </tr>
        <tr>
            <td align="center">    <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="1" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
        CssPostfix="DevEx" Width="500px" 
        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
        onactivetabchanged="ASPxPageControl2_ActiveTabChanged">
        <TabPages>
            <dx:TabPage Text="Cadastro">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <table class="style1">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientIDMode="AutoID" 
                                        Text="Cargo:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="tbDescricaoCargo" runat="server" 
                                        Width="300px" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" 
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ErrorMessage="*" ControlToValidate="tbDescricaoCargo"></asp:RequiredFieldValidator>
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
                                                <dx:ASPxButton ID="btLista" runat="server" 
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                    OnClick="btLista_Click" 
                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Lista" 
                                                    Width="100px" CausesValidation="False" Theme="iOS" ToolTip="Lista">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btVoltarCad" runat="server" 
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                    OnClick="btVoltarCad_Click" 
                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                                                    Width="100px" CausesValidation="False" Theme="iOS" ToolTip="Voltar">
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
            <dx:TabPage Text="Lista">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <table class="style1">
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="gridCargo" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" DataMember="TBCargo" KeyFieldName="IDCargo" 
                                        Width="700px">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Identificação" FieldName="IDCargo" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Descrição" FieldName="DSCargo" 
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <SettingsPager PageSize="20">
                                            <AllButton Text="All">
                                            </AllButton>
                                            <NextPageButton Text="Próx. &gt;">
                                            </NextPageButton>
                                            <PrevPageButton Text="&lt; Ant">
                                            </PrevPageButton>
                                            <Summary Text="Página {0} de {1} ({2} itens)" />
                                        </SettingsPager>
                                        <Settings ShowFilterRow="True" ShowVerticalScrollBar="True" />
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
                                </td>
                            </tr>
                        </table>
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
