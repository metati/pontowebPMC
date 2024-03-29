﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPainel1-1.aspx.cs" Inherits="Painel_frmPainel1_1" %>

<%@ Register assembly="DevExpress.XtraCharts.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.XtraCharts.v13.1.Web, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var timeout, pagina;
        function AbreGauge() {

            LoopGauge();
        }
        function FechaGauge() {
            LoopGauge();
        }
        function LoopGauge() {
            window.clearTimeout(timeout);
            timeout = window.setTimeout(PreencheGrafico, 120000); //300000 - 5 minutos é o padrão 
        }
        function PreencheGrafico() {

            cbGeral.PerformCallback();
        }
    </script>
    <style type="text/css">

.dxflInternalEditorTable_DevEx {
    width: 100%;
}
.dxpControl_DevEx
{
	font: 11px Verdana, Geneva, sans-serif;
	color: #201f35;
}
.dxpControl_DevEx td.dxpCtrl
{
	padding: 5px 2px;
}
.dxpSummary_DevEx
{
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
	padding: 1px 4px;
}
.dxpDisabled_DevEx
{
	color: #b1b1b8;
	border-color: #f2f2f4;
	cursor: default;
}
.dxpDisabledButton_DevEx
{
	color: #b1b1b8;
	text-decoration: none;
}
.dxpButton_DevEx
{
	text-decoration: none;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
}

.dxWeb_pPrevDisabled_DevEx {
    background-position: -60px -20px;
    width: 19px;
    height: 18px;
}

