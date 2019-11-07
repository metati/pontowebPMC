<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmFechamentoFolha.aspx.cs" Inherits="FechamentoFolha_frmFechamentoFolha" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style3 {
            width: 96px;
        }

        .style4 {
            width: 84px;
        }

        .auto-style5 {
            width: 482px;
        }

        .style5 {
            width: 100%;
        }

        .auto-style3 {
            height: 24px;
            width: 26px;
        }

        .NoStyle {
            background: none;
            border: 0;
            color: black;
            padding: 0;
            height: auto;
        }

        .scrollPanel {
            overflow-y: auto;
            overflow-x: hidden;
        }

        .button {
            padding: 5px 5px 5px 15px;
            border-radius: 5px;
            text-align: left;
        }

        .UpdatePG_Centro {
            z-index: 30;
            position: fixed;
            top: 45%;
            left: 30%;
            text-align: center;
            width: 250px;
            height: 100px;
            font-weight: bold;
            font-size: 13px;
            color: #ff4500;
            font-family: Verdana;
        }
    </style>
    <script type="text/javascript" src="../Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/swfobject.js"></script>
    <script type="text/javascript" src="../Scripts/scriptcam.js"></script>

    <script type="text/javascript">

        function FechaPopSetor() {
            popSetor.Hide();
        };

        function FechaPopCargo() {
            popCargo.Hide();
        };
        function BindEvents() {
            $(function () {
                $("[id*=chkAllSetor]").bind("click", function () {
                    if ($(this).is(":checked")) {
                        $("[id*=chekSetor] input").attr("checked", "checked");
                    } else {
                        $("[id*=chekSetor] input").removeAttr("checked");
                    }
                });
                $("[id*=chekSetor] input").bind("click", function () {
                    if ($("[id*=chekSetor] input:checked").length == $("[id*=chekSetor] input").length) {
                        $("[id*=chkAllSetor]").attr("checked", "checked");
                    } else {
                        $("[id*=chkAllSetor]").removeAttr("checked");
                    }
                });

                $("[id*=chkAllCargo]").bind("click", function () {
                    if ($(this).is(":checked")) {
                        $("[id*=chekCargo] input").attr("checked", "checked");
                    } else {
                        $("[id*=chekCargo] input").removeAttr("checked");
                    }
                });
                $("[id*=chekCargo] input").bind("click", function () {
                    if ($("[id*=chekCargo] input:checked").length == $("[id*=chekCargo] input").length) {
                        $("[id*=chkAllCargo]").attr("checked", "checked");
                    } else {
                        $("[id*=chkAllCargo]").removeAttr("checked");
                    }
                });
                $("[id*=btnImprimir] input").bind("click", function () {
                    window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=' + cbMesHidden.Get("IDMes") + '&Rel=frmFechamentoRel');

                });
            });
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="scriptmanager1" runat="server" AsyncPostBackTimeout="600"></asp:ScriptManager>

    <asp:UpdatePanel ID="panelButton" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
             //   Sys.Application.add_load(MudaPagina);
            </script>
            <table class="dxflInternalEditorTable_MetropolisBlue" id="TBprocessamento">
                <tr>
                    <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                        <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">FECHAMENTO DE FOLHA</div>

                    </td>
                </tr>
                <tr>
                    <td align="left" class="auto-style5">
                        <strong style="font-size: 16px; font-weight: normal;">
                            <img alt="" class="auto-style3" src="../Images/Imagem43.png" />
                            Fechamento de Folha
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <asp:UpdateProgress ID="progress" runat="server" AssociatedUpdatePanelID="panelButton">
                        <ProgressTemplate>
                            <div style="margin-left: 200px" class="UpdatePG_Centro">
                                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Icones/progress.gif" AlternateText="Processing" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td align="center">

                        <table class="style1">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbMes" runat="server" ClientIDMode="AutoID" Text="Mes:*"></dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <dx:ASPxComboBox ID="cbMes" AutoPostBack="true" OnTextChanged="cbMes_TextChanged" runat="server" ClientInstanceName="cbMes" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                        EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" Spacing="0" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSMes" ValueField="IDMes" Width="100px">
                                        <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif" />
                                        <LoadingPanelStyle ImageSpacing="5px" />
                                        <ValidationSettings ValidationGroup="ValidaMes">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxHiddenField ID="cbMesHidden" runat="server" ClientInstanceName="cbMesHidden" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbSecretaria" runat="server" ClientIDMode="AutoID" Text="Secretaria:*"></dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <dx:ASPxComboBox ID="cbSecretaria" OnTextChanged="cbSecretaria_TextChanged" Enabled="false" AutoPostBack="true" runat="server" ClientInstanceName="cbSecretaria" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                        EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" Spacing="0" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSEmpresa" ValueField="IDEmpresa" Width="400px">
                                        <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif" />
                                        <LoadingPanelStyle ImageSpacing="5px" />
                                        <ValidationSettings ValidationGroup="ValidaEmpresa">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbSetor" runat="server" ClientIDMode="AutoID" Text="Setor:*"></dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <asp:ImageButton runat="server" ID="btnAbrirSetores" Enabled="false" OnClick="btnAbrirSetores_Click" ToolTip="Apresentar Lista de Setores" Height="25px" Width="25px" ImageUrl="~/Icones/Lupa.png"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbCargo" runat="server" ClientIDMode="AutoID" Text="Cargo:*"></dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <asp:ImageButton runat="server" ID="btnAbriCargos" Enabled="false" OnClick="btnAbriCargos_Click" ToolTip="Apresentar Lista de Cargos" Height="25px" Width="25px" ImageUrl="~/Icones/Lupa.png"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lbValidar" runat="server" Visible="false" ForeColor="Red" Width="150px"></asp:Label>

                                </td>
                            </tr>

                            <tr>
                                <td></td>
                                <td style="width: 100px">
                                    <dx:ASPxButton ID="btnProcessar" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btnProcessar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Processar" Theme="iOS" ToolTip="Processar" Width="100px">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnImprimir" runat="server" Enabled="false" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Exportar Excel" Theme="iOS" ToolTip="Exportar" Width="134px">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lbRetorno" runat="server" Visible="false" ForeColor="Blue" Width="250px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table class="style1">
                            <tr>
                                <td style="margin-left: 40px">
                                    <dx:ASPxGridView ID="gridProcessados" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                        CssPostfix="DevEx" DataMember="TBFechamentoFolha" Width="900px" KeyFieldName="IDFechamento" OnPageIndexChanged="gridProcessados_PageIndexChanged">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Código" FieldName="IDFechamento" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0" Width="50px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mes Fechamento" FieldName="Mes" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Secretaria" FieldName="DSEmpresa" ShowInCustomizationForm="True" VisibleIndex="2" Width="300px">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Data Processamento" FieldName="DataProcessamento" ShowInCustomizationForm="True" VisibleIndex="3">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Situação" FieldName="Situacao" ShowInCustomizationForm="True" VisibleIndex="4">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Reprocessar" Width="40px">
                                                <DataItemTemplate>
                                                    <asp:ImageButton runat="server" ID="btnReprocessar" ToolTip="Reprocessar" Height="19px" Width="19px" OnClick="btnReprocessar_Click" CssClass="button" ImageUrl="~/Icones/refresh_arrow_1546.png"></asp:ImageButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" SortMode="Value" />
                                        <SettingsPager PageSize="12">
                                            <AllButton Text="All">
                                            </AllButton>
                                            <NextPageButton Text="Próx. &gt;">
                                            </NextPageButton>
                                            <PrevPageButton Text="&lt;Ant.">
                                            </PrevPageButton>
                                            <Summary AllPagesText="Page: {0} - {1} ({2} items)"
                                                Text="Página {0} of {1} ({2} items)" />
                                        </SettingsPager>
                                        <Settings ShowFilterRow="True" />
                                        <SettingsLoadingPanel Text="Processando&amp;hellip;" />
                                        <Images SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                                            <LoadingPanelOnStatusBar Url="~/App_Themes/DevEx/GridView/StatusBarLoading.gif">
                                            </LoadingPanelOnStatusBar>
                                            <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </Images>
                                        <ImagesFilterControl>
                                            <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </ImagesFilterControl>
                                        <Styles CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                            CssPostfix="DevEx">
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                            <AlternatingRow Enabled="True">
                                            </AlternatingRow>
                                            <LoadingPanel ImageSpacing="5px">
                                            </LoadingPanel>
                                        </Styles>
                                        <StylesEditors ButtonEditCellSpacing="0">
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <dx:ASPxPopupControl ID="popSetor" runat="server" ClientInstanceName="popSetor" CssPostfix="DevEx" CssClass="scrollPanel"
                HeaderText="Selecionar Setores" Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter" AllowDragging="true" DragElement="Window"
                PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" EnableHotTrack="False" Width="1024px" Height="650px" ContentStyle-VerticalAlign="Top" ScrollBars="Vertical">
                <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif"></LoadingPanelImage>
                <HeaderStyle>
                    <Paddings PaddingLeft="7px" />
                    <Paddings PaddingRight="6px"></Paddings>
                </HeaderStyle>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <asp:HiddenField ID="IDEmpresa" runat="server" />
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkAllSetor" Text="Todos" runat="server" Font-Bold="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="chekSetor" DataTextField="DSSetor" DataValueField="IDSetor" RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnFecharSetor" runat="server" AutoPostBack="False" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                        Text="Fechar" Width="100px" ClientInstanceName="btnFecharUser" Theme="iOS">
                                        <ClientSideEvents Click="function(s, e) {FechaPopSetor();}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>

            <dx:ASPxPopupControl ID="popCargo" runat="server" ClientInstanceName="popCargo" CssPostfix="DevEx" CssClass="scrollPanel"
                HeaderText="Selecionar Cargos" Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter" AllowDragging="true" DragElement="Window"
                PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" EnableHotTrack="False" Width="1024px" Height="650px" ContentStyle-VerticalAlign="Top" ScrollBars="Vertical">
                <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif"></LoadingPanelImage>
                <HeaderStyle>
                    <Paddings PaddingLeft="7px" />
                    <Paddings PaddingRight="6px"></Paddings>
                </HeaderStyle>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkAllCargo" Text="Todos" runat="server" Font-Bold="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="chekCargo" DataTextField="DSCargo" DataValueField="IDCargo" RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnFecharCargo" runat="server" AutoPostBack="False" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                        Text="Fechar" Width="100px" ClientInstanceName="btnFecharCargo" Theme="iOS">
                                        <ClientSideEvents Click="function(s, e) {FechaPopCargo();}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
