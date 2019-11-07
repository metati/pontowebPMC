﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmRelFrequencia.aspx.cs" Inherits="Relatorio_frmRelFrequencia" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        function AtualizaComboUsuario() {

            cbUsuario.PerformCallback(cbSetor.GetValue());
            cbUsuario.SetValue('Todos os usuários');

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            font-size: medium;
        }
        .style3
        {
            width: 90px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="style1">
        <tr>
            <td class="style2">
                <strong>Relatório de Frequência Mensal</strong></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetor" 
                    CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" DataMember="TBSetor" 
                    CssPostfix="Office2003Olive" 
                    SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" 
                    TextField="DSSetor" ValueField="IDSetor" 
                    LoadingPanelText="Processando&amp;hellip;" Width="300px" 
                    IncrementalFilteringMode="StartsWith">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	AtualizaComboUsuario();
}" />
                    <LoadingPanelImage Url="~/App_Themes/Office2003Olive/Web/Loading.gif">
                    </LoadingPanelImage>
                    <ButtonStyle Width="13px">
                    </ButtonStyle>
                    <ValidationSettings CausesValidation="True" ValidationGroup="ValidaFiltro">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dx:ASPxComboBox ID="cbUsuario" runat="server" ClientInstanceName="cbUsuario" 
                    CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" DataMember="vwNomeUsuario" 
                    CssPostfix="Office2003Olive" 
                    SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" 
                    TextField="Nome" ValueField="IDUsuario" 
                    LoadingPanelText="Processando&amp;hellip;" Width="300px" 
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" 
                    oncallback="cbUsuario_Callback" DropDownStyle="DropDown">
                    <LoadingPanelImage Url="~/App_Themes/Office2003Olive/Web/Loading.gif">
                    </LoadingPanelImage>
                    <ButtonStyle Width="13px">
                    </ButtonStyle>
                    <ValidationSettings CausesValidation="True" ValidationGroup="ValidaFiltro">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dx:ASPxComboBox ID="cbMes" runat="server" ClientInstanceName="cbMes" 
                    CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" DataMember="TBMes" 
                    CssPostfix="Office2003Olive" 
                    SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" 
                    TextField="DSMes" ValueField="IDMes" Width="300px" 
                    LoadingPanelText="Processando&amp;hellip;" DropDownStyle="DropDown" 
                    IncrementalFilteringMode="StartsWith">
                    <LoadingPanelImage Url="~/App_Themes/Office2003Olive/Web/Loading.gif">
                    </LoadingPanelImage>
                    <ButtonStyle Width="13px">
                    </ButtonStyle>
                    <ValidationSettings CausesValidation="True" ValidationGroup="ValidaFiltro">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="style3">
                <dx:ASPxButton ID="btOk" runat="server" 
                    CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" 
                    CssPostfix="Office2003Olive" onclick="btOk_Click" 
                    SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" Text="Ok" 
                    ValidationGroup="ValidaFiltro" Width="100px">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btVoltar" runat="server" 
                    CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" 
                    CssPostfix="Office2003Olive" onclick="btVoltar_Click" 
                    SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" Text="Voltar" 
                    ValidationGroup="ValidaFiltro" Width="100px">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
</asp:Content>
