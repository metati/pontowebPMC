<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmManutencaoFrequencia.aspx.cs" Inherits="Manutencao_frmManutencaoFrequencia" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxwschsc" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxDataView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .dxeBase {
            font-family: Tahoma;
            font-size: x-small;
            color: #000000;
            font-weight: 700;
        }

        .style6 {
        }

        .style7 {
            width: 52px;
        }

        .style9 {
            font-size: medium;
        }

        .style11 {
        }

        .style13 {
            height: 26px;
        }

        .style15 {
        }

        .style16 {
            width: 90%;
            margin-left: 105px;
        }

        .style17 {
            width: 61px;
        }

        .style18 {
            width: 63px;
        }

        .style19 {
            width: 83px;
        }

        .style24 {
            width: 646px;
        }

        .style25 {
            width: 76px;
        }

        .dxpcControl_Office2003Olive {
            font: 12px Tahoma;
            color: black;
            background-color: white;
            border: 1px solid #758D5E;
            width: 200px;
        }

        .dxpcHeader_Office2003Olive {
            border-bottom: 1px solid #758D5E;
            padding: 6px 6px 6px 12px;
        }

        .dxpcCloseButton_Office2003Olive {
            padding: 1px 1px 1px 2px;
        }

        .dxpcContent_Office2003Olive {
            background-color: white;
            white-space: normal;
            border-width: 0px;
            vertical-align: top;
        }

        .dxpcContentPaddings_Office2003Olive {
            padding: 9px 12px 30px;
        }

        .style27 {
            width: 273px;
        }

        .style28 {
            width: 81px;
        }

        .style29 {
            width: 94px;
        }

        .style30 {
            height: 26px;
            width: 94px;
        }

        .style31 {
            width: 90px;
        }

        .style38 {
            width: 216px;
        }

        .style40 {
            width: 543px;
        }

        .style41 {
            width: 227px;
        }

        .style42 {
            width: 248px;
        }

        .style43 {
            width: 277px;
        }

        .auto-style3 {
            height: 25px;
            width: 25px;
        }

        .auto-style4 {
            width: 1224px;
        }

        .auto-style5 {
            font-size: medium;
            width: 1224px;
        }

        .auto-style7 {
            width: 78%;
        }

        .auto-style8 {
            width: 506px;
        }

        .auto-style10 {
            width: 417px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">

        var Fase;
        var TextoOBS = "";
        var idU = <%=Session["IDUsuario"].ToString()%>;
        var idE = <%=Session["IDEmpresa"].ToString()%>;
        function MudaPagina() {

            var Ano, hoje, idsetor;
            idsetor = coIDUsuarioSetorManut.Get("IDsetorFolha");
            idusuario = coIDUsuarioSetorManut.Get("IDusuarioFolha");


            hoje = new Date();
            if (rbAnoManutencao.GetValue() == "Ano Corrente") {

                Ano = 0;
            }
            else
                Ano = 1;

            window.open('/Relatorio/frmVizualizaRelatorio.aspx?Mes=' + cbMesManut.GetValue() + '&Setor=' + coIDUsuarioSetorManut.Get("IDsetorFolha") + '&User=' + coIDUsuarioSetorManut.Get("IDusuarioFolha") + '&Ano=' + Ano + '&IDVinculoUsuario=' + coIDUsuarioSetorManutV.Get("IDVinculoUsuario") + '&Rel=frmZurel');

            gridFrequencia.PerformCallback(' ');
        }

        function FecharPopJustificativaColetiva() {
            pcJustificativaColetiva.Hide();
        }

        function AbreJustificativaColetiva() {

            Fase = 0;
            TextoOBS = "";
            hdItensJustificativa.Clear();
            hdItensJustificativa.Set("Modo", 'Coletivo');
            FechaPopJust();
            pcCadastraFalta.Show();
            //pcJustificativaColetiva.Show();
        }

        function PreenchecbUsuario() {
            cbUsuarioManut.PerformCallback();
        }
        function PreenchecoUsuario(param) {
            cbMesManut.PerformCallback();
        }

        function PreenchecoUsuarioMotivo(param) {
            cbMotivoFaltaManut.PerformCallback();
        }
        function JustificativaColetiva() {
            gridFrequencia.PerformCallback('1');
            pcJustificativaColetiva.Hide();
        }

        function VinculaSetor() {
            hdItensJustificativa.Set("SetorColetiva", '');
            hdItensJustificativa.Set("SetorColetiva", cbSetorModo.GetValue());
        }

        function VinculaData() {
            hdItensJustificativa.Set("DataColetiva", '');
            hdItensJustificativa.Set("DataColetiva", deDataFaltaModo.GetDate());
        }

        function Justificar(IDUsuario, IDFrequencia, DataFrequencia, HorasTotais, TotalHorasDiarias, IDVinculoUsuario) {
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

        function ControleJustificativa() {
            debugger
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
                gridFrequencia.PerformCallback('');
            }
        }

        function RespostaJustificativa() {
            lbResposta.SetVisible(false);
        }

        function AtividadeAnterior() {

            Fase--;

            if (btRetornar.GetText() == "Cancelar") {
                Fase = 0;
                TextoOBS = "";
            }
            cpJust.PerformCallback(Fase);
        }

        function FechaPopJust() {
            cpJust.PerformCallback(0);
        }

        function AbrePopDetalheFrequencia(Nome, Data, Motivo, OBS, sitJust, IDFrequencia, HorasTotais, TotalHorasDiarias, IDMotivoFalta, IDUsuario, TPJust, IDVinculoUsuario) {
            debugger
            //trata o nome
            Nome = Nome.substring(1, Nome.length - 1);

            if (Nome.indexOf("1") > (0)) {
                var posicao = Nome.indexOf("1");
                Nome = Nome.substring(0, posicao) + "'" + Nome.substring(posicao + 1, (Nome.length));
            }

            $('.limiteTexto').hide();
           // $.getJSON('http://localhost:65424/vs1/justificativas/GetDetalhes/' + IDFrequencia, function (data) 
            $.getJSON('../api/vs1/justificativas/GetDetalhes/' + IDFrequencia, function (data) 
                {
                    if (sitJust == 1) {
                        hdItensSolicitacao.Clear();
                        hdItensSolicitacao.Set("Modo", 'Unitario');
                        hdItensSolicitacao.Set("IDUsuario", data.tb.idUsuario);
                        hdItensSolicitacao.Set("IDFrequencia", data.tb.idFrequencia);
                        hdItensSolicitacao.Set("DTJust", data.tb.dtJust);
                        hdItensSolicitacao.Set("TPJust", data.tb.idtpJustificativa);
                        hdItensSolicitacao.Set("TotDIa", data.tb.dtJust + ' ' + HorasTotais);
                        hdItensSolicitacao.Set("TotalHorasDiarias", TotalHorasDiarias);
                        hdItensSolicitacao.Set("IDMotivoFalta", data.tb.idMotivoFalta);
                        //hdItensSolicitacao.Set("Resp", data.tb.);
                        hdItensSolicitacao.Set("OBS", data.tb.obs);
                        hdItensSolicitacao.Set("IDVinculoUsuario", data.tb.idVinculoUsuario);
                        hdItensSolicitacao.Set("IDFrequencia", data.tb.idFrequencia);
                        hdItensSolicitacao.Set("INDEX", data.tb.indexU);
                    }
                    if (IDFrequencia != 0) {
                        tbData.SetValue(data.dtFrequencia);
                        tbMotivoFalta.SetValue(data.dsMotivoFalta);
                        memoOBSS.SetValue(data.obs);
                        tbNome.SetText(data.dsUsuario);
                        tbData.SetText(data.dtFrequencia);
                        tbMotivoFalta.SetText(data.dsMotivoFalta);
                        memoOBSSGestor.SetText(data.obsGestor);
                        $('#btnSolict').show();
                        $('#tdEditarGestor').hide();
                    }
                    else {
                        tbData.SetText(Data);
                        tbMotivoFalta.SetText(Motivo);
                        memoOBSS.SetText(OBS);
                        tbNome.SetText(Nome);
                        tbData.SetText(Data);
                        tbMotivoFalta.SetText(Motivo);
                        memoOBSSGestor.SetText("");
                        $('#tdEditarGestor').hide();
                    }

                    if (data != null && data.situacao == 2) {
                        btnAceitaSolict.SetVisible(true);
                        btnRejeitaSolict.SetVisible(true);
                        memoOBSSGestor.SetEnabled(true)
                        $('.descGestor').show()
                        $('#tdEditarGestor').hide();
                    } else {
                        btnAceitaSolict.SetVisible(false);
                        btnRejeitaSolict.SetVisible(false);
                        if (data != null && data.obsGestor.length > 0) {
                            window.idJustificativaPedido = data.tb.idJustificativaPedido;
                            memoOBSSGestor.SetValue(data.obsGestor);
                            memoOBSSGestor.SetEnabled(false)
                            $('.descGestor').show();
                            $('#tdEditarGestor').show();
                            $('#trEditarGestorSalvar').hide();
                            $('#trEditarGestor').show();
                        } else {
                            $('.descGestor').hide();
                        }
                    }
                    popDetalheFrequencia.Show();

                })

            //alert("Entrou");


            //hdItensSolicitacao.Set("Modo", 'Unitario');
            //hdItensSolicitacao.Set("IDUsuario", IDUsuario);
            //hdItensSolicitacao.Set("IDFrequencia", IDFrequencia);
            //hdItensSolicitacao.Set("DTJust", Data);
            //hdItensSolicitacao.Set("TPJust", TPJust);
            //hdItensSolicitacao.Set("TotDIa", Data + ' ' + HorasTotais);
            //hdItensSolicitacao.Set("TotalHorasDiarias", TotalHorasDiarias);
            //hdItensSolicitacao.Set("IDMotivoFalta", IDMotivoFalta);
            //hdItensSolicitacao.Set("IDVinculoUsuario", IDVinculoUsuario);
            //hdItensSolicitacao.Set("SitJust", sitJust);
            ////hdItensSolicitacao.Set("Resp", data.tb.);
            //hdItensSolicitacao.Set("OBS", OBS);
            //hdItensSolicitacao.Set("IDVinculoUsuario",);
            //hdItensSolicitacao.Set("IDFrequencia", IDFrequencia);
            //hdItensSolicitacao.Set("INDEX", data.tb.indexU);


        }

        function EditarGestor() {
            $('#trEditarGestor').hide();
            $('#trEditarGestorSalvar').show();
            memoOBSSGestor.SetEnabled(true);
        }

        function CancelarEditGestor() {
            $('#trEditarGestor').show();
            $('#trEditarGestorSalvar').hide();
            memoOBSSGestor.SetEnabled(false);
        }

        function SalvarEditGestor() {
            var det = {
                idJustificativaPedido: window.idJustificativaPedido,
                idEmpresa: idE,
                obsGestor: memoOBSSGestor.GetValue()
            }

            $.post('../api/vs1/justificativas/editarObs/' + idU, det, function (data) {
                $('#trEditarGestor').show();
                $('#trEditarGestorSalvar').hide();
                memoOBSSGestor.SetEnabled(false);
            });
        }

        function RejeitarJustificativa() {
            var det = {
                idFrequencia: hdItensSolicitacao.Get("IDFrequencia"),
                idEmpresa: idE,
                obsGestor: memoOBSSGestor.GetValue()
            }

            $.post('../api/vs1/justificativas/rejeitar/' + idU, det, function (data) {
                gridFrequencia.PerformCallback('REJU');
                popDetalheFrequencia.Hide();
                memoOBSSGestor.SetValue('');
                //alert('Solicitação rejeitada!');
            });
        }


        function AceitarJustificativa() {
            var det = {
                idFrequencia: hdItensSolicitacao.Get("IDFrequencia"),
                idEmpresa: idE,
                obsGestor: memoOBSSGestor.GetValue()
            }

            $.post('../api/vs1/justificativas/aceitar/' + idU, det, function (data) {
                gridFrequencia.PerformCallback('SOJU');
                popDetalheFrequencia.Hide();
                memoOBSSGestor.SetValue('');
                //alert('Solicitação rejeitada!');
            });
        }

        function keyUpMemoSold() {
            var text = memoOBS.GetValue();
            $('.limiteTexto').show();
            $('.limiteTexto span').text(text.length);
            if (text.length > 150) {
                memoOBS.SetValue(text.substring(0, 150))
            }
        }

        function keyUpMemoGestor() {
            var text = memoOBSSGestor.GetValue();
            $('.limiteTexto').show();
            $('.limiteTexto span').text(text.length);
            if (text.length > 150) {
                memoOBSSGestor.SetValue(text.substring(0, 150))
            }
        }

        function AbrePopExcluirJust(IDFrequencia, IDUsuario, dtfrequencia, IDVinculoUsuario) {

            document.getElementById("<%=coIDFrequencia.ClientID %>").value = IDFrequencia;
            document.getElementById("<%=coIDUsuario.ClientID %>").value = IDUsuario;
            document.getElementById("<%=coDTFrequencia.ClientID %>").value = dtfrequencia;
            document.getElementById("<%=coIDVinculoUsuario.ClientID %>").value = IDVinculoUsuario;
            popExcluirJust.Show();
        }

        function AbrePopExcluir(IDFrequencia) {
            document.getElementById("<%=coIDFrequencia.ClientID %>").value = IDFrequencia;
            popExcluir.Show();
        }
        function FecharPopExcluir() {
            popExcluir.Hide();
        }
        function FecharPopExcluirJust() {
            popExcluirJust.Hide();
            gridFrequencia.PerformCallback('J');
        }

        function FecharPopExcluirJust1() {
            popExcluirJust.Hide();
        }
        function AbrePopJustificativa() {
            var Opcao = cbSetor.GetValue();

            if (Opcao == '') {
                window.alert('Favor aplicar os filtros necessários para fazer justificativa ou selecionar uma frequência válida.');
            }
            else if (Opcao == 0) {
                window.alert('Favor aplicar os filtros necessários para fazer justificativa ou selecionar uma frequência válida.');
            }
            else if (Opcao > 0) {
                pcCadastraFalta.Show();
            }
        }

        function AbrePopLancaFalta(IDFrequencia) {
            document.getElementById("<%=coIDFrequencia.ClientID %>").value = IDFrequencia;
            pcLancaFalta.Show();
        }
        function FechaPopJustificativa() {
            pcCadastraFalta.Hide();
        }
        function FechaPopResposta() {
            popResposta.Hide();
        }
        function AbrePopSemRegistro() {
            popSemRegistro.Show();
            gridSemRegistro.PerformCallback();
        }
        function AbrePopFoto(Nome, IDUsuario, DSUsuario) {
            popImagem.SetContentUrl("/Cadastro/frmfoto.aspx?ID=" + IDUsuario + "&nome=" + Nome + "&Sobre=" + DSUsuario);
            popImagem.Show();
        }

        function RecebeIDgrid() {

            gridFrequencia.GetRowValues(gridFrequencia.GetFocusedRowIndex(), 'DSUsuario;DataFrequencia;OBS;MF', PegarValoresGrid);

            tbNome.SetText("Aguarde...");
            tbData.SetText("Aguarde...");
            tbMotivoFalta.SetText('Aguarde...');
            memoOBSS.SetText("Aguarde...");

            //window.alert(Item);
        }
        function PegarValoresGrid(valor) {
            tbNome.SetText(valor[0]);
            tbData.SetText(valor[1]);
            memoOBSS.SetText(valor[2]);
            tbMotivoFalta.SetText(valor[3]);
        }
        function LimpaCampos() {
            tbNome.SetText('');
            tbData.SetText('');
            memoOBSS.SetText('');
            tbMotivoFalta.SetText('');
        }
    </script>
    <table class="style38">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style4">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">REALIZAR JUSTIFICATIVA</div>

            </td>
            <tr>
                <td class="auto-style5">
                    <strong style="font-weight: normal">
                        <img alt="" class="auto-style3" src="../Images/Imagem20.png" />Manutenção de Registros</strong></td>
                <tr>
                    <td class="auto-style5">
                        <table class="auto-style7" style="font-weight: normal">
                            <tr>
                                <td align="right" class="auto-style8">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Setor:" Font-Size="Small" Font-Bold="False">
                                    </dx:ASPxLabel>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetorManut"
                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" DataMember="TBSetor"
                                        DropDownStyle="DropDown"
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor"
                                        ValueField="IDSetor" Width="400px" Spacing="0" IncrementalFilteringMode="Contains"
                                        EnableIncrementalFiltering="True"
                                        LoadingPanelText="Processando&amp;hellip;" Theme="DevEx">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {PreenchecbUsuario();}" />
                                        <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <ValidationSettings ValidationGroup="ValidaGrupo">
                                            <RequiredField IsRequired="False" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="auto-style8">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Servidor:"
                                        Font-Size="Small" Font-Bold="False">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                        TextField="DSUsuario" ValueField="IDVinculoUsuario" Spacing="0" Width="400px"
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="vwNomeUsuario"
                                        CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                        ClientInstanceName="cbUsuarioManut" ID="cbUsuario" OnCallback="cbUsuario_Callback" OnSelectedIndexChanged="cbUsuario_SelectedIndexChanged">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
                                        <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

                                        <LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                                        <ValidationSettings ValidationGroup="ValidaGrupo">
                                            <RequiredField IsRequired="False"></RequiredField>
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>


                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="auto-style8">
                                    <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Mês Referência:"
                                        Font-Size="Small" Font-Bold="False">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                        TextField="DSMes" ValueField="IDMes" Spacing="0" Width="400px"
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" DataMember="TBMes"
                                        CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                        ClientInstanceName="cbMesManut" ID="cbMes" OnCallback="cbMes_Callback">
                                        <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

                                        <LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

                                        <ValidationSettings ValidationGroup="ValidaGrupo">
                                            <RequiredField IsRequired="False"></RequiredField>
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>


                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="auto-style8">&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="cbOcorrencia" runat="server" Checked="False" Text="Filtrar por atrasos e faltas injustificadas" />


                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="auto-style8">
                                    <asp:HiddenField ID="dtJust" runat="server" />
                                </td>
                                <td>
                                    <dx:ASPxRadioButtonList ID="rbAno" runat="server"
                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                        SelectedIndex="0"
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px"
                                        ClientInstanceName="rbAnoManutencao" RepeatDirection="Horizontal" Theme="DevEx">
                                        <Items>
                                            <dx:ListEditItem Selected="True" Text="Ano Corrente" Value="Ano Corrente" />
                                            <dx:ListEditItem Text="Ano Anterior" Value="Ano Anterior" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>


                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="auto-style8">
                                    <asp:HiddenField ID="coTotDia" runat="server" />
                                </td>
                                <td align="left">
                                    <dx:ASPxButton ID="btFiltraBuscaManutencao" runat="server"
                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Buscar"
                                        Width="100px"
                                        ValidationGroup="ValidaGrupo" AutoPostBack="False"
                                        ClientInstanceName="btFiltraBuscaManutencao" Theme="iOS" ToolTip="Buscar">
                                        <ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))PesquisaGridFrequencia();
}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <table class="style1" style="color: #000000">
                        <tr>
                            <td class="style29"></td>
                            <td class="style15">
                                <table class="style16">
                                    <tr>
                                        <td align="right" class="auto-style10">
                                            <dx:ASPxButton ID="btJustificativaColetiva" runat="server"
                                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Lançar justificativa Coletiva"
                                                Width="300px" AutoPostBack="False"
                                                ClientInstanceName="btJustificativaColetiva" Theme="DevEx">
                                                <ClientSideEvents Click="function(s, e) {
	AbreJustificativaColetiva();	
}" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td align="left">
                                            <dx:ASPxButton ID="btGerarFolha" runat="server"
                                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Gerar espelho de ponto"
                                                Width="300px"
                                                ValidationGroup="ValidaGrupo"
                                                ClientInstanceName="btGerarFolha" Theme="DevEx" OnClick="btGerarFolha_Click"
                                                AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {}" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="color: #000000">
                                <dx:ASPxGridView ID="gridFrequencia" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                    CssPostfix="DevEx" DataMember="vWHorasDia"
                                    Width="1200px" ClientInstanceName="gridFrequencia"
                                    KeyFieldName="IDFrequencia"
                                    OnBeforeColumnSortingGrouping="gridFrequencia_BeforeColumnSortingGrouping"
                                    OnCustomCallback="gridFrequencia_CustomCallback"
                                    OnPageIndexChanged="gridFrequencia_PageIndexChanged"
                                    OnProcessColumnAutoFilter="gridFrequencia_ProcessColumnAutoFilter"
                                    OnHtmlRowPrepared="gridFrequencia_HtmlRowPrepared"
                                    OnCustomColumnGroup="gridFrequencia_CustomColumnGroup"
                                    OnCustomColumnSort="gridFrequencia_CustomColumnSort"
                                    OnDetailRowExpandedChanged="gridFrequencia_DetailRowExpandedChanged"
                                    OnDetailsChanged="gridFrequencia_DetailsChanged" EnableTheming="True" Theme="DevEx">
                                    <ClientSideEvents RowDblClick="function(s, e) {	RecebeIDgrid();}" EndCallback="function(s, e) {
}" />
                                    <ClientSideEvents RowDblClick="function(s, e) {	RecebeIDgrid();}"></ClientSideEvents>
                                    <Settings ShowFilterRow="True" ShowVerticalScrollBar="True" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="IDFrequencia" Visible="False"
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IDUsuario"
                                            Visible="False" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nome" FieldName="DSUsuario"
                                            VisibleIndex="6" Width="110px">
                                            <Settings AllowAutoFilter="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Data" FieldName="DataFrequencia"
                                            VisibleIndex="7" Width="95px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EntradaManha" VisibleIndex="8"
                                            Width="100px" Caption="Entrada 1">
                                            <Settings AllowAutoFilter="False" />
                                            <Settings AllowAutoFilter="False"></Settings>
                                            <EditCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </EditCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SaidaManha" VisibleIndex="9"
                                            Width="100px" Caption="Saída 1">
                                            <Settings AllowAutoFilter="False" />
                                            <Settings AllowAutoFilter="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EntradaTarde" VisibleIndex="10"
                                            Width="100px" Caption="Entrada 2">
                                            <Settings AllowAutoFilter="False" />
                                            <Settings AllowAutoFilter="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SaidaTarde" VisibleIndex="11"
                                            Width="100px" Caption="Saída 2">
                                            <Settings AllowAutoFilter="False" />
                                            <Settings AllowAutoFilter="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Total do Dia" FieldName="HorasDia"
                                            VisibleIndex="15" Width="100px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TotHorasDiarias" Visible="False"
                                            VisibleIndex="14">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="MF"
                                            Visible="False" VisibleIndex="13">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IMF" Visible="False" VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="OBS" Visible="False" VisibleIndex="12">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Justificar" VisibleIndex="0" Width="55px">
                                            <EditCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </EditCellStyle>
                                            <DataItemTemplate>
                                                <a href="javascript:void(0);" onclick="Justificar('<%# Eval("IDusuario") %>','<%# Eval("IDFrequencia") %>','<%# Eval("DataFrequencia") %>','<%# Eval("HorasDia") %>','<%# Eval("TotHorasDiarias") %>','<%# Eval("IDVinculoUsuario") %>')"><font color="#000000">Justificar</font></a>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Detalhar Justificativa" VisibleIndex="1"
                                            Width="98px">
                                            <DataItemTemplate>
                                                <a href="javascript:void(0);" onclick="AbrePopDetalheFrequencia('<%# TrataNome(Container) %>','<%# Eval("DataFrequencia") %>','<%# Eval("MF") %>','','<%# Eval("SituacaoJustificativa") %>', '<%# Eval("IDFrequencia") %>', '<%# Eval("HorasDia") %>' ,'<%# Eval("TotHorasDiarias") %>','<%# Eval("IMF") %>','<%# Eval("IDusuario") %>','<%# Eval("IDTPJustificativa") %>','<%# Eval("IDVinculoUsuario") %>')"><font color="#000000">Detalhar Just.</font></a>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Excluir Justificativa" VisibleIndex="2"
                                            Width="94px">
                                            <DataItemTemplate>
                                                <a href="javascript:void(0);" onclick="AbrePopExcluirJust('<%# Eval("IDFrequencia") %>','<%# Eval("IDusuario") %>','<%# Eval("DataFrequencia") %>','<%# Eval("IDVinculoUsuario") %>')"><font color="#000000">Excluir Justif.</font></a>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IDVinculoUsuario" Visible="False"
                                            VisibleIndex="21">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IDRegimeHora" Visible="False" VisibleIndex="20">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SituacaoJustificativa" Visible="False" VisibleIndex="21">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IDusuario" Visible="False" VisibleIndex="19">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IDTPJustificativa" Visible="False" VisibleIndex="18">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DescontoTotalJornada" Visible="False" VisibleIndex="17">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IsencaoPonto" Visible="False" VisibleIndex="16">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsPager>
                                        <AllButton Text="All">
                                        </AllButton>
                                        <NextPageButton Text="Próx &gt;">
                                        </NextPageButton>
                                        <PrevPageButton Text="&lt; Ant">
                                        </PrevPageButton>
                                        <Summary Text="Página {0} de {1} ({2} itens)" />

                                        <Summary Text="P&#225;gina {0} de {1} ({2} itens)"></Summary>
                                    </SettingsPager>
                                    <Settings ShowVerticalScrollBar="True" />
                                    <SettingsText EmptyDataRow="Sem dados para exibir"
                                        GroupPanel="Para agrupar arraste uma coluna aqui" />
                                    <SettingsLoadingPanel Text="Processando&amp;hellip;" />

                                    <Settings ShowVerticalScrollBar="True"></Settings>

                                    <SettingsText GroupPanel="Para agrupar arraste uma coluna aqui" EmptyDataRow="Sem dados para exibir"></SettingsText>

                                    <SettingsLoadingPanel Text="Processando&amp;hellip;"></SettingsLoadingPanel>

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
                                <table class="style16">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="lblegenda2" runat="server" Font-Size="Medium"
                                                Text="Jornada Completa">
                                            </dx:ASPxLabel>
                                            Jornada Completa</td>
                                        <td>
                                            <dx:ASPxLabel ID="lblegenda3" runat="server" Font-Size="Medium"
                                                Text="Jornada na Tolerância">
                                            </dx:ASPxLabel>
                                            Jornada Tolerada</td>
                                        <td>
                                            <dx:ASPxLabel ID="lblegenda1" runat="server" Font-Size="Medium"
                                                Text="Jornada Justificada">
                                            </dx:ASPxLabel>
                                            Jornada Justificada</td>
                                        <td>
                                            <dx:ASPxLabel ID="lblegenda4" runat="server" Font-Size="Medium"
                                                Text="Jornada Incompleta">
                                            </dx:ASPxLabel>
                                            Jornada Incompleta</td>
                                        <td>
                                            <dx:ASPxLabel ID="lblegenda5" runat="server" Font-Size="Medium"
                                                Text="Justificativa Pendente de Análise">
                                            </dx:ASPxLabel>
                                            Justificativa Pendente de Análise</td>
                                        <td>
                                            <dx:ASPxLabel ID="lblegenda6" runat="server" Font-Size="Medium"
                                                Text="Justificativa Rejeitada">
                                            </dx:ASPxLabel>
                                            Justificativa Rejeitada</td>
                                    </tr>
                                </table>
                            </td>
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
                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" ClientIDMode="AutoID"
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
                                                            ValueField="IDMotivoFalta" Width="400px">
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
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" ClientIDMode="AutoID"
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
                                                                                    ClientInstanceName="memoOBS"
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
                                                                                    <ClientSideEvents Click="function(s, e) {if(ASPxClientEdit.ValidateGroup('GrupoJustificado'))ControleJustificativa();}"></ClientSideEvents>
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
                                <dx:ASPxHiddenField ID="hdItensSolicitacao" runat="server"
                                    ClientInstanceName="hdItensSolicitacao">
                                </dx:ASPxHiddenField>
                                <dx:ASPxHiddenField ID="coIDUsuarioSetorManut" runat="server" ClientInstanceName="coIDUsuarioSetorManut"></dx:ASPxHiddenField>
                                <dx:ASPxHiddenField ID="coIDUsuarioSetorManutV" runat="server" ClientInstanceName="coIDUsuarioSetorManutV"></dx:ASPxHiddenField>

                                <asp:HiddenField ID="coIDUsuario" runat="server" />
                                <dx:ASPxPopupControl ID="popDetalheFrequencia" runat="server"
                                    ClientInstanceName="popDetalheFrequencia" CloseAction="CloseButton"
                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                    HeaderText="Detalhe da frequência"
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
                                                    <td>Nome:</td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="tbNome" runat="server" ClientIDMode="AutoID"
                                                            ClientInstanceName="tbNome" ReadOnly="True" Width="420px">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Data:</td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="tbData" runat="server" ClientIDMode="AutoID"
                                                            ClientInstanceName="tbData" ReadOnly="True" Width="420px">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Motivo:</td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="tbMotivoFalta" runat="server" ClientIDMode="AutoID"
                                                            ClientInstanceName="tbMotivoFalta" ReadOnly="True" Width="420px">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Descr.<br />
                                                        Solicit.:</td>
                                                    <td>
                                                        <dx:ASPxMemo ID="memoOBSS" runat="server" ClientIDMode="AutoID"
                                                            ClientInstanceName="memoOBSS" Height="50px" ReadOnly="True" Width="420px"
                                                            MaxLength="150">
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr class="descGestor">
                                                    <td>Descr.<br />
                                                        Gestor:</td>
                                                    <td>
                                                        <dx:ASPxMemo ID="memoOBSS0" runat="server" ClientIDMode="AutoID" ClientInstanceName="memoOBSSGestor" Height="50px" MaxLength="150" Width="420px" ClientSideEvents-KeyPress="keyUpMemoGestor">
                                                            <ValidationSettings CausesValidation="True" ErrorText="" ValidationGroup="ValidaJust">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                        <div style="display: none" class="limiteTexto"><span>0</span>/150</div>
                                                    </td>
                                                </tr>
                                                <tr id="btnSolict">
                                                    <td colspan="2">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxButton ID="btnAceitaSolict" runat="server" ClientIDMode="Static"
                                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Aceitar Justificativa"
                                                                        Width="150px" AutoPostBack="False" ValidationGroup="ValidaJust">
                                                                        <ClientSideEvents Click="function(s, e) {
