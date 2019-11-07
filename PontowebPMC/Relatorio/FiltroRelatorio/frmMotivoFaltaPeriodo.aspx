<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmMotivoFaltaPeriodo.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmMotivoFaltaPeriodo" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-size: small;
        }

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

        .style3
        {
            width: 98px;
        }
        .style4
        {
            width: 78px;
        }
        .style5
        {
            width: 94px;
        }
        .auto-style3
        {
            width: 410px;
        }
        .auto-style4
        {
            width: 417px;
        }
        .auto-style5
        {
            width: 23px;
            height: 23px;
        }
        .auto-style6
        {
            height: 25px;
        }
        .auto-style7
        {
            width: 727px;
        }
        .auto-style8
        {
            width: 717px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
            <table class="dxflInternalEditorTable_MetropolisBlue">
                <tr>
                    <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DE RELAÇÃO DE JUSTIFICATIVAS POR PERÍODO</div> 

            </td>
                </tr>
            </table>
            <table class="dxflInternalEditorTable_MetropolisBlue">
                <tr>
                    <td colspan="2">
                        <img alt="" class="auto-style5" src="../../Images/RImagem52.png" /> Relação de justificativas por período</td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="dxflInternalEditorTable_MetropolisBlue">
                            <tr>
                                <td align="right" class="auto-style4">Setor:</td>
                                <td align="left">
                <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbSetorMotivoFalta" ID="cbSetorMotivoFalta">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuario();
}" />
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
}">
                    </ClientSideEvents>
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <RequiredField IsRequired="True">
                        </RequiredField>
                    </ValidationSettings>
                </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="auto-style4">Justificativa:</td>
                                <td align="left">
                <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="DSMotivoFalta" ValueField="IDMotivoFalta" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBMotivoFalta" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbMotivoFaltaRel" ID="cbMotivoFaltaRel" 
                    DropDownStyle="DropDown">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuario();
}" />
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
}">
                    </ClientSideEvents>
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="auto-style4">Data Inicial:</td>
                                <td align="left">
                            <dx:ASPxDateEdit ID="deMotivoFalta" runat="server" ClientInstanceName="deMotivoFalta" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                Date="01/12/2013 14:38:46" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
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
                                <td align="right" class="auto-style4">Data Final:</td>
                                <td align="left">
                            <dx:ASPxDateEdit ID="deMotivoFaltaFim" runat="server" ClientInstanceName="deMotivoFaltaFim" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                Date="01/12/2013 14:38:46" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
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
                                <td align="right" class="auto-style4">&nbsp;</td>
                                <td align="left">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="style4" align="right">
                &nbsp;</td>
            <td align="left" class="auto-style3">
                            <dx:ASPxHiddenField ID="coMotivoFaltaPeriodo" runat="server" 
                                ClientInstanceName="coMotivoFaltaPeriodo">
                            </dx:ASPxHiddenField>
                            </td>
        </tr>
    </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="auto-style6"></td>
                </tr>
                <tr>
                    <td align="right" class="auto-style7">
                <dx:ASPxButton ID="btRelacaoDia" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok" 
                            Width="100px" 
                            ValidationGroup="ValidaGrupo" AutoPostBack="False" 
                            ClientInstanceName="btMotivoFaltaRel" Theme="iOS" 
                    onclick="btRelacaoDia_Click" ToolTip="Ok">
                    <ClientSideEvents Click="function(s, e) {
}" />
                </dx:ASPxButton>
                    </td>
                    <td align="left" class="auto-style8">
                <dx:ASPxButton ID="btCancelDia" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                            CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                            Width="100px" onclick="btVoltar_Click" 
                    ClientInstanceName="btCancelMotivoFalta" Theme="iOS" ToolTip="V oltar">
                </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">

                function MudaPagina() {

                    if (deMotivoFalta.GetDate() <= deMotivoFaltaFim.GetDate())
                        window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=0&Setor=' + coMotivoFaltaPeriodo.Get("IDSetor") + '&Dia=' + deMotivoFalta.GetText() + '&MotivoFalta=' + cbMotivoFaltaRel.GetValue() + '&DiaFinal=' + deMotivoFaltaFim.GetText() + '&Rel=frmMotivoFalta');
                    else
                        alert('A data inicial não pode ser menor que a final. Repita o processo');
                }

    </script>
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
&nbsp;&nbsp;&nbsp; 
</asp:Content>

