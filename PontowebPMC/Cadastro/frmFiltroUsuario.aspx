﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmFiltroUsuario.aspx.cs" Inherits="Cadastro_frmCadastraUsuario" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
    <script type="text/javascript" src="../Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/swfobject.js"></script>
    <script type="text/javascript" src="../Scripts/scriptcam.js"></script>
    
    <script type="text/javascript">

        var IDUsuarioFoto;
        var tpuserJ;
        var Atualizacao = false;

        var idtpusuarioV;
        var idsetorV;
        var idcargoV;
        var idstatusV;
        var idregimehoraV;
        var tothorasdiariasV;
        var entrada1V;
        var entrada2V;
        var saida1V;
        var saida2V;
        var cadastrabiometriaV;
        var paineldashboardsV;
        var idvinculoV;
        var cpfV;
        var nomeV;
        var DescontoJornada;
        var varIsencaoPonto;

        function FechaPopVinculoUsuario() {
            //VinculosCamposLimpos();
            popVinculoUsuario.Hide();
        }
        
        function FechaPopaADDVinculo() {
            popVinculoUsuario.Hide();
            //coIDSetorVinculoUsuario.Set("IDSetor", cbSetor.GetValue());
            gridVinculoUsuario.PerformCallback('manut');
        }
        
        function VizualizaGestorSetor() {
            pcVinculoUsuario.PerformCallback("GestorSetor");
        }
        
        function PreencheHoras() {
            pcVinculoUsuario.PerformCallback("RegimeHora");
        }
        
        function AbrepopErroKey() {
            popErroKey.Show();
        }

        function FechapopErroKey() {
            popErroKey.Hide();
        }
        
        function ApresentaCO() {
            alert(coBuscaSEAP.Get("Nome"));
        }

        function PreencheManutUsuario(nome) {

                nome = rbListaServidor.GetSelectedItem();
                pos = nome.text.indexOf('-');
                tbNome.SetText(nome.text.substring(0, pos - 1));

                pos = 0;
                pos = nome.text.indexOf(':');

                horasemanal = '';
                horasemanal = nome.text.substring((pos + 1), pos + 4);

                if (horasemanal == ' 40') {

                    tbEntradaManha.SetText('08:00');
                    tbSaidaManha.SetText('12:00');
                    tbEntradaTarde.SetText('14:00');
                    tbSaidaTarde.SetText('18:00');
                    tbTotHoras.SetText('8');
                }
                else if (horasemanal == ' 30') {
                    tbEntradaManha.SetText('00:00');
                    tbSaidaManha.SetText('10:00');
                    tbEntradaTarde.SetText('12:00');
                    tbSaidaTarde.SetText('18:00');
                    tbTotHoras.SetText('6');
                }

                cbTPUsuario.SetText('Funcionário');
                //cbVinculo.SetText('Funcionário Público');
            
                popBuscaSEAP.Hide();
        }


        function AbreBuscaSEAP() {
            BuscaServidorSEAP();
            popBuscaSEAP.Show();
        }

        function BuscaServidorSEAP() 
        {
            cpBuscaSEAP.PerformCallback('Buscar');
        }

        function ValidaSelecao() {
            cpPreencheItens.PerformCallback(document.getElementById("<%=coIDUsuario.ClientID %>").value);
            popGestorSetor.Hide();
        }
        
        function FechaPopGestorSetor() {
            popGestorSetor.Hide();
        }

        function VerificaTpUser2() {

            if (cbTPUsuario.GetValue() != "3") 
            {
                cbGestorSetor.SetVisible(false);
            }
            else if ((cbTPUsuario.GetValue() == "3" || cbTPUsuario.GetValue() == "9") && cbTPUsuario.GetSelectedItem()) 
            {
                cpPreencheItens.PerformCallback(0);
                cbGestorSetor.SetVisible(true);
            }
        }
        function VerificaTpUser() {
            if (cbTPUsuario.GetValue() == "3" || cbTPUsuario.GetValue() == "9") 
                {
                    cpPreencheItens.PerformCallback(0);
                    cbGestorSetor.SetVisible(true);
                }
                else if (cbTPUsuario.GetValue() != "3") 
                {
                    cbGestorSetor.SetVisible(false);
                }
        }

        function AbrePopGestorSetor() {
            popGestorSetor.Show();
            cbGestorSetor.SetValue(false);

        }
        
        function desfazFiltro() {
            tpuserJ = cotpuser.Get("tpuser").toString();

            if (tpuserJ == "1" || tpuserJ == "7") {
                cbSetorPrincipal.SetValue('');
                Pesquisa();
            }
        }
        
        function FechaPopUpLoad() {
            popUpLoud.Hide();
        }
        
        function AbrePopUpLoud(IDUsuario) {
            
            IDUsuarioFoto = IDUsuario;
            popUpLoud.Show();
        }

        function UpFoto_OnUploadStart() {
            btUpload.SetEnabled(false);

        }
        function AtulizarbtUpload() {
            btUpload.SetEnabled(upFoto.GetText(0) != "");
        }
        function FechaAtualiza() {

            document.getElementById("<%=coIDUsuario.ClientID %>").value = IDUsuarioFoto;
            upFoto.Upload();
            
            //popLoud.hide();
           
        }
        function UpFoto_OnFilesUploadComplete(args) {
            AtulizarbtUpload();
        }
        
        function base64_toimage(IMG) {
            //$('#image').attr("src", "data:image/png;base64," + IMG);
        //document.getElementById("Fotobase").value = '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';
    }
 
        //$('#Fotobase64').focus(function(){base64_toimage();});
        
        function valida_cpf(cpf) {
            var numeros, digitos, soma, i, resultado, digitos_iguais;
            digitos_iguais = 1;
            if (cpf.length < 11)
                return false;
            for (i = 0; i < cpf.length - 1; i++)
                if (cpf.charAt(i) != cpf.charAt(i + 1)) {
                    digitos_iguais = 0;
                    break;
                }
            if (!digitos_iguais) {
                numeros = cpf.substring(0, 9);
                digitos = cpf.substring(9);
                soma = 0;
                for (i = 10; i > 1; i--)
                    soma += numeros.charAt(10 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(0))
                    return false;
                numeros = cpf.substring(0, 10);
                soma = 0;
                for (i = 11; i > 1; i--)
                    soma += numeros.charAt(11 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(1))
                    return false;
                return true;
            }
            else
                return false;
        }
    
    function AbrePopFoto(Nome, IDUsuario, DSUsuario) {
        popImagem.Show();
        popImagem.SetContentUrl("");
        popImagem.SetContentUrl("/Cadastro/frmfoto.aspx?ID=" + IDUsuario + "&nome=" + Nome + "&Sobre=" + DSUsuario);

    }
    function AbrePopManutencaoUsuario(TelefoneSMS, Matricula, PrimeiroNome, IDUsuario, NomeCompleto, DTNascimento, DTAdmissao, PIS, CPF, IDSetor, IDTPUsuario, IDCargo, IDStatus, EntradaManha, EntradaTarde, SaidaManha, SaidaTarde, TotHorasDiarias, AcessoEspecial, SenhaDigital, IDEntidade, Dashboard, CadastraDigital, FinalizaDiaSeguinte, IDRegimeHora, Desconto, IDVinculoP, IsencaoPonto)
    {
        tbPIS.SetText(Matricula);
        tbTelSMS.SetText(TelefoneSMS);
		
		NomeCompleto = NomeCompleto.substring(1, NomeCompleto.length - 1);

        if (NomeCompleto.indexOf("1") > (0))
        {
            var posicao = NomeCompleto.indexOf("1");
            NomeCompleto = NomeCompleto.substring(0, posicao) + "'" + NomeCompleto.substring(posicao + 1, (NomeCompleto.length));
        }
		
        
        document.getElementById("<%=coIDVinculoUsuario.ClientID %>").value = IDVinculoP;

        coIDSetorVinculoUsuario.Set("IDSetor", IDSetor);

        document.getElementById("<%=coOperacaoVinculo.ClientID %>").value = 'Alteracao';

        document.getElementById("<%=coOperacao.ClientID %>").value = 'Alterar';
        
        rbSenhaDigitalFiltro.SetVisible(false);

        document.getElementById("<%=coIDUsuario.ClientID %>").value = IDUsuario;
         
         Atualizacao = true;
         cpPreencheItens.PerformCallback('PreencherCBL');

         //tpCadastro.SetActiveTab(0);
         var tpUser = coIDTPUsuarioGeral2.Get("tpusuario").toString();

         tbNome.SetText(NomeCompleto);
         nomeV = NomeCompleto;
        //deNascimento.SetText(DTNascimento);
        //deAdmissao.SetText(DTAdmissao);
        //tbPIS.SetText(PIS);
         tbCPF.SetText(CPF);

         cpfV = CPF;

        //if (tpUser == "1") {
        cbSetor.SetValue(IDSetor);
        idsetorV = IDSetor;
        //}

        if (cotpuser.Get("tpuser") == "1" || cotpuser.Get("tpuser") == "7" || cotpuser.Get("tpuser") == "8")
        {
                rbSenhaDigitalFiltro.SetVisible(true);
        }

        if (IDTPUsuario == "3" || IDTPUsuario == "9")
        {
            cbGestorSetor.SetVisible(true);
        }
        else {
            cbGestorSetor.SetVisible(false);
        }

        cbTPUsuario.SetValue(IDTPUsuario);

        idtpusuarioV = IDTPUsuario;

        cbCargo.SetValue(IDCargo);

        idcargoV = IDCargo;
        
        cbStatus.SetValue(IDStatus);

        idstatusV = IDStatus
        //cbVinculo.SetVisible(false);
        //cbVinculo.SetValue(IDEntidade);
        idvinculoV = IDEntidade;


        tbTotHoras.SetText(TotHorasDiarias);

        tothorasdiariasV = TotHorasDiarias;

        tbEntradaManha.SetText(EntradaManha);

        entrada1V = EntradaManha;
        entrada2V = EntradaTarde;
        saida1V = SaidaManha;
        saida2V = SaidaTarde;
        //DescontoJornada = Desconto;

        idregimehoraV = IDRegimeHora;

        tbSaidaManha.SetText(SaidaManha);
        tbEntradaTarde.SetText(EntradaTarde);
        tbSaidaTarde.SetText(SaidaTarde);
        
        cbRegimeHora.SetValue(IDRegimeHora);
       

        rbSenha.SetChecked(false);
       
        if (SenhaDigital == 1)
            rbSenhaDigitalFiltro.SetChecked(true);
        else
            rbSenhaDigitalFiltro.SetChecked(false);


        if (FinalizaDiaSeguinte != null) {
            if(FinalizaDiaSeguinte.toString() == 'True')
                cbPermiteDiaSeguinte.SetChecked(true);
        else
            cbPermiteDiaSeguinte.SetChecked(false);
            }


        //CadastraDigital
        if (CadastraDigital != null)
        {
            if (CadastraDigital.toString() == 'True') {
                cbCadastraDigital.SetChecked(true);
                cadastrabiometriaV = true;
            }
            else {
                cbCadastraDigital.SetChecked(false);
                cadastrabiometriaV = false;
            }

        }

        //Jornedtotal
        if (Desconto != null) {
            if (Desconto.toString() == 'True' || Desconto.toString() == '1') {
                DescontoJornada = true;
            }
            else
                DescontoJornada = false;
        }

        //Jornedtotal
        if (IsencaoPonto != null) {
            if (IsencaoPonto.toString() == 'True' || IsencaoPonto.toString() == '1') {
                varIsencaoPonto = true;
            }
            else
                varIsencaoPonto = false;
        }

        //Painel
        if (Dashboard != null) {
            if (Dashboard.toString() == 'True' || Dashboard.toString() == '1') {
                cbDashboard.SetChecked(true);
                paineldashboardsV = true;
            }
            else {
                cbDashboard.SetChecked(false);
                paineldashboardsV = false;
            }
        }
        else {

            cbDashboard.SetChecked(false);
            paineldashboardsV = false;
        }

        if (AcessoEspecial.toString() == 'True') {
            rbAcessoEspecialFiltro.SetChecked(true);
        }
        else {
            rbAcessoEspecialFiltro.SetChecked(false);
        }

        //cbIsencaoPonto.SetChecked(IsencaoPonto);

        //Vínculos
        gridVinculoUsuario.PerformCallback('');
        
        popManutencaoUsuario.Show();
    }

    function AbrePopManutencao2() {
        popManutencaoUsuario.Show();
    }

    function Vinculos(IDVinculoUsuario, IDStatus) {

        //alert(varIsencaoPonto);
        //alert(DescontoJornada);
        if (IDVinculoUsuario != "0") {
            document.getElementById("<%=coOperacaoVinculo.ClientID %>").value = 'Alteracao';
            document.getElementById("<%=coIDVinculoUsuario.ClientID %>").value = IDVinculoUsuario;

            tbNome.SetText(nomeV);

            tbCPF.SetText(cpfV);
            cbTPUsuario.SetValue(idtpusuarioV);
            cbSetor.SetValue(idsetorV);
            //cbVinculo.SetVisible(false);
            //cbVinculo.SetValue(idvinculoV);
            cbCargo.SetValue(idcargoV);
            cbStatus.SetValue(IDStatus);
            cbRegimeHora.SetValue(idregimehoraV);
            tbTotHoras.SetText(tothorasdiariasV);
            tbEntradaManha.SetText(entrada1V);
            tbSaidaManha.SetText(saida1V);
            tbEntradaTarde.SetText(entrada2V);
            tbSaidaTarde.SetText(saida2V);
            cbCadastraDigital.SetChecked(cadastrabiometriaV);
            cbDashboard.SetChecked(paineldashboardsV);
            cbDescontoTotalJornada.SetChecked(DescontoJornada);
            cbIsencaoPonto.SetChecked(varIsencaoPonto);

        }
       
        cbGestorSetor.GetVisible(false);
       

        popVinculoUsuario.Show();
    }
    
    function VinculosCamposLimpos() {

        rbSenhaDigitalFiltro.SetVisible(false);

        document.getElementById("<%=coOperacaoVinculo.ClientID %>").value = 'Inclusao';
        //tpCadastro.SetActiveTab(0);

        //tbNome.SetText('');
        //deNascimento.SetText('');
        //deAdmissao.SetText('');
        tbPIS.SetText('');
        //tbCPF.SetText('');
        if (coIDTPUsuarioGeral2.Get("tpusuario") == "1" || coIDTPUsuarioGeral2.Get("tpusuario") == "7")
            cbSetor.SetValue();

        cbTPUsuario.SetValue();
        //cbVinculo.SetValue();
        cbCargo.SetValue();
        cbStatus.SetValue(1);
        tbTotHoras.SetText('');
        tbEntradaManha.SetText('');
        tbSaidaManha.SetText('');
        tbEntradaTarde.SetText('');
        tbSaidaTarde.SetText('');
        cbDashboard.SetChecked(false);
        cbCadastraDigital.SetChecked(false);
        cbPermiteDiaSeguinte.SetChecked(false);
        cbDescontoTotalJornada.SetChecked(false);

        cbRegimeHora.SetText('');

        cbGestorSetor.SetVisible(false);

        if (cotpuser.Get("tpuser") == "1" || cotpuser.Get("tpuser") == "7") {
            rbSenhaDigitalFiltro.SetVisible(true);
        }

        if (rbSenhaDigitalFiltro.GetVisible() != 'false')
            rbSenhaDigitalFiltro.SetChecked(false);

        rbAcessoEspecialFiltro.SetChecked(true);
        rbSenha.SetChecked(false);

        //document.getElementById("<%=coOperacao.ClientID %>").value = 'Incluir';

        popVinculoUsuario.Show();
    }
    
    function AbrePopManutencao() {

       
        rbSenhaDigitalFiltro.SetVisible(false);
        //tpCadastro.SetActiveTab(0);
        
        tbNome.SetText('');
        //deNascimento.SetText('');
        //deAdmissao.SetText('');
        tbPIS.SetText('');
        tbCPF.SetText('');
        if (coIDTPUsuarioGeral2.Get("tpusuario") == "1" || coIDTPUsuarioGeral2.Get("tpusuario") == "7")
            cbSetor.SetValue();
        
        cbTPUsuario.SetValue();
        //cbVinculo.SetValue();
        cbCargo.SetValue();
        cbStatus.SetValue(1);
        tbTotHoras.SetText('');
        tbEntradaManha.SetText('');
        tbSaidaManha.SetText('');
        tbEntradaTarde.SetText('');
        tbSaidaTarde.SetText('');
        cbDescontoTotalJornada.SetChecked(false);
        cbDashboard.SetChecked(false);
        cbCadastraDigital.SetChecked(false);
        cbPermiteDiaSeguinte.SetChecked(false);

        cbGestorSetor.SetVisible(false);

        if (cotpuser.Get("tpuser") == "1" || cotpuser.Get("tpuser") == "7") {
            rbSenhaDigitalFiltro.SetVisible(true);
        }

        //tbNomePai.SetText('');
        //tbNomeMae.SetText('');
        //tbCartTrabalho.SetText('');
        //deCTPS.SetText('');
        //tbCartReservista.SetText('');
        //tbTituloEleitor.SetText('');
        //tbRG.SetText('');
        //deRG.SetText('');
        //tbOrgaoExp.SetText('');
       // tbLogradouro.SetText('');
        //tbNumero.SetText('');
        //tbCEP.SetText('');
        //tbCidade.SetText('');
        //tbFone.SetText('');
        //tbCelular.SetText('');
        //cbGrauInstr.SetValue();
        //memoObs.SetText('');
        //cbGenero.SetValue();

        if(rbSenhaDigitalFiltro.GetVisible() != 'false')
            rbSenhaDigitalFiltro.SetChecked(false);
        
        rbAcessoEspecialFiltro.SetChecked(true);
        rbSenha.SetChecked(false);
        cbIsencaoPonto.SetChecked(false);

        document.getElementById("<%=coOperacao.ClientID %>").value = 'Incluir';

        gridVinculoUsuario.PerformCallback('Zerar');

        popManutencaoUsuario.Show();
    }

    function FechaPopManutencao() {
        
        popManutencaoUsuario.Hide();
    }
    
    function FechaPopUsuarioSalvar() 
    {
        if (!valida_cpf(tbCPF.GetValue())) {

            document.getElementById("<%=coCPF.ClientID %>").value = 'False';
            alert('CPF inválido. Verifique antes de salvar.');
        }
        else 
        {
            document.getElementById("<%=coCPF.ClientID %>").value = 'True';

            if (tbTotHoras.GetText() != '0') 
            {
                var PermiteFechar= false;


                if (document.getElementById("<%=coRegHora.ClientID %>").value == '1') {
                    switch (tbTotHoras.GetText()) {
                        case '6':
                            if (tbEntradaManha.GetText() == '00:00' && tbSaidaManha.GetText() == '00:00' && tbEntradaTarde.GetText() == '00:00' && tbSaidaTarde.GetText() == '00:00') {
                                PermiteFechar = false;
                                tbSaidaManha.SetIsValid(false);
                                tbEntradaManha.SetIsValid(false);
                                tbSaidaTarde.SetIsValid(false);
                                tbEntradaTarde.SetIsValid(false);
                            }
                            else if (tbEntradaManha.GetText() != '00:00' && tbSaidaManha.GetText() == '00:00') {
                                tbSaidaManha.SetIsValid(false);
                                PermiteFechar = false;
                            }
                            else if (tbEntradaManha.GetText() == '00:00' && tbSaidaManha.GetText() != '00:00') {
                                tbEntradaManha.SetIsValid(false);
                                PermiteFechar = false;
                            }
                            else if (tbEntradaTarde.GetText() != '00:00' && tbSaidaTarde.GetText() == '00:00') {
                                tbSaidaTarde.SetIsValid(false);
                                PermiteFechar = false;
                            }
                            else if (tbEntradaTarde.GetText() == '00:00' && tbSaidaTarde.GetText() != '00:00') {
                                tbEntradaTarde.SetIsValid(false);
                                PermiteFechar = false;
                            }
                            else if (tbEntradaManha.GetText() != '00:00' && tbSaidaManha.GetText() != '00:00') {
                                tbEntradaManha.SetIsValid(true);
                                tbSaidaManha.SetIsValid(true);
                                PermiteFechar = true;
                            }
                            else if (tbEntradaTarde.GetText() != '00:00' && tbSaidaTarde.GetText() != '00:00') {
                                tbEntradaTarde.SetIsValid(true);
                                tbSaidaTarde.SetIsValid(true);
                                PermiteFechar = true;
                            }
                            break;

                        case '4':
                            if (tbEntradaManha.GetText() == '00:00' && tbSaidaManha.GetText() == '00:00' && tbEntradaTarde.GetText() == '00:00' && tbSaidaTarde.GetText() == '00:00') {
                                PermiteFechar = false;
                                tbSaidaManha.SetIsValid(false);
                                tbEntradaManha.SetIsValid(false);
                                tbSaidaTarde.SetIsValid(false);
                                tbEntradaTarde.SetIsValid(false);
                            }
                            else if (tbEntradaManha.GetText() != '00:00' && tbSaidaManha.GetText() == '00:00') {
                                tbSaidaManha.SetIsValid(false);
                                PermiteFechar = false;
                            }
                            else if (tbEntradaManha.GetText() == '00:00' && tbSaidaManha.GetText() != '00:00') {
                                tbEntradaManha.SetIsValid(false);
                                PermiteFechar = false;
                            }
                            else if (tbEntradaTarde.GetText() != '00:00' && tbSaidaTarde.GetText() == '00:00') {
                                tbSaidaTarde.SetIsValid(false);
                                PermiteFechar = false;
                            }
                            else if (tbEntradaTarde.GetText() == '00:00' && tbSaidaTarde.GetText() != '00:00') {
                                tbEntradaTarde.SetIsValid(false);
                                PermiteFechar = false;
                            }
                            else if (tbEntradaManha.GetText() != '00:00' && tbSaidaManha.GetText() != '00:00') {
                                tbEntradaManha.SetIsValid(true);
                                tbSaidaManha.SetIsValid(true);
                                PermiteFechar = true;
                            }
                            else if (tbEntradaTarde.GetText() != '00:00' && tbSaidaTarde.GetText() != '00:00') {
                                tbEntradaTarde.SetIsValid(true);
                                tbSaidaTarde.SetIsValid(true);
                                PermiteFechar = true;
                            }
                            break;

                        case '8':
                            if (tbEntradaManha.GetText() == '00:00' && tbSaidaManha.GetText() == '00:00' && tbEntradaTarde.GetText() == '00:00' && tbSaidaTarde.GetText() == '00:00') {
                                PermiteFechar = false;
                                tbSaidaManha.SetIsValid(false);
                                tbEntradaManha.SetIsValid(false);
                                tbSaidaTarde.SetIsValid(false);
                                tbEntradaTarde.SetIsValid(false);
                            }
                            else if (tbEntradaManha.GetText() == '00:00') {
                                PermiteFechar = false;
                                tbEntradaManha.SetIsValid(false);
                            }
                            else if (tbSaidaManha.GetText() == '00:00') {
                                PermiteFechar = false;
                                tbSaidaManha.SetIsValid(false);
                            }
                            else if (tbEntradaTarde.GetText() == '00:00') {
                                PermiteFechar = false;
                                tbEntradaTarde.SetIsValid(false);
                            }
                            else if (tbSaidaTarde.GetText() == '00:00') {
                                PermiteFechar = false;
                                tbEntradaManha.SetIsValid(false);
                            }
                            else if (tbEntradaManha.GetText() != '00:00' && tbSaidaManha.GetText() != '00:00' && tbEntradaTarde.GetText() != '00:00' && tbSaidaTarde.GetText() != '00:00') {
                                PermiteFechar = true;
                                tbSaidaManha.SetIsValid(true);
                                tbEntradaManha.SetIsValid(true);
                                tbSaidaTarde.SetIsValid(true);
                                tbEntradaTarde.SetIsValid(true);
                            }
                            break;
                    }
                }
                else {
                    PermiteFechar = true;
                }

                //PL

                if (PermiteFechar) {
                    document.getElementById("<%=coPermiteExecutar.ClientID %>").value = 'True';
                }
                else {
                    document.getElementById("<%=coPermiteExecutar.ClientID %>").value = 'False';
                }

                popManutencaoUsuario.Hide();
            }
            else 
            {
                alert('Para terminar o cadastro, favor adicionar um vínculo clicando em Add. Vínculo!');
            }
        }
    }
    </script>
    <style type="text/css">
        .style1
    {
        width: 679px;
            margin-left: 0px;
        }
    .style2
    {
    }
    .style3
    {
            width: 68px;
            font-weight: 700;
        }
    .style4
    {}
    .style7
    {
        font-weight: 700;
    }
    .style8
    {
        width: 281px;
    }
    .style9
    {
    }
    .style10
    {
        width: 69px;
    }
    .style13
    {
        width: 82px;
    }
        .style14
        {
            width: 147px;
        }
        .style15
        {
        }
        .style18
        {
            width: 11px;
        }
        .style19
        {
            width: 107px;
        }
        .style21
        {
            width: 395px;
        }
        .style22
        {
            width: 173px;
        }
        .style23
        {
            width: 769px;
        }
        .style27
        {
            width: 137px;
        }
        .style28
        {
            width: 100%;
        }
        .style29
        {
            width: 140px;
        }
        .style31
        {
            width: 174px;
        }
        .style33
        {
            width: 94px;
        }
        .style34
        {
            width: 942px;
        }
        .style35
        {
            width: 98px;
        }
        .style36
        {
            width: 62px;
        }
        .style37
        {
            width: 127px;
        }
        .style38
        {
            width: 418px;
            text-align: left;
        }
        .style39
        {
            text-align: left;
        }
        .style40
        {
            text-align: left;
            }
        .style41
        {
            text-align: left;
            width: 72px;
        }
        .auto-style3
        {
            height: 26px;
            width: 26px;
        }
        .auto-style4
        {
            width: 4px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="style28">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3" colspan="2">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">MANUTENÇÃO DE COLABORADOR/USUÁRIO</div> 

            </td>
        </tr>
        <tr>
            <td colspan="2"><strong style="font-size: 16px; font-weight: normal;">
                <img alt="" class="auto-style3" src="../Images/Imagem4.png" /> Manutenção de colaborador/usuário </strong></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                                    <dx:ASPxComboBox runat="server" 
                            IncrementalFilteringMode="Contains" TextField="DSSetor" ValueField="IDSetor" 
                            Spacing="0" Width="400px" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                            CssPostfix="DevEx" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                            ClientInstanceName="cbSetorPrincipal" ID="cbSetorPrincipal" DropDownStyle="DropDown">
<LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif"></LoadingPanelImage>

<LoadingPanelStyle ImageSpacing="5px"></LoadingPanelStyle>

<ValidationSettings ValidationGroup="ConfirmaBusca">
<RequiredField IsRequired="True"></RequiredField>
</ValidationSettings>
</dx:ASPxComboBox>


                    </td>
        </tr>
        <tr>
            <td align="right">
            <dx:ASPxButton ID="btBuscar" runat="server" 
                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Filtrar" 
                Width="100px" AutoPostBack="False" ClientInstanceName="btBuscar" 
                            ValidationGroup="ConfirmaBusca" Theme="iOS">
                <ClientSideEvents Click="function(s, e) {
Pesquisa();
}" />
            </dx:ASPxButton>
                    </td>
            <td>
            <dx:ASPxButton ID="btDesfazer" runat="server" 
                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Desfazer" 
                Width="100px" AutoPostBack="False" ClientInstanceName="btDesfazer" 
                            ValidationGroup="ConfirmaBusca" Theme="iOS">
                <ClientSideEvents Click="function(s, e) {