if(ASPxClientEdit.ValidateGroup('ValidaJust'))
AceitarJustificativa();
}" />
                                                                        <ClientSideEvents Click="function(s, e) {
AceitarJustificativa();
}"></ClientSideEvents>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxButton ID="btnRejeitaSolict" runat="server" AutoPostBack="False" ClientIDMode="Static"
                                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Rejeitar Justificativa"
                                                                        Width="150px" ValidationGroup="ValidaJust">
                                                                        <ClientSideEvents Click="function(s, e) {
if(ASPxClientEdit.ValidateGroup('ValidaJust'))
RejeitarJustificativa();
}" />
                                                                        <ClientSideEvents Click="function(s, e) {
	RejeitarJustificativa();
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
                                                <tr id="tdEditarGestor">
                                                    <td colspan="2">
                                                        <table>
                                                            <tr id="trEditarGestorSalvar">
                                                                <td>
                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" ClientIDMode="Static"
                                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cancelar"
                                                                        Width="150px" AutoPostBack="False" ValidationGroup="ValidaJust">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                            CancelarEditGestor();
                                                                        }" />
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                            CancelarEditGestor();
                                                                         }"></ClientSideEvents>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxButton ID="btnSalvarGestor" runat="server" AutoPostBack="False" ClientIDMode="Static"
                                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar"
                                                                        Width="150px" ValidationGroup="ValidaJust">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                SalvarEditGestor();
                                                                            }" />
                                                                        <ClientSideEvents Click="function(s, e) {
	                                                                        SalvarEditGestor();
                                                                        }"></ClientSideEvents>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                            <tr id="trEditarGestor">
                                                                <td>
                                                                    <dx:ASPxButton ID="btnEditarGestor" runat="server" AutoPostBack="False" ClientIDMode="Static"
                                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Editar Texto"
                                                                        Width="150px" ValidationGroup="ValidaJust">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                EditarGestor();
                                                                        }" />
                                                                        <ClientSideEvents Click="function(s, e) {
	                                                                        EditarGestor();
                                                                        }"></ClientSideEvents>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                </tr>
                                            </table>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
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
                                <dx:ASPxPopupControl ID="popSemRegistro" runat="server"
                                    ClientInstanceName="popSemRegistro"
                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                    HeaderText="Funcionários sem registros para hoje"
                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="320px"
                                    Modal="True" EnableHotTrack="False">
                                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                                    </LoadingPanelImage>
                                    <HeaderStyle>
                                        <Paddings PaddingLeft="7px" />
                                    </HeaderStyle>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ContentCollection>
                                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="gridSemRegistro" runat="server"
                                                AutoGenerateColumns="False" ClientInstanceName="gridSemRegistro"
                                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                DataMember="TBUsuario" KeyFieldName="IDUsuario"
                                                OnCustomCallback="gridSemRegistro_CustomCallback" Width="300px"
                                                OnPageIndexChanged="gridSemRegistro_PageIndexChanged">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Nome" FieldName="PrimeiroNome"
                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Sobrenome" FieldName="DSUsuario"
                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Foto" FieldName="FotoUsuario"
                                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <a href="javascript:void(0);" onclick="AbrePopFoto('<%#Eval("PrimeiroNome") %>','<%# Eval("IDUsuario") %>','<%# Eval("DSUsuario") %>')">
                                                                <dx:ASPxBinaryImage ID="ImgFoto" Width="40px" Height="20px" runat="server" Value='<%# Eval("FotoUsuario") %>'>
                                                                    <EmptyImage AlternateText="Sem Imagem"></EmptyImage>
                                                                </dx:ASPxBinaryImage>
                                                            </a>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowGroup="False" AllowSort="False" />
                                                <SettingsPager>
                                                    <AllButton Text="All">
                                                    </AllButton>
                                                    <NextPageButton Text="Próx&gt;">
                                                    </NextPageButton>
                                                    <PrevPageButton Text="&lt; Ant">
                                                    </PrevPageButton>
                                                    <Summary Text="Página {0} de {1} ({2} itens)" />
                                                </SettingsPager>
                                                <Settings ShowVerticalScrollBar="True" />
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
                                                            AutoPostBack="False" Theme="DevEx">
                                                            <ClientSideEvents Click="function(s, e) {FecharPopExcluirJust();}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btCancel0" runat="server"
                                                            ClientInstanceName="btCancelExcluirJust"
                                                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                            AutoPostBack="False"
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

