var itemDrag;
var elAntTarget;
var elAnt;
var listUsuariosModal = [];
var listCalen = [];
var visibleModal = false;
var tdInserir;
var listaEscalas = [];
var escalaSelecionada;
var listaUsuariosEscala = [];
function carregaTabela(mes) {
    $.getJSON('api/vs1/escala/getCalendario/' + mes + "/" + daUsu.idEmpresa).done(function (data) {
        if (data.length > 0) {
            $('#mes').val(data[0].mesAno);
            $('.tblHeader').attr('colspan', data.length + 1)
            var classeSemana = 'semana5';
            var htmlTrDias = '<tr class="limpar"><th>&gt;&gt;&gt;</th>';
            var htmlTrDiasSem = '<tr class="limpar"><th>&gt;&gt;&gt;</th>';
            data.forEach(function (item, i) {
                if (item.numDiaSemana == 1) {
                    classeSemana = GetClasseSemana(parseInt(item.diaMes));
                }
                htmlTrDias += '<th class="' + classeSemana + '">' + item.diaMes + '</th>';
                htmlTrDiasSem += '<th class="' + classeSemana + '">' + item.diaSemana + '</th>';;
            })
            htmlTrDias += '</tr>';
            htmlTrDiasSem += '</tr>';
            $('#tabela tbody').html(htmlTrDias + htmlTrDiasSem);
            listCalen = data;
            if (mes != 0) {
                GetEscalas();
            }
        }
    });
}

function GetClasseSemana(i) {
    var retorno = '';
    if (i >= 1 && i <= 7) {
        retorno = 'semana1';
    } else if (i >= 8 && i <= 14) {
        retorno = 'semana2';
    } else if (i >= 15 && i <= 21) {
        retorno = 'semana3';
    } else if (i >= 22 && i <= 28) {
        retorno = 'semana4';
    } else {
        retorno = 'semana5';
    }
    return retorno;
}

function GetEscalas(primeiro = false) {
    $('.trDados').remove();
    $('.loadDados').show();
    var filtro = {};
    filtro.usu = daUsu;
    filtro.nome = $('#nome').val();
    filtro.matricula = $('#matricula').val();
    filtro.idSetor = $('#sltSetor').val();
    filtro.mes = $('#mes').val();
    filtro.idTipoRegime = $('#sltRegime').val();
    filtro.numeroPagina = $('.pagination .active').text();
    if (filtro.numeroPagina == '') {
        filtro.numeroPagina = 1;
    }
    if (primeiro) {
        $('.paginationTr').hide();
    }

    $.post('api/vs1/escala/GetEscalasUsuarios', filtro).done(function (data) {

        listaEscalas = data;
        $('.loadDados').hide();
        $(".tdEscala span").draggable("destroy");
        if (primeiro) {
            MontaPaginacao(data[0].numeroLinhas);
        }
        data.forEach(function (regime, i) {
            MontaRegime(regime);
            regime.usuarios.forEach(function (usuario, j) {
                MontaTrFunc(usuario, i, j, regime.idRegime);
            })

        });

        listaUsuariosEscala.forEach(function (item, i) {
            var trEl = $('th[data-idvinregime="' + item + '"]').parent();
            $('td', trEl).each(function (j, itemTd) {
                if (itemTd.innerHTML.length == 0) {
                    itemTd.innerHTML = '<img src="img/folga.png" class="imgFolga" alt="Folga" width="20px"/>';
                }
            })
        })

        setTimeout('DragDrop()', 3000);
    })
}

function MontaRegime(regime) {
    //DROP FILTER
    //var html = '<tr class="limpar"><th><span class="dropdown"><span class="glyphicon glyphicon-filter dropdown-toggle" data-toggle="dropdown" aria-expanded="true"></span><ul class="dropdown-menu" role="menu">';
    //html += '<li class="checkbox keep-open"><label><input type="checkbox">option 1</label></li>';
    //html += '<li class="checkbox keep-open"><label><input type="checkbox">option 2</label></li>';
    //html += '<li class="checkbox keep-open"><label><input type="checkbox">option 3</label></li>';
    //html += '<li class="checkbox keep-open"><label><input type="checkbox">option 4</label></li>';
    //html += '<li><a class="btn">select</a></li>';
    var html = '<tr class="trDados"><th colspan="31">Cod. ' + regime.idRegime + ' - ' + regime.regime + '</th></tr>';
    $('#tabela tbody').append(html);
}

