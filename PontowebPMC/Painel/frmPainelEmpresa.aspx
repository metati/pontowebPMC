﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPainelEmpresa.aspx.cs" Inherits="Painel_frmPainelEmpresa" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v13.1.Web, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSplitter" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx1" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            timeout = window.setTimeout(AtualizaPainel, 300000); //300000 - 5 minutos é o padrão

            if (Visao() == '1') {
                gridRegistro.PerformCallback();
            }
        }

        function Visao()
        {
            if (coEmpresaSetor.Get("tpuser") == '1') 
            {
                
                if (cbGrupoGeral.GetChecked())
                    return '0';

                if (cbEmpresaPainel.GetText() != '' && cbSetorPainel.GetText() == '')
                    return '1';

                if (cbEmpresaPainel.GetText() != '' && cbSetorPainel.GetText() != '')
                    return '2';
            }

            if (coEmpresaSetor.Get("tpuser") == '7' || coEmpresaSetor.Get("tpuser") == '8') 
            {
                if (cbSetorPainel.GetText() == '')
                    return '1';
                else
                    return '2';
            }

            if (coEmpresaSetor.Get("tpuser") == '3' || coEmpresaSetor.Get("tpuser") == '9') {
                return '2'
            }
            
        }

        function IniciaCombo() {
            cbSetorPainel.SetVisible(false);
            cbEmpresaPainel.SetVisible(false);
            lbOrgaoPainel.SetVisible(false);
            lbSetorPainel.SetVisible(false);
        }
        
        function VisaoCombos() 
        {
            if (coEmpresaSetor.Get("tpuser") == '1') 
            {
                if (cbGrupoGeral.GetChecked()) {
                    cbSetorPainel.SetVisible(false);
                    cbEmpresaPainel.SetVisible(false);
                    lbSetorPainel.SetVisible(false);
                }
                else {
                    cbSetorPainel.SetVisible(true);
                    cbEmpresaPainel.SetVisible(true);
                    lbOrgaoPainel.SetVisible(true);
                    lbSetorPainel.SetVisible(true);
                }
            }
            else {
                cbSetorPainel.SetVisible(true);
                lbSetorPainel.SetVisible(true);  
            }
        }
        
        function PreenchecbSetor() {
            cbSetorPainel.PerformCallback(cbEmpresaPainel.GetValue());
        }
        
        function AbrePopPeriodo() {
            popPeriodoGeral.Show();
        }

        function AbrePopVisualizacao() {
            VisaoCombos();
            popEmpresaSetorVisu.Show();
        }

        function AtualizaPainel() {
            cbGeralEmpresa.PerformCallback(Visao());
        }

        function InicioVisao() {
            MudaVisao();
        }
        
        function MudaVisao() {

            Visao();
            cpVisaoComponente.PerformCallback();
            cbGeralEmpresa.PerformCallback(Visao());
        }
    </script>
    <title>Visão geral dos clientes - Meta Tecnologia da Informação</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style4
        {
            width: 183px;
        }
        .style5
        {
            width: 144px;
        }
        .style8
        {
            width: 104px;
        }
        .style9
        {
            height: 39px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    <dx:ASPxSplitter ID="spUnico" runat="server" ClientInstanceName="spUnico" 
                        EnableTheming="True" Height="900px" Orientation="Vertical" Theme="DevEx" 
                        Width="1550px">
                        <panes>
                            <dx:SplitterPane AllowResize="False" Collapsed="True" MaxSize="75px" 
                                MinSize="35px" ShowCollapseBackwardButton="True" 
                                ShowCollapseForwardButton="True">
                                <Separator Visible="True">
                                </Separator>
                                <ContentCollection>
<dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
    <table class="style1">
        <tr>
            <td class="style4">
                <dx:ASPxHiddenField ID="coEmpresaSetor" runat="server" 
                    ClientInstanceName="coEmpresaSetor">
                </dx:ASPxHiddenField>
                <dx:ASPxPopupControl ID="popPeriodoGeral" runat="server" 
                    ClientInstanceName="popPeriodoGeral" CloseAction="CloseButton" 
                    HeaderText="Definindo períodicidade para acompanhamento" 
                    LoadingPanelText="Abrindo&amp;hellip;" RenderMode="Lightweight" 
                    Theme="DevEx" PopupHorizontalAlign="WindowCenter" 
                    PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table class="style1">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Visão:" Theme="DevEx">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButtonList ID="rbVisao" runat="server" 
                                            ClientInstanceName="rbVisao" EnableTheming="True" Height="10px" 
                                            RepeatDirection="Horizontal" SelectedIndex="0" Theme="DevEx">
                                            <ClientSideEvents Init="function(s, e) {
	InicioVisao();
}" SelectedIndexChanged="function(s, e) {
	MudaVisao();
}" />
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	MudaVisao();
}" Init="function(s, e) {
	InicioVisao();
}"></ClientSideEvents>
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Dia" Value="0" />
                                                <dx:ListEditItem Text="Mês/Ano" Value="1" />
                                                <dx:ListEditItem Text="Ano" Value="2" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Período:" Theme="DevEx">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxCallbackPanel ID="cpVisaoComponente0" runat="server" 
                                            ClientInstanceName="cpVisaoComponente" 
                                            LoadingPanelText="Atualizando&amp;hellip;" 
                                            OnCallback="cpVisaoComponente_Callback" Theme="DevEx" Width="251px">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="deDTReferencia" runat="server" 
                                                        ClientInstanceName="deDTReferencia" Date="06/08/2015 14:32:00" Height="10px" 
                                                        Theme="DevEx" Width="200px">
                                                        <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                        </CalendarProperties>
                                                        <ClientSideEvents DateChanged="function(s, e) {
	AtualizaPainel();
}" />

