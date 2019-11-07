<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmSituacaousuario.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmSituacaousuario" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">


.dxeButtonEditButton_DevEx,
.dxeSpinLargeIncButton_DevEx,
.dxeSpinLargeDecButton_DevEx,
.dxeSpinIncButton_DevEx,
.dxeSpinDecButton_DevEx
{
	background: White none;
}

.dxeButtonEditButton_DevEx
{
	border-top-width: 0;
	border-right-width: 0;
	border-bottom-width: 0;
	border-left-width: 1px;
}
.dxeButtonEditButton_DevEx,
.dxeButtonEdit_DevEx .dxeSBC
{
	border-style: solid;
	border-color: Transparent;
	-border-color: White;
}

.dxeButtonEditButton_DevEx,
.dxeCalendarButton_DevEx,
.dxeSpinIncButton_DevEx,
.dxeSpinDecButton_DevEx,
.dxeSpinLargeIncButton_DevEx,
.dxeSpinLargeDecButton_DevEx
{
	vertical-align: middle;
	cursor: pointer;
}
        .style2
        {
            width: 72px;
        }
        .style3
        {
            width: 99px;
        }
        .style4
        {
            width: 4%;
        }
        .style5
        {
            width: 104px;
        }
        .auto-style3
        {
            width: 24px;
            height: 23px;
        }
        .auto-style4
        {
            width: 35%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DE SITUAÇÃO CADASTRAL DOS USUÁRIOS</div> 

            </td>
        </tr>
        <tr>
            <td colspan="2">
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td>
                <img alt="" class="auto-style3" src="../../Images/RImagem51.png" /> Situação Cadastral dos usuários</td>
        </tr>
    </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="auto-style4" align="right">
                Setor:</td>
            <td class="auto-style3" align="left">
                                    <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                    TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbSetorSituacaoUser" ID="cbSetorFolha">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
<ClientSideEvents SelectedIndexChanged="function(s, e) {
}"></ClientSideEvents>

<LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

<LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

<ValidationSettings ValidationGroup="ValidaGrupo">
</ValidationSettings>
</dx:ASPxComboBox>


            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">Para listar o órgão deixe o campo setor em branco.</td>
        </tr>
        <tr>
            <td align="right">
                        <dx:ASPxButton ID="btrelasituacao" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok" 
                            Width="100px" 
                            ValidationGroup="ValidaGrupo" AutoPostBack="False" 
                            ClientInstanceName="btrelasituacao" onclick="btrelasituacao_Click" Theme="iOS" ToolTip="Ok">
                        </dx:ASPxButton>
                    </td>
            <td align="left">
                        <dx:ASPxButton ID="btVoltar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                            CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                            Width="100px" onclick="btVoltar_Click" Theme="iOS" ToolTip="Voltar">
                        </dx:ASPxButton>
                    </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="style5">
                        &nbsp;</td>
            <td>
                        &nbsp;</td>
        </tr>
    </table>
                        </td>
        </tr>
    </table>
    <script type="text/javascript">
        function MudaPagina() {
            var Ano, hoje;

            hoje = new Date();


            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=' + '&Setor=' + coSituacaoCadastral.Get("IDSetor") + '&Rel=frmsitu')
        }

        function PreencheCO() {
            cbco.PerformCallback();
        }
    </script>
                        <dx:ASPxHiddenField ID="coSituacaoCadastral" runat="server" 
                            ClientInstanceName="coSituacaoCadastral">
                        </dx:ASPxHiddenField>
                    </asp:Content>

