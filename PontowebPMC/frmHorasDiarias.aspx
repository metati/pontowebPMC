<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmHorasDiarias.aspx.cs" Inherits="frmHorasDiarias" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx1" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxwschsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        function keyUpMemoSold() {
            var text = memoOBS.GetValue();
            $('.limiteTexto').show();
            $('.limiteTexto span').text(text.length);
            if (text.length > 150) {
                memoOBS.SetValue(text.substring(0, 150))
            }
        }

        function AbreRelatorio() {

            var idusuario = coIDusuario.Get("iduMinhasHoras").toString();
            var idvinculousuario = coIDusuario.Get("iduvMinhasHoras").toString();
            var idsetor = coIDSetorMinhas.Get("idsetorMinhasHoras").toString();

            var Ano, hoje;

            hoje = new Date();

            if (rbAnoHoraDiaria.GetValue() == "Ano Corrente") {

                Ano = 0;
            }
            else
                Ano = 1;

            //alert(idsetor);

            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=' + cbMesN.GetValue() + '&cbOrgaoFolha=0' + '&Setor=' + idsetor + '&IDVinculoUsuario=' + idvinculousuario + '&User=' + idusuario + '&Ano=' + Ano + '&Rel=frmZuxa')
        }

        function PreencheGrid() {
            gridHoras.PerformCallback();
        }

        function Informacao() {
            gridHoras.GetRowValues(gridHoras.GetFocusedRowIndex(), 'OBS', ValorPop);
            memoOBS.SetText('Aguarde...');
        }
        function ValorPop(Valor) {
            memoOBS.SetText(Valor);

            popInformacao.Show();
        }

        function Justificar(IDUsuario, IDFrequencia, DataFrequencia, HorasTotais, TotalHorasDiarias, IDVinculoUsuario, Situacao) {
            $('.limiteTexto').hide();
            Fase = 0;
            TextoOBS = "";
            hdJustificativa.Clear();

            FechaPopJust();

            hdItensJustificativa.Clear();

            hdItensJustificativa.Set("Modo", 'Unitario');

            hdItensJustificativa.Set("IDUsuario", IDUsuario);
            hdItensJustificativa.Set("IDFrequencia", IDFrequencia);
            hdItensJustificativa.Set("DTJust", DataFrequencia);
            hdItensJustificativa.Set("TotDIa", DataFrequencia + ' ' + HorasTotais);
            hdItensJustificativa.Set("TotalHorasDiarias", TotalHorasDiarias);
            hdItensJustificativa.Set("Resp", '');
            hdItensJustificativa.Set("IDVinculoUsuario", IDVinculoUsuario);
            //Quando abrir zerar a justifativa.
            //As combos e memos deve estar invisiveis.
            //cbMotivoFaltaManut.SetVisible(false);
            //memoOBS.SetVisible(false);
            //lbMotivo.SetVisible(false);
            //lbOBS.SetVisible(false);
            //lbConfirmacao.SetVisible(false);
            //btRetornar.SetVisible(false);
            pcCadastraFalta.Show();
        }

        function VerDetalhes(IDUsuario, IDFrequencia, DataFrequencia, HorasTotais, TotalHorasDiarias, IDVinculoUsuario, Situacao, obs, MF) {
            $.getJSON('api/vs1/justificativas/GetDetalhesPedido/' + IDFrequencia, function (data)
            {
                if (data != null)
                {
                    hdItensJustificativa.Set("IDFrequencia", IDFrequencia);
                    tbDataUsuario.SetValue(data.dtFrequencia);
                    tbMotivoFaltaUsuario.SetValue(data.dsMotivoFalta);
                    tbMotivoFaltaUsuario.SetValue(data.dsMotivoFalta);
                    memoOBSSUsuario.SetValue(data.obs);
                    if (data.obsGestor.length > 0) {
                        memoOBSSUsuarioGestor.SetValue(data.obsGestor);
                        $('.descGestor').show();
                    } else {
                        $('.descGestor').hide();
                        memoOBSSUsuarioGestor.SetValue('');
                    }


                    if (data.situacao == '' || data.situacao == '0' || data.situacao == '2') {
                        btExcluirSolicitacao.SetVisible(false);
                    } else {
                        btExcluirSolicitacao.SetVisible(true);
                    }
                } else {
                    hdItensJustificativa.Set("IDFrequencia", '');
                    tbDataUsuario.SetValue('');
                    tbMotivoFaltaUsuario.SetValue('');
                    tbMotivoFaltaUsuario.SetValue('');
                    memoOBSSUsuario.SetValue('');
                }
                popDetalheFrequencia.Show();
            })
            tbMotivoFaltaUsuario.SetText(DataFrequencia);

            hdItensJustificativa.Set("IDUsuario", IDUsuario);
            hdItensJustificativa.Set("IDFrequencia", IDFrequencia);
            hdItensJustificativa.Set("IDVinculoUsuario", IDVinculoUsuario);
            hdItensJustificativa.Set("DTJust", DataFrequencia);
        }

        function ExcuirPopSolJusti() {
            if (confirm("Tem certeza que deseja excluir?")) {
                $.post('../api/vs1/justificativas/excluirPedido/' + hdItensJustificativa.Get('IDFrequencia'), function (data) {
                    alert('Excluído com sucesso!')
                    popDetalheFrequencia.Hide();
                    gridHoras.PerformCallback(0);
                })
            }
            popDetalheFrequencia.Hide();
            gridHoras.PerformCallback(0);
        }

        function PopSolJusti() {
            popDetalheFrequencia.Hide();
        }

        function FechaPopJust() {
            cpJust.PerformCallback(0);
        }

        function VinculaData() {
            hdItensJustificativa.Set("DataColetiva", '');
            hdItensJustificativa.Set("DataColetiva", deDataFaltaModo.GetDate());
        }

        function ControleJustificativa() {

            Fase++;
            if (Fase == 1)
                hdJustificativa.Set("TPJust", rbList.GetSelectedIndex());
            else if (Fase == 2 && (rbList.GetSelectedIndex() == 0 || rbList.GetSelectedIndex() == 1)) {
                hdJustificativa.Set("INDEX", 0);
                hdJustificativa.Set("INDEX", rbList.GetSelectedIndex());
            }

            if (Fase >= 2) {
                hdJustificativa.Set("IDMotivoFalta", 0);
                hdJustificativa.Set("IDMotivoFalta", cbMotivoFaltaManut.GetValue());

                if (TextoOBS == "" && hdJustificativa.Get("TPJust") == 2) {

                    hdJustificativa.Set("INDEX", 0);
                    hdJustificativa.Set("INDEX", rbList.GetSelectedIndex());

                    TextoOBS = memoOBS.GetText();
                    hdJustificativa.Set("OBS", 0);
                    hdJustificativa.Set("OBS", TextoOBS);
                }
            }

            if (Fase >= 3 && hdJustificativa.Get("TPJust") != 2) {
                //index da justificativa 3
                hdJustificativa.Set("INDEX", 0);
                hdJustificativa.Set("INDEX", rbList.GetSelectedIndex());

                if (TextoOBS == "") {
                    TextoOBS = memoOBS.GetText();
                    hdJustificativa.Set("OBS", 0);
                    hdJustificativa.Set("OBS", TextoOBS);
                }

            }

            if (btSalvarAvanc.GetText() == "Avançar") {
                if (Fase > 3 && hdJustificativa.Get("TPJust") != 2) {
                    cpJust.PerformCallback(3);
                }
                else if (Fase >= 3 && hdJustificativa.Get("TPJust") == 2) {
                    cpJust.PerformCallback(2);
                }
                else {
                    cpJust.PerformCallback(Fase);
                }
            }
            else if (btSalvarAvanc.GetText() == "Salvar") {
                if (hdItensJustificativa.Get("Modo").toString() != 'Coletivo') {
                    //gridFrequencia.PerformCallback('JU');
                    cpJust.PerformCallback('JU');
                }
                else
                    cpJust.PerformCallback('JC');
            }

            if (btSalvarAvanc.GetText() == "Finalizar") {
                pcCadastraFalta.Hide();
                gridHoras.PerformCallback('');
            }
        }

        function AtividadeAnterior() {

            Fase--;

            if (btRetornar.GetText() == "Cancelar") {
                Fase = 0;
                TextoOBS = "";
            }
            cpJust.PerformCallback(Fase);
        }

    </script>
    <style type="text/css">
        .style1 {
            width: 1045px;
        }

        .style2 {
            width: 81px;
        }

        .style3 {
            width: 241px;
        }

        .style5 {
            width: 100%;
        }

        .style7 {
            width: 78px;
        }

        .dxeBase {
            font: 12px Tahoma, Geneva, sans-serif;
        }

        .dxeBase {
            font-family: Tahoma;
            font-size: x-small;
            color: #000000;
            font-weight: 700;
        }

        .style9 {
            width: 100%;
        }

        .auto-style3 {
            width: 25px;
            height: 25px;
        }

        .auto-style4 {
            height: 31px;
        }

        .auto-style5 {
            width: 482px;
        }

        .auto-style6 {
            width: 98px;
        }

        .auto-style7 {
            width: 489px;
        }

        .auto-style8 {
            width: 44px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    &nbsp;
    <table class="style5">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">Meus registros</div>

            </td>
        </tr>
        <tr>
            <td align="left" class="auto-style5">
                <strong style="font-weight: normal;">
                    <img alt="" class="auto-style3" src="Images/Imagem29.png" />relação das marcações realizadas diariamente </strong></td>
        </tr>
        <tr>
            <td align="center" class="auto-style4">
                <dx1:ASPxRadioButtonList ID="rbAno" runat="server"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SelectedIndex="0"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px"
                    ClientInstanceName="rbAnoHoraDiaria" RepeatDirection="Horizontal"
                    Theme="DevEx">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreencheGrid();
}" />
                    <Items>
                        <dx:ListEditItem Selected="True" Text="Ano Corrente" Value="Ano Corrente" />
                        <dx:ListEditItem Text="Ano Anterior" Value="Ano Anterior" />
                    </Items>
                </dx1:ASPxRadioButtonList>


            </td>
        </tr>
        <tr>
            <td>
                <table class="style5">
                    <tr>
                        <td align="right" class="auto-style5">Mês:</td>
                        <td align="left">
                            <dx:ASPxComboBox ID="cbMesN" runat="server" ClientInstanceName="cbMesN"
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                CssPostfix="DevEx" DataMember="TBMes"
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSMes"
                                ValueField="IDMes" DropDownStyle="DropDown" Width="253px" Spacing="0"
                                IncrementalFilteringMode="Contains" Height="24px" Style="margin-left: 0px">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreencheGrid();
}" />
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreencheGrid();
}"></ClientSideEvents>

                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <ValidationSettings ValidationGroup="ValidaGrupo">
                                    <RequiredField IsRequired="True" />

                                    <RequiredField IsRequired="True"></RequiredField>
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
                <table class="style5">
                    <tr>
                        <td class="auto-style8" colspan="0" rowspan="0" style="font-size: 19px; font-weight: bold;">Legenda:</td>
                        <td colspan="0" rowspan="0" style="font-size: 9px; text-decoration: overline;">&nbsp;As cores diferenciadas indicam qual a situação do dia! </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="style9">
        <tr>
            <td>
                <dx1:ASPxLabel ID="lblegenda2" runat="server" Font-Size="Medium"
                    Text="Jornada na Tolerância">
                </dx1:ASPxLabel>
                Jornada Completa</td>
            <td>
                <dx1:ASPxLabel ID="lblegenda3" runat="server" Font-Size="Medium"
                    Text="Jornada na Tolerância">
                </dx1:ASPxLabel>
                Jornada Tolerada</td>
            <td>
                <dx1:ASPxLabel ID="lblegenda1" runat="server" Font-Size="Medium"
                    Text="Jornada Justificada">
                </dx1:ASPxLabel>
                Jornada Justificada</td>
            <td>
                <dx1:ASPxLabel ID="lblegenda4" runat="server" Font-Size="Medium"
                    Text="Jornada Incompleta">
                </dx1:ASPxLabel>
                Jornada Incompleta</td>
            <td>
                <dx1:ASPxLabel ID="lblegenda5" runat="server" Font-Size="Medium"
                    Text="Esperando Aprovação">
                </dx1:ASPxLabel>
                Esperando Aprovação</td>
            <td>
                <dx1:ASPxLabel ID="lblegenda6" runat="server" Font-Size="Medium"
                    Text="Solicitação de justificativa rejeitada">
                </dx1:ASPxLabel>
                Solicitação de justificativa rejeitada</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="style2">
                <dx:ASPxGridView ID="gridHoras" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    CssPostfix="DevEx" DataMember="vWHorasDia" Width="1200px"
                    OnHtmlRowPrepared="gridHoras_HtmlRowPrepared"
                    OnPageIndexChanged="gridHoras_PageIndexChanged"
                    ClientInstanceName="gridHoras" OnCustomCallback="gridHoras_CustomCallback"
                    Font-Size="Small" Theme="iOS">
                    <ClientSideEvents RowClick="function(s, e) {
}"
                        RowDblClick="function(s, e) {
}" />
                    <SettingsBehavior AllowSort="False" />
                    <Settings ShowVerticalScrollBar="True" />
                    <SettingsText EmptyDataRow="Sem dados para exibir" />
                    <ClientSideEvents RowClick="function(s, e) {
}"
                        RowDblClick="function(s, e) {
}"></ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="IDFrequencia" Visible="False"
                            VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Usuário" FieldName="PrimeiroNome"
                            VisibleIndex="1" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Data"
                            FieldName="DataFrequencia" VisibleIndex="2" Width="90px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Entrada 1" FieldName="EntradaManha"
                            VisibleIndex="3" Width="90px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Saída 1" FieldName="SaidaManha"
                            VisibleIndex="4" Width="90px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Entrada 2"
                            FieldName="EntradaTarde" VisibleIndex="5" Width="90px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Saída 2" FieldName="SaidaTarde"
                            VisibleIndex="6" Width="90px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Total" FieldName="HorasDia"
                            VisibleIndex="8" Width="90px" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IMF" Visible="False" VisibleIndex="9"
                            Width="50px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Justificativa" FieldName="MF"
                            VisibleIndex="10" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="OBS" VisibleIndex="11" Width="20%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDUsuario" Visible="False"
                            VisibleIndex="15">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sugestão Saída" VisibleIndex="7"
                            Width="125px" Visible="False">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="LBSugestao" runat="server" Text="<%# TerminoJornada(Container) %>" Font-Size="Small" ForeColor="<%# CorDaFonte() %>">
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Total Horas"
                            FieldName="TotHorasDiarias" VisibleIndex="12" Width="100PX" CellStyle-Wrap="True" Visible="false">
                            <CellStyle Wrap="True"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDVinculoUsuario" Visible="False"
                            VisibleIndex="14">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDRegimeHora" Visible="False" VisibleIndex="13">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="*" VisibleIndex="15" Width="60px" FieldName="SituacaoJustificativa1">
                            <EditCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </EditCellStyle>
                            <DataItemTemplate>
                                <a href="javascript:void(0);" onclick="Justificar('<%# Eval("IDusuario") %>','<%# Eval("IDFrequencia") %>','<%# Eval("DataFrequencia") %>','<%# Eval("HorasDia") %>','<%# Eval("TotHorasDiarias") %>','<%# Eval("IDVinculoUsuario") %>', '<%# Eval("SituacaoJustificativa") %>')">Justificar</font></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="*" VisibleIndex="16" Width="60px" FieldName="SituacaoJustificativa1">
                            <EditCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </EditCellStyle>
                            <DataItemTemplate>
                                <a href="javascript:void(0);" class="btnVisualizar" onclick="VerDetalhes('<%# Eval("IDusuario") %>','<%# Eval("IDFrequencia") %>','<%# Eval("DataFrequencia") %>','<%# Eval("HorasDia") %>','<%# Eval("TotHorasDiarias") %>','<%# Eval("IDVinculoUsuario") %>','<%# Eval("SituacaoJustificativa") %>','','<%# Eval("MF") %>')">Visualizar</font></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDusuario" Visible="False" VisibleIndex="16">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SituacaoJustificativa" Visible="False" VisibleIndex="23">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="DescontoTotalJornada" Visible="False" VisibleIndex="22">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="HoraEntradaManha" Visible="False" VisibleIndex="21">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="HoraSaidaManha" Visible="False" VisibleIndex="20">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="HoraEntradaTarde" Visible="False" VisibleIndex="19">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="HoraSaidaTarde" Visible="False" VisibleIndex="18">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IsencaoPonto" Visible="False" VisibleIndex="17">
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowSort="False"></SettingsBehavior>

                    <SettingsPager NumericButtonCount="31" PageSize="31">
                        <AllButton Text="All">
                        </AllButton>
                        <NextPageButton Text="Próx &gt;">
                        </NextPageButton>
                        <PrevPageButton Text="&lt; Ant.">
                        </PrevPageButton>
                        <Summary Text="Página {0} de {1} ({2} itens)" />

                        <Summary Text="P&#225;gina {0} de {1} ({2} itens)"></Summary>
                    </SettingsPager>

                    <Settings ShowVerticalScrollBar="True"></Settings>

                    <SettingsText EmptyDataRow="Sem dados para exibir"></SettingsText>

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
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="auto-style6">
                <%--<a href="javascript:void(0);" onclick="Justificar('<%# Eval("IDUsuario") %>','<%# Eval("IDFrequencia") %>','<%# Eval("DataFrequencia") %>','<%# Eval("HorasDia") %>','<%# Eval("TotHorasDiarias") %>','<%# Eval("IDVinculoUsuario") %>')"><font color="#000000">Justificar</font></a>--%>
                &nbsp;</td>
            <td class="style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">
                <table class="auto-style7">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
            <td class="style3">
                <dx:ASPxButton ID="btPDF" runat="server"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                    Text="Imprimir ou salvar em PDF" Width="255px"
                    ClientInstanceName="btPDF" AutoPostBack="False"
                    ValidationGroup="ValidaGrupo" EnableTheming="True" Theme="iOS" ToolTip="Imprimir ou salvar em PDF">
                    <ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))AbreRelatorio();
}" />
                    <Image Url="~/ícones/Impressoras/Impressora32.png" Width="25px">
                    </Image>
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxHiddenField ID="coIDusuario" runat="server"
                    ClientInstanceName="coIDusuario">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="coIDSetorMinhas" runat="server"
                    ClientInstanceName="coIDSetorMinhas">
                </dx:ASPxHiddenField>
            </td>
        </tr>
    </table>


    <table class="style1" style="color: #000000">
        <tr>
            <td class="style29"></td>
            <td class="style15">
                <table class="style16">
                    <tr>
                        <td align="right" class="auto-style10">&nbsp;</td>
                        <td align="left">&nbsp;</td>
                    </tr>
                </table>
                <td>&nbsp;</td>
        </tr>

        <tr>
            <td colspan="3">
                <table class="style1">
                </table>
                <dx:ASPxPopupControl ID="pcCadastraFaltaOLD" runat="server"
                    ClientInstanceName="pcCadastraFaltaOLD"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    HeaderText="Justificar Falta ou Atraso"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="500px"
                    Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
                    EnableHotTrack="False">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="7px" />
                        <Paddings PaddingLeft="7px"></Paddings>
                    </HeaderStyle>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table class="style1">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" ClientIDMode="AutoID"
                                            Text="Motivo:">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="cbMotivoFaltaManutOLD" runat="server"
                                            ClientInstanceName="cbMotivoFaltaManutOLD"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            DataMember="TBMotivoFalta" DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                            Height="20px" IncrementalFilteringMode="StartsWith" Spacing="0"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSMotivoFalta"
                                            ValueField="IDMotivoFalta" Width="500px">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
}"></ClientSideEvents>

                                            <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                            </LoadingPanelImage>
                                            <LoadingPanelStyle ImageSpacing="5px">
                                            </LoadingPanelStyle>
                                            <ValidationSettings ValidationGroup="ValidaMotivoFalta">
                                                <RequiredField IsRequired="True" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" ClientIDMode="AutoID"
                                            Text="Descrição:">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxMemo ID="memoOBSOLD" runat="server" ClientIDMode="AutoID"
                                            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue"
                                            Height="100px" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css"
                                            Width="400px">
                                            <ValidationSettings>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="memoOBSOLD" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                            </table>
                            <table class="style1">
                                <tr>
                                    <td class="style7">
                                        <dx:ASPxButton ID="btSalvar" runat="server" ClientInstanceName="btSalvar"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="OK"
                                            Width="100px" AutoPostBack="False" OnClick="btSalvar_Click"
                                            ValidationGroup="ValidaMotivoFalta">
                                            <ClientSideEvents Click="function(s, e) {
