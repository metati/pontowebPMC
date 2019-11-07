<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmGeraFolhaFrequencia.aspx.cs" Inherits="Manutencao_frmGeraFolhaFrequencia" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function PreenchecbUsuario() {
            cbUsuarioFolha.PerformCallback();
        }
        function MudaPagina() {
            var Ano, hoje;

            var idusuario = coIDUsuarioSetorFFrequencia.Get("IDSetor");

            hoje = new Date();
            if (rbAno.GetValue() == "Ano Corrente") {

                Ano = 0;
            }
            else
                Ano = 1;

            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=' + cbMes.GetValue() + '&Setor=' + coIDUsuarioSetorFFrequencia.Get("IDSetor") + '&cbOrgaoFolha=0' + '&User=' + coIDUsuarioSetorFFrequenciaV.Get("IDUsuario") + '&Ano=' + Ano + '&IDVinculoUsuario=' + coIDUsuarioSetorFFrequencia.Get("IDVinculoUsuario") + '&Rel=frmZurel')
        }
    </script>
    <style type="text/css">
        .style4 {
            font-size: large;
        }

        .style5 {
            width: 81px;
        }

        .style6 {
            width: 65px;
        }

        .style7 {
            width: 316px;
        }

        .style8 {
            width: 62px;
        }

        .auto-style3 {
            width: 2%;
            height: 24px;
        }

        .auto-style4 {
            font-size: large;
            height: 35px;
        }

        .auto-style5 {
            width: 117px;
        }

        .auto-style6 {
            width: 100%;
        }

        .auto-style7 {
            width: 121px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="auto-style3">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">&nbsp;ESPELHO DE PONTO</div>

            </td>
        </tr>
        <tr>
            <td style="color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <strong style="color: #666666; font-size: 16px; font-weight: normal;">
                    <img alt="" class="auto-style3" src="../Images/Imagem15.png" />Gerar espelho de ponto</strong></td>
        </tr>
    </table>
    <table class="style1" style="width: 1225px">
        <tr>
            <td class="auto-style4" colspan="3"></td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="style7" align="right">Mês:</td>
            <td>
                <dx:ASPxComboBox runat="server"
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains"
                    TextField="DSMes" ValueField="IDMes" Spacing="0" Width="400px"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBMes"
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    ClientInstanceName="cbMes" ID="cbMes">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

                    <LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                    <ValidationSettings ValidationGroup="ConfirmaBusca">
                        <RequiredField IsRequired="True"></RequiredField>
                    </ValidationSettings>
                </dx:ASPxComboBox>


            </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="style7" align="right">Setor:</td>
            <td>
                <dx:ASPxComboBox runat="server"
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains"
                    TextField="DSSetor" ValueField="IDSetor" Spacing="0" Width="400px"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBSetor"
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    ClientInstanceName="cbSetorFolha" ID="cbSetorFolha" OnCallback="cbSetorFolha_Callback1">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {PreenchecbUsuario();}" />
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

                    <LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                    <ValidationSettings ValidationGroup="ConfirmaBusca">
                        <RequiredField IsRequired="True"></RequiredField>
                    </ValidationSettings>
                </dx:ASPxComboBox>


            </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="style7" align="right">Colaborador:</td>
            <td>
                <dx:ASPxComboBox runat="server"
                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains"
                    TextField="DSUsuario" ValueField="IDVinculoUsuario" Spacing="0" Width="400px"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="vwNomeUsuario"
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    ClientInstanceName="cbUsuarioFolha" ID="cbUsuario" OnCallback="cbUsuario_Callback">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

                    <LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                    <ValidationSettings>
                        <RequiredField IsRequired="True"></RequiredField>
                    </ValidationSettings>
                </dx:ASPxComboBox>


            </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="style7" align="right">Ano:</td>
            <td>
                <dx:ASPxRadioButtonList ID="rbAno" runat="server"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SelectedIndex="0"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px"
                    ClientInstanceName="rbAno">
                    <Items>
                        <dx:ListEditItem Selected="True" Text="Ano Corrente" Value="Ano Corrente" />
                        <dx:ListEditItem Text="Ano Anterior" Value="Ano Anterior" />
                    </Items>
                </dx:ASPxRadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="style7">
                <table class="style1">
                    <tr>
                        <td class="style5">&nbsp;</td>
                        <td class="style6">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
                <dx:ASPxHiddenField ID="coIDUsuarioSetorFFrequencia" runat="server"
                    ClientInstanceName="coIDUsuarioSetorFFrequencia">
                </dx:ASPxHiddenField>
                 <dx:ASPxHiddenField ID="coIDUsuarioSetorFFrequenciaV" runat="server"
                    ClientInstanceName="coIDUsuarioSetorFFrequenciaV">
                </dx:ASPxHiddenField>
                <asp:HiddenField id="IDUsuario" runat="server"/>
                <table class="auto-style6">
                    <tr>
                        <td class="auto-style7">
                            <dx:ASPxButton ID="btGerarFolhaFrequencia" runat="server"
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="OK"
                                Width="100px" OnClick="btGerarFolhaFrequencia_Click"
                                ValidationGroup="ValidaGrupo" AutoPostBack="False"
                                ClientInstanceName="btGerarFolhaFrequencia" Theme="iOS" ToolTip="Gerar">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btVoltar" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                CssPostfix="DevEx"
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar"
                                Width="100px" OnClick="btVoltar_Click" Theme="iOS" ToolTip="Voltar">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style7">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</asp:Content>

