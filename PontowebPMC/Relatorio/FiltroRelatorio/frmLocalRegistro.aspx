<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmLocalRegistro.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmLocalRegistro" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

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

        .dxeButtonEditButton_DevEx
{
	vertical-align: middle;
	cursor: pointer;
}
        

        .dxeButtonEditButton_DevEx
{
	border-style: solid;
	border-color: Transparent;
	-border-color: White;
}

        .dxeButtonEditButton_DevEx
{
	border-top-width: 0;
	border-right-width: 0;
	border-bottom-width: 0;
	border-left-width: 1px;
}


        .dxeButtonEditButton_DevEx
{
	background: White none;
}

        .dxeButtonEditButton_DevEx
{
	vertical-align: middle;
	cursor: pointer;
}


.dxeButtonEditButton_DevEx
{
	border-style: solid;
	border-color: Transparent;
	-border-color: White;
}

.dxeButtonEditButton_DevEx
{
	border-top-width: 0;
	border-right-width: 0;
	border-bottom-width: 0;
	border-left-width: 1px;
}


.dxeButtonEditButton_DevEx
{
	background: White none;
}

        .style1
        {
            font-size: medium;
        }
        .style2
        {
            width: 103px;
        }
        .style3
        {
            width: 173px;
        }
        .style4
        {
            width: 163px;
        }
        .style5
        {
            width: 9%;
        }
        .auto-style3
        {
            font-size: medium;
            width: 550px;
        }
        .auto-style4
        {
            width: 433px;
        }
        .auto-style5
        {
            font-size: medium;
            width: 667px;
        }
        .auto-style6
        {
            width: 26px;
            height: 25px;
        }
        .auto-style7
        {
            font-size: medium;
            width: 667px;
            height: 32px;
        }
        .auto-style8
        {
            font-size: medium;
            width: 550px;
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DE REGISTRO DE PONTO E LOCAL DE MARCAÇÃO</div> 

            </td>
        </tr>
        <tr>
            <td align="left">
                <img alt="" class="auto-style6" src="../../Images/Imagem21.png" />Local de marcação de ponto frequência</td>
        </tr>
        <tr>
            <td align="center">&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="auto-style4" align="right">
                Data Inicial:</td>
            <td align="left">
                            <dx:ASPxDateEdit ID="deDataRelacao" runat="server" ClientInstanceName="deLocalRegistroInicial" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                Date="01/12/2013 14:38:46" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Width="400px">
                                <CalendarProperties ClearButtonText="Apagar" TodayButtonText="Hoje">
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ClientSideEvents DateChanged="function(s, e) {
}" />
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                                <ValidationSettings ValidationGroup="ValidaGrupo">
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                            </td>
        </tr>
        <tr>
            <td class="auto-style4" align="right">
                Data Final:</td>
            <td align="left">
                            <dx:ASPxDateEdit ID="deDataRelacao0" runat="server" ClientInstanceName="deLocalRegistroFinal" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                Date="01/12/2013 14:38:46" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Width="400px">
                                <CalendarProperties ClearButtonText="Apagar" TodayButtonText="Hoje">
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ClientSideEvents DateChanged="function(s, e) {
}" />
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                                <ValidationSettings ValidationGroup="ValidaGrupo">
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                            </td>
        </tr>
        <tr>
            <td class="auto-style4" align="right">
                Setor:</td>
            <td align="left">
                                    <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbSetorLocalRegistro" ID="cbSetorLocalRegistro">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuarioLocalRegistro();
}" />
<LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

<LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

<ValidationSettings ValidationGroup="ValidaGrupo">
<RequiredField IsRequired="True"></RequiredField>
</ValidationSettings>
</dx:ASPxComboBox>


            </td>
        </tr>
        <tr>
            <td class="auto-style4" align="right">
                Servidor:</td>
            <td align="left">
                                    <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="Nome" ValueField="IDUsuario" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="vwNomeUsuario" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbUsuarioLocalRegistro45" ID="cbUsuarioBancoHora" 
                                        oncallback="cbUsuarioBancoHora_Callback" DropDownStyle="DropDown">
<LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

<LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

</dx:ASPxComboBox>


            </td>
        </tr>
        <tr>
            <td class="auto-style4" align="right">
                Filtrar por registro manual:</td>
            <td align="left">
                                    <dx:ASPxCheckBox ID="cbRegistroManualLocalRegistro" runat="server" 
                                        ClientInstanceName="cbRegistroManualLocalRegistro" Theme="DevEx">
                                    </dx:ASPxCheckBox>


            </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function MudaPagina() {
            var IDUsuario = null;

            if (cbUsuarioLocalRegistro45.GetText != '')
                IDUsuario = cbUsuarioLocalRegistro45.GetValue();
            window.open('/Relatorio/frmVizualizaRelatorio.aspx?DataIni=' + deLocalRegistroInicial.GetText() + '&DataFim=' + deLocalRegistroFinal.GetText() + '&Setor=' + coLocalMarcacao.Get("IDSetor") + '&User=' + coLocalMarcacao.Get("IDUsuario") + '&Ano=0' + '&Rel=frmLocalRegistro' + '&FiltroManual=' + cbRegistroManualLocalRegistro.GetChecked());
        }

        function PreenchecbUsuarioLocalRegistro() {
            cbUsuarioLocalRegistro45.PerformCallback(cbSetorLocalRegistro.GetValue());
        }

    </script>
    <table class="dxflInternalEditorTable_DevEx" style="width: 90%; margin-top: 0px">
        <tr>
            <td class="auto-style7" align="right">
                </td>
            <td class="auto-style8" align="left">
                </td>
        </tr>
        <tr>
            <td class="auto-style5" align="right">
                <dx:ASPxButton ID="btrelaBancohora" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok" 
                            Width="100px" 
                            ValidationGroup="ValidaGrupo" AutoPostBack="False" 
                            ClientInstanceName="btLocalRegistro" 
                    onclick="btrelaBancohora_Click" Theme="iOS" ToolTip="Ok">
                    <ClientSideEvents Click="function(s, e) {
}" />
                </dx:ASPxButton>
            </td>
            <td class="auto-style3" align="left">
                <dx:ASPxButton ID="btVoltar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                            CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                            Width="100px" onclick="btVoltar_Click" Theme="iOS" ToolTip="Voltar">
                </dx:ASPxButton>
                </td>
        </tr>
    </table>
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td>
                <dx:ASPxHiddenField ID="coLocalMarcacao" runat="server" 
                    ClientInstanceName="coLocalMarcacao">
                </dx:ASPxHiddenField>
            </td>
        </tr>
    </table>
</asp:Content>