if(ASPxClientEdit.ValidateGroup('ValidaMotivoFalta'))FechaPopJustificativa();
}" />
                                            <ClientSideEvents Click="function(s, e) {
if(ASPxClientEdit.ValidateGroup(&#39;ValidaMotivoFalta&#39;))FechaPopJustificativa();
}"></ClientSideEvents>
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btFechar" runat="server" AutoPostBack="False" ClientInstanceName="btFechar"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Fechar"
                                            Width="100px">
                                            <ClientSideEvents Click="function(s, e) {
	FechaPopJustificativa();
}" />
                                            <ClientSideEvents Click="function(s, e) {
	FechaPopJustificativa();
}"></ClientSideEvents>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style6" colspan="2">&nbsp;</td>
                                </tr>
                            </table>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl ID="pcCadastraFalta" runat="server"
                    ClientInstanceName="pcCadastraFalta"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    HeaderText="Justificar Falta ou Atraso"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="550px"
                    Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
                    EnableHotTrack="False" EnableTheming="True" Theme="DevEx">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <ClientSideEvents CloseButtonClick="function(s, e) {
}" />
                    <HeaderStyle>
                        <Paddings PaddingLeft="7px" />
                        <Paddings PaddingLeft="7px"></Paddings>
                    </HeaderStyle>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowHeader="False"
                                Theme="DevEx" Width="550px">
                                <PanelCollection>
                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxCallbackPanel ID="cpJust" runat="server" ClientInstanceName="cpJust"
                                            LoadingPanelText="Processando" Theme="DevEx" Width="550px"
                                            OnCallback="cpJust_Callback1">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                    <table class="style40">
                                                        <tr>
                                                            <td class="style41">
                                                                <dx:ASPxRadioButtonList ID="rbList" runat="server" ClientInstanceName="rbList"
                                                                    EnableTheming="True" Theme="DevEx" Width="230px"
                                                                    Style="font-size: x-large">
                                                                    <Items>
                                                                        <dx:ListEditItem Text="Justificativa de registro único" Value="0" />
                                                                        <dx:ListEditItem Text="Justificativa de meio perído" Value="1" />
                                                                        <dx:ListEditItem Text="Justificativa de período integral" Value="2" />
                                                                    </Items>
                                                                    <ValidationSettings ValidationGroup="GrupoJustificado">
                                                                        <RequiredField IsRequired="True" />
                                                                        <RequiredField IsRequired="True"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxRadioButtonList>
                                                            </td>
                                                            <td width="100%">
                                                                <dx:ASPxLabel ID="lbConfirmacao" runat="server"
                                                                    ClientInstanceName="lbConfirmacao" Text="lbConfirmação" Theme="DevEx">
                                                                </dx:ASPxLabel>
                                                                <br />
                                                                <dx:ASPxLabel ID="lbResposta" runat="server" ClientInstanceName="lbResposta"
                                                                    Text="RespostaOperacao">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="style1">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="lbSetorModo" runat="server" ClientInstanceName="lbDataModo"
                                                                    Text="Setor:">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxComboBox ID="cbSetorModo" runat="server" ClientInstanceName="cbSetorModo"
                                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                                    DataMember="TBSetor" DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                                                    IncrementalFilteringMode="StartsWith" Spacing="0"
                                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor"
                                                                    ValueField="IDSetor" Width="500px">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	VinculaSetor();
}" />
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	VinculaSetor();
}"></ClientSideEvents>

                                                                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                                                    </LoadingPanelImage>
                                                                    <LoadingPanelStyle ImageSpacing="5px">
                                                                    </LoadingPanelStyle>
                                                                    <ValidationSettings ValidationGroup="GrupoJustificado">
                                                                        <RequiredField IsRequired="True" />
                                                                        <RequiredField IsRequired="True"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="lbMotivo" runat="server" ClientInstanceName="lbMotivo"
                                                                    Text="Motivo:">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxComboBox ID="cbMotivoFaltaManut" runat="server"
                                                                    ClientInstanceName="cbMotivoFaltaManut"
                                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                                    DataMember="TBMotivoFalta" DropDownStyle="DropDown"
                                                                    EnableIncrementalFiltering="True" Height="20px"
                                                                    IncrementalFilteringMode="StartsWith" Spacing="0"
                                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSMotivoFalta"
                                                                    ValueField="IDMotivoFalta" Width="500px">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
}"></ClientSideEvents>

                                                                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                                                    </LoadingPanelImage>
                                                                    <LoadingPanelStyle ImageSpacing="5px">
                                                                    </LoadingPanelStyle>
                                                                    <ValidationSettings ValidationGroup="GrupoJustificado">
                                                                        <RequiredField IsRequired="True" />
                                                                        <RequiredField IsRequired="True"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="lbDataModo" runat="server" ClientInstanceName="lbDataModo"
                                                                    Text="Data:">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxDateEdit ID="deDataFaltaModo" runat="server"
                                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" Spacing="0"
                                                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="500px"
                                                                    ClientInstanceName="deDataFaltaModo" EnableTheming="True" Theme="DevEx">
                                                                    <CalendarProperties ClearButtonText="Deletar" TodayButtonText="Hoje">

                                                                        <HeaderStyle Spacing="1px" />

                                                                    </CalendarProperties>
                                                                    <ClientSideEvents DateChanged="function(s, e) {
	VinculaData();
}" />

                                                                    <ClientSideEvents DateChanged="function(s, e) {
	VinculaData();
}"></ClientSideEvents>

                                                                    <ValidationSettings ValidationGroup="GrupoJustificado">
                                                                        <RequiredField IsRequired="True" />
                                                                        <RequiredField IsRequired="True"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="lbOBS" runat="server" ClientInstanceName="lbOBS"
                                                                    Text="Obs:">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxMemo ID="memoOBS" runat="server" ClientIDMode="AutoID"
                                                                    ClientInstanceName="memoOBS" CssClass="memoOBS"
                                                                    CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue"
                                                                    Height="100px" SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css"
                                                                    Width="500px" MaxLength="150" ClientSideEvents-KeyPress="keyUpMemoSold">