<ClientSideEvents DateChanged="function(s, e) {
	AtualizaPainel();
}"></ClientSideEvents>
                                                    </dx:ASPxDateEdit>
                                                    <dx:ASPxComboBox ID="cbMesAnoReferencia" runat="server" 
                                                        ClientInstanceName="cbMesAnoReferencia" EnableTheming="True" Height="10px" 
                                                        LoadingPanelText="Processando&amp;hellip;" TextFormatString="{0}/{1}" 
                                                        Theme="DevEx" Width="200px">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	AtualizaPainel();
}" />
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	AtualizaPainel();
}"></ClientSideEvents>
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="Mês" FieldName="MesRef" Width="100px" />
                                                            <dx:ListBoxColumn Caption="Ano" FieldName="AnoRef" Width="100px" />
                                                        </Columns>
                                                    </dx:ASPxComboBox>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxCallbackPanel>
                                    </td>
                                </tr>
                            </table>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl ID="popEmpresaSetorVisu" runat="server" 
                    ClientInstanceName="popEmpresaSetorVisu" CloseAction="CloseButton" 
                    HeaderText="Definindo órgão/setor para visualização" 
                    LoadingPanelText="Abrindo&amp;hellip;" PopupHorizontalAlign="WindowCenter" 
                    PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" Theme="DevEx" 
                    Width="550px">
                    <ClientSideEvents Init="function(s, e) {
	IniciaCombo();
}" />
<ClientSideEvents Init="function(s, e) {
	IniciaCombo();
}"></ClientSideEvents>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table class="style1">
                                <tr>
                                    <td class="style8">
                                        &nbsp;</td>
                                    <td>
                                        <dx:ASPxCheckBox ID="cbGrupoGeral" runat="server" CheckState="Unchecked" 
                                            ClientInstanceName="cbGrupoGeral" Text="Visualizar o Grupo" Theme="DevEx">
                                            <ClientSideEvents ValueChanged="function(s, e) {
	VisaoCombos();
}" />
<ClientSideEvents ValueChanged="function(s, e) {
	VisaoCombos();
}"></ClientSideEvents>
                                        </dx:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8">
                                        <dx:ASPxLabel ID="lbOrgaoPainel" runat="server" Text="Filtrar por órgão:" 
                                            Theme="DevEx" ClientInstanceName="lbOrgaoPainel" Width="120px">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="cbEmpresaPainel" runat="server" 
                                            ClientInstanceName="cbEmpresaPainel" Theme="DevEx" Width="350px" 
                                            IncrementalFilteringMode="Contains">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbSetor();
}" />
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbSetor();
}"></ClientSideEvents>
                                            <ValidationSettings ValidationGroup="ValidaGrupoPainel">
                                                <RequiredField IsRequired="True" />
<RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8">
                                        <dx:ASPxLabel ID="lbSetorPainel" runat="server" Text="Filtrar por setor:" 
                                            Theme="DevEx" Width="120px" ClientInstanceName="lbSetorPainel">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="cbSetorPainel" runat="server" 
                                            ClientInstanceName="cbSetorPainel" OnCallback="cbSetorPainel_Callback" 
                                            Theme="DevEx" Width="350px" 
                                            IncrementalFilteringMode="Contains" DropDownStyle="DropDown">
                                            <ValidationSettings ValidationGroup="ValidaGrupoPainel">
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                            <table class="style1">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btFiltraPainel" runat="server" 
                                            ClientInstanceName="btFiltraPainel" Text="Filtrar" Theme="DevEx" 
                                            Width="100px" ValidationGroup="ValidaGrupoPainel" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
if(ASPxClientEdit.ValidateGroup('ValidaGrupoPainel')){
AtualizaPainel();
popEmpresaSetorVisu.Hide();	
}
}" />
<ClientSideEvents Click="function(s, e) {
if(ASPxClientEdit.ValidateGroup(&#39;ValidaGrupoPainel&#39;)){
AtualizaPainel();
popEmpresaSetorVisu.Hide();	
}
}"></ClientSideEvents>
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="style5">
                <dx:ASPxButton ID="btAbrepopPeriodo2" runat="server" 
                    ClientInstanceName="btAbrepopPeriodo" EnableTheming="True" 
                    Text="Definir visualização" Theme="DevEx" Width="200px" 
                    AutoPostBack="False">
                    <ClientSideEvents Click="function(s, e) {
	AbrePopVisualizacao();
}" />
<ClientSideEvents Click="function(s, e) {
	AbrePopVisualizacao();
}"></ClientSideEvents>
          </dx:ASPxButton>
            </td>
            <td class="style5">
                <dx:ASPxButton ID="btAbrepopPeriodo" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btAbrepopPeriodo" EnableTheming="True" 
                    Text="Definir período" Theme="DevEx" Width="200px">
                    <ClientSideEvents Click="function(s, e) {
	AbrePopPeriodo();
}" />
<ClientSideEvents Click="function(s, e) {
	AbrePopPeriodo();
}"></ClientSideEvents>
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btAbrepopPeriodo1" runat="server" 
                    ClientInstanceName="btAbrepopPeriodo" Text="Retornar ao menu principal" 
                    Theme="DevEx" Width="200px" OnClick="btAbrepopPeriodo1_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
                                    </dx:SplitterContentControl>
</ContentCollection>
                            </dx:SplitterPane>
                            <dx:SplitterPane>
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <dx:ASPxCallbackPanel ID="cbGeralEmpresa" runat="server" 
                                                        ClientInstanceName="cbGeralEmpresa" LoadingPanelText="Atualizando&amp;hellip;" 
                                                        OnCallback="cbGeralEmpresa_Callback" Theme="DevEx" Width="1250px">
                                                        <ClientSideEvents EndCallback="function(s, e) {
	FechaGauge();
}" Init="function(s, e) {
	AbreGauge();
}" />
<ClientSideEvents EndCallback="function(s, e) {
	FechaGauge();
}" Init="function(s, e) {
	AbreGauge();
}"></ClientSideEvents>
                                                        <PanelCollection>
                                                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                <table class="style1">
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <dx:ASPxLabel ID="lblarica" runat="server" ClientInstanceName="lblarica" 
                                                                                Font-Bold="True" Font-Size="Large" Text="ASPxLabel" Theme="DevEx">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table class="style1">
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <dxchartsui:WebChartControl runat="server" Width="490px" Height="580px" SideBySideEqualBarWidth="True" ClientInstanceName="graficoGeralEmpresa" CrosshairEnabled="False" ID="graficoGeral">
<EmptyChartText Text="Selecione um setor para confer&#234;ncia"></EmptyChartText>

<FillStyle><OptionsSerializable>
<cc1:SolidFillOptions></cc1:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