function MontaPaginacao(count) {
    var numPagina = parseInt(count / 50);
    var html = '';
    for (var i = 1; numPagina >= i; i++) {
        if (i == 1) {
            html += '<li class="active"><a href="#">' + i + '</a></li>';
        } else {
            html += '<li><a href="#">' + i + '</a></li>';
        }
    }
    $('.paginationTr').show();
    $('.pagination').html(html);

    $('.pagination li').click(function (e) {
        var elClick = $(e.currentTarget);
        $('.pagination li').removeClass('active');
        elClick.addClass('active');
        console.log(elClick.text())
        GetEscalas()
    })
}

function MontaTrFunc(usuario, indexRegime, indexUsuario, idRegime) {
    var classeSemana = 'semana5';
    var htmlTrDias = '<tr class="limpar trDados"><th class="operadores" data-id="' + usuario.idUsuario + '" data-idVinRegime="' + usuario.idVinculoRegime + '" data-idRegime="' + idRegime + '"><span class="dadosUsuario" onclick="GetDadosUsuarios(' + usuario.idUsuario + ')" >' + usuario.nome + '</span></th>';

    var data = listCalen;
    var temItem = false;
    data.forEach(function (item, i) {
        if (item.numDiaSemana == 1) {
            classeSemana = GetClasseSemana(parseInt(item.diaMes));
        }
        usuario.datas.forEach(function (itemData, j) {
            if (itemData.diaMes == item.diaMes) {
                htmlTrDias += '<td class="tdEscala ' + classeSemana + ' ' + $('#tabela tbody tr').length + '" data-data="' + item.dtDiasAno.split(' ')[0] + '">' + '<span onclick="GetDadosItem(' + indexRegime + ', ' + indexUsuario + ', ' + j + ', this)"  class="event" id="' + itemData.idEscala + '" draggable="true"><img src="img/p.png"></span>' + '</td>';
                temItem = true;
                if (listaUsuariosEscala.indexOf(usuario.idVinculoRegime) == -1) {
                    listaUsuariosEscala.push(usuario.idVinculoRegime);
                }
            }
        });

        if (!temItem) {
            htmlTrDias += '<td class="tdEscala ' + classeSemana + ' ' + $('#tabela tbody tr').length + '" data-data="' + item.dtDiasAno.split(' ')[0] + '"></td>';
        }
        temItem = false;
    });
    htmlTrDias += '</tr>';
    $('#tabela tbody').append(htmlTrDias);

}

function CompletaDatas(datas, callback) {
    var datasResult;
    var dataObje = {};
    listCalen.forEach(function (item, i) {
        datas.forEach(function (itemData) {
            if (itemData.diaMes == item.diaMes) {
                datasResult.push(itemData);
            }
        });
        if ((datasResult - 1) != i) {
            datasResult.push(
                {
                    dataEntrada: item.diaMes,
                    idEscala: null
                }
            );
        }
    });
    callback(datasResult);
}

$(function () {
    $('#btnEscolher').on('click', function (e) {
        e.preventDefault();
        carregaTabela($('#mes').val());
    })
})

function DragDrop() {
    $(function () {
        $('.event').on("dragstart", function (event) {
            var dt = event.originalEvent.dataTransfer;
            itemDrag = $(event.currentTarget)
            dt.setData('Text', $(this).attr('id'));
            elAntTarget = event.currentTarget.offsetParent.className;
            elAnt = event.currentTarget.offsetParent;
        });

        $('table td').on("dragenter dragover drop", function (event) {
            event.preventDefault();
            if (event.type === 'drop' && event.currentTarget.className == elAntTarget) {
                var data = event.originalEvent.dataTransfer.getData('Text', $(this).attr('id'));
                de = $('#' + data).detach();
                $(this).html('');
                de.appendTo($(this));
                itemDrag.addClass('alterar');
                $('#btnCancelarAlteracoes').show();
                $('.thCancelarAlteracoes').show();
                elAnt.innerHTML = '<img src="img/folga.png" class="imgFolga" width="20px">';
            }
        });

        $('.keep-open').click(function (e) {
            if (/input|label/i.test(e.target.tagName)) {
                var parent = $(e.target).parent();
                if (parent.hasClass('checkbox')) {
                    var checkbox = parent.find('input[type=checkbox]');
                    checkbox.prop("checked", !checkbox.prop("checked"));
                    return false;
                }
            }
        });

        $('.tdEscala').dblclick(function (e) {
            if (!visibleModal) {
                tdInserir = e.target;
                $('#btnEscalaModal').click();
                visibleModal = true;
                if (tdInserir.className == "imgFolga") {
                    var data = ConvertStringToData($(tdInserir).parent().attr('data-data'));
                    $('#txtDataEntrada').val(data);
                    $('#txtDataSaida').val(data);
                    var tr = $(tdInserir).parent().parent();
                    $('#operadorEscalaModal').text($('th:first-child', tr).text());
                    $('#btnDeletarEscala').hide();
                    $('#btnSalvarEscalaTodas').show();
                    $('#btnSalvarEscala').attr('onclick', 'SalvarEscala(0,0)');
                    $('#btnSalvarEscalaTodas').attr('onclick', 'SalvarEscala(0,1)');
                } else {
                    var data = ConvertStringToData($(tdInserir).attr('data-data'));
                    $('#txtDataEntrada').val(data);
                    $('#txtDataSaida').val(data);
                    var tr = $(tdInserir).parent()
                    $('#operadorEscalaModal').text($('th:first-child', tr).text());
                    $('#btnDeletarEscala').hide();
                    $('#btnSalvarEscalaTodas').show();
                    $('#btnSalvarEscala').attr('onclick', 'SalvarEscala(0,0)');
                    $('#btnSalvarEscalaTodas').attr('onclick', 'SalvarEscala(0,1)');
                }
            }
        });
    })
}


