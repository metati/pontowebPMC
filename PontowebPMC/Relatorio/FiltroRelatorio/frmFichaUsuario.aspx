<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmFichaUsuario.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmFichaUsuario" %>

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
        .auto-style3
        {
            width: 426px;
        }
        .auto-style4
        {
            width: 25px;
            height: 23px;
        }
        .auto-style5
        {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">RELATÓRIO DE FICHA DE COLABORADORES</div> 

            </td>
        </tr>
        <tr>
            <td colspan="2">
                <img alt="" class="auto-style4" src="../../Images/RImagem49.png" /> Ficha de servidores</td>
        </tr>
        <tr>
            <td align="center" colspan="2"><table class="dxflInternalEditorTable_DevEx">
            <tr>
                <td class="auto-style3" align="right">
                    Setor:</td>
                <td align="left">
                                    <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                    TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbSetorFichaServidor" ID="cbSetorFolha">
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
                <td class="auto-style3" align="right">
                    Servidor:</td>
                <td align="left">
                                    <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                    TextField="Nome" ValueField="IDUsuario" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="vwNomeUsuario" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbUsuarioFichaServidor" ID="cbUsuarioBancoHora" 
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
                <td class="auto-style3" align="right">
                    Filtrar situação:</td>
                <td align="left">
                                    <dx:ASPxRadioButtonList ID="rbSituRel" runat="server" 
                                        ClientInstanceName="rbSituRel" RepeatDirection="Horizontal" SelectedIndex="0" 
                                        Theme="DevEx">
                                        <Items>
                                            <dx:ListEditItem Selected="True" Text="Nenhum" Value="0" />
                                            <dx:ListEditItem Text="Ativados" Value="1" />
                                            <dx:ListEditItem Text="Desativados" Value="2" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>


                </td>
            </tr>
        </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style5" colspan="2"></td>
        </tr>
        <tr>
            <td align="right">
                <dx:ASPxButton ID="btrelaBancohora" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok" 
                            Width="100px" 
                            ValidationGroup="ValidaGrupo" AutoPostBack="False" 
                            ClientInstanceName="btrelaBancohora" 
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

            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Setor=' + coIDUsuarioServidorFicha.Get("IDSetor") + '&Situ=' + rbSituRel.GetValue() + '&User=' + coIDUsuarioServidorFicha.Get("IDUsuario") + '&Ano=0 &Rel=frmfca');
        }

        function PreenchecbUsuario() {
            cbUsuarioFichaServidor.PerformCallback();
        }

    </script>
    <p>
        <table class="dxflInternalEditorTable_DevEx">
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    <dx:ASPxHiddenField ID="coIDUsuarioServidorFicha" runat="server" 
                        ClientInstanceName="coIDUsuarioServidorFicha">
                    </dx:ASPxHiddenField>
                </td>
            </tr>
        </table>
    </p>
</asp:Content>