<ClientSideEvents KeyPress="keyUpMemoSold"></ClientSideEvents>

                                                                    <ValidationSettings ValidationGroup="GrupoJustificado">
                                                                        <RequiredField IsRequired="True"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxMemo>
                                                                <div style="display: none" class="limiteTexto"><span>0</span>/150</div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <dx:ASPxLabel ID="lbAlerta" runat="server" Visible="false" Text="" ForeColor="Red"></dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="style1">
                                                        <tr>
                                                            <td class="style31">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style31">
                                                                <dx:ASPxButton ID="btRetornar" runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btRetornar" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                                                    CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                                                    Text="Retornar" Theme="DevEx" ValidationGroup="ValidaMotivoFalta" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {
AtividadeAnterior();
}" />
                                                                    <ClientSideEvents Click="function(s, e) {
AtividadeAnterior();
}"></ClientSideEvents>
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btSalvarAvanc" runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btSalvarAvanc"
                                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                                    EnableTheming="True" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                                                    Text="Avançar" Theme="DevEx" ValidationGroup="ValidaMotivoFalta" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {if(ASPxClientEdit.ValidateGroup('GrupoJustificado'))ControleJustificativa();}" />
                                                                    <ClientSideEvents Click="function(s, e) {if(ASPxClientEdit.ValidateGroup(&#39;GrupoJustificado&#39;))ControleJustificativa();}"></ClientSideEvents>
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxCallbackPanel>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <asp:HiddenField ID="coIDFrequencia" runat="server" />
                <asp:HiddenField ID="coIDVinculoUsuario" runat="server" />
                <asp:HiddenField ID="coDTFrequencia" runat="server" />

                <dx:ASPxHiddenField ID="hdJustificativa" runat="server"
                    ClientInstanceName="hdJustificativa">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="hdItensJustificativa" runat="server"
                    ClientInstanceName="hdItensJustificativa">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="coIDUsuarioSetorManut" runat="server"
                    ClientInstanceName="coIDUsuarioSetorManut">
                </dx:ASPxHiddenField>
                <asp:HiddenField ID="coIDUsuarioModal" runat="server" />
                <dx:ASPxPopupControl ID="popDetalheFrequencia" runat="server"
                    ClientInstanceName="popDetalheFrequencia" CloseAction="CloseButton"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    HeaderText="Detalhe da solicitação de justificativa"
                    Modal="True" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                    Width="400px" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" EnableHotTrack="False">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="7px" />
                    </HeaderStyle>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table class="style1">
                                <tr>
                                    <td>Data:</td>
                                    <td>
                                        <dx:ASPxTextBox ID="tbData" runat="server" ClientIDMode="Predictable"
                                            ClientInstanceName="tbDataUsuario" ReadOnly="True" Width="200px" CssClass="tbData">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Motivo:</td>
                                    <td>
                                        <dx:ASPxTextBox ID="tbMotivoFalta" runat="server" ClientIDMode="AutoID"
                                            ClientInstanceName="tbMotivoFaltaUsuario" ReadOnly="True" Width="400px" CssClass="tbMotivoFalta">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Descrição</td>
                                    <td>
                                        <dx:ASPxMemo ID="memoOBSS" runat="server" ClientIDMode="AutoID"
                                            ClientInstanceName="memoOBSSUsuario" Height="50px" ReadOnly="True" Width="420px"
                                            MaxLength="150" CssClass="memoOBSS">
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>
                                <tr class="descGestor">
                                    <td>Descrição Gestor</td>
                                    <td>
                                        <dx:ASPxMemo ID="memoOBSSUsuarioGestor" runat="server" ClientIDMode="AutoID"
                                            ClientInstanceName="memoOBSSUsuarioGestor" Height="50px" ReadOnly="True" Width="420px"
                                            MaxLength="150" CssClass="">
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2">
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btExcluirSolicitacao" runat="server" ClientIDMode="AutoID" ClientInstanceName="btExcluirSolicitacao"
                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Excluir Solicitação"
                                                        Width="150px" AutoPostBack="False">
                                                        <ClientSideEvents Click="function(s, e) {
