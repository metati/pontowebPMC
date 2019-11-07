﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLancaFalta.aspx.cs" Inherits="Manutencao_frmLancaFalta" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script type="text/javascript">
            function PreenchecbUsuario() {
                cbUsuario.PerformCallback();
            }
    </script>
    <title></title>
    <style type="text/css">

        .style1
        {
            width: 100%;
        }
        .style5
        {
            font-size: small;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style6
        {
            width: 96px;
        }
        .style7
        {
            width: 77px;
            font-size: small;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style8
        {
            width: 77px;
        }
        .style9
        {
            width: 297px;
            margin-left: 40px;
        }
    .dxICheckBox_Youthful 
{
	display: inline-block;
	margin: auto;
	vertical-align: middle;
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="style1">
        <tr>
            <td class="style5" colspan="3">
                <strong>Lançar Faltas</strong></td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;</td>
            <td class="style9">
                <dx:ASPxCheckBox ID="cheqSetor" runat="server" CheckState="Unchecked" 
                    ClientInstanceName="cheqSetor" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Lançar para todo o setor selecionado">
                </dx:ASPxCheckBox>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7">
                Setor:</td>
            <td class="style9">
                <dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetor" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" DataMember="TBSetor" 
                    DropDownStyle="DropDown" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor" 
                    ValueField="IDSetor" Width="300px" Spacing="0" 
                    IncrementalFilteringMode="StartsWith">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreenchecbUsuario();
}" />
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7">
                Usuário:</td>
            <td class="style9">
                <dx:ASPxComboBox ID="cbUsuario" runat="server" ClientInstanceName="cbUsuario" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    DataMember="vwNomeUsuario" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="Nome" 
                    ValueField="IDUsuario" Width="300px" oncallback="cbUsuario_Callback" 
                    DropDownStyle="DropDown" Spacing="0" IncrementalFilteringMode="StartsWith">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                </dx:ASPxComboBox>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7">
                Motivo Falta:</td>
            <td class="style9">
                <dx:ASPxComboBox ID="cbMotivoFalta" runat="server" ClientInstanceName="cbMotivoFalta" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" DataMember="TBMotivoFalta" 
                    DropDownStyle="DropDown" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSMotivoFalta" 
                    ValueField="IDMotivoFalta" Width="300px" Spacing="0" 
                    IncrementalFilteringMode="StartsWith">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7">
                Data:</td>
            <td class="style9">
                <dx:ASPxDateEdit ID="deDataFalta" runat="server" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px" 
                    Spacing="0">
                    <CalendarProperties ClearButtonText="Deletar" TodayButtonText="Hoje">
                        <HeaderStyle Spacing="1px" />
                    </CalendarProperties>
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxDateEdit>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7">
                Observação:</td>
            <td class="style9">
                <dx:ASPxMemo ID="memoOBS" runat="server" 
                    CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                    Height="36px" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                    Width="300px">
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxMemo>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td class="style9">
                <table class="style1">
                    <tr>
                        <td class="style6">
                            <dx:ASPxButton ID="btSalvar" runat="server" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                onclick="btSalvar_Click" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar" 
                                Width="100px" ValidationGroup="ValidaGrupo">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <div>
    
    </div>
    </form>
</body>
</html>