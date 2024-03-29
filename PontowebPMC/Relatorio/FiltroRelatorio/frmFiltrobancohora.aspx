﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmFiltrobancohora.aspx.cs" Inherits="Relatorio_FiltroRelatorio_frmFiltrobancohora" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .style2
        {
            width: 72px;
        }
        
.dxeButtonEditButton_DevEx
{
	vertical-align: middle;
	cursor: pointer;
}
        .dxeButtonEditButton_DevEx
{
	border-style: solid;
	border-color: Transparent;
	-border-color: White;
}

.dxeButtonEditButton_DevEx
{
	border-top-width: 0;
	border-right-width: 0;
	border-bottom-width: 0;
	border-left-width: 1px;
}


.dxeButtonEditButton_DevEx
{
	background: White none;
}

        .style3
        {
            width: 99px;
        }
        .style4
        {
            width: 6%;
        }
    .style5
    {
        width: 93px;
        height: 58px;
    }
    .style6
    {
        height: 58px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        function MudaPagina() {
            var Ano, hoje;

            if (rbAnoFiltroBancoHora.GetValue() == "Ano Corrente") {

                Ano = 0;
            }
            else
                Ano = 1;

            hoje = new Date();

            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=' + cbMesBancoHoraMes.GetValue() + '&Setor=' + coIDSetorBancoo.Get("IDSetor") + '&Ano=' + Ano + '&Rel=frmbcoHoraMes')
        }
    </script>
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="style4">
                Setor:</td>
            <td>
                <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbSetorBancohoraMes" ID="cbSetorFolha">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuario();
}" />
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
}">
                    </ClientSideEvents>
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
                Mês ref:</td>
            <td>
                                    <dx:ASPxComboBox runat="server" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                    TextField="DSMes" ValueField="IDMes" Spacing="0" Width="400px" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBMes" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    ClientInstanceName="cbMesBancoHoraMes" ID="cbMes">
<LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

<LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

<ValidationSettings ValidationGroup="ConfirmaBusca">
<RequiredField IsRequired="True"></RequiredField>
</ValidationSettings>
</dx:ASPxComboBox>


            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td>
                                    <dx:ASPxRadioButtonList ID="rbAno" runat="server" 
                                        Theme="DevEx" RepeatDirection="Horizontal" 
                                        ClientInstanceName="rbAnoFiltroBancoHora" SelectedIndex="0">
                                        <Items>
                                            <dx:ListEditItem Text="Ano Corrente" Value="Ano Corrente" Selected="True" />
                                            <dx:ListEditItem Text="Ano Anterior" Value="Ano Anterior" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>


            </td>
        </tr>
    </table>
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td class="style5">
                <dx:ASPxButton ID="btrelasituacao" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ok" 
                            Width="100px" 
                            ValidationGroup="ValidaGrupo" AutoPostBack="False" 
                            ClientInstanceName="btrelBancoHoraMes" 
                    onclick="btrelasituacao_Click">
                    <ClientSideEvents Click="function(s, e) {
}" />
                </dx:ASPxButton>
            </td>
            <td class="style6">
                <dx:ASPxButton ID="btVoltar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                            CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                            Width="100px" onclick="btVoltar_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
                <dx:ASPxHiddenField ID="coIDSetorBancoo" runat="server" 
                    ClientInstanceName="coIDSetorBancoo">
                </dx:ASPxHiddenField>
            </asp:Content>

