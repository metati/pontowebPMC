<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmHorasExtras.aspx.cs" Inherits="Manutencao_frmHorasExtras" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx1" %>


<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>


<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery.formatter.min.js"></script>

    <style>
        .valideHorasDia {
            color: red;
        }
    </style>
    <script type="text/javascript">
        var valorSelecionado = 1255;
        var funcSelect = {};
        var selectCol = "";

        function gridH_SelectionChanged(s) {
            s.GetSelectedFieldValues("Codigo", function (array) { array[0]; });
            rbAnoHoraDiaria.SetValue("1244");
            console.log(s);
        }

        function grid_SelectionChanged(s, e) {
            selectCol = "Codigo";
            s.GetSelectedFieldValues("Codigo", function (array) { funcSelect.codigo = array[0]; });
            selectCol = "DSUsuario";
            s.GetSelectedFieldValues("DSUsuario", function (array) { funcSelect.usuario = array[0]; });
            selectCol = "Matricula";
            s.GetSelectedFieldValues("Matricula", function (array) { funcSelect.matricula = array[0]; });
            selecFuncionario();
        }

        function GetSelectedFieldValuesCallback(values, teste) {
            try {
                for (var i = 0; i < values.length; i++) {
                    //console.log(values[i])
                    switch (selectCol) {
                        case "Codigo":
                            funcSelect.codigo = values[i];
                        case "DSUsuario":
                            funcSelect.usuario = values[i];
                        case "Matricula":
                            funcSelect.matricula = values[i];
                    }

                }
                return values[0];
            } finally {

            }

            //console.log(gridUsuario.GetSelectedRowCount());
        }


        function validaHoras(e) {
            if (e.value.length > 4) {
                var val;
                try {
                    var hora = e.value.split(':');
                    if (hora[1] > 59) {
                        $(e).val(hora[0] + ':59')
                        val = hora[0] + '.' + 59;
                    } else {
                        val = hora[0] + '.' + hora[1];
                    }
                } catch (e) { }
                finally {
                    val = parseFloat(val);
                }
                if (val > 2 && valorSelecionado == "1255") {
                    $('.valideHorasDia').text('Não é permitido acima de 2 horas diárias').show();
                    $(e).val('02:00');
                } else if (val > 7 && valorSelecionado == "1244") {
                    $('.valideHorasDia').text('Não é permitido acima de 7 horas diárias').show();
                    $(e).val('07:00');
                } else if (val == 0 || val < 0) {
                    $('.valideHorasDia').text('Favor informar um valor válido!').show();
                    $(e).val('00:10');
                } else {
                    $('.valideHorasDia').hide();
                }
            }
        }

        function validateQty(event, e) {
            var key = window.event ? event.keyCode : event.which;
            if (event.keyCode == 8 || event.keyCode == 46
                || event.keyCode == 37 || event.keyCode == 39) {
                return true;
            }
            else if (key < 48 || key > 57) {
                return false;
            }
            else {
                return true;
            }
        };

        function selectChange(e) {
            valorSelecionado = e.valueInput.value;
            $('.txtHorasDia').val('')
        }

        function btnSeleciona() {
            if ($('.gridUsuario .dxgvDataRow').length > 1) {
                alert(1)
            }
        }

        function VerDetalhes() {
            $('#divFuncionarios').hide();
            //$('#btnSelect').html('<div style="padding: 10px" onclick="selecFuncionario()">SELECIONAR OUTRO COLABORADOR</div>');
        }

        function selecFuncionario() {
            $('#divFuncionarios').show();
            //$('#btnSelect').html('<div style="padding: 10px" onclick="VerDetalhes()">VER DETALHES DO COLABORADOR</div>');
        }

        function AtualizaServidor() {
            $('#detCodigo').text(funcSelect.codigo);
            $('#detServidor').text(funcSelect.usuario);
            $('#detMatricula').text(funcSelect.matricula);
            setTimeout('AtualizaServidor()', 3000);
        }
        //AtualizaServidor();
        function FormatarCampos() {
            $('.txtHorasDia').formatter({
                'pattern': '{{99}}:{{99}}'
            });
        }

        setTimeout('FormatarCampos()', 3000);
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="divFuncionarios" style="width: 100%" runat="server">
        <table class="dxeLyGroup_MetropolisBlue">
            <tr>
                <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="2">
                    <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #000000;">Lançamento Horas Extras</div>
                    <div style="color: black; font-size: 10px; width: 100%; text-align: center">SOMENTE PARA SERVIDORES COM PERMISSÕES DE HORAS EXTRAS/ADICIONAL NOTURNO CADASTRADOS</div>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td style="width: 10%">FILTROS:</td>
                <td style="width: 50%">Setor:
                <dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetorDefault"
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
            <tr>
                <th colspan="5" style="background: #cccccc;">&nbsp;</th>
            </tr>
        </table>
        <dx:ASPxGridView ID="gridUsuario" ClientInstanceName="gridUsuario" CssClass="gridUsuario" runat="server" KeyFieldName="Codigo" Width="100%" SettingsBehavior-AllowSelectByRowClick="true" SettingsBehavior-AllowSelectSingleRowOnly="true" OnBeforeColumnSortingGrouping="btFilter_Click" OnPageIndexChanged="btFilter_Click">
            <ClientSideEvents SelectionChanged="grid_SelectionChanged" />

<SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>
            <SettingsText EmptyDataRow=" " EmptyHeaders=" " />
        </dx:ASPxGridView>
    </div>
    <table width="100%">
        <tr>
            <th colspan="7" style="background: #cccccc; cursor: pointer;">
                <asp:Button ID="btnSelect" Style="padding: 10px; width: 100%;" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Theme="iOS" Text="LANÇAR HORA EXTRA/ADICIONAL NOTURNO" OnClick="btnSelect_Click" /></th>
        </tr>
    </table>
    <div style="width: 100%" id="divDetalhes" runat="server">
        <table width="100%">
            <tr>
                <th colspan="6">&nbsp;</th>
            </tr>
            <tr>
                <td style="width: 5%;">Código:</td>
                <td id="detCodigo" runat="server"></td>
                <td style="width: 5%;">Servidor:</td>
                <td style="width: 50%;" id="detServidor" runat="server"></td>
                <td style="width: 5%;">Matrícula:</td>
                <td id="detMatricula" runat="server"></td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <th colspan="6">&nbsp;</th>
            </tr>

            <tr>
                <td style="width: 30%">Código da Verba
                <dx1:ASPxRadioButtonList ID="rbAno" runat="server" ClientSideEvents-SelectedIndexChanged="selectChange"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SelectedIndex="0"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100%"
                    ClientInstanceName="rbAnoHoraDiaria" RepeatDirection="Horizontal"
                    Theme="DevEx">
                    <Items>
                        <dx:ListEditItem Selected="True" Text="1235 - Hora Extra" Value="1235" />
                        <dx:ListEditItem Text="1244 - Adicional Noturno" Value="1244" />
                    </Items>
                </dx1:ASPxRadioButtonList>
                </td>
                <td style="width: 10%"></td>
                <td style="width: 15%">Quantidade de Horas (Ex.: 00:59)
                <asp:TextBox ID="txtHorasDia" runat="server" CssClass="textEntry txtHorasDia" Width="80%" onkeypress='return validateQty(event);' onkeyup="validaHoras(this)"></asp:TextBox>
                    <div class="valideHorasDia" style="display: none">Não é permitido acima de 2 horas diárias</div>
                </td>

                <td>Data da Ocorrência
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
        <dx:ASPxGridView ID="gridHoras" ClientInstanceName="gridHoras" CssClass="gridHoras" runat="server" KeyFieldName="Codigo" Width="100%" OnBeforeColumnSortingGrouping="btFilterLacamento_Click"  OnPageIndexChanged="btFilterLacamento_Click">
            <ClientSideEvents SelectionChanged="gridH_SelectionChanged" />
            <SettingsText EmptyDataRow="..." EmptyHeaders="..." />
        </dx:ASPxGridView>
        <table style="width: 100%">
            <tr>
                <td style="background: #CCCCCC" colspan="2"></td>
            </tr>
            <tr>
                <td style="width: 80%">&nbsp
                </td>
                <td style="width: 30%">&nbsp;
                <dx:ASPxButton ID="btDeletar" runat="server"
                    ClientInstanceName="btDeletar"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Deletar"
                    Width="150px" Theme="iOS" ToolTip="Salvar" CssClass="right" OnClick="btDeletar_Click">
                </dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