desfazFiltro();
}" />
            </dx:ASPxButton>
                    </td>
        </tr>
        </table>
    <table class="style28">
        <tr>
            <td align="center">
    <table class="style1">
    <tr>
        <td class="style7">
            &nbsp;</td>
        <td class="style3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2" colspan="2">
            <table class="style1">
                <tr>
                    <td colspan="2">
                        <br />
                        <dx:ASPxGridView ID="gridUsuario" runat="server" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            Width="1100px" AutoGenerateColumns="False" ClientInstanceName="gridUsuario" 
                            DataMember="vwUsuarioGridAdmin" KeyFieldName="IDUsuario" 
                            oncustomcallback="gridUsuario_CustomCallback1" 
                            onpageindexchanged="gridUsuario_PageIndexChanged" 
                            onprocesscolumnautofilter="gridUsuario_ProcessColumnAutoFilter" 
                            onbeforecolumnsortinggrouping="gridUsuario_BeforeColumnSortingGrouping" 
                            Theme="DevEx" 
                            ondetailrowexpandedchanged="gridUsuario_DetailRowExpandedChanged">

                            <ClientSideEvents FocusedRowChanged="function(s, e) {}" 
                                DetailRowExpanding="function(s, e) {}" />

                            <Settings ShowFilterRow="True" ShowVerticalScrollBar="True" />
                            <SettingsText EmptyDataRow="Selecione um Setor" 
                                GroupPanel="Arraste uma coluna para formar grupo." />

                            <SettingsLoadingPanel Text="Processando&amp;hellip;" />

