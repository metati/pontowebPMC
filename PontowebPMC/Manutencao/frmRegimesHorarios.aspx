<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmRegimesHorarios.aspx.cs" Inherits="Manutencao_frmRegimesHorarios" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx1" %>


<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .rightAlign {
            float: right;
        }

        .leftAlign {
            float: right;
        }
        .auto-style3 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="dxeLyGroup_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #000000;">Regimes de horários</div>
            </td>
        </tr>
    </table>
    <div id="divLista" style="width: 100%" runat="server">
        <table>
            <tr><td>&nbsp;</td></tr>
            <tr style="width: 100%">
                <td style="width: 90%"></td>
                <td>
                    <dx:ASPxButton ID="btnNovo" runat="server"
                        ClientInstanceName="btnNovo"
                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Novo"
                        Width="100px" Theme="iOS" ToolTip="Filtrar" CssClass="right" OnClick="btNovo_Click">
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="btnAlterar" runat="server"
                        ClientInstanceName="btnAlterar"
                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Alterar"
                        Width="100px" Theme="iOS" ToolTip="Filtrar" CssClass="right" OnClick="btAlterar_Click">
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton2" runat="server"
                        ClientInstanceName="btExcluir"
                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Excluir"
                        Width="100px" Theme="iOS" ToolTip="Filtrar" CssClass="right" OnClick="btExcluir_Click">
                    </dx:ASPxButton>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
        </table>
        <dx:ASPxGridView ID="gridRegimes" ClientInstanceName="gridRegimes" CssClass="gridRegimes" runat="server" KeyFieldName="Codigo" Width="100%" SettingsBehavior-AllowSelectByRowClick="true" SettingsBehavior-AllowSelectSingleRowOnly="true" OnBeforeColumnSortingGrouping="btFilter_Click" OnPageIndexChanged="btFilter_Click" ShowDeleteButton="true">
        </dx:ASPxGridView>
    </div>
    <div id="divForm" runat="server">
        <table style="width: 100%">
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" id="textoForm" runat="server" style="text-align: center; background: #f2f2f2">Cadastro de Regime de Horas</td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right;">Código:</td>
                <td>
                    <asp:TextBox ID="txtCodigo" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;">Descrição Regime Hora:</td>
                <td>
                    <asp:TextBox ID="txtDescricao" runat="server" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;">Total Hora Semana:</td>
                <td>
                    <asp:TextBox ID="txtTotalHoraSemana" runat="server" TextMode="Number"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;" class="auto-style3">Total Hora Dia:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtTotalHoraDia" runat="server" TextMode="Number"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;">Regime Plantonista:</td>
                <td>
                    <asp:CheckBox ID="cbxRegime" runat="server" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">Permite Horas Extras:</td>
                <td>
                    <asp:CheckBox ID="cbxHorasExtra" runat="server" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">Total Maximo Hora Extra Dia:</td>
                <td>
                    <asp:TextBox ID="txtTotalMaxHoraDia" runat="server" TextMode="Number"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;">Total Maximo Hora Extra Mes:</td>
                <td>
                    <asp:TextBox ID="txtTotalMaxHoraMes" runat="server" TextMode="Number"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;" class="auto-style3">Total Horas Folga Plantonista:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtTotalHorasFolga" runat="server" TextMode="Number"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td colspan="2" style="background:#f2f2f2">&nbsp;</td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td style="text-align: center;">
                    <div style="float: left">
                        <dx:ASPxButton ID="btnCancelar" runat="server"
                            ClientInstanceName="btnCancelar"
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cancelar"
                            Width="100px" Theme="iOS" ToolTip="Filtrar" CssClass="" OnClick="btCancelar_Click">
                        </dx:ASPxButton>
                    </div>
                </td>
                <td style="text-align: center;">
                    <div style="float: right">
                        <dx:ASPxButton ID="btnSalvar" runat="server"
                            ClientInstanceName="btnSalvar"
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar"
                            Width="100px" Theme="iOS" ToolTip="Filtrar" CssClass="" OnClick="btSalvar_Click">
                        </dx:ASPxButton>
                    </div>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

