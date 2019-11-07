<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmRelAusenteInjust.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmAusenteInjust" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 145px;
        }
        .style2
        {
            width: 98px;
        }
        .style4
        {
            width: 8%;
        }
        .auto-style1 {
            width: 10%;
        }
        .auto-style3
        {
            width: 26px;
            height: 23px;
        }
        .auto-style4
        {
            width: 442px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DE FALTAS E AUSÊNCIA DE MARCAÇÃO</div> 

            </td>
        </tr>
        <tr>
            <td colspan="2">
                <img alt="" class="auto-style3" src="../../Images/Imagem5.png" />Relação de falta/registro&nbsp; ausencia</td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
    <p>
        <table class="dxflInternalEditorTable_DevEx" style="width: 122%">
            <tr>
                <td class="auto-style4" align="right">
                    Setor:</td>
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
                <td class="auto-style4" align="right">
                    Servidor:</td>
                <td align="left">
                                    <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                    TextField="Nome" ValueField="IDUsuario" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="vwNomeUsuario" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbServidorAusenteInjust" ID="cbServidorAusenteInjust" 
                    oncallback="cbUsuario_Callback" DropDownStyle="DropDown" EnableSynchronization="False">
<LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

<LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                                        <DropDownButton ToolTip="Filtrar por servidor">
                                        </DropDownButton>

<ValidationSettings ValidationGroup="ValidaGrupo">
</ValidationSettings>
</dx:ASPxComboBox>


                </td>
            </tr>
            <tr>
                <td class="auto-style4" align="right">
                    Data Início</td>
                <td align="left">
                                    <dx:ASPxDateEdit ID="dtAusenteInjustInicio" runat="server" ClientInstanceName="dtAusenteInjustInicio" Theme="DevEx" Width="300px">
                                    </dx:ASPxDateEdit>


                </td>
            </tr>
            <tr>
                <td class="auto-style4" align="right">
                    Data Final</td>
                <td align="left">
                                    <dx:ASPxDateEdit ID="dtAusenteInjustFim" runat="server" ClientInstanceName="dtAusenteInjustFinal" Theme="DevEx" Width="300px">
                                    </dx:ASPxDateEdit>


                </td>
            </tr>
            <tr>
                <td class="auto-style4" align="right">
                    Relatório:</td>
                <td align="left">
                                    <dx:ASPxRadioButtonList ID="rbRelAusenteInjust" runat="server" 
                                        ClientInstanceName="rbRelAusenteInjust" RepeatDirection="Horizontal" SelectedIndex="0" 
                                        Theme="DevEx">
                                        <Items>
                                            <dx:ListEditItem Text="Falta injustificada" Value="0" Selected="True" />
                                            <dx:ListEditItem Text="Ausência de registro" Value="1" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>


                </td>
            </tr>
        </table>
        <table class="dxflInternalEditorTable_DevEx">
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td>
                    <dx:ASPxHiddenField ID="coIDUsuarioAusenteInjust" runat="server" 
                        ClientInstanceName="coIDUsuarioAusenteInjust">
                    </dx:ASPxHiddenField>
                </td>
            </tr>
        </table>
    </p>
            </td>
        </tr>
        <tr>
            <td align="right">
                <dx:ASPxButton ID="btRelAusenteInjust" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok" 
                            Width="100px" 
                            ValidationGroup="ValidaGrupo" AutoPostBack="False" 
                            ClientInstanceName="btRelAusenteInjust" 
                    onclick="btrelaBancohora_Click" Theme="iOS" ToolTip="Ok">
                    <ClientSideEvents Click="function(s, e) {
}" />
                </dx:ASPxButton>
                </td>
            <td align="left">
                <dx:ASPxButton ID="btVoltar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                            CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                            Width="100px" onclick="btVoltar_Click" Theme="iOS" ToolTip="Voltar">
                </dx:ASPxButton>
                    </td>
        </tr>
    </table>
    <script type="text/javascript">

        function MudaPagina() {

            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Setor=' + coIDUsuarioAusenteInjust.Get("IDSetor") + '&Situ=' + rbRelAusenteInjust.GetValue() + '&User=' + coIDUsuarioAusenteInjust.Get("IDUsuario") + '&DTInicio=' + dtAusenteInjustInicio.GetText() + '&DTFinal=' + dtAusenteInjustFinal.GetText() + '&Ano=0 &Rel=frmfaltaInjust');
        }

        function PreenchecbUsuario() {
            cbServidorAusenteInjust.PerformCallback();
        }

    </script>
    </asp:Content>