.dxWeb_rpHeaderTopLeftCorner_DevEx,
.dxWeb_rpHeaderTopRightCorner_DevEx,
.dxWeb_rpBottomLeftCorner_DevEx,
.dxWeb_rpBottomRightCorner_DevEx,
.dxWeb_rpTopLeftCorner_DevEx,
.dxWeb_rpTopRightCorner_DevEx,
.dxWeb_rpGroupBoxBottomLeftCorner_DevEx,
.dxWeb_rpGroupBoxBottomRightCorner_DevEx,
.dxWeb_rpGroupBoxTopLeftCorner_DevEx,
.dxWeb_rpGroupBoxTopRightCorner_DevEx,
.dxWeb_mHorizontalPopOut_DevEx,
.dxWeb_mVerticalPopOut_DevEx,
.dxWeb_mVerticalPopOutRtl_DevEx,
.dxWeb_mSubMenuItem_DevEx,
.dxWeb_mSubMenuItemChecked_DevEx,
.dxWeb_mScrollUp_DevEx,
.dxWeb_mScrollDown_DevEx,
.dxWeb_tcScrollLeft_DevEx,
.dxWeb_tcScrollRight_DevEx,
.dxWeb_tcScrollLeftHover_DevEx,
.dxWeb_tcScrollRightHover_DevEx,
.dxWeb_tcScrollLeftPressed_DevEx,
.dxWeb_tcScrollRightPressed_DevEx,
.dxWeb_tcScrollLeftDisabled_DevEx,
.dxWeb_tcScrollRightDisabled_DevEx,
.dxWeb_nbCollapse_DevEx,
.dxWeb_nbExpand_DevEx,
.dxWeb_splVSeparator_DevEx,
.dxWeb_splVSeparatorHover_DevEx,
.dxWeb_splHSeparator_DevEx,
.dxWeb_splHSeparatorHover_DevEx,
.dxWeb_splVCollapseBackwardButton_DevEx,
.dxWeb_splVCollapseBackwardButtonHover_DevEx,
.dxWeb_splHCollapseBackwardButton_DevEx,
.dxWeb_splHCollapseBackwardButtonHover_DevEx,
.dxWeb_splVCollapseForwardButton_DevEx,
.dxWeb_splVCollapseForwardButtonHover_DevEx,
.dxWeb_splHCollapseForwardButton_DevEx,
.dxWeb_splHCollapseForwardButtonHover_DevEx,
.dxWeb_pcCloseButton_DevEx,
.dxWeb_pcPinButton_DevEx,
.dxWeb_pcRefreshButton_DevEx,
.dxWeb_pcCollapseButton_DevEx,
.dxWeb_pcMaximizeButton_DevEx,
.dxWeb_pcSizeGrip_DevEx,
.dxWeb_pcSizeGripRtl_DevEx,
.dxWeb_pPopOut_DevEx,
.dxWeb_pPopOutDisabled_DevEx,
.dxWeb_pAll_DevEx,
.dxWeb_pAllDisabled_DevEx,
.dxWeb_pPrev_DevEx,
.dxWeb_pPrevDisabled_DevEx,
.dxWeb_pNext_DevEx,
.dxWeb_pNextDisabled_DevEx,
.dxWeb_pLast_DevEx,
.dxWeb_pLastDisabled_DevEx,
.dxWeb_pFirst_DevEx,
.dxWeb_pFirstDisabled_DevEx,
.dxWeb_tvColBtn_DevEx,
.dxWeb_tvColBtnRtl_DevEx,
.dxWeb_tvExpBtn_DevEx,
.dxWeb_tvExpBtnRtl_DevEx,
.dxWeb_ncBackToTop_DevEx,
.dxWeb_smBullet_DevEx,
.dxWeb_tiBackToTop_DevEx,
.dxWeb_fmFolder_DevEx,
.dxWeb_fmFolderLocked_DevEx,
.dxWeb_fmCreateButton_DevEx,
.dxWeb_fmMoveButton_DevEx,
.dxWeb_fmRenameButton_DevEx,
.dxWeb_fmDeleteButton_DevEx,
.dxWeb_fmRefreshButton_DevEx,
.dxWeb_fmDwnlButton_DevEx,
.dxWeb_fmCreateButtonDisabled_DevEx,
.dxWeb_fmMoveButtonDisabled_DevEx,
.dxWeb_fmRenameButtonDisabled_DevEx,
.dxWeb_fmDeleteButtonDisabled_DevEx,
.dxWeb_fmRefreshButtonDisabled_DevEx,
.dxWeb_fmDwnlButtonDisabled_DevEx,
.dxWeb_fmThumbnailCheck_DevEx,
.dxWeb_ucClearButton_DevEx,
.dxWeb_isPrevBtnHor_DevEx,
.dxWeb_isNextBtnHor_DevEx,
.dxWeb_isPrevBtnVert_DevEx,
.dxWeb_isNextBtnVert_DevEx,
.dxWeb_isPrevPageBtnHor_DevEx,
.dxWeb_isNextPageBtnHor_DevEx,
.dxWeb_isPrevPageBtnVert_DevEx,
.dxWeb_isNextPageBtnVert_DevEx,
.dxWeb_isPrevBtnHorDisabled_DevEx,
.dxWeb_isNextBtnHorDisabled_DevEx,
.dxWeb_isPrevBtnVertDisabled_DevEx,
.dxWeb_isNextBtnVertDisabled_DevEx,
.dxWeb_isPrevPageBtnHorDisabled_DevEx,
.dxWeb_isNextPageBtnHorDisabled_DevEx,
.dxWeb_isPrevPageBtnVertDisabled_DevEx,
.dxWeb_isNextPageBtnVertDisabled_DevEx,
.dxWeb_isDot_DevEx,
.dxWeb_isDotDisabled_DevEx,
.dxWeb_isDotSelected_DevEx
.dxWeb_ucClearButtonDisabled_DevEx,
.dxWeb_isPlayBtn_DevEx,
.dxWeb_isPauseBtn_DevEx,
.dxWeb_igCloseButton_DevEx,
.dxWeb_igNextButton_DevEx,
.dxWeb_igPrevButton_DevEx,
.dxWeb_igPlayButton_DevEx,
.dxWeb_igPauseButton_DevEx,
.dxWeb_igNavigationBarMarker_DevEx
{ 

    background-repeat: no-repeat;
    background-color: transparent;
    display:block;
}
.dxpCurrentPageNumber_DevEx
{
	background-color: #e2eafd;
	text-decoration: none;
	padding: 1px 4px 2px;
	border: 1px solid #c9d7fb;
	white-space: nowrap;
}
.dxpPageNumber_DevEx
{
	text-decoration: none;
	text-align: center;
	vertical-align: middle;
	padding: 1px 6px 2px;
}

