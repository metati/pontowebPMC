<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmControleHorasExtras.aspx.cs" Inherits="Manutencao_frmControleHorasExtras" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <style type="text/css">
        .dxeButtonEditButton_DevEx,
        .dxeSpinLargeIncButton_DevEx,
        .dxeSpinLargeDecButton_DevEx,
        .dxeSpinIncButton_DevEx,
        .dxeSpinDecButton_DevEx {
            background: White none;
        }

        .dxeButtonEditButton_DevEx {
            border-top-width: 0;
            border-right-width: 0;
            border-bottom-width: 0;
            border-left-width: 1px;
        }

        .dxeButtonEditButton_DevEx,
        .dxeButtonEdit_DevEx .dxeSBC {
            border-style: solid;
            border-color: Transparent;
            -border-color: White;
        }

        .dxeButtonEditButton_DevEx,
        .dxeCalendarButton_DevEx,
        .dxeSpinIncButton_DevEx,
        .dxeSpinDecButton_DevEx,
        .dxeSpinLargeIncButton_DevEx,
        .dxeSpinLargeDecButton_DevEx {
            vertical-align: middle;
            cursor: pointer;
        }

        .style1 {
            width: 260px;
        }

        .auto-style2 {
            width: 1229px;
        }

        .right {
            float: right;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="dxeLyGroup_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #000000;">Controle de Horas Extras</div>

            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td style="width: 10%">FILTROS:</td>
            <td style="width: 50%">Setor:<dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetorDefault"
                ssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" DataMember="TBSetor"
                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor"
                ValueField="IDSetor" Width="100%" Spacing="0" IncrementalFilteringMode="Contains"
                EnableIncrementalFiltering="True" Theme="DevEx" OnSelectedIndexChanged="cbSetor_SelectedIndexChanged" AutoPostBack="True">
                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                </LoadingPanelImage>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
            </dx:ASPxComboBox>
            </td>
            <td style="width: 18%">Nome<asp:TextBox ID="txtNome" runat="server" CssClass="textEntry" Width="100%"></asp:TextBox></td>
            <td style="width: 18%">Matrícula<asp:TextBox ID="txtMatricula" runat="server" CssClass="textEntry" Width="100%"></asp:TextBox></td>
            <td style="padding-top: 10px;">
                <dx:ASPxButton ID="btnFilter" runat="server"
                    ClientInstanceName="btSalvar"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Filtrar"
                    Width="100px" Theme="iOS" ToolTip="Filtrar" CssClass="right" OnClick="btFilter_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <th colspan="4" style="background: #cccccc;">&nbsp;</th>
        </tr>
        <tr>
            <td style="width: 80%">&nbsp;</td>
            <td style="width: 5%">Data início
                <dx:ASPxDateEdit ID="deDataInicio" runat="server"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100px"
                    Spacing="0">
                    <CalendarProperties ClearButtonText="Deletar" TodayButtonText="Hoje">
                        <HeaderStyle Spacing="1px" />
                    </CalendarProperties>
                    <ValidationSettings ValidationGroup="ValidaGrupo">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 5%">Data fim
                <dx:ASPxDateEdit ID="deDataFim" runat="server"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100px"
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
                <dx:ASPxButton ID="btNovo" runat="server"
                    ClientInstanceName="btSalvar"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar"
                    Width="150px" Theme="iOS" ToolTip="Salvar" CssClass="right" OnClick="btNovo_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="gridUsuario" ClientInstanceName="gridUsuario" runat="server" KeyFieldName="Codigo" Width="100%" OnBeforeColumnSortingGrouping="btFilter_Click" OnPageIndexChanged="btFilter_Click">
        <SettingsText EmptyDataRow=" " EmptyHeaders=" " />
    </dx:ASPxGridView>
    <%--<dx:ASPxGridView ID="gridUsuario" ClientInstanceName="gridUsuario" runat="server" DataSourceID="ObjectDataSource1" KeyFieldName="IDUsuario" Width="100%" AutoGenerateColumns="False">
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" />
            <dx:GridViewDataTextColumn FieldName="IDUsuario" Caption="Codigo">
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Matricula" Caption="Matrícula">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DSUsuario" Caption="Nome">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetDataByIDSetor" TypeName="MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="cbSetor" Name="Param1" PropertyName="Value" Type="Int32" />
            <asp:SessionParameter Name="IDEmpresa" SessionField="IDEmpresa" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
</asp:Content>