<ClientSideEvents FocusedRowChanged="function(s, e) {}"></ClientSideEvents>

                            <Columns>
                                <dx:GridViewDataTextColumn Caption="ID" FieldName="IDUsuario" Visible="False" 
                                    VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Nome Completo" FieldName="DSUsuario" 
                                    VisibleIndex="1" Width="280px">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Data de Cadastro" FieldName="DTCadastro" 
                                    VisibleIndex="6" Width="100px" UnboundType="String">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager NumericButtonCount="30" PageSize="30">
                                <AllButton Text="All">
                                </AllButton>
                                <NextPageButton Text="Next &gt;">
                                </NextPageButton>
                                <PrevPageButton Text="&lt; Prev">
                                </PrevPageButton>
                                <Summary Text="Página {0} de {1} ({2} itens)" />

<Summary Text="P&#225;gina {0} de {1} ({2} itens)"></Summary>
                            </SettingsPager>

<Settings ShowFilterRow="True" ShowVerticalScrollBar="True"></Settings>

<SettingsText GroupPanel="Arraste uma coluna para formar grupo." EmptyDataRow="Selecione um Setor"></SettingsText>

<SettingsLoadingPanel Text="Processando&amp;hellip;"></SettingsLoadingPanel>

                            <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />

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
                            <Templates>
                                <DetailRow>
                            <dx:ASPxGridView ID="gridUsuarioDetalhe" runat="server" 
                            AutoGenerateColumns="False" ClientInstanceName="gridUsuarioDetalhe" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            DataSourceID="sqlGridUsuarioDetalhe" KeyFieldName="IDUsuario" 
                            OnBeforePerformDataSelect="gridUsuarioDetalhe_DataSelect" Theme="DevEx" 
                            Width="800px">
                            <ClientSideEvents FocusedRowChanged="function(s, e) {}" />
                            <Settings ShowFilterRow="True" ShowVerticalScrollBar="False" />
                            <SettingsText EmptyDataRow="Selecione um Setor" 
                                GroupPanel="Arraste uma coluna para formar grupo." />
                            <SettingsLoadingPanel Text="Processando&amp;hellip;" />
                            <ClientSideEvents FocusedRowChanged="function(s, e) {}" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="ID" FieldName="IDUsuario" Visible="False" 
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Nome Completo" FieldName="DSUsuario" 
                                    Visible="False" VisibleIndex="2" Width="280px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Setor" FieldName="DSSetor" VisibleIndex="4" 
                                    Width="250px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Situação" FieldName="DSStatus" VisibleIndex="40" 
                                    Width="50px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Data de Cadastro" FieldName="DataCadastro" 
                                    VisibleIndex="5" Width="60px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDSetor" Visible="False" 
                                    VisibleIndex="32">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDCargo" Visible="False" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Login" Visible="False" VisibleIndex="33">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDStatus" Visible="False" 
                                    VisibleIndex="34">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDTPUsuario" Visible="False" 
                                    VisibleIndex="35">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDEntidade" Visible="False" 
                                    VisibleIndex="36">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TotHorasDiarias" Visible="False" 
                                    VisibleIndex="37">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="HoraEntradaManha" Visible="False" 
                                    VisibleIndex="38">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="HoraEntradaTarde" Visible="False" 
                                    VisibleIndex="39">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="HoraSaidaManha" Visible="False" 
                                    VisibleIndex="40">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="HoraSaidaTarde" Visible="False" 
                                    VisibleIndex="41">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SenhaDigital" Visible="False" 
                                    VisibleIndex="42">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AcessoEspecial" Visible="False" 
                                    VisibleIndex="11">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="30px">
                                    <DataItemTemplate>
                                        <a href="javascript:void(0);" 
                                            onclick='AbrePopManutencaoUsuario(&#039;<%#Eval("TelefoneSMS") %>&#039;,&#039;<%#Eval("Matricula") %>&#039;,&#039;<%#Eval("PrimeiroNome") %>&#039;,&#039;<%# Eval("IDUsuario") %>&#039;,&#039"<%# TrataNome(Container) %>"&#039;,&#039;<%# Eval("DTNascimento") %>&#039;,&#039;<%# Eval("DTAdmissao") %>&#039;,&#039;<%# Eval("PIS") %>&#039;,&#039;<%# Eval("Login") %>&#039;,&#039;<%# Eval("IDSetor") %>&#039;,&#039;<%# Eval("IDTPUsuario") %>&#039;,&#039;<%# Eval("IDCargo") %>&#039;,&#039;<%# Eval("IDStatus") %>&#039;,&#039;<%# Eval("HoraEntradaManha") %>&#039;,&#039;<%# Eval("HoraEntradaTarde") %>&#039;,&#039;<%# Eval("HoraSaidaManha") %>&#039;,&#039;<%# Eval("HoraSaidaTarde") %>&#039;,&#039;<%# Eval("TotHorasDiarias") %>&#039;,&#039;<%# Eval("AcessoEspecial") %>&#039;,&#039;<%# Eval("SenhaDigital") %>&#039;,&#039;<%# Eval("IDEntidade") %>&#039;,&#039;<%# Eval("DashboardCorporativo") %>&#039;,&#039;<%# Eval("CadastraDigital") %>&#039;,&#039;<%# Eval("FinalizaDiaSeguinte") %>&#039;,&#039;<%# Eval("IDRegimeHora") %>&#039;,&#039;<%# Eval("descontototaljornada") %>&#039;,&#039;<%# Eval("IDVinculoUsuario") %>&#039;,&#039;<%# Eval("IsencaoPonto") %>&#039;)'>
                                        <img src="../Icones/Editar.png" height="16px" width="16px" border="0" />Editar</a>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Matricula" Visible="True" VisibleIndex="31" Width="45px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DTAdmissao" Visible="False" 
                                    VisibleIndex="30">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DTNascimento" Visible="False" 
                                    VisibleIndex="29">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDEntidade" Visible="False" 
                                    VisibleIndex="8">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DashboardCorporativo" 
                                    UnboundType="Boolean" Visible="False" VisibleIndex="10">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CadastraDigital" Visible="False" 
                                    VisibleIndex="9">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FinalizaDiaSeguinte" Visible="False" 
                                    VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDRegimeHora" Visible="False" 
                                    VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                               <dx:GridViewDataTextColumn FieldName="descontototaljornada" Visible="False" 
                                    VisibleIndex="43">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IsencaoPonto" Visible="False" 
                                    VisibleIndex="44">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager NumericButtonCount="5" PageSize="5">
                                <AllButton Text="All">
                                </AllButton>
                                <NextPageButton Text="Next &gt;">
                                </NextPageButton>
                                <PrevPageButton Text="&lt; Prev">
                                </PrevPageButton>
                                <Summary Text="Página {0} de {1} ({2} itens)" />
                                <Summary Text="Página {0} de {1} ({2} itens)" />
                            </SettingsPager>
                            <Settings ShowFilterRow="True" ShowVerticalScrollBar="False" />
                            <SettingsText EmptyDataRow="Selecione um Setor" 
                                GroupPanel="Arraste uma coluna para formar grupo." />
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
                            <Styles CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx">
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
                                </DetailRow>
                            </Templates>
                        </dx:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                                    <dx:ASPxButton ID="btCadastrar" runat="server" 
                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cadastrar" 
                                        Width="150px" CausesValidation="False" 
                                        PostBackUrl="~/Cadastro/frmCadastraUsuario.aspx" 
                                        ClientInstanceName="btCadastrar" AutoPostBack="False" EnableTheming="True" 
                                        Theme="iOS">
                                        <ClientSideEvents Click="function(s, e) 
{
	AbrePopManutencao();

}" />
                                    </dx:ASPxButton>
                    </td>
                    <td align="left">
                                    <dx:ASPxButton ID="btVoltar" runat="server" 
                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                                        Width="150px" onclick="btVoltar_Click" CausesValidation="False" Theme="iOS">
                                    </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="style1">
                            <tr>
                                <td class="style10">
                                    &nbsp;</td>
                                <td class="style14">
                                    &nbsp;</td>
                                <td>

                                    &nbsp;</td>
                                <td class="auto-style4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style9" colspan="4">
                                    <table class="style1">
                                        <tr>
                                            <td align="center">
                                                <asp:HiddenField ID="FotoUsuario" runat="server" />
                                                <asp:HiddenField ID="coIDEmpresa" runat="server" />
                                                <asp:HiddenField ID="coRegHora" runat="server" />
                                                <asp:HiddenField ID="coIDVinculoUsuario" runat="server" />
                                                <asp:HiddenField ID="coEntradaManhaPadrao" runat="server" />
                                                <dx:ASPxHiddenField ID="coIDSetorVinculoUsuario" runat="server" 
                                                    ClientInstanceName="coIDSetorVinculoUsuario">
                                                </dx:ASPxHiddenField>
                                                <dx:ASPxHiddenField ID="coRegimeHora" runat="server" 
                                                    ClientInstanceName="coRegimeHora">
                                                </dx:ASPxHiddenField>
                                                <asp:HiddenField ID="coEntradaTardePadrao" runat="server" />
                                                <asp:HiddenField ID="coSaindaManhaPadrao" runat="server" />
                                                <asp:HiddenField ID="coSaidaTardePadrao" runat="server" />
                                                <br />
                                                <asp:HiddenField ID="coOperacao" runat="server" />
                                                <asp:HiddenField ID="coOperacaoVinculo" runat="server" />
                                                <dx:ASPxHiddenField ID="cotpuser" runat="server" ClientInstanceName="cotpuser">
                                                </dx:ASPxHiddenField>
                                                <dx:ASPxHiddenField ID="coGestorSetor" runat="server" 
                                                    ClientInstanceName="coGestorSetor">
                                                </dx:ASPxHiddenField>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
            </td>
        </tr>
    </table>
