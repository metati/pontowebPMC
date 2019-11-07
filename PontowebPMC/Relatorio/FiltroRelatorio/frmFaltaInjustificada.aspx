﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmFaltaInjustificada.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmFaltaInjustificada" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style4
        {
            font-weight: normal;
            width: 84px;
        }
        .style5
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        function MudaPagina() {
            var Ano, hoje;

            hoje = new Date();

            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Setor=' + coFaltaInjustificada.Get("IDSetor") + '&DiaIni=' + deDataInicioFaltaInjustificada.GetText() + '&DiaFim=' + deDataFimFaltaInjustificada.GetText() + '&User=' + coFaltaInjustificada.Get("IDVinculoUsuario") + '&Rel=frmfi');
        }

        function PreenchecbUsuarioRelacaoPontoDia() {
            cbUsuarioFaltaInjustificada.PerformCallback(cbSetorFaltaInjustificada.GetValue());
        }
    </script>
    <p>
        Relação de faltas injustificadas<table class="dxflInternalEditorTable_DevEx">
            <tr>
                <td class="style4">
                    Setor:</td>
                <td>
                <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbSetorFaltaInjustificada" ID="cbSetorFaltaInjustificada">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
PreenchecbUsuarioRelacaoPontoDia();
}" />  
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <RequiredField IsRequired="True">
                        </RequiredField>
                    </ValidationSettings>
                </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Servidor:</td>
                <td>
                                    <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="Nome" ValueField="IDUsuario" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="vwNomeUsuario" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbUsuarioFaltaInjustificada" ID="cbUsuarioFaltaInjustificada" 
                                        oncallback="cbUsuarioRelacaoPontoDia_Callback" 
                    DropDownStyle="DropDown">
<LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

<LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

</dx:ASPxComboBox>


                </td>
            </tr>
            <tr>
                <td class="style4">
                    Data inicial:</td>
                <td>
                            <dx:ASPxDateEdit ID="deDataInicioFaltaInjustificada" runat="server" ClientInstanceName="deDataInicioFaltaInjustificada" 
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
                <td class="style4">
                    Data Final:</td>
                <td>
                            <dx:ASPxDateEdit ID="deDataFimFaltaInjustificada" runat="server" ClientInstanceName="deDataFimFaltaInjustificada" 
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
                            <dx:ASPxHiddenField ID="coFaltaInjustificada" runat="server" 
                        ClientInstanceName="coFaltaInjustificada">
                    </dx:ASPxHiddenField>
                </td>
            </tr>
        </table>
        <table class="dxflInternalEditorTable_DevEx">
            <tr>
                <td class="style5">
                <dx:ASPxButton ID="btFaltaInjustificada" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok" 
                            Width="100px" 
                            ValidationGroup="ValidaGrupo" AutoPostBack="False" 
                            ClientInstanceName="btFaltaInjustificada" onclick="btRelacaoDia_Click">
                    <ClientSideEvents Click="function(s, e) {
}" />
                </dx:ASPxButton>
                </td>
                <td>
                <dx:ASPxButton ID="btCancelaFaltaInjustificada" runat="server" 
                        CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                            CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                            Width="100px" onclick="btVoltar_Click" 
                    ClientInstanceName="btCancelaFaltaInjustificada">
                </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
