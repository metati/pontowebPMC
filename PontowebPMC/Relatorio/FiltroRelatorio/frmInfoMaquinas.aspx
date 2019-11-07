<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmInfoMaquinas.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmInfoMaquinas" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .dxeButtonEditButton_DevEx,
        .dxeSpinLargeIncButton_DevEx,
        .dxeSpinLargeDecButton_DevEx,
        .dxeSpinIncButton_DevEx,
        .dxeSpinDecButton_DevEx {
            background: White none;
        }

        .dxeButtonEditButton_DevEx {
            border-top-width: 0;
            border-right-width: 0;
            border-bottom-width: 0;
            border-left-width: 1px;
        }

        .dxeButtonEditButton_DevEx,
        .dxeButtonEdit_DevEx .dxeSBC {
            border-style: solid;
            border-color: Transparent;
            -border-color: White;
        }

        .dxeButtonEditButton_DevEx,
        .dxeCalendarButton_DevEx,
        .dxeSpinIncButton_DevEx,
        .dxeSpinDecButton_DevEx,
        .dxeSpinLargeIncButton_DevEx,
        .dxeSpinLargeDecButton_DevEx {
            vertical-align: middle;
            cursor: pointer;
        }


        .dxeButtonEditButton_DevEx {
            background: White none;
        }

        .dxeButtonEditButton_DevEx {
            border-top-width: 0;
            border-right-width: 0;
            border-bottom-width: 0;
            border-left-width: 1px;
        }


        .dxeButtonEditButton_DevEx {
            border-style: solid;
            border-color: Transparent;
            -border-color: White;
        }

        .dxeButtonEditButton_DevEx {
            vertical-align: middle;
            cursor: pointer;
        }

        .style2 {
            width: 97px;
        }

        .style4 {
            width: 7%;
        }

        .style5 {
            width: 6%;
        }

        .auto-style3 {
            width: 36%;
        }

        .auto-style8 {
            width: 47%;
        }

        .auto-style9 {
            width: 23px;
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DE AUDITORIA DE LOGS DAS MÁQUINAS</div>

            </td>
        </tr>
        <tr>
            <td colspan="2">
                </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <table class="dxflInternalEditorTable_DevEx">
                    <tr>
                        <td class="auto-style3" align="right">&nbsp;</td>
                        <td align="left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3" align="right">Setor:</td>
                        <td align="left">
                            <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px"
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor"
                                CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                ClientInstanceName="cbSetorAusenteInjust" ID="cbSetorAusenteInjust">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuario();
}" />
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuario();
}"></ClientSideEvents>

                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

                                <LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                                <ValidationSettings ValidationGroup="ValidaGrupo">
                                    <RequiredField IsRequired="True"></RequiredField>
                                </ValidationSettings>
                            </dx:ASPxComboBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3" align="right">Data Justificada Inicial:</td>
                        <td align="left">
                            <dx:ASPxDateEdit ID="deDataRelacao" runat="server" ClientInstanceName="deIniAuditRel"
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
                        <td class="auto-style3" align="right">Data Justificada Final:</td>
                        <td align="left">
                            <dx:ASPxDateEdit ID="deDataRelacao0" runat="server" ClientInstanceName="deFimAuditRel"
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
                        <td class="auto-style3" align="right">&nbsp;</td>
                        <td align="left">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <dx:ASPxButton ID="btJustiAudit" runat="server"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok"
                    Width="103px"
                    ValidationGroup="ValidaGrupo" AutoPostBack="False"
                    ClientInstanceName="btJustiAudit" OnClick="btRelacaoDia_Click" Theme="iOS" ToolTip="Ok">
                    <ClientSideEvents Click="function(s, e) {}" />
                </dx:ASPxButton>
            </td>
            <td align="left" class="auto-style8">
                <dx:ASPxButton ID="ASPxButton1" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar"
                    Width="100px" OnClick="btVoltar_Click"
                    ClientInstanceName="btCancelDia" Theme="iOS" ToolTip="Voltar">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>

    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DAS MÁQUINAS INSTALADAS</div>

            </td>
        </tr>
        <tr>
            <td colspan="2">
                </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <table class="dxflInternalEditorTable_DevEx">
                    <tr>
                        <td class="auto-style3" align="right">&nbsp;</td>
                        <td align="left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3" align="right">Setor:</td>
                        <td align="left">
                            <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px"
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor"
                                CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                ClientInstanceName="cbSetorAusenteInjust2" ID="cbSetorAusenteInjust2">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuario2();
}" />
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuario2();
}"></ClientSideEvents>

                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

                                <LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                                <ValidationSettings ValidationGroup="ValidaGrupo">
                                    <RequiredField IsRequired="True"></RequiredField>
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                  
                    <tr>
                        <td class="auto-style3" align="right">&nbsp;</td>
                        <td align="left">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <dx:ASPxButton ID="btMaquinasInstaladas" runat="server"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok"
                    Width="103px"
                    ValidationGroup="ValidaGrupo" AutoPostBack="False"
                    ClientInstanceName="btMaquinasInstaladas" OnClick="btMaquinasInstaladas_Click" Theme="iOS" ToolTip="Ok">
                    <ClientSideEvents Click="function(s, e) {}" />
                </dx:ASPxButton>
            </td>
            <td align="left" class="auto-style8">
                <dx:ASPxButton ID="btCancelDia" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar"
                    Width="100px" OnClick="btVoltar_Click"
                    ClientInstanceName="btCancelDia" Theme="iOS" ToolTip="Voltar">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        function MudaPagina() {
            var Ano, hoje;

            hoje = new Date();

            window.open('/Relat/Relatorio/ws/frmVizualizaRelatorio.aspx?Mes=0' + '&DiaIni=' + deIniAuditRel.GetText() + '&DiaFim=' + deFimAuditRel.GetText() + '&&IDSetor=1&Emp=54&Rel=frmLogMaqRel');
        }

        function PreenchecbUsuarioRelacaoPontoDia() {
            cbUsuarioRelacaoPontoDia.PerformCallback(cbSetorRelacaoDia.GetValue());
        }


        function GetMaquinas() {
            var Ano, hoje;

            hoje = new Date();

            window.open('/Relat/Relatorio/ws/frmVizualizaRelatorio.aspx?Mes=0' + '&DiaIni=' + deIniAuditRel.GetText() + '&DiaFim=' + deFimAuditRel.GetText() + '&&IDSetor=1&Emp=54&Rel=frmInfoMaqRel');
        }

        function PreenchecbUsuarioRelacaoPontoDia() {
            cbUsuarioRelacaoPontoDia.PerformCallback(cbSetorRelacaoDia.GetValue());
        }
    </script>
    <table class="dxflInternalEditorTable_DevEx" style="height: 1px; width: 99%">
        <tr>
            <td>
                <dx:ASPxHiddenField ID="coRelJustAudit" runat="server"
                    ClientInstanceName="coRelJustAudit">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="coIDUsuarioRelJustAudit" runat="server"
                    ClientInstanceName="coIDUsuarioRelJustAudit">
                </dx:ASPxHiddenField>
            </td>
        </tr>
    </table>
</asp:Content>

