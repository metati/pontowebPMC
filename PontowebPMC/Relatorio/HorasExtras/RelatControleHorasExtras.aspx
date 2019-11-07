<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RelatControleHorasExtras.aspx.cs" Inherits="Relatorio_HorasExtras_RelatControleHorasExtras" %>


<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .right {
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="dxeLyGroup_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #000000;">Relatório de Controle de Horas Extras</div>

            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td style="width: 10%">FILTROS:</td>
            <td style="width: 50%">Setor:<dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetorDefault"
                ssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" DataMember="TBSetor"
                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor"
                ValueField="IDSetor" Width="100%" Spacing="0" IncrementalFilteringMode="Contains"
                EnableIncrementalFiltering="True" Theme="DevEx" OnSelectedIndexChanged="cbSetor_SelectedIndexChanged" AutoPostBack="True">
                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                </LoadingPanelImage>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
            </dx:ASPxComboBox>
            </td>
            <td style="width: 18%">Nome<asp:TextBox ID="txtNome" runat="server" CssClass="textEntry" Width="100%"></asp:TextBox></td>
            <td style="width: 18%">Matrícula<asp:TextBox ID="txtMatricula" runat="server" CssClass="textEntry" Width="100%"></asp:TextBox></td>
            <td style="padding-top: 10px;">
                <dx:ASPxButton ID="btnFilter" runat="server"
                    ClientInstanceName="btSalvar"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Filtrar"
                    Width="100px" Theme="iOS" ToolTip="Filtrar" CssClass="right" OnClick="btFilter_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    
    <dx:ASPxGridView ID="gridUsuario" ClientInstanceName="gridUsuario" runat="server" KeyFieldName="Codigo" Width="100%">
        <SettingsText EmptyDataRow=" " EmptyHeaders=" " />
    </dx:ASPxGridView>
    <table style="width: 100%">
        <tr><td style="background:#CCCCCC" colspan="2"></td></tr>
        <tr>
            <td style="width: 80%">&nbsp
            </td>
            <td style="width: 30%">&nbsp;
                <dx:ASPxButton ID="btDeletar" runat="server"
                    ClientInstanceName="btDeletar"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Deletar"
                    Width="150px" Theme="iOS" ToolTip="Salvar" CssClass="right" OnClick="btDeletar_Click">
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