ExcuirPopSolJusti();
}" />
                                                        <ClientSideEvents Click="function(s, e) {
ExcuirPopSolJusti();
}"></ClientSideEvents>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False"
                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Fechar"
                                                        Width="100px">
                                                        <ClientSideEvents Click="function(s, e) {
	PopSolJusti();
}" />
                                                        <ClientSideEvents Click="function(s, e) {
	PopSolJusti();
}"></ClientSideEvents>
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style6" colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                            </table>
                        </dx:PopupControlContentControl>

                    </ContentCollection>
                    <ClientSideEvents CloseUp="function(s, e) {  }" PopUp="function(s, e) { }" />
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl ID="pclancaFalta" runat="server"
                    ClientInstanceName="pcLancaFalta"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    HeaderText="Permitir frequência ou lançar falta"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="570px"
                    Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
                    ContentUrl="~/Manutencao/frmLancaFalta.aspx" Height="400px"
                    EnableHotTrack="False">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="7px" />
                    </HeaderStyle>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl ID="pcJustificativaColetiva" runat="server"
                    ClientInstanceName="pcJustificativaColetiva"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    HeaderText="Justificativa Coletiva"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="570px"
                    Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
                    EnableHotTrack="False">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="7px" />
                    </HeaderStyle>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table class="style1">
                                <tr>
                                    <td class="style28">Setor:</td>
                                    <td>
                                        <dx:ASPxComboBox ID="cbSetor0" runat="server" ClientInstanceName="cbSetor"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            DataMember="TBSetor" DropDownStyle="DropDown"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor"
                                            ValueField="IDSetor" Width="300px" Spacing="0"
                                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
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
                                </tr>
                                <tr>
                                    <td class="style28">Motivo:</td>
                                    <td>
                                        <dx:ASPxComboBox ID="cbMotivoFalta" runat="server"
                                            ClientInstanceName="cbMotivoFalta"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            DataMember="TBMotivoFalta" DropDownStyle="DropDown"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                            TextField="DSMotivoFalta" ValueField="IDMotivoFalta" Width="300px"
                                            Spacing="0" EnableIncrementalFiltering="True"
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
                                </tr>
                                <tr>
                                    <td class="style28">Data:</td>
                                    <td>
                                        <dx:ASPxDateEdit ID="deDataFalta" runat="server"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                            CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                            Width="300px" Spacing="0">
                                            <CalendarProperties ClearButtonText="Deletar" TodayButtonText="Hoje">
                                                <HeaderStyle Spacing="1px" />
                                            </CalendarProperties>
                                            <ValidationSettings ValidationGroup="ValidaGrupo">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style28">Observação:</td>
                                    <td>
                                        <dx:ASPxMemo ID="memoOBS0" runat="server"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            Height="36px" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                            Width="300px">
                                            <ValidationSettings ValidationGroup="ValidaGrupo">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                            <table class="style1">
                                <tr>
                                    <td class="style29">
                                        <dx:ASPxButton ID="btOKJustificativa" runat="server" CausesValidation="False"
                                            ClientInstanceName="btOKJustificativa"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="OK"
                                            Width="100px" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