.dxWeb_pNextDisabled_DevEx {
    background-position: -80px -20px;
    width: 19px;
    height: 18px;
}

        .style1
        {
            width: 100%;
        }
        .style3
        {
            width: 277px;
        }
        .style4
        {
            width: 457px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxCallbackPanel ID="cbGeral" runat="server" ClientInstanceName="cbGeral" 
            LoadingPanelText="Atualizando&amp;hellip;" Theme="DevEx" Width="1300px" 
            oncallback="cbGeral_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	FechaGauge();
}" Init="function(s, e) {
		AbreGauge();
}" />
            <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <table class="style1">
        <tr>
            <td>
                <dx:ASPxHyperLink ID="hlRetornar" runat="server" NavigateUrl="~/Default.aspx" 
                    Text="Retornar a página principal" Theme="DevEx">
                </dx:ASPxHyperLink>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="style3" valign="top">
                <dx:ASPxGridView ID="gridRegistro" runat="server" AutoGenerateColumns="False" 
                    ClientInstanceName="gridRegistro" DataMember="vwLogRegistroSetor" 
                    EnableTheming="True" Theme="DevEx" Width="250px">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Situação" ShowInCustomizationForm="True" 
                            VisibleIndex="0" Width="10px">
                            <DataItemTemplate>
                                <dx:ASPxImage ID="icone0" runat="server" 
                                    ImageUrl="<%# PegarImagem(Container) %>">
                                </dx:ASPxImage>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Setor" FieldName="Sigla" 
                            ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Última Conexão" FieldName="MaiorRegistro" 
                            ShowInCustomizationForm="True" VisibleIndex="1" Width="100px">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager NumericButtonCount="30" PageSize="20">
                        <Summary Text="Página{0} de {1} ({2} itens)" />
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
            <td valign="top">
                <table class="style1">
                    <tr>
                        <td class="style4">
                            <table class="style1">
                                <tr>
                                    <td>
                                        <dxchartsui:WebChartControl ID="graficoGeral" runat="server" 
                                            ClientInstanceName="graficoGeral" CrosshairEnabled="False" Height="580px" 
                                            SideBySideEqualBarWidth="True" Width="430px">
                                            <emptycharttext text="Selecione um setor para conferência" />
<EmptyChartText Text="Selecione um setor para confer&#234;ncia"></EmptyChartText>

                                            <fillstyle>
                                                <optionsserializable>
                                                    <cc1:SolidFillOptions />
                                                </optionsserializable>
                                            </fillstyle>
                                            <seriestemplate>
                                                <viewserializable>
                                                    <cc1:SideBySideBarSeriesView>
                                                    </cc1:SideBySideBarSeriesView>
                                                </viewserializable>
                                                <labelserializable>
                                                    <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                                        <fillstyle>
                                                            <optionsserializable>
                                                                <cc1:SolidFillOptions />
                                                            </optionsserializable>
                                                        </fillstyle>
                                                        <pointoptionsserializable>
                                                            <cc1:PointOptions>
                                                            </cc1:PointOptions>
                                                        </pointoptionsserializable>
                                                    </cc1:SideBySideBarSeriesLabel>
                                                </labelserializable>
                                                <legendpointoptionsserializable>
                                                    <cc1:PointOptions>
                                                    </cc1:PointOptions>
                                                </legendpointoptionsserializable>
                                            </seriestemplate>
                                            <crosshairoptions>
                                                <commonlabelpositionserializable>
                                                    <cc1:CrosshairMousePosition />
                                                </commonlabelpositionserializable>
                                            </crosshairoptions>
                                            <tooltipoptions>
                                                <tooltippositionserializable>
                                                    <cc1:ToolTipMousePosition />
                                                </tooltippositionserializable>
                                            </tooltipoptions>
                                        </dxchartsui:WebChartControl>
                                    </td>
                                    <td>
                                        <dxchartsui:WebChartControl ID="graficoGeralJustificativa" runat="server" 
                                            ClientInstanceName="graficoGeralJustificativa" CrosshairEnabled="False" 
                                            Height="580px" PaletteName="Apex" SideBySideEqualBarWidth="True" 
                                            Width="630px">
                                            <emptycharttext text="Selecione um setor para conferência" />