<dx:ASPxPopupControl ID="popManutencaoUsuario" runat="server" ClientInstanceName="popManutencaoUsuario" 
    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
    HeaderText="Manutenção de Usuário" 
    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="1000px" 
    Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" 
        EnableHotTrack="False" 
        onwindowcallback="popManutencaoUsuario_WindowCallback" Height="100%">
    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
    </LoadingPanelImage>
    <HeaderStyle>
    <Paddings PaddingLeft="7px" />
<Paddings paddingright="6px"></Paddings>
    </HeaderStyle>
    <LoadingPanelStyle ImageSpacing="5px">
    </LoadingPanelStyle>
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPageControl ID="tpCadastro" runat="server" ActiveTabIndex="0" 
        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100%" 
        ClientInstanceName="tpCadastro">
        <TabPages>
            <dx:TabPage Text="Dados Gerais">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <table class="style34">
                            <tr>
                                <td class="style15" colspan="3">
                                    <strong>Dados gerais:</strong></td>
                            </tr>
                            <tr>
                                <td class="style27">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" ClientIDMode="AutoID" Text="CPF:">
                                    </dx:ASPxLabel>
                                </td>
                                <td class="style21">
                                    <dx:ASPxTextBox ID="tbCPFMatricula" runat="server" ClientIDMode="AutoID" 
                                        ClientInstanceName="tbCPF" style="margin-left: 0px" Width="400px">
                                        <MaskSettings IncludeLiterals="None" 
                                            Mask="&lt;000..999&gt;,&lt;000..999&gt;,&lt;000..999&gt;-&lt;000..99&gt;" />
                                        <ValidationSettings ErrorText="CPF Inválido" ValidationGroup="ValidaCadastro">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style27">
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" ClientIDMode="AutoID" 
                                        Text="Nome Completo:">
                                    </dx:ASPxLabel>
                                </td>
                                <td class="style21">
                                    <dx:ASPxTextBox ID="tbNomeCompleto" runat="server" ClientInstanceName="tbNome" 
                                        CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                        SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                                        style="margin-left: 0px" Width="400px">
                                        <ValidationSettings ValidationGroup="ValidaCadastro">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table class="style28">
                            <tr>
                                <td>

                                    <dx:ASPxGridView ID="gridVinculoUsuario" runat="server" 
                                        AutoGenerateColumns="False" ClientInstanceName="gridVinculoUsuario" 
                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                        DataMember="vwVinculoUsuario" KeyFieldName="IDVinculoUsuario" 
                                        OnBeforeColumnSortingGrouping="gridVinculoUsuario_BeforeColumnSortingGrouping" 
                                        OnCustomCallback="gridVinculoUsuario_CustomCallback" 
                                        OnPageIndexChanged="gridVinculoUsuario_PageIndexChanged" 
                                        OnProcessColumnAutoFilter="gridVinculoUsuario_ProcessColumnAutoFilter" 
                                        Width="990px">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {}" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="#" ShowInCustomizationForm="True" 
                                                VisibleIndex="0" Width="50px">
                                                <DataItemTemplate>
                                                    <a href="javascript:void(0);" 
                                                        onclick='Vinculos(<%#Eval("IDVinculoUsuario") %>,<%#Eval("IDStatus") %>)'>
                                                    <img src="../Icones/Editar.png" height="16px" width="16px" border="0" />
                                                    Editar</a>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Setor" FieldName="DSSetor" 
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tipo" FieldName="DSTPUsuario" 
                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="40px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Início" FieldName="DTInicioVinculo" 
                                                ShowInCustomizationForm="True" VisibleIndex="5" Width="40px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Situação" FieldName="DSStatus" 
                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="50px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDVinculoUsuario" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDTPUsuario" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDRegimeHora" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Regime de hora" FieldName="DSRegimeHora" 
                                                ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDStatus" ShowInCustomizationForm="True" 
                                                Visible="False" VisibleIndex="18">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="TotalHoraDia" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ManutencaoDigital" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="AcessoDashBoards" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="HoraSaidaTarde" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="HoraSaidaManha" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="HoraEntradaTarde" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="HoraEntradaManha" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDCargo" ShowInCustomizationForm="True" 
                                                Visible="False" VisibleIndex="10">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDEntidade" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="descontototaljornada" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Matricula" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7" Width="30px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IsencaoPonto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager NumericButtonCount="5" PageSize="5">
                                            <AllButton Text="All">
                                            </AllButton>
                                            <NextPageButton Text="Next &gt;">
                                            </NextPageButton>
                                            <PrevPageButton Text="&lt; Prev">
                                            </PrevPageButton>
                                            <Summary Text="Página {0} de {1} ({2} itens)" />
                                        </SettingsPager>
                                        <Settings ShowFilterRow="True" />
                                        <SettingsText EmptyDataRow="Selecione um Setor" 
                                            GroupPanel="Arraste uma coluna para formar grupo." />
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
                                        <Styles CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx">
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
                        <table class="style28">
                            <tr>
                                <td class="style31">
                                    <dx:ASPxButton ID="btCadastraVinculo" runat="server" AutoPostBack="False" 
                                        ClientInstanceName="btCadastraVinculo" 
                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Add Vínculo" 
                                        Theme="iOS" ValidationGroup="ValidaCadastro" Width="128px">
                                        <ClientSideEvents Click="function(s, e) {
	VinculosCamposLimpos();
}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
        </LoadingPanelImage>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
        <ContentStyle>
            <Paddings Padding="12px" />
            <Border BorderColor="#9DA0AA" BorderStyle="Solid" BorderWidth="1px" />
        </ContentStyle>
    </dx:ASPxPageControl>
    <table class="style23">
        <tr>
            <td class="style14">
                <dx:ASPxCheckBox ID="rbAcessoEspecial" runat="server" CheckState="Unchecked" 
                    ClientInstanceName="rbAcessoEspecialFiltro" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Ponto Especial" 
                    Width="135px">
                </dx:ASPxCheckBox>
            </td>
            <td class="style18">
                <dx:ASPxCheckBox ID="rbSenha" runat="server" CheckState="Unchecked" 
                    ClientInstanceName="rbSenha" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Resetar Senha" Width="135px">
                </dx:ASPxCheckBox>
            </td>
            <td>
                <dx:ASPxCheckBox ID="cbEspecial" runat="server" CheckState="Unchecked" 
                    ClientInstanceName="rbSenhaDigitalFiltro" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Usuário não possui digital">
                </dx:ASPxCheckBox>
            </td>
            <td>
                &nbsp;</td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <dx:ASPxCheckBox ID="cbPermiteDiaSeguinte" runat="server" 
                    CheckState="Unchecked" ClientInstanceName="cbPermiteDiaSeguinte" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Permitir finalizar jornada no dia seguinte">
                </dx:ASPxCheckBox>
            </td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;</td>
            <td class="style22">&nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="style13">
                <dx:ASPxButton ID="btSalvar" runat="server" AutoPostBack="False" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    OnClick="btSalvar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Salvar" ValidationGroup="ValidaCadastro" Width="100px" Theme="iOS">
                    <ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('ValidaCadastro'))FechaPopUsuarioSalvar();
}" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btCancelar" runat="server" AutoPostBack="False" 
                    CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Cancelar" Width="100px" ClientInstanceName="btCancelarGestor" Theme="iOS">
                    <ClientSideEvents Click="function(s, e) {
	FechaPopManutencao();
}" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
</dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popVinculoUsuario" runat="server" 
        ClientInstanceName="popVinculoUsuario" CloseAction="None" 
        HeaderText="Adicionar vínculo" RenderMode="Lightweight" ShowCloseButton="False" 
        Theme="DevEx" Width="800px" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" Modal="True">
        <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxCallbackPanel ID="pcVinculoUsuario" runat="server" 
        ClientInstanceName="pcVinculoUsuario" 
        LoadingPanelText="Processando&amp;hellip;" Theme="DevEx" Width="750px" 
        OnCallback="pcVinculoUsuario_Callback">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <table class="style28">
                    <tr>
                        <td class="style37">
                            Telefone:</td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="tbTelSMS" runat="server" ClientIDMode="AutoID" ClientInstanceName="tbTelSMS" style="margin-left: 0px" Width="400px">
                                <MaskSettings IncludeLiterals="None" Mask="(&lt;00..99&gt;),&lt;00000..99999&gt; &lt;0000..9999&gt;" />
                                <ValidationSettings ErrorText="CPF Inválido" ValidationGroup="ValidaCadastro">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style37">Matricula:</td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="tbPIS" runat="server" ClientIDMode="AutoID" ClientInstanceName="tbPIS" style="margin-left: 0px" Width="400px">
                                <MaskSettings IncludeLiterals="None" Mask="0000000" />
                                <ValidationSettings ErrorText="CPF Inválido" ValidationGroup="ValidaCadastro">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style37">Tipo de Usuário:</td>
                        <td colspan="2">
                            <dx:ASPxComboBox ID="cbTPUsuario" runat="server" ClientInstanceName="cbTPUsuario" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" DataMember="TBTipoUsuario" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Spacing="0" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSTPUsuario" ValueField="IDTPUsuario" Width="400px">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) 
{VizualizaGestorSetor()}" />
                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <ValidationSettings ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style37">
                            Setor:</td>
                        <td class="style38">
                            <dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetor" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" 
                                Spacing="0" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                                TextField="DSSetor" ValueField="IDSetor" Width="400px">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	VerificaTpUser();
}" />
                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <ValidationSettings ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="style39">
                            <dx:ASPxCheckBox ID="cbGestorSetor0" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="cbGestorSetor" EnableTheming="True" 
                                Text="Gestão de mais setores" Theme="DevEx">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	AbrePopGestorSetor();
}" />
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style37">
                            Cargo:</td>
                        <td colspan="2">
                            <dx:ASPxComboBox ID="cbCargo" runat="server" ClientInstanceName="cbCargo" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                DataMember="TBCargo" EnableIncrementalFiltering="True" 
                                IncrementalFilteringMode="Contains" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSCargo" 
                                ValueField="IDCargo" Width="400px">
                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <ValidationSettings ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style37">
                            Situação:</td>
                        <td colspan="2">
                            <dx:ASPxComboBox ID="cbStatus" runat="server" ClientInstanceName="cbStatus" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                DataMember="TBStatus" EnableIncrementalFiltering="True" 
                                IncrementalFilteringMode="Contains" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSStatus" 
                                ValueField="IDStatus" Width="400px">
                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <ValidationSettings ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style37">
                            Regime de Horas:</td>
                        <td colspan="2">
                            <dx:ASPxComboBox ID="cbRegimeHora" runat="server" 
                                ClientInstanceName="cbRegimeHora" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                DataMember="TBRegimeHora" EnableIncrementalFiltering="True" 
                                EnableTheming="True" IncrementalFilteringMode="Contains" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSRegimeHora" 
                                Theme="DevEx" ValueField="IDRegimeHora" Width="400px">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {PreencheHoras();}" />
                                <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <ValidationSettings ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style37">
                            Total de horas diárias:</td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="tbTotHoras" runat="server" ClientInstanceName="tbTotHoras" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" style="margin-left: 0px" 
                                Width="100px">
                                <MaskSettings Mask="&lt;0..100&gt;" />
                                <ValidationSettings ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
                <table class="style28">
                    <tr>
                        <td class="style40" width="80">
                            Entrada 1:</td>
                        <td class="style33" width="100">
                            <dx:ASPxTextBox ID="tbEntradaManha" runat="server" 
                                ClientInstanceName="tbEntradaManha" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" style="margin-left: 0px" 
                                Width="100px">
                                <MaskSettings Mask="&lt;00..24&gt;:&lt;00..59&gt;" />
                                <ValidationSettings ErrorText="Hora inválida" SetFocusOnError="True" 
                                    ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="style41" width="60">
                            Saída 1:</td>
                        <td class="style35" width="100">
                            <dx:ASPxTextBox ID="tbSaidaManha" runat="server" 
                                ClientInstanceName="tbSaidaManha" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" style="margin-left: 0px" 
                                Width="100px">
                                <MaskSettings Mask="&lt;00..24&gt;:&lt;00..59&gt;" />
                                <ValidationSettings ErrorText="Hora Inválida" ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="style36">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style40" width="80">
                            Entrada 2:</td>
                        <td class="style33" width="100">
                            <dx:ASPxTextBox ID="tbEntradaTarde" runat="server" 
                                ClientInstanceName="tbEntradaTarde" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" style="margin-left: 0px" 
                                Width="100px">
                                <MaskSettings Mask="&lt;00..24&gt;:&lt;00..59&gt;" />
                                <ValidationSettings ErrorText="Hora Inválida" ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="style41" width="60">
                            Saída 2:</td>
                        <td class="style35" width="100">
                            <dx:ASPxTextBox ID="tbSaidaTarde" runat="server" 
                                ClientInstanceName="tbSaidaTarde" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" style="margin-left: 0px" 
                                Width="100px">
                                <MaskSettings Mask="&lt;00..24&gt;:&lt;00..59&gt;" />
                                <ValidationSettings ErrorText="Hora Inválida" SetFocusOnError="True" 
                                    ValidationGroup="ValidaVinculo">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="style36">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style40" colspan="8">
                            <dx:ASPxCheckBox ID="cbDescontoTotalJornada" runat="server" CheckState="Unchecked" ClientInstanceName="cbDescontoTotalJornada" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Aplicar regras de desconto no total da jornada" ToolTip="Desconsidera portaria ao aplicar o desconto no total da jornada diária" Width="300px">
                            </dx:ASPxCheckBox>
                            <dx:ASPxCheckBox ID="cbIsencaoPonto" runat="server" CheckState="Unchecked" ClientInstanceName="cbIsencaoPonto" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Isenção do ponto">
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
                <table class="style28">
                    <tr>
                        <td class="style29">
                            <dx:ASPxCheckBox ID="cbCadastraDigital" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="cbCadastraDigital" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cadastra Biometria">
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="cbDashboard" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="cbDashboard" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Painel Dashboard">
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
              
                    <tr>
                        <td class="style29">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
              
                </table>
                <table class="style28">
                    <tr>
                        <td class="style33">
                            <dx:ASPxButton ID="btSalvarVinculo" runat="server" AutoPostBack="False" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                                Text="OK" ValidationGroup="ValidaVinculo" Width="100px" 
                                ClientInstanceName="btSalvarVinculo" Theme="iOS">
                                <ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('ValidaVinculo'))FechaPopaADDVinculo();
}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btCancelar0" runat="server" AutoPostBack="False" 
                                CausesValidation="False" ClientInstanceName="btCancelarGestor" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cancelar" 
                                Width="100px" EnableTheming="True" Theme="iOS">
                                <ClientSideEvents Click="function(s, e) {
	FechaPopVinculoUsuario();
}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
</ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popImagem" runat="server" 
        ClientInstanceName="popImagem" 
        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
        CssPostfix="DevEx" HeaderText="Funcionário" 
        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
        Width="220px" ContentUrl="~/Cadastro/frmfoto.aspx" Height="270px" 
        Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" EnableHotTrack="False" 
        LoadingPanelText="Processando&amp;hellip;">
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
<dx:ASPxPopupControl ID="popGestorSetor" runat="server" ClientInstanceName="popGestorSetor" 
    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
    HeaderText="Escolha os setores para a gestão do usuário" 
    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="800px" 
    Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="TopSides" CloseAction="CloseButton" 
        EnableHotTrack="False" 
        onwindowcallback="popManutencaoUsuario_WindowCallback" Height="100%">
    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
    </LoadingPanelImage>
    <HeaderStyle>
    <Paddings PaddingLeft="7px" />
