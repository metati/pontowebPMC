<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmRelFaltaInjustificada.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmRelregistroAusente" %><%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style4
        {
            width: 84px;
        }
        .style5
        {
            width: 98px;
        }
        .auto-style3
        {
            width: 28px;
            height: 26px;
        }
        .auto-style4
        {
            height: 25px;
        }
        .auto-style5
        {
            width: 438px;
        }
        .auto-style6
        {
            width: 616px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DE RELAÇÃO DE FALTAS</div> 

            </td>
        </tr>
        <tr>
            <td>
                <img alt="" class="auto-style3" src="../../Images/Imagem44.png" /> Relação de faltas</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="center" class="auto-style4">
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="auto-style5" align="right">
                Setor:</td>
            <td align="left">
                <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                    TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbRelSetorFaltaInjustificada" 
                    ID="cbRelSetorFaltaInjustificada">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {preencheCBRELUsuarioInjustificada();}" />  
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
            <td class="auto-style5" align="right">
                Servidor:</td>
            <td align="left">
                                    <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="Nome" ValueField="IDUsuario" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="vwNomeUsuario" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbRelUsuarioFaltaInjustificada" ID="cbRelUsuarioFaltaInjustificada" 
                    DropDownStyle="DropDown" OnCallback="cbRelUsuarioFaltaInjustificada_Callback">
<LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

<LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

</dx:ASPxComboBox>


            </td>
        </tr>
        <tr>
            <td class="auto-style5" align="right">
                Data Inicial:</td>
            <td align="left">
                            <dx:ASPxDateEdit ID="deReldtInicioFaltaInjustificada" 
                    runat="server" ClientInstanceName="deReldtInicioFaltaInjustificada" 
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
            <td class="auto-style5" align="right">
                Data Final:</td>
            <td align="left">
                            <dx:ASPxDateEdit ID="deReldtFimFaltaInjustificada" runat="server" ClientInstanceName="deReldtFimFaltaInjustificada" 
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
            <td class="auto-style5" align="right">
                Relatório:</td>
            <td align="left">
                            <dx:ASPxRadioButtonList ID="rbTipoRelFaltaInjustificada" runat="server" EnableTheming="True" Theme="DevEx" Width="168px" SelectedIndex="0" ToolTip="Selecione para escolher o tipo do relatório" ClientInstanceName="rbTipoRelFaltaInjustificada">
                                <Items>
                                    <dx:ListEditItem Text="Falta Injustificada" Value="0" Selected="True" />
                                    <dx:ListEditItem Text="Registro Ausente" Value="1" />
                                </Items>
                            </dx:ASPxRadioButtonList>
                            <dx:ASPxHiddenField ID="coRelFalta" runat="server">
                            </dx:ASPxHiddenField>
                            <dx:ASPxHiddenField ID="coRelFaltaUsuario" runat="server">
                            </dx:ASPxHiddenField>
                            </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        function MudaPagina() {
            var Ano, hoje;

            hoje = new Date();

            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=0&Setor=' + coRelFalta.Get("IDSetor") + '&DiaIni=' + deReldtInicioFaltaInjustificada.GetText() + '&DiaFim=' + deReldtFimFaltaInjustificada.GetText() + '&User=' + coRelFaltaUsuario.Get("IDUsuario") + '&Ano=' + Ano + '&Ausencia=' + rbTipoRelFaltaInjustificada.GetValue() + '&Rel=frmfi');
        }

        function preencheCBRELUsuarioInjustificada()
        {
            cbRelUsuarioFaltaInjustificada.PerformCallback(cbRelSetorFaltaInjustificada.GetValue());
        }
    </script>
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="auto-style6" align="right">
                <dx:ASPxButton ID="btOkFaltaInjustificada" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok" 
                            Width="100px" 
                            ValidationGroup="ValidaGrupo" AutoPostBack="False" 
                            ClientInstanceName="btOkFaltaInjustificada" Theme="iOS" ToolTip="Ok">
                    <ClientSideEvents Click="function(s, e) {}" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btCancelDia" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                            CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                            Width="100px" onclick="btVoltar_Click" 
                    ClientInstanceName="btCancelFaltaInjustificada" Theme="iOS" ToolTip="Voltar">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
</asp:Content>

