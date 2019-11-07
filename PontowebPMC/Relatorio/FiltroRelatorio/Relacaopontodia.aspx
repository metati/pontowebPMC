<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Relacaopontodia.aspx.cs" Inherits="Relatorio_FiltroRelatorio_Relacaopontodia" %>

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

        .auto-style7 {
            width: 47%;
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
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DE RELAÇÃO DE MARCAÇÃO DE PONTO POR PERÍODO</div>

            </td>
        </tr>
        <tr>
            <td colspan="2">
                <img alt="" class="auto-style9" src="../../Images/RImagem50.png" />Marcação de frequência por dia de referência</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <table class="dxflInternalEditorTable_DevEx">
                    <tr>
                        <td class="auto-style3" align="right">Setor:</td>
                        <td align="left">
                            <dx:ASPxComboBox runat="server"
                                EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains"
                                TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px"
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor"
                                CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                ClientInstanceName="cbSetorRelacaoDia" ID="cbRelacaoDia">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
PreenchecbUsuarioRelacaoPontoDia();
}" />
                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <ValidationSettings ValidationGroup="ValidaGrupo">
                                    <RequiredField IsRequired="True"></RequiredField>
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3" align="right">Servidor:</td>
                        <td align="left">
                            <dx:ASPxComboBox runat="server"
                                EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains"
                                TextField="DSUsuario" ValueField="IDVinculoUsuario" Spacing="0" Width="400px"
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="vwNomeUsuario"
                                CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                ClientInstanceName="cbUsuarioRelacaoPontoDia" ID="cbUsuarioRelacaoPontoDia"
                                OnCallback="cbUsuarioRelacaoPontoDia_Callback"
                                DropDownStyle="DropDown">
                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

                                <LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                            </dx:ASPxComboBox>


                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3" align="right">Data Inicial:</td>
                        <td align="left">
                            <dx:ASPxDateEdit ID="deDataRelacao" runat="server" ClientInstanceName="deDataRelacaoDia"
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
                        <td class="auto-style3" align="right">Data Final:</td>
                        <td align="left">
                            <dx:ASPxDateEdit ID="deDataRelacao0" runat="server" ClientInstanceName="deDataRelacaoDiaFinal"
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
                        <td align="left">
                            <dx:ASPxCheckBox ID="cbFaltosos"  runat="server" ClientInstanceName="cbFaltosos"
                                Text="Filtrar por ausência de registros" Theme="DevEx">
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <dx:ASPxButton ID="btRelacaoDia" runat="server"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok"
                    Width="103px"
                    ValidationGroup="ValidaGrupo" AutoPostBack="False"
                    ClientInstanceName="btRelacaoDia" OnClick="btRelacaoDia_Click" Theme="iOS" ToolTip="Ok">
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
            debugger;
            var Ano, hoje, usuario, usuarioVinculo;

            hoje = new Date();
            //if (coidusuariorelacaopontodiav.get("idusuario") == "")
            //usuario = coidusuariorelacaopontodiav.get("idusuario");
            //usuariovinculo = coidusuariorelacaopontodia.get("idvinculousuario");

                window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=0&Setor=' + coRelacaoPontoDia.Get("IDSetor") + '&DiaIni=' + deDataRelacaoDia.GetText() + '&DiaFim=' + deDataRelacaoDiaFinal.GetText() + '&User=' + coIDUsuarioRelacaoPontoDiaV.Get("IDUsuario") + '&Ano=' + Ano + '&Ausencia=' + cbFaltosos.GetValue() + '&IDVinculoUsuario=' + coIDUsuarioRelacaoPontoDia.Get("IDVinculoUsuario") + '&Rel=frmRelacDia');
        }

        function PreenchecbUsuarioRelacaoPontoDia() {
            cbUsuarioRelacaoPontoDia.PerformCallback(cbSetorRelacaoDia.GetValue());
        }

    </script>
    <table class="dxflInternalEditorTable_DevEx" style="height: 1px; width: 99%">
        <tr>
            <td>
                <dx:ASPxHiddenField ID="coRelacaoPontoDia" runat="server"
                    ClientInstanceName="coRelacaoPontoDia">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="coIDUsuarioRelacaoPontoDia" runat="server"
                    ClientInstanceName="coIDUsuarioRelacaoPontoDia">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="coIDUsuarioRelacaoPontoDiaV" runat="server"
                    ClientInstanceName="coIDUsuarioRelacaoPontoDiaV">
                </dx:ASPxHiddenField>
                <asp:HiddenField ID="IDUsuario" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