<Paddings paddingright="6px"></Paddings>
    </HeaderStyle>
    <LoadingPanelStyle ImageSpacing="5px">
    </LoadingPanelStyle>
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table class="style1">
        <tr>
            <td>
                <dx:ASPxCallbackPanel ID="cpPreencheItens" runat="server" 
                    ClientInstanceName="cpPreencheItens" EnableTheming="True" 
                    LoadingPanelText="Abrindo&amp;hellip;" OnCallback="cpPreencheItens_Callback" 
                    Theme="DevEx" Width="200px">
                    <PanelCollection>
                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxListBox ID="cbLSetores" runat="server" ClientInstanceName="cbLSetores" 
                                Height="350px" SelectionMode="CheckColumn" TextField="DSSetor" 
                                ValueField="IDSetor" Width="750px">
                            </dx:ASPxListBox>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="style13">
                <dx:ASPxButton ID="btOkSetorGestor" runat="server" AutoPostBack="False" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Ok" ValidationGroup="ValidaGestorSetor" Width="100px" 
                    ClientInstanceName="btOkSetorGestor" Theme="iOS">
                    <ClientSideEvents Click="function(s, e) {if(ASPxClientEdit.ValidateGroup('ValidaGestorSetor'))ValidaSelecao();}" />
<ClientSideEvents Click="function(s, e) {if(ASPxClientEdit.ValidateGroup(&#39;ValidaGestorSetor&#39;))ValidaSelecao();}"></ClientSideEvents>
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btCancelarSetorGestor" runat="server" AutoPostBack="False" 
                    CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Cancelar" Width="100px" ClientInstanceName="btCancelarSetorGestor" Theme="iOS">
                    <ClientSideEvents Click="function(s, e) {FechaPopGestorSetor();}" />
                    <ClientSideEvents Click="function(s, e) {FechaPopGestorSetor();}"></ClientSideEvents>
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
</dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="popBuscaSEAP" runat="server" ClientInstanceName="popBuscaSEAP" 
    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
    HeaderText="Servidor encontrado" 
    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="650px" 
    Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" 
        EnableHotTrack="False" 
        onwindowcallback="popManutencaoUsuario_WindowCallback" Height="100%">
    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
    </LoadingPanelImage>
    <HeaderStyle>
    <Paddings PaddingLeft="7px" />
