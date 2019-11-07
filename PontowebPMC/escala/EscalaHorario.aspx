<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EscalaHorario.aspx.cs" Inherits="PontoWeb.API.EscalaHorario" %>

<!DOCTYPE html>

<html>
<head>
    <title>Escala Horários</title>
    <link rel="stylesheet" type="text/css" href="jquery/jquery-ui.min.css">
    <link rel="stylesheet" type="text/css" href="jquery/jquery-ui.theme.css">
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/w3.css">
    <link href="css/estilo.css" rel="stylesheet">

    <style type="text/css">
        img {
            width: 20px;
        }

        .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > td, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > thead > tr > th {
            border: 1px solid silver;
        }

        body {
            -webkit-print-color-adjust: exact;
        }

        table span {
            display: block;
            height: 100%;
            width: 100%;
        }

        .event {
            cursor: pointer;
        }

        .page-header {
            background: #007C26;
            color: white;
            PADDING: 9px;
            margin: 0px;
            border-bottom: 1px solid #eee;
        }

        #regimeHorario .modal-dialog {
            width: 900px;
        }

        #tabela tbody th:first-child {
            white-space: nowrap;
        }

        .alterar {
            background: red;
        }

        .tblFilter {
            padding: 5px 0px 5px 0px;
        }

        .imgFolga {
            margin-bottom: 0px;
        }

        .dadosUsuario {
            color: #337ab7 !important;
            text-decoration: none !important;
            cursor: pointer;
        }
    </style>

    <script src="jquery/external/jquery/jquery.js"></script>
    <script src="jquery/jquery-ui.js"></script>

    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/jquery.formatter.min.js"></script>
    <meta charset="utf-8" />