function GetFiltros() {
    $.post("api/vs1/escala/GetSetores", daUsu).done(function (dados) {
        var html = '';
        dados.forEach(function (item, i) {
            html += '<option value=' + item.idSetor + '>' + item.descricao + '</option>'
        });
        $('.sltSetor').html(html);
    });
}

function GetDadosItem(i, j, k, el, abrir) {
    elTd = $(el).parent();
    escalaSelecionada = { item: listaEscalas[i].usuarios[j].datas[k], i: i, j: j, k: k };
    var dataTd = elTd.attr('data-data');

    var start = new Date(FormataStringData(escalaSelecionada.item.dataEntrada))  
    end   = new Date(FormataStringData(escalaSelecionada.item.dataSaida))  
    diff  = new Date(end - start),  
    difDatas  = diff/1000/60/60/24; 
    //var difDatas = parseInt(escalaSelecionada.item.dataSaida.substring(0, 2)) - parseInt(escalaSelecionada.item.dataEntrada.substring(0, 2));

    var dataEntrada = dataTd;
    var dataSaidaJS = new Date(FormataStringData(dataTd));
    dataSaidaJS = addDays(dataSaidaJS,difDatas);
    var dataSaida = (escalaSelecionada.item.dataSaida);

    $('#txtDataEntrada').val(ConvertStringToData(dataEntrada));
    $('#txtHoraEntrada').val(escalaSelecionada.item.horaEntrada);
    $('#txtDataSaida').val(ConvertStringToData(dataSaida));
    $('#txtHoraSaida').val(escalaSelecionada.item.horaSaida);
    if (abrir != undefined) {
    }
    else {
        $('#btnEscalaModal').click();
        $('#btnSalvarEscalaTodas').hide();
        $('#btnDeletarEscala').show().attr('onclick', 'ExcluirEscala(' + escalaSelecionada.item.idEscala + ')');;
        $('#btnSalvarEscala').attr('onclick', 'SalvarEscala(' + escalaSelecionada.item.idEscala + ')');
    }
    return true;
}

function GetRegimes() {
    $.post("api/vs1/escala/GetRegimes", daUsu).done(function (dados) {
        var html = '';
        var htmlSel = '';
        window.listaRegimes = dados;
        dados.forEach(function (item, i) {
            if (i != 0) {
                html += '<tr>';
                html += '<td>' + item.idRegimeHora + '</td>';
                html += '<td>' + item.dsRegimeHora + '</td>';
                html += '<td>' + item.totalHoraSemana + '</td>';
                html += '<td>' + item.totalHoraDia + '</td>';
                html += '<td>' + item.regimePlantonista + '</td>';
                html += '<td>' + item.permitehoraExtra + '</td>';
                html += '<td>' + item.totalMaximoHoraExtraDia + '</td>';
                html += '<td>' + item.totalMaximoHoraExtraMes + '</td>';
                html += '<td>' + item.totalHorasFolgaPlantonista + '</td>';
                html += '<td>' + item.horaEntrada + '</td>';
                html += '<td>' + item.horaSaida + '</td>';
                html += '</tr>';
                htmlSel += '<option value="' + item.idRegimeHora + '">Cod. ' + item.idRegimeHora + ' - ' + item.dsRegimeHora + '</option>';
            } else {
                htmlSel += '<option value=""></option>';
            }

        });
        $('#sltRegimes').html(htmlSel);
        $('#sltRegime').html(htmlSel);
        $('#tblRegimes tbody').html(html);
        $('#tblRegimes').hide();
    });
}