<SeriesTemplate><ViewSerializable>
<cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<cc1:SideBySideBarSeriesLabel LineVisible="True">
<FillStyle><OptionsSerializable>
<cc1:SolidFillOptions></cc1:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
<PointOptionsSerializable>
<cc1:PointOptions></cc1:PointOptions>
</PointOptionsSerializable>
</cc1:SideBySideBarSeriesLabel>
</LabelSerializable>
<LegendPointOptionsSerializable>
<cc1:PointOptions></cc1:PointOptions>
</LegendPointOptionsSerializable>
</SeriesTemplate>

<CrosshairOptions><CommonLabelPositionSerializable>
<cc1:CrosshairMousePosition></cc1:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions><ToolTipPositionSerializable>
<cc1:ToolTipMousePosition></cc1:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>
</dxchartsui:WebChartControl>

                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            &nbsp;</td>
                                                                        <td style="text-align: center">
                                                                            <dxchartsui:WebChartControl ID="graficoGeralJustificativa" runat="server" 
                                                                                ClientInstanceName="graficoGeralJustificativaEmpresa" CrosshairEnabled="False" 
                                                                                Height="580px" PaletteName="Apex" SideBySideEqualBarWidth="True" Width="730px">
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
<ValueNumericOptions Format="Percent"></ValueNumericOptions>
                                                                                                    </cc1:PiePointOptions>
                                                                                                </pointoptionsserializable>
                                                                                            </cc1:PieSeriesLabel>
                                                                                        </labelserializable>
                                                                                        <legendpointoptionsserializable>
                                                                                            <cc1:PiePointOptions PointView="ArgumentAndValues">
<ValueNumericOptions Format="Percent"></ValueNumericOptions>
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
<ValueNumericOptions Format="General"></ValueNumericOptions>
                                                                                                </cc1:PiePointOptions>
                                                                                            </pointoptionsserializable>
                                                                                        </cc1:PieSeriesLabel>
                                                                                    </labelserializable>
                                                                                    <legendpointoptionsserializable>
                                                                                        <cc1:PiePointOptions>
<ValueNumericOptions Format="General"></ValueNumericOptions>
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
                                                                        <td style="text-align: center" valign="top">
                                                                            <dx:ASPxGridView ID="gridRegistro" runat="server" AutoGenerateColumns="False" 
                                                                                ClientInstanceName="gridRegistro" DataMember="vwLogRegistroSetor" 
                                                                                EnableTheming="True" OnCustomCallback="gridRegistro_CustomCallback" 
                                                                                Theme="DevEx" Width="250px">
                                                                                <Columns>
                                                                                    <dx:GridViewDataTextColumn Caption="Situação" ShowInCustomizationForm="True" 
                                                                                        VisibleIndex="0" Width="10px">
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxImage ID="icone2" runat="server" 
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
                                                                    </tr>
                                                                </table>
                                                                <table class="style1">
                                                                    <tr>
                                                                        <td>
                                                                            <dxchartsui:WebChartControl ID="graficoTop5Ausencia" runat="server" 
                                                                                ClientInstanceName="graficoTop5Ausencia" CrosshairEnabled="False" 
                                                                                Height="220px" PaletteBaseColorNumber="2" SideBySideEqualBarWidth="True" 
                                                                                Width="350px">
                                                                                <emptycharttext text="Sem dados para exibir" />
<EmptyChartText Text="Sem dados para exibir"></EmptyChartText>

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
                                                                            <dxchartsui:WebChartControl ID="graficoTop5Registro" runat="server" 
                                                                                ClientInstanceName="graficoTop5Registro" CrosshairEnabled="False" 
                                                                                Height="220px" PaletteBaseColorNumber="1" SideBySideEqualBarWidth="True" 
                                                                                Width="350px">
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
                                                                            <dxchartsui:WebChartControl ID="graficoTop5Justificativa" runat="server" 
                                                                                ClientInstanceName="graficoTop5Justificativa" CrosshairEnabled="False" 
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
                                                                        <td style="font-weight: 700">
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </td>
                                                <td style="text-align: left" valign="top" class="style9" height="10%">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" valign="top">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>
                        </panes>
                    </dx:ASPxSplitter>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>