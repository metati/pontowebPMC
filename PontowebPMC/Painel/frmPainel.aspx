<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmPainel.aspx.cs" Inherits="Painel_frmPainel" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.XtraCharts.v13.1.Web, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 40px;
        }
        .style3
        {
            width: 970px;
            margin-left: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <dx:ASPxCallbackPanel ID="cbGeral" runat="server" ClientInstanceName="cbGeral" 
        LoadingPanelText="Atualizando&amp;hellip;" Theme="Aqua" Width="1220px">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <table class="dxflInternalEditorTable_DevEx">
                    <tr>
                        <td class="style3" valign="top">
                            <dx:ASPxHyperLink ID="hlRetorno" runat="server" 
                                Text="Retornar a página principal">
                            </dx:ASPxHyperLink>
                            <dxchartsui:WebChartControl ID="graficoGeral" runat="server" 
                                ClientInstanceName="graficoGeral" CrosshairEnabled="False" Height="600px" 
                                SideBySideEqualBarWidth="True" Width="400px">
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
                        <td valign="top">
                            <dx:ASPxGridView ID="gridRegistro" runat="server" AutoGenerateColumns="False" 
                                ClientInstanceName="gridRegistro" DataMember="vwLogRegistroSetor" 
                                Width="600px" EnableTheming="True" Theme="DevEx">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Situação" ShowInCustomizationForm="True" 
                                        VisibleIndex="0" Width="10px">
                                    <DataItemTemplate>
                                       <dx:ASPxImage runat="server" ID="icone" ImageUrl="<%# PegarImagem(Container) %>"></dx:ASPxImage> 
                                    </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Setor" FieldName="DSSetor" 
                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="450px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Último Registro" FieldName="MaiorRegistro" 
                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager NumericButtonCount="30" PageSize="20">
                                    <Summary Text="Página{0} de {1} ({2} itens)" />
                                </SettingsPager>
                            </dx:ASPxGridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" valign="top">
                            <table class="dxflInternalEditorTable_Aqua">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbRegistro" runat="server" ClientInstanceName="lbRegistro" 
                                            Text="Registros Efetuados:" Theme="DevEx">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbJustificado" runat="server" 
                                            ClientInstanceName="lbJustificado" Text="Registros Justificados:" Theme="DevEx">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbAusencia" runat="server" ClientInstanceName="lbAusencia" 
                                            Text="Ausência de Registros:" Theme="DevEx">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            &nbsp;</td>
                    </tr>
                </table>
                <table class="dxflInternalEditorTable_Aqua">
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td class="style2">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>