function filtrarModal() {
    var filtro = {};
    filtro.usu = daUsu;
    filtro.nome = $('#nomeModal').val();
    filtro.matricula = $('#matriculaModal').val();
    filtro.idSetor = $('#sltSetorModal').val();
    $.post('api/vs1/escala/GetUsuarios', filtro).done(function (data) {
        listUsuariosModal = data;
        RenderTabelaUsuarioModal();
    });
}

function RenderTabelaUsuarioModal() {
    var html = '';
    var idRegimeSelect = $('#sltRegimes').val();
    listUsuariosModal.forEach(function (item, i) {
        html += '<tr>';
        html += '<td> <div class="checkbox"><label><input type="checkbox" value="" ' + ((item.idRegimeHora == idRegimeSelect) ? 'checked' : '') + '></label></div></td>';
        html += '<td>' + item.codigo + '</td>';
        html += '<td>' + item.nome + '</td>';
        html += '<td>' + item.matricula + '</td>';
        html += '<td>' + item.idRegimeHora + '</td>';
        html += '</tr>';
    });
    $('#tblUsuariosModal tbody').html(html);
}

function procurarTabela(ipt, table) {
    // Declare variables 
    var input, filter, table, tr, td, i;
    input = document.getElementById(ipt);
    filter = input.value.toUpperCase();
    table = document.getElementById(table);
    tbody = table.getElementsByTagName("tbody");
    tr = tbody[0].getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        if (tr[i].innerHTML.toUpperCase().indexOf(filter) > -1) {
            tr[i].style.display = "";
        } else {
            tr[i].style.display = "none";
        }
    }
}


function LimparControles() {
    listUsuariosModal = [];
    $('#sltSetorModal').val('');
    $('#sltRegimes').val('');
    $('#txtUsuarioModal').val('');
    RenderTabelaUsuarioModal();
}

function SalvarVinculoRegime() {
    var list = [];
    var obj = { idRegime: $('#sltRegimes').val(), lista: [] };
    if (obj.idRegime == "" || obj.idRegime == null) {
        alert('Favor selecionar um Regime de horário!');
        return false;
    }
    $('#tblUsuariosModal tbody tr').each(function (i, item) {
        if ($('td input', item).is(':checked')) {
            list.push($('td:nth-child(2)', item).text());
        }
    })
    if (list.length == 0) {
        alert('Favor selecionar os servidores para este regime!');
        return false;
    }
    obj.lista = list;
    obj.usu = daUsu;
    $.post('api/vs1/escala/salvarRegimeVinculo', obj).done(function (data) {
        alert('Alterações salvas com sucesso!');
        $('#btnModal').click();
    })
}

function ConvertStringToData(data) {
    var arrData = [];
    var stringFormatada = '';
    try {
        arrData = data.split('/');

        stringFormatada = arrData[2] + '-' + arrData[1] + '-' + ((arrData[0].length == 2) ? arrData[0] : '0' + arrData[0]);
    } catch (e) { }
    //var dataFormatada1 = new Date(stringFormatada);
    return stringFormatada;
}


function SalvarEscala(idEscala, criarTodas) {
    var obj = {};
    obj.dataEscala = $('#txtDataEntrada').val();
    obj.dataEntrada = $('#txtDataEntrada').val();
    obj.horaEntrada = $('#txtHoraEntrada').val();
    obj.dataSaida = $('#txtDataSaida').val();
    obj.horaSaida = $('#txtHoraSaida').val();
    if (idEscala == 0) {
        var th;
        if (tdInserir.className == "imgFolga") {
            th = $('th', ($(tdInserir).parent().parent()));
        } else {
            th = $('th', ($(tdInserir).parent()));
        }

        obj.idUsuario = th.attr('data-id');
        obj.idVinculoRegime = th.attr('data-idVinRegime');
        $.post('api/vs1/escala/salvarEscala/' + daUsu.idEmpresa + '/' + daUsu.idUsuario + '/' + criarTodas, obj).done(function () {
            carregaTabela($('#mes').val());
            $('#btnEscalaModal').click();
        })
    } else {
        var th = $('th', ($('#' + idEscala).parent().parent()));
        obj.idUsuario = th.attr('data-id');
        obj.idVinculoRegime = th.attr('data-idVinRegime');
        obj.idEscala = idEscala;
        $('#' + idEscala).removeClass('alterar');
        $.post('api/vs1/escala/alterarEscala/' + daUsu.idEmpresa + '/' + daUsu.idUsuario, obj).done(function (data) {
            listaEscalas[escalaSelecionada.i].usuarios[escalaSelecionada.j].datas[escalaSelecionada.k] = data;
            $('#btnEscalaModal').click();
            if ($('.alterar').length == 0) {
                $('.thCancelarAlteracoes').hide();
                $('#btnCancelarAlteracoes').hide();

            }
        })
    }
}


