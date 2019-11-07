<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmLancarFalta.aspx.cs" Inherits="Manutencao_frmLancarFalta" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        function PreenchecbUsuario() 
        {
            cbUsuario.PerformCallback();
        }
    </script>
    <style type="text/css">
    .style4
    {
        width: 79px;
    }
    .style5
    {
        font-size: medium;
    }
    .style6
    {
            width: 399px;
        }
        .style7
        {
            width: 368px;
        }
        .auto-style3
        {
            width: 2%;
            height: 35px;
        }
        .auto-style4
        {
            width: 153px;
        }
        .auto-style5
        {
            width: 151px;
        }
        .auto-style6
        {
            width: 415px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p class="style5">
        <table class="auto-style3">
            <tr>
                <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">LANÇAR FALTA</div> 

            </td>
            </tr>
            </table>
        <strong style="font-size: 16px; font-weight: normal;"><img alt="" class="auto-style3" src="../Images/Imagem34.png" />Lançar Falta</strong></p>
    <table class="style1" style="width: 1225px">
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style6" align="right">
                Setor:</td>
            <td class="style7" colspan="4">
                <dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetorLancaFalta" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" DataMember="TBSetor" 
                    DropDownStyle="DropDown" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor" 
                    ValueField="IDSetor" Width="600px" Spacing="0" EnableTheming="True" 
                    IncrementalFilteringMode="Contains" Theme="DevEx">
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
            <td class="auto-style6">
                <dx:ASPxCheckBox ID="cheqSetor" runat="server" CheckState="Unchecked" 
                    ClientInstanceName="cheqSetor" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Lançar para todo o setor selecionado">
                </dx:ASPxCheckBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style6" align="right">
                Usuário:</td>
            <td class="style7" colspan="4">
                <dx:ASPxComboBox ID="cbUsuario" runat="server" ClientInstanceName="cbUsuario" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    DataMember="vwNomeUsuario" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="Nome" 
                    ValueField="IDUsuario" Width="600px" oncallback="cbUsuario_Callback" 
                    DropDownStyle="DropDown" Spacing="0" IncrementalFilteringMode="Contains" 
                    Theme="DevEx">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                </dx:ASPxComboBox>
            </td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style6" align="right">
                Data:</td>
            <td class="style7" colspan="4">
                <dx:ASPxDateEdit ID="deData" runat="server" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="350px" 
                    Spacing="0" Theme="DevEx">
                    <CalendarProperties>
                        <HeaderStyle Spacing="1px" />
                    </CalendarProperties>
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxDateEdit>
            </td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style6" align="right">
                Observação:</td>
            <td class="style7" colspan="4">
                <dx:ASPxMemo ID="memoOBS" runat="server" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    Height="71px" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Width="600px" Theme="DevEx">
                    <ValidationSettings>
                        <RequiredField ErrorText="Adicione uma observação" IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxMemo>
            </td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style7" colspan="2">
                &nbsp;</td>
            <td class="auto-style4">
                &nbsp;</td>
            <td class="auto-style5">
                &nbsp;</td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style7" align="right">
                            <dx:ASPxButton ID="btSalvar" runat="server" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                onclick="btSalvar_Click" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar" 
                                Width="106px" ValidationGroup="ValidaGrupo" Theme="iOS" ToolTip="Salvar">
                            </dx:ASPxButton>
                        </td>
            <td class="auto-style4" align="left">
                            <dx:ASPxButton ID="btVoltar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                                style="margin-left: 0px" Text="Voltar" Width="101px" 
                                onclick="btVoltar_Click" Theme="iOS" ToolTip="Voltar">
                            </dx:ASPxButton>
                        </td>
            <td class="auto-style4" align="right">
                &nbsp;</td>
            <td class="auto-style5">
                &nbsp;</td>
            <td class="auto-style6">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

