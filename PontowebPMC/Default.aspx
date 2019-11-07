<%@ Page Title="Pontoweb - Sistema de gestão em marcação de ponto." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraCharts.v13.1.Web, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 835px;
        }
        .style3
        {
            width: 92px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function AbrePop() {
            popEmpresa.Show();
        }
        function ChamaGrafico() {
            cbPGrafico.PerformCallback();
        }
        function FechaPop() {
            popEmpresa.Hide();
        }
        function DefineSession() {
            cbEmpresa.PerformCallback(cbEmpresa.GetValue());
            popEmpresa.Hide();
        }
    </script>
    <h2>
        <table class="style1">
            <tr>
                <td>
                    <table class="style1">
                        <tr>
                            <td class="style3" runat="server" id="celula">
                                Setor:</td>
                            <td>
                <dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetorDefault" 
                    ssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" DataMember="TBSetor" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor" 
                    ValueField="IDSetor" Width="400px" Spacing="0" IncrementalFilteringMode="Contains" 
                                EnableIncrementalFiltering="True" Theme="DevEx">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {ChamaGrafico();}" />
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" 
                        ClientInstanceName="cbPGrafico" Height="350px" LoadingPanelDelay="100" 
                        Width="1110px" LoadingPanelText="Carregando&amp;hellip;" 
                        oncallback="ASPxCallbackPanel1_Callback" Theme="DevEx">
                        <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dxchartsui:WebChartControl ID="graficoFaltaSetor" runat="server" 
        CrosshairEnabled="False" Height="345px" SideBySideEqualBarWidth="True" 
        Width="1105px">
        <emptycharttext text="Selecione um setor para conferência" />
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
                            </dx:PanelContent>
</PanelCollection>
                    </dx:ASPxCallbackPanel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </h2>
    <p>
        &nbsp;</p>
    <p align="center">
        &nbsp;</p>
    <p>
        <dx:ASPxPopupControl ID="popEmpresa" runat="server" 
            ClientInstanceName="popEmpresa" Modal="True" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
            HeaderText="Usuário Adm. Escolha uma empresa para conferência" 
            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" 
            CloseAction="None" ShowCloseButton="False" EnableHotTrack="False">
            <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
            </LoadingPanelImage>
            <HeaderStyle>
            <Paddings PaddingLeft="7px" />
            </HeaderStyle>
            <LoadingPanelStyle ImageSpacing="5px">
            </LoadingPanelStyle>
            <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table class="style1">
        <tr>
            <td>
                Empresa:</td>
            <td>
                <dx:ASPxComboBox ID="cbEmpresa" runat="server" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" DataMember="TBEmpresa" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px" 
                    ClientInstanceName="cbEmpresa" 
                    OnCallback="cbEmpresa_Callback" TextField="DSEmpresa" 
                    ValueField="IDEmpresa" Spacing="0" AutoPostBack="True" 
                    OnSelectedIndexChanged="cbEmpresa_SelectedIndexChanged">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	DefineSession();
}" />
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                </dx:ASPxComboBox>
            </td>
        </tr>
    </table>
                </dx:PopupControlContentControl>
</ContentCollection>
        </dx:ASPxPopupControl>
</p>
</asp:Content>