function GetPopOvers(i, j, k, el) {
    elTd = $(el).parent();
    var item = listaEscalas[i].usuarios[j].datas[k];
}

function ExcluirEscala(idEscala) {
    if (confirm('Tem certeza que deseja excluir?')) {
        $.post('api/vs1/escala/deletar/' + idEscala, daUsu).done(function (data) {
            var td = $('#' + idEscala).parent();
            $('#' + idEscala).remove();
            $('#btnEscalaModal').click();
            td.html('<img src="img/folga.png" class="imgFolga" alt="Folga" width="20px">');
            alert('Excluido com sucesso!')
        })
    }
}

function CancelarAlteracoes(el) {
    GetEscalas();
    $(el).hide();
    $('.thCancelarAlteracoes').hide();
}


function GetDadosUsuarios(idUsuario) {
    $.getJSON('api/vs1/escala/getDadosUsuario/' + idUsuario).done(function (data) {
        $('#btnDadosUsuarioModal').click();
        $('#txtNomeServidor').val(data.nome);
        $('#txtMatricula').val(data.matricula);
        $('#txtSetor').val(data.setor);
        $('#txtEntrada1').val(data.entrada1);
        $('#txtSaida1').val(data.saida1);
        $('#txtEntrada2').val(data.entrada2);
        $('#txtSaida2').val(data.saida2);

        $('#btnExcluirLancamentos').attr('onclick', 'ExcluirLancamentos(' + idUsuario + ')')
    })
}

function ExcluirLancamentos(idUsuario) {
    if (confirm('Deseja realmente excluir todos os lançamentos deste usuário neste mês?')) {
        $.post('api/vs1/escala/deletarLancamentos/' + idUsuario + '/' + $('#mes').val()).done(function () {
            $('#btnFecharDadosModal').click();
            carregaTabela($('#mes').val());
        })
    }

}

GetRegimes();
GetFiltros();

(function () {
    "use strict";
    var date = new Date();
    var mes = date.getMonth() + 1
    $('#mes').val(mes);
    $('.paginationTr').hide();
    carregaTabela(0);
    $('#mes').change(function (e) {
        $.getJSON('api/vs1/escala/getCalendario/' + $('#mes').val() + "/" + daUsu.idEmpresa).done(function (data) {
            if (data.length > 0) {
                $('#mes').val(data[0].mesAno);
                $('.tblHeader').attr('colspan', data.length + 1)
                var classeSemana = 'semana5';
                var htmlTrDias = '<tr class="limpar"><th>&gt;&gt;&gt;</th>';
                var htmlTrDiasSem = '<tr class="limpar"><th>&gt;&gt;&gt;</th>';
                data.forEach(function (item, i) {
                    if (item.numDiaSemana == 1) {
                        classeSemana = GetClasseSemana(parseInt(item.diaMes));
                    }
                    htmlTrDias += '<th class="' + classeSemana + '">' + item.diaMes + '</th>';
                    htmlTrDiasSem += '<th class="' + classeSemana + '">' + item.diaSemana + '</th>';;
                })
                htmlTrDias += '</tr>';
                htmlTrDiasSem += '</tr>';
                $('#tabela tbody').html(htmlTrDias + htmlTrDiasSem);
                listCalen = data;
            }
        });
    })
})();




function FormataStringData(data) {
  var dia  = data.split("/")[0];
  var mes  = data.split("/")[1];
  var ano  = data.split("/")[2];

  return ano + '-' + ("0"+mes).slice(-2) + '-' + ("0"+dia).slice(-2);
}

function addDays(date, days) {
  var result = new Date(date);
  result.setDate(result.getDate() + days);
  return result;
}