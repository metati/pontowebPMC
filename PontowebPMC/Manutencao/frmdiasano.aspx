<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmdiasano.aspx.cs" Inherits="Manutencao_frmdiasano" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
            margin-left: 277px;
        }
        .auto-style3
        {
            height: 32px;
            width: 33px;
        }
        .auto-style4
        {
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">MANUTENÇÃO DE DATAS</div> 

            </td>
        </tr>
        <tr>
            <td class="auto-style4"><strong style="font-size: 16px; font-weight: normal;">
                <img alt="" class="auto-style3" src="../Images/Imagem10.png" /> Gerar dias do ano corrente </strong></td>
        </tr>
        <tr>
            <td align="center">
    <table class="style1">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dx:ASPxButton ID="btGerarDia" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btGerarDia" Text="Gerar dias do ano corrente" Theme="DevEx" 
                    Width="318px">
                    <ClientSideEvents Click="function(s, e) {
	Progresso();
}" />
                </dx:ASPxButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dx:ASPxCallbackPanel ID="cbDia" runat="server" ClientInstanceName="cbDia" 
                    LoadingPanelText="Gerando datas&amp;hellip;" Theme="DevEx" Width="600px" 
                    oncallback="cbDia_Callback">
                    <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxProgressBar ID="pbDia" runat="server" ClientInstanceName="pbDia" 
        CustomDisplayFormat="" Height="21px" Position="50" Theme="DevEx" 
        Width="200px" Visible="False">
    </dx:ASPxProgressBar>
                        </dx:PanelContent>
</PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function Progresso(s, e) {
            cbDia.PerformCallback();
        }
    </script>
    </asp:Content>

