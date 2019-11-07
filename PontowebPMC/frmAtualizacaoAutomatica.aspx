<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAtualizacaoAutomatica.aspx.cs" Inherits="frmAtualizacaoAutomatica" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraCharts.v13.1.Web, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var timeout;
        var Tipo = 0;
        function AgoraVai() {
            window.clearTimeout(timeout);
            timeout = window.setTimeout(Preenche, 2000);
        }
        function Dado() {
            //alert(Tipo);
            if (Tipo == 0)
                Tipo = 1;
            else if (Tipo == 1)
                Tipo = 2;
            else if (Tipo == 2)
                Tipo = 1;
        }

        function Preenche() {
            //gridHora.Refresh();
            Dado();
            gridHora.PerformCallback();
            Grafico.PerformCallback(Tipo);
        }
        function IniciaGrid() {
            AgoraVai();
            //alert('entrou');
        }
        function InicioCallbackGrid() {
            window.clearTimeout(timeout); //não precisa usar esta função.
        }
        function FimCallbackGrid() {
            //Chama novamente o callback
            AgoraVai();
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 373px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <SettingsLoadingPanel ImagePosition="Top" />
        <Paddings Padding="1px" />
        <Images SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css">
            <LoadingPanelOnStatusBar Url="~/App_Themes/SoftOrange/GridView/gvLoadingOnStatusBar.gif">
            </LoadingPanelOnStatusBar>
            <LoadingPanel Url="~/App_Themes/SoftOrange/GridView/Loading.gif">
            </LoadingPanel>
        </Images>
        <ImagesFilterControl>
            <LoadingPanel Url="~/App_Themes/SoftOrange/Editors/Loading.gif">
            </LoadingPanel>
        </ImagesFilterControl>
        <Styles CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" 
            CssPostfix="SoftOrange" GroupButtonWidth="28">
            <Header ImageSpacing="5px" SortingImageSpacing="5px">
            </Header>
            <LoadingPanel ImageSpacing="8px">
            </LoadingPanel>
        </Styles>
        <StylesEditors>
            <CalendarHeader Spacing="1px">
            </CalendarHeader>
            <ProgressBar Height="29px">
            </ProgressBar>
        </StylesEditors>
    </dx:ASPxGridView>
    <table class="style1">
        <tr>
            <td class="style2">
    
        <dx:ASPxGridView ID="gridHora" runat="server" AutoGenerateColumns="False" 
            DataMember="TBTesteAutomatico" ClientInstanceName="gridHora" 
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
            Width="400px">
            <ClientSideEvents Init="function(s, e) {
	IniciaGrid();
}" EndCallback="function(s, e) {
	FimCallbackGrid();
}" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="IDInsert" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DataHora" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager Mode="ShowAllRecords" ShowDefaultImages="False">
                <AllButton Text="All">
                </AllButton>
                <NextPageButton Text="Next &gt;">
                </NextPageButton>
                <PrevPageButton Text="&lt; Prev">
                </PrevPageButton>
            </SettingsPager>
            <SettingsLoadingPanel Text="Atualizando&amp;hellip;" />
            <Images SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
                <LoadingPanelOnStatusBar Url="~/App_Themes/PlasticBlue/GridView/gvLoadingOnStatusBar.gif">
                </LoadingPanelOnStatusBar>
                <LoadingPanel Url="~/App_Themes/PlasticBlue/GridView/Loading.gif">
                </LoadingPanel>
            </Images>
            <ImagesFilterControl>
                <LoadingPanel Url="~/App_Themes/PlasticBlue/Editors/Loading.gif">
                </LoadingPanel>
            </ImagesFilterControl>
            <Styles CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                CssPostfix="PlasticBlue">
                <Header ImageSpacing="10px" SortingImageSpacing="10px">
                </Header>
            </Styles>
            <StylesEditors>
                <CalendarHeader Spacing="11px">
                </CalendarHeader>
                <ProgressBar Height="25px">
                </ProgressBar>
            </StylesEditors>
        </dx:ASPxGridView>
    
            </td>
            <td>
                <dxchartsui:WebChartControl ID="Grafico" runat="server" 
                    ClientInstanceName="Grafico" DataMember="viewDado" Height="220px" 
                    oncustomcallback="Grafico_CustomCallback" SeriesDataMember="DSDado" 
                    Width="480px" LoadingPanelText="Atualizando&amp;hellip;">
                    <diagramserializable>
                        <cc1:XYDiagram>
                            <axisx visibleinpanesserializable="-1">
                                <range sidemarginsenabled="True" />
                            </axisx>
                            <axisy visibleinpanesserializable="-1">
                                <range sidemarginsenabled="True" />
                            </axisy>
                        </cc1:XYDiagram>
                    </diagramserializable>
<FillStyle><OptionsSerializable>
<cc1:SolidFillOptions></cc1:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

<SeriesTemplate argumentdatamember="DSDado" valuedatamembersserializable="TotalDado"><ViewSerializable>
    <cc1:SideBySideBarSeriesView>
    </cc1:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
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
</LabelSerializable>
<LegendPointOptionsSerializable>
    <cc1:PointOptions>
    </cc1:PointOptions>
</LegendPointOptionsSerializable>
</SeriesTemplate>
                    <clientsideevents endcallback="function(s, e) {
	FimCallbackGrid();
}" init="function(s, e) {
	IniciaGrid();
}" />

<ClientSideEvents EndCallback="function(s, e) {
	FimCallbackGrid();
}" Init="function(s, e) {
	IniciaGrid();
}" begincallback="function(s, e) {
	InicioCallbackGrid();
}"></ClientSideEvents>
                </dxchartsui:WebChartControl>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>