<EmptyChartText Text="Selecione um setor para confer&#234;ncia"></EmptyChartText>
                                            <diagramserializable>
                                                <cc1:SimpleDiagram EqualPieSize="False">
                                                </cc1:SimpleDiagram>
                                            </diagramserializable>
                                            <fillstyle>
                                                <optionsserializable>
                                                    <cc1:SolidFillOptions />
                                                </optionsserializable>
                                            </fillstyle>
                                            <seriesserializable>
                                                <cc1:Series LabelsVisibility="True" Name="Series 1">
                                                    <viewserializable>
                                                        <cc1:PieSeriesView RuntimeExploding="False">
                                                        </cc1:PieSeriesView>
                                                    </viewserializable>
                                                    <labelserializable>
                                                        <cc1:PieSeriesLabel LineVisible="True" Position="TwoColumns">
                                                            <fillstyle>
                                                                <optionsserializable>
                                                                    <cc1:SolidFillOptions />
                                                                </optionsserializable>
                                                            </fillstyle>
                                                            <pointoptionsserializable>
                                                                <cc1:PiePointOptions PointView="ArgumentAndValues">
                                                                    <valuenumericoptions format="Percent" />
                                                                </cc1:PiePointOptions>
                                                            </pointoptionsserializable>
                                                        </cc1:PieSeriesLabel>
                                                    </labelserializable>
                                                    <legendpointoptionsserializable>
                                                        <cc1:PiePointOptions PointView="ArgumentAndValues">
                                                            <valuenumericoptions format="Percent" />
                                                        </cc1:PiePointOptions>
                                                    </legendpointoptionsserializable>
                                                </cc1:Series>
                                            </seriesserializable>
                                            <seriestemplate>
                                                <viewserializable>
                                                    <cc1:PieSeriesView RuntimeExploding="True">
                                                    </cc1:PieSeriesView>
                                                </viewserializable>
                                                <labelserializable>
                                                    <cc1:PieSeriesLabel LineVisible="True">
                                                        <fillstyle>
                                                            <optionsserializable>
                                                                <cc1:SolidFillOptions />
                                                            </optionsserializable>
                                                        </fillstyle>
                                                        <pointoptionsserializable>
                                                            <cc1:PiePointOptions>
                                                                <valuenumericoptions format="General" />
                                                            </cc1:PiePointOptions>
                                                        </pointoptionsserializable>
                                                    </cc1:PieSeriesLabel>
                                                </labelserializable>
                                                <legendpointoptionsserializable>
                                                    <cc1:PiePointOptions>
                                                        <valuenumericoptions format="General" />
                                                    </cc1:PiePointOptions>
                                                </legendpointoptionsserializable>
                                            </seriestemplate>
                                            <crosshairoptions>
                                                <commonlabelpositionserializable>
                                                    <cc1:CrosshairMousePosition />
                                                </commonlabelpositionserializable>
                                            </crosshairoptions>
                                            <tooltipoptions>
                                                <tooltippositionserializable>
                                                    <cc1:ToolTipMousePosition />
                                                </tooltippositionserializable>
                                            </tooltipoptions>
                                        </dxchartsui:WebChartControl>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4">
                            &nbsp;</td>
                        <td valign="bottom">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style4" colspan="2">
                            <table class="style1">
                                <tr>
                                    <td>
                                        <dxchartsui:WebChartControl ID="graficoTop5" runat="server" 
                                            ClientInstanceName="graficoTop5" CrosshairEnabled="False" Height="220px" 
                                            PaletteBaseColorNumber="2" SideBySideEqualBarWidth="True" Width="350px">
                                            <emptycharttext text="Selecione um setor para conferência" />
<EmptyChartText Text="Selecione um setor para confer&#234;ncia"></EmptyChartText>

                                            <fillstyle>
                                                <optionsserializable>
                                                    <cc1:SolidFillOptions />
                                                </optionsserializable>
                                            </fillstyle>
                                            <seriestemplate>
                                                <viewserializable>
                                                    <cc1:SideBySideBarSeriesView>
                                                    </cc1:SideBySideBarSeriesView>
                                                </viewserializable>
                                                <labelserializable>
                                                    <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                                        <fillstyle>
                                                            <optionsserializable>
                                                                <cc1:SolidFillOptions />
                                                            </optionsserializable>
                                                        </fillstyle>
                                                        <pointoptionsserializable>
                                                            <cc1:PointOptions>
                                                            </cc1:PointOptions>
                                                        </pointoptionsserializable>
                                                    </cc1:SideBySideBarSeriesLabel>
                                                </labelserializable>
                                                <legendpointoptionsserializable>
                                                    <cc1:PointOptions>
                                                    </cc1:PointOptions>
                                                </legendpointoptionsserializable>
                                            </seriestemplate>
                                            <crosshairoptions>
                                                <commonlabelpositionserializable>
                                                    <cc1:CrosshairMousePosition />
                                                </commonlabelpositionserializable>
                                            </crosshairoptions>
                                            <tooltipoptions>
                                                <tooltippositionserializable>
                                                    <cc1:ToolTipMousePosition />
                                                </tooltippositionserializable>
                                            </tooltipoptions>
                                        </dxchartsui:WebChartControl>
                                    </td>
                                    <td>
                                        <dxchartsui:WebChartControl ID="graficoTop5Batido" runat="server" 
                                            ClientInstanceName="graficoTop5Batido" CrosshairEnabled="False" Height="220px" 
                                            PaletteBaseColorNumber="1" SideBySideEqualBarWidth="True" Width="350px">
                                            <emptycharttext text="Selecione um setor para conferência" />
<EmptyChartText Text="Selecione um setor para confer&#234;ncia"></EmptyChartText>

                                            <fillstyle>
                                                <optionsserializable>
                                                    <cc1:SolidFillOptions />
                                                </optionsserializable>
                                            </fillstyle>
                                            <seriestemplate>
                                                <viewserializable>
                                                    <cc1:SideBySideBarSeriesView>
                                                    </cc1:SideBySideBarSeriesView>
                                                </viewserializable>
                                                <labelserializable>
                                                    <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                                        <fillstyle>
                                                            <optionsserializable>
                                                                <cc1:SolidFillOptions />
                                                            </optionsserializable>
                                                        </fillstyle>
                                                        <pointoptionsserializable>
                                                            <cc1:PointOptions>
                                                            </cc1:PointOptions>
                                                        </pointoptionsserializable>
                                                    </cc1:SideBySideBarSeriesLabel>
                                                </labelserializable>
                                                <legendpointoptionsserializable>
                                                    <cc1:PointOptions>
                                                    </cc1:PointOptions>
                                                </legendpointoptionsserializable>
                                            </seriestemplate>
                                            <crosshairoptions>
                                                <commonlabelpositionserializable>
                                                    <cc1:CrosshairMousePosition />
                                                </commonlabelpositionserializable>
                                            </crosshairoptions>
                                            <tooltipoptions>
                                                <tooltippositionserializable>
                                                    <cc1:ToolTipMousePosition />
                                                </tooltippositionserializable>
                                            </tooltipoptions>
                                        </dxchartsui:WebChartControl>
                                    </td>
                                    <td>
                                        <dxchartsui:WebChartControl ID="graficoTop5Justificado" runat="server" 
                                            ClientInstanceName="graficoTop5Justificado" CrosshairEnabled="False" 
                                            Height="220px" PaletteBaseColorNumber="3" SideBySideEqualBarWidth="True" 
                                            Width="350px">
                                            <emptycharttext text="Selecione um setor para conferência" />
<EmptyChartText Text="Selecione um setor para confer&#234;ncia"></EmptyChartText>

                                            <fillstyle>
                                                <optionsserializable>
                                                    <cc1:SolidFillOptions />
                                                </optionsserializable>
                                            </fillstyle>
                                            <seriestemplate>
                                                <viewserializable>
                                                    <cc1:SideBySideBarSeriesView>
                                                    </cc1:SideBySideBarSeriesView>
                                                </viewserializable>
                                                <labelserializable>
                                                    <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                                        <fillstyle>
                                                            <optionsserializable>
                                                                <cc1:SolidFillOptions />
                                                            </optionsserializable>
                                                        </fillstyle>
                                                        <pointoptionsserializable>
                                                            <cc1:PointOptions>
                                                            </cc1:PointOptions>
                                                        </pointoptionsserializable>
                                                    </cc1:SideBySideBarSeriesLabel>
                                                </labelserializable>
                                                <legendpointoptionsserializable>
                                                    <cc1:PointOptions>
                                                    </cc1:PointOptions>
                                                </legendpointoptionsserializable>
                                            </seriestemplate>
                                            <crosshairoptions>
                                                <commonlabelpositionserializable>
                                                    <cc1:CrosshairMousePosition />
                                                </commonlabelpositionserializable>
                                            </crosshairoptions>
                                            <tooltipoptions>
                                                <tooltippositionserializable>
                                                    <cc1:ToolTipMousePosition />
                                                </tooltippositionserializable>
                                            </tooltipoptions>
                                        </dxchartsui:WebChartControl>
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
                            </table>
                        </td>
                        <td valign="bottom">
                            &nbsp;</td>
                    </tr>
                </table>
                <table class="style1">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
                </dx:PanelContent>
</PanelCollection>
        </dx:ASPxCallbackPanel>
    
    </div>
    </form>
</body>
</html>