JustificativaColetiva();
}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btCancelJustificativa" runat="server"
                                            ClientInstanceName="btCancelJustificativa"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                            Style="margin-left: 0px" Text="Cancelar" Width="100px"
                                            AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
	FecharPopJustificativaColetiva();
}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl ID="popImagem" runat="server"
                    ClientInstanceName="popImagem"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    CssPostfix="DevEx" HeaderText="Funcionário"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                    Width="220px" ContentUrl="~/Cadastro/frmfoto.aspx" Height="270px"
                    Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" EnableHotTrack="False">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="7px" />
                    </HeaderStyle>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <asp:HiddenField ID="coJustColetiva" runat="server" />
                <dx:ASPxPopupControl ID="popExcluir" runat="server"
                    ClientInstanceName="popResposta" CloseAction="None"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    HeaderText="Justificativa" PopupAction="None"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="420px"
                    EnableHotTrack="False">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="7px" />
                    </HeaderStyle>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table class="style1">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <table class="style1">
                                <tr>
                                    <td class="style25" style="text-align: right">
                                        <dx:ASPxButton ID="btOK" runat="server"
                                            ClientInstanceName="btOkExlcuir"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="OK"
                                            Width="100px" CausesValidation="False"
                                            AutoPostBack="False" Theme="DevEx">
                                            <ClientSideEvents Click="function(s, e) {