<Paddings paddingright="6px"></Paddings>
    </HeaderStyle>
    <LoadingPanelStyle ImageSpacing="5px">
    </LoadingPanelStyle>
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table class="dxflInternalEditorTable">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="popErroKey" runat="server" ClientInstanceName="popErroKey" 
    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
    HeaderText="Matricula já cadastrada." 
    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="650px" 
    Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" 
        EnableHotTrack="False" 
        onwindowcallback="popManutencaoUsuario_WindowCallback" Height="100%">
    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
    </LoadingPanelImage>
    <HeaderStyle>
    <Paddings PaddingLeft="7px" />
<Paddings paddingright="6px"></Paddings>
    </HeaderStyle>
    <LoadingPanelStyle ImageSpacing="5px">
    </LoadingPanelStyle>
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table class="dxflInternalEditorTable">
        <tr>
            <td>
                <dx:ASPxLabel ID="lbPesquisaNull0" runat="server" 
                    ClientInstanceName="lbPesquisaNull" ForeColor="Red" 
                    Text="O vínculo informado já está cadastrado para este Órgão! Se deseja mudar o setor, edite o vínculo." 
                    Theme="DevEx" Width="650px">
                </dx:ASPxLabel>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <dx:ASPxButton ID="btErroKey" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btErroKey" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                    Text="Ok" ValidationGroup="ValidaGestorSetor" Width="100px" 
                    EnableTheming="True" Theme="iOS">
                    <ClientSideEvents Click="function(s, e) {FechapopErroKey();}" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
</dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

                        <asp:HiddenField ID="coIDUsuario" runat="server" />
                        <asp:HiddenField ID="coPermiteExecutar" runat="server" />
                        <asp:HiddenField ID="coIDUsuarioFoto" runat="server" />
                        <asp:SqlDataSource ID="sqlGridUsuarioDetalhe" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:PontoFrequenciaNConnectionString3 %>" 
                            SelectCommand="SELECT * FROM [vwUsuariogrid] WHERE (([IDEmpresa] = @IDEmpresa) AND ([IDUsuario] = @IDUsuario))" OnSelecting="sqlGridUsuarioDetalhe_Selecting">
                            <SelectParameters>
                                <asp:SessionParameter Name="IDEmpresa" 
                                    SessionField="IDEmpresaSQL" Type="Int32" DefaultValue="" />
                                <asp:SessionParameter Name="IDUsuario" 
                                    SessionField="IDUsuarioSQL" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sqlGridUsuarioDetalhe0" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:PontoFrequenciaNConnectionString5 %>" 
                            SelectCommand="SELECT * FROM [vwUsuariogrid] WHERE ([IDEmpresa] = @IDEmpresa)">
                            <SelectParameters>
                                <asp:SessionParameter Name="IDEmpresa" 
                                    SessionField="IDempresa" Type="Int32" DefaultValue="0" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:HiddenField ID="coCPF" runat="server" />
                        <asp:HiddenField ID="coNome" runat="server" />
                        
    </asp:Content>