</head>
<body>
    <div class="page-header" style="height: 94px">
        <img src="img/logo.jpg" style="float: left; position: absolute; width: 100px" />
        <h4>ESCALA DE HORÁRIOS<br />
            <small style="color: white"><%=Empresa %></small></h4>

    </div>
    <div class="">
        <table bordercolor="#003399" height="20" cellspacing="1" bordercolordark="#003399" bordercolorlight="#003399" border="1" id="tabela" class="tamanho w3-card-8">
            <thead>
                <tr>
                    <th colspan="41" class="azul tblHeader">
                        <select id="mes" class="input-lg" style="width: 20%; text-align: center; text-align-last: center;">
                            <option value="0"></option>
                            <option value="1">Janeiro</option>
                            <option value="2">Fevereiro</option>
                            <option value="3">Março</option>
                            <option value="4">Abril</option>
                            <option value="5">Maio</option>
                            <option value="6">Junho</option>
                            <option value="7">Julho</option>
                            <option value="8">Agosto</option>
                            <option value="9">Setembro</option>
                            <option value="10">Outubro</option>
                            <option value="11">Novembro</option>
                            <option value="12">Dezembro</option>
                        </select>
                        <%--<button id="btnEscolher" class="btn btn-success">Carregar</button>--%>
                    </th>
                </tr>
                <tr>
                    <th colspan="41" class="azul tblHeader paginationTr">
                        <div>Paginação: </div>
                        <ul class="pagination" style="margin: 2px;">
                            <li class="active"><a href="#">1</a></li>
                        </ul>
                    </th>
                </tr>
                <tr>
                    <th colspan="10" class="azul tblFilter">
                        <label for="sltRegime">Tipo Regime: </label>
                        <select id="sltRegime" class="input-lg" style="width: 99%;">
                        </select>
                    </th>
                    <th colspan="10" class="azul tblFilter">
                        <label for="sltSetor">Setor: </label>
                        <select id="sltSetor" class="input-lg sltSetor" style="width: 99%;">
                        </select>
                    </th>
                    <th colspan="6" class="azul tblFilter">
                        <label for="nome">Nome Servidor: </label>
                        <input id="nome" type="text" value="" class="input-lg" style="width: 90%;" />
                    </th>
                    <th colspan="4" class="azul tblFilter">
                        <label for="matricula">Matrícula: </label>
                        <input id="matricula" type="text" class="input-lg" value="" style="width: 90%;" />
                    </th>
                    <th colspan="2" class="azul tblFilter">
                        <button id="btnFiltrar" onclick="GetEscalas(true)" class="btn btn-success" style="margin-top: 10px;">Carregar</button>
                    </th>
                </tr>
                <tr class="limpar thCancelarAlteracoes" style="display: none;">
                    <th colspan="31" class="tblHeader">
                        <button type="button" id="btnModal" onclick="LimparControles();" class="btn btn-warning hide" data-toggle="modal" data-target="#regimeHorario">Regimes de Horários</button>
                        <button type="button" id="btnCancelarAlteracoes" onclick="CancelarAlteracoes(this)" class="btn btn-danger" style="display: none">Cancelar Alterações</button>
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <div class="row loadDados" style="display: none">
            <img src="img/loader.gif" style="display: block; margin-left: auto; margin-right: auto; width: 40%; width: 100px" />
        </div>
    </div>
    <div tabindex="-1" role="dialog" class="ui-dialog ui-corner-all ui-widget ui-widget-content ui-front ui-draggable ui-resizable" aria-describedby="dialog2" aria-labelledby="ui-id-1" style="display: none; position: absolute;">
        <div class="ui-dialog-titlebar ui-corner-all ui-widget-header ui-helper-clearfix ui-draggable-handle">
            <span id="ui-id-1" class="ui-dialog-title">Alerta</span>
            <button type="button" class="ui-dialog-titlebar-close"></button>
        </div>
        <div id="dialog2" style="" class="ui-dialog-content ui-widget-content">
            <p>Escolha um mês Para Definir as Folgas</p>
        </div>
        <div class="ui-resizable-handle ui-resizable-n" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-e" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-s" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-w" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-se ui-icon ui-icon-gripsmall-diagonal-se" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-sw" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-ne" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-nw" style="z-index: 90;"></div>
    </div>
    <div tabindex="-1" role="dialog" class="ui-dialog ui-corner-all ui-widget ui-widget-content ui-front ui-draggable ui-resizable" aria-describedby="dialog" style="display: none; position: absolute;" aria-labelledby="ui-id-2">
        <div class="ui-dialog-titlebar ui-corner-all ui-widget-header ui-helper-clearfix ui-draggable-handle">
            <span id="ui-id-2" class="ui-dialog-title">Escolha o Dia da Semana</span>
            <button type="button" class="ui-dialog-titlebar-close"></button>
        </div>
        <div id="dialog" style="" class="ui-dialog-content ui-widget-content">
            <select id="dias">
                <option value="1">Seg</option>
                <option value="2">Ter</option>
                <option value="3">Qua</option>
                <option value="4">Qui</option>
                <option value="5">Sex</option>
                <option value="6">Sab</option>
                <option value="7">Dom</option>
            </select>
            <button id="btn-usar" class="button">
                Usar
            </button>
            <button id="btn" class="button2">
                Fechar
            </button>
        </div>
        <div class="ui-resizable-handle ui-resizable-n" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-e" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-s" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-w" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-se ui-icon ui-icon-gripsmall-diagonal-se" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-sw" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-ne" style="z-index: 90;"></div>
        <div class="ui-resizable-handle ui-resizable-nw" style="z-index: 90;"></div>
    </div>

    <div id="regimeHorario" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Regime de Horário</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <label for="sltSetor">Setor: </label>
                            <select id="sltSetorModal" class="input-sm sltSetor" style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-lg-2">
                            <label for="nomeModal">Nome Servidor: </label>
                            <input id="nomeModal" type="text" value="" class="input-sm" style="width: 100%;" />
                        </div>
                        <div class="col-lg-2">
                            <label for="matriculaModal">Matrícula: </label>
                            <input id="matriculaModal" type="text" value="" class="input-sm" style="width: 100%;" />
                        </div>
                        <div class="col-lg-2">
                            <br />
                            <button id="btnFiltroModal" onclick="filtrarModal()" class="btn btn-success">Filtrar</button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-8">
                            <label for="sltRegimes" class="center-block text-center">Selecione um regime</label>
                            <select id="sltRegimes" class="input-lg center-block" onchange="RenderTabelaUsuarioModal()">
                            </select>
                        </div>
                        <div class="col-lg-4">
                            <br />
                            <button id="btnRegimes" class="btn btn-success oculto center-block">Mostrar Detalhes dos Regimes</button>
                        </div>
                    </div>
                    <div class="row">
                        <br />
                        <table id="tblRegimes" class="table table-striped">
                            <thead>
                                <tr>
                                    <th colspan="11" style="font-size: 18px;">Lista de Regimes de Horários</th>
                                </tr>
                                <tr>
                                    <th>Cod</th>
                                    <th>Desc.</th>
                                    <th>Total Hora Semana</th>
                                    <th>Total Hora Dia</th>
                                    <th>Regime Plantonista</th>
                                    <th>Permite Hora Extra</th>
                                    <th>Total Max. Hora Extra Dia</th>
                                    <th>Total Max. Hora Extra Mes</th>
                                    <th>Total Horas Folga Plantonista</th>
                                    <th>Hora Entrada</th>
                                    <th>Hora Saida </th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                    <div class="row">
                        <table id="tblUsuariosModal" class="table table-striped">
                            <thead>
                                <tr>
                                    <th colspan="5" style="font-size: 18px;">Lista de Servidores</th>
                                </tr>
                                <tr>
                                    <th>#</th>
                                    <th>Codigo</th>
                                    <th>Nome</th>
                                    <th>Matricula</th>
                                    <th>Código Regime</th>
                                </tr>
                                <tr>
                                    <th colspan="5">
                                        <input id="txtUsuarioModal" style="width: 100%" onkeyup="procurarTabela('txtUsuarioModal', 'tblUsuariosModal')" placeholder="Digite para pesquisar" /></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" data-dismiss="modal" onclick="LimparControles()">Cancelar</button>
                    <button type="button" class="btn btn-success" onclick="SalvarVinculoRegime()">Salvar</button>
                </div>
            </div>

        </div>
    </div>

    <button type="button" id="btnEscalaModal" class="btn btn-warning hide" data-toggle="modal" data-target="#modalFormEscala">Modal</button>
    <div id="modalFormEscala" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Escala de horários<br />
                        <small id="operadorEscalaModal"></small></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <form>
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <label for="txtDataEntrada">Data de Entrada</label>
                                        <input id="txtDataEntrada" type="date" class="form-control txtDataEscala">
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="">Hora da Entrada</label>
                                        <input type="text" class="form-control txtHorasDia" value="00:00" id="txtHoraEntrada">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <label for="txtDataSaida">Data de Saída</label>
                                        <input id="txtDataSaida" type="date" class="form-control txtDataEscala">
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="">Hora da Saída</label>
                                        <input type="text" class="form-control txtHorasDia" value="00:00" id="txtHoraSaida">
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" data-dismiss="modal" onclick="">Cancelar</button>
                    <button id="btnDeletarEscala" type="button" class="btn btn-danger" onclick="">Excluir</button>
                    <button id="btnSalvarEscala" type="button" class="btn btn-success" onclick="">Salvar</button>
                    <button id="btnSalvarEscalaTodas" type="button" class="btn btn-primary" onclick="">Salvar e criar o restante</button>
                </div>
            </div>

        </div>
    </div>
    <button type="button" id="btnDadosUsuarioModal" class="btn btn-warning hide" data-toggle="modal" data-target="#modalDadosUsuario">Modal</button>
    <div id="modalDadosUsuario" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Dados do Servidor<br />
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <form>
                                <div class="form-group">
                                    <div class="col-sm-9">
                                        <label for="txtNomeServidor">Nome</label>
                                        <input id="txtNomeServidor" type="text" class="form-control" readonly="readonly">
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="">Matrícula</label>
                                        <input type="text" class="form-control" id="txtMatricula" readonly="readonly">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <label for="txtSetor">Setor</label>
                                        <input id="txtSetor" type="text" class="form-control" readonly="readonly">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label for="">Hora da Entrada 1</label>
                                        <input type="text" class="form-control txtHorasDia" id="txtEntrada1" readonly="readonly" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="">Hora da Saída 1</label>
                                        <input type="text" class="form-control txtHorasDia" id="txtSaida1" readonly="readonly" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="">Hora da Entrada 2</label>
                                        <input type="text" class="form-control txtHorasDia" id="txtEntrada2" readonly="readonly" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="">Hora da Saída 2</label>
                                        <input type="text" class="form-control txtHorasDia" id="txtSaida2" readonly="readonly" />
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" id="btnExcluirLancamentos" data-dismiss="modal" onclick="">Excluir todos os lancamentos deste mês</button>
                    <button type="button" class="btn btn-warning" id="btnFecharDadosModal" data-dismiss="modal" onclick="">Fechar</button>
                </div>
            </div>

        </div>
    </div>
    <script>
        var daUsu = { idEmpresa:  <%= IdEmpresa %>, idUsuario: <%= IdUsuario %>};
        $('#btnRegimes').click(function (e) {
            var elBtn = $(e.target);
            if (elBtn.hasClass('oculto')) {
                $('#tblRegimes').show();
                elBtn.removeClass('btn-success');
                elBtn.removeClass('oculto');
                elBtn.addClass('btn-warning mostrado');
                elBtn.text('Ocultar detalhes dos regimes');
            } else {
                $('#tblRegimes').hide();
                elBtn.removeClass('btn-warning');
                elBtn.removeClass('mostrado');
                elBtn.addClass('btn-success oculto');
                elBtn.text('Mostrar Detalhes dos Regimes');
            }
        });

        $('#modalFormEscala').on('hidden.bs.modal', function (e) {
            visibleModal = false;
        })

        $('.txtHorasDia').formatter({
            'pattern': '{{99}}:{{99}}'
        });

        $('.thCancelarAlteracoes').hide();
        $('#btnCancelarAlteracoes').hide();

        //$('.txtDataEscala').formatter({
        //    'pattern': '{{99}}/{{99}}/{{9999}}'
        //});




        function postEscala() {
            var mapForm = document.createElement("form");
            mapForm.target = "Map";
            mapForm.method = "POST"; // or "post" if appropriate
            mapForm.action = "EscalaHorario.aspx";

            var mapInput = document.createElement("input");
            mapInput.type = "text";
            mapInput.name = "addrs";
            mapInput.value = { idU: 1, idE: 1 };
            mapForm.appendChild(mapInput);

            document.body.appendChild(mapForm);

            map = window.open("", "Map", "status=0,title=0,height=" + screen.availHeight + ",width=" + screen.availWidth + ",scrollbars=1");

            if (map) 
            {
                mapForm.submit();
            } else 
            {
                alert('You must allow popups for this map to work.');
            }
        }
    </script>
    <script src="assets/app/Calendario.js"></script>
    <script>

</script>
</body>
</html>