FechaPopResposta();
}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl ID="popExcluir0" runat="server"
                    ClientInstanceName="popExcluirJust" CloseAction="None"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    HeaderText="Excluir Justificativa" Modal="True" PopupAction="None"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="320px"
                    EnableHotTrack="False">
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <HeaderStyle>
                        <Paddings PaddingLeft="7px" />
                    </HeaderStyle>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table class="style43">
                                <tr>
                                    <td class="style42">Deseja realmente excluir a justificativa selecionada ?</td>
                                </tr>
                            </table>
                            <table class="style1">
                                <tr>
                                    <td class="style25" style="text-align: right">
                                        <dx:ASPxButton ID="btOK0" runat="server"
                                            ClientInstanceName="btOkExlcuirJust"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="OK"
                                            Width="100px" CausesValidation="False"
                                            AutoPostBack="False" OnClick="btOK0_Click" Theme="DevEx">
                                            <ClientSideEvents Click="function(s, e) {FecharPopExcluirJust();}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btCancel0" runat="server"
                                            ClientInstanceName="btCancelExcluirJust"
                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                            Style="margin-left: 0px" Text="Cancelar" Width="100px">
                                            <ClientSideEvents Click="function(s, e) {
	FecharPopExcluirJust1();
}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </td>
        </tr>
    </table>
    </td>
        </tr>
    </table>


    
</asp:Content>

