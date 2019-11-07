/*
function $(){  // SUBSTITUI document.getElementById
	
	
	var elements = new Array();
	
	for (i=0; i<arguments.length;i++){
		var element = arguments[i];
		
		if (typeof(element)=="string")
			element = document.getElementById(element);
		
		if (arguments.length == 1)
			return element;
		
		elements.push(element);
	}
	return elements;
}
*/

//-------------------A J A X ---------------------------
function createXMLHTTP()
{
	/*
	var xmlHttpObject = null;
	try
 		{
  		// Firefox, Opera 8.0+, Safari...
  		xmlHttpObject = new XMLHttpRequest();
 	}
 	catch(ex)
 	{
  	// Internet Explorer...
		try
  		{
   			xmlHttpObject = new ActiveXObject('Msxml2.XMLHTTP');
  		}
  		catch(ex)
  		{
   			xmlHttpObject = new ActiveXObject('Microsoft.XMLHTTP');
  		}
 	}
	return xmlHttpObject;
	*/

	var ajax;
	try
	{
		ajax = new ActiveXObject("Microsoft.XMLHTTP");
	}
	catch(e)
	{
		try
		{
			ajax = new ActiveXObject("Msxml2.XMLHTTP");
			alert(ajax);
		}
		catch(ex)
		{
			try
			{
				ajax = new XMLHttpRequest();
			}
			catch(exc)
			{
				 alert("Esse browser não tem recursos para uso do Ajax");
				 ajax = null;
			}
		}
		return ajax;
	}

	   var arrSignatures = ["MSXML2.XMLHTTP.5.0", "MSXML2.XMLHTTP.4.0",
							"MSXML2.XMLHTTP.3.0", "MSXML2.XMLHTTP",
							"Microsoft.XMLHTTP"];
	   for (var i=0; i < arrSignatures.length; i++)
	   {
			try
			{
				var oRequest = new ActiveXObject(arrSignatures[i]);

				return oRequest;
			}
			catch (oError)
			{
			}
	   }
		throw new Error("MSXML is not installed on your system.");

}

function atualizaMenuTree(UsuId, Objeto , Simulacao){
		var proc ='apmontarmenuflow.aspx?'+UsuId+","+Simulacao;
		var titulo = document.getElementById('CAPTIONTITULO').innerHTML;
	
		document.getElementById('CAPTIONTITULO').innerHTML = "<img src='../static/imagens/carregando.gif' height=17>"
		var objAjax = createXMLHTTP();
		objAjax.open("GET", proc, true);
		objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
		objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
		objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
		objAjax.setRequestHeader("Pragma", "no-cache");
		objAjax.setRequestHeader("Expires", "-1");
	
		objAjax.onreadystatechange=function(){
			if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
				  document.getElementById(Objeto.id).innerHTML = objAjax.responseText;
				  document.getElementById('CAPTIONTITULO').innerHTML = titulo;
			}
		}
		objAjax.send(null);

}

function caixaEntrada(UsuId, Situacao, Simulacao){
	var proc ='hwmcaixadeentrada2.aspx?'+UsuId+","+Situacao+","+Simulacao;
	//var titulo = document.getElementById('CAPTIONTITULO').innerHTML;
	//document.getElementById('CAPTIONTITULO').innerHTML = "<img src='../static/imagens/carregando.gif' height=17>"
	var objAjax = createXMLHTTP();
	objAjax.open("GET", proc, true);
	objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
	objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
	objAjax.setRequestHeader("Pragma", "no-cache");
	objAjax.setRequestHeader("Expires", "-1");

	objAjax.onreadystatechange=function(){
		if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
              alert(objAjax.responseText);
			  document.getElementById('EMBPAGE').innerHTML = objAjax.responseText;

			  document.getElementById('CAPTIONTITULO').innerHTML = titulo;
		}
	}
	objAjax.send(null);
}

function favoritos(UsuId, SisId, objFavoritos){
    proc = '/apgetfavoritos.aspx'
	var objAjax = createXMLHTTP();
	objAjax.open("POST", proc, true);
	objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
	objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
	objAjax.setRequestHeader("Pragma", "no-cache");
	objAjax.setRequestHeader("Expires", "-1");
	objAjax.setRequestHeader("UsuId", UsuId);
	objAjax.setRequestHeader("SisId", SisId);

	objAjax.onreadystatechange=function(){
		if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
				document.getElementById(objFavoritos.id).innerHTML = objAjax.responseText;
		}
	}
	objAjax.send(null);
}

function atualizaFavoritos(UsuId, SisId, ObjNome, MenDsc, Mode, objFavoritos){
	if (MenDsc=="descricao" && Mode=='INS'){
		MenDsc = prompt("Informe o nome do favorito.", "");
	}
    proc = '/apmanterfavoritos.apsx'
	//document.getElementById('GX_ATUALIZANDO_MPAGE').innerHTML += "<img src='../static/imagens/carregando.gif' height=17>"
	var objAjax = createXMLHTTP();
	objAjax.open("POST", proc, true);
	objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
	objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
	objAjax.setRequestHeader("Pragma", "no-cache");
	objAjax.setRequestHeader("Expires", "-1");
	objAjax.setRequestHeader("UsuId", UsuId);
	objAjax.setRequestHeader("SisId", SisId);
	objAjax.setRequestHeader("ObjNome", ObjNome);
	objAjax.setRequestHeader("MenDsc", MenDsc);
    objAjax.setRequestHeader("Mode", Mode);

	objAjax.onreadystatechange=function(){
		if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
        	 	   favoritos(UsuId, SisId, objFavoritos);
        	 	   alert('Atualizado!');
		}
	}
	objAjax.send(null);
}

function buscaAcaoObjeto(SisId, UsuLogin, ObjNome){
	proc = 'apveracaoobjeto.aspx?'+campoChave+","+codId;
	var objAjax = createXMLHTTP();
	objAjax.open("GET", proc, true);
	objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
	objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
	objAjax.setRequestHeader("Pragma", "no-cache");
	objAjax.setRequestHeader("Expires", "-1");

	objAjax.onreadystatechange=function(){
		if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
 			if (op==1){
				document.getElementById(objId.id).innerHTML = objAjax.responseText;
				document.getElementById(objId.id).value = objAjax.responseText;
		    }else{
				document.getElementById(objId.id).value = objAjax.responseText;
		   	}
		}
	}
	objAjax.send(null);
}


function MudaDirecionamento(Tipo) {
	if (Tipo == 1) {
		this.gxenableform();
		document.forms[0].target="_blank";

		}
	else {
		if (Tipo == 2) {
			document.forms[0].target="_self";
		}
	}
    return true
}



function MudaDirecionamento2(Tipo) {
	if (Tipo == 1) {
		gxenableform()
		document.forms[0].target="_blank"
	}
	else {
		if (Tipo == 2) {
			document.forms[0].target="_self"
		}
	}
	return true
}

function ResizePagina(objeto, wi, hg) {
    var tx=screen.width;
    var ty=screen.height;
    var dif=(ty * 0.10);
    var lf=(tx/2) - (wi/2) - 5;
    var tp=(ty/2) - (hg/2) - (dif/2) + 0.01;
    var status = "Resolução:"+ tx + " x " + ty;
    var Opcoes="status=yes, scrollbars=no, fullscreen=no, top="+tp+",left="+lf+",width="+wi+",height="+hg;
    window.open(objeto,"_blank", Opcoes);
}



function resizePrompt(wi, hg){
	var tx=screen.width;
	var ty=screen.height;
	var lf=(tx/2) - (wi/2);
	var tp=(ty/2) - (hg/2) - 50;

	window.resizeTo(wi, hg);
}

function pcall(Objeto){
	proc = Objeto + '.aspx';
	document.getElementById('GX_ATUALIZANDO_MPAGE').innerHTML += "<img src='../static/imagens/carregando.gif' height=17>"
	var objAjax = createXMLHTTP();
	objAjax.open("GET", proc, true);
	objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
	objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
	objAjax.setRequestHeader("Pragma", "no-cache");
	objAjax.setRequestHeader("Expires", "-1");
    alert('chamando:'+Objeto);
	objAjax.onreadystatechange=function(){
		if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
		        //alert(objAjax.responseText);
				document.getElementById('gx_pcall').innerHTML = objAjax.responseText;
            	document.getElementById('GX_ATUALIZANDO_MPAGE').innerHTML = " "
		}
	}
	objAjax.send(null);
}

function atualizaPainel(Sisbase, SisId, UsuId, Objeto){
	//proc = Sisbase + '/appainelatividades?'+SisId+","+UsuId;
	proc ="/appainelatividades.aspx?"+SisId+","+UsuId;

	document.getElementById('GX_ATUALIZANDO_MPAGE').innerHTML += "<img src='../static/imagens/carregando.gif' height=17>"
	var objAjax = createXMLHTTP();
	objAjax.open("GET", proc, true);
	objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
	objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
	objAjax.setRequestHeader("Pragma", "no-cache");
	objAjax.setRequestHeader("Expires", "-1");

	objAjax.onreadystatechange=function(){
		if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
				document.getElementById(Objeto.id).innerHTML = objAjax.responseText;
            	document.getElementById('GX_ATUALIZANDO_MPAGE').innerHTML = " "
		}
	}
	objAjax.send(null);
}


function consultaXML(procConsulta, parm1, objRetorno){
	proc = procConsulta+".aspx?"+parm1;
	var objAjax = createXMLHTTP();
	objAjax.open("get", proc, true);
	objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
	objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
	objAjax.setRequestHeader("Pragma", "no-cache");
	objAjax.setRequestHeader("Expires", "-1");

	objAjax.onreadystatechange=function(){
		if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
		    updateChartXML(objRetorno, objAjax.responseText);
		}
	}
	objAjax.send(null);
}
function buscadescricao(obj2, campoChave , codId){							
		//Content-Type: text/html; charset=iso-8859-1
		proc = 'appdescricao.aspx';
		var objAjax = createXMLHTTP();
		//objAjax.open("get", proc, true);
		objAjax.open("post", proc, true);
		//objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
		objAjax.setRequestHeader("Content-Type", "text/html; charset=ISO-8859-1");
		objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
		objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
		objAjax.setRequestHeader("Pragma", "no-cache");
		objAjax.setRequestHeader("campoChave", campoChave);
		objAjax.setRequestHeader("codId", codId);
		objAjax.onreadystatechange = function()
		{
			if (objAjax.readyState == 4)
			{// abaixo o texto do gerado no arquivo que e colocado no div
				document.getElementById(obj2.id).innerHTML = objAjax.responseText;
				obj3 = obj2.id;
				obj3 = obj3.substr(5, obj3.length - 5);
				descricao = objAjax.responseText;
				//document.getElementById(obj3).value = descricao.substr(43, descricao.length - 43);
				
				$("input[name="+obj3+"]").val(descricao.substr(43, descricao.length - 43));
			}
		}
	
	//objAjax.send();
	objAjax.send(null);
}

function buscadescricao2(obj2, campoChave , codId){	
		//Content-Type: text/html; charset=iso-8859-1
		proc = 'appdescricao.aspx';
		var objAjax = createXMLHTTP();
		//objAjax.open("get", proc, true);
		objAjax.open("post", proc, true);
		//objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
		objAjax.setRequestHeader("Content-Type", "text/html; charset=ISO-8859-1");
		objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
		objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
		objAjax.setRequestHeader("Pragma", "no-cache");
		objAjax.setRequestHeader("campoChave", campoChave);
		objAjax.setRequestHeader("codId", codId);
		objAjax.onreadystatechange = function()
		{
			if (objAjax.readyState == 4)
			{// abaixo o texto do gerado no arquivo que e colocado no div
				document.getElementById(obj2.id).innerHTML = objAjax.responseText;
				obj3 = obj2.id;
				obj3 = obj3.substr(5, obj3.length - 5);
				descricao = objAjax.responseText;
				//document.getElementById(obj3).value = descricao.substr(43, descricao.length - 43);
				
				$("input[name="+obj3+"]").val(descricao.substr(43, descricao.length - 43));
				
				showhideelem('_SGTVISTLIDSCPRJTIPO','TABCONTRATO',3,2);
			}
		}
	
	//objAjax.send();
	objAjax.send(null);
}
/*
function buscadescricao(objId, campoChave , codId, op){
	// op = 0  - somente leitura
	// op = 1  - editável

    document.getElementById('gx_aguarde').innerHTML = "<img src='../static/imagens/carregando.gif' height=17>";

	//CHAMADA VIA POST
	proc = 'apdescricao.aspx'
	var objAjax = createXMLHTTP();
	objAjax.open("POST", proc, true);

	objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
	objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
	objAjax.setRequestHeader("Pragma", "no-cache");
	objAjax.setRequestHeader("Expires", "-1");

	//CHAMADA VIA POST
	objAjax.setRequestHeader("CampoChave", campoChave);
	objAjax.setRequestHeader("ValorChave", codId);

	objAjax.onreadystatechange=function(){
		if (objAjax.readyState==4){// abaixo o texto do gerado no arquivo que e colocado no div
 			if (op==1){
				document.getElementById(objId.id).innerHTML = objAjax.responseText;
				document.getElementById(objId.id).value = objAjax.responseText;
				document.getElementById('gx_aguarde').innerHTML = " ";
		    	}
		   	else{
				document.getElementById(objId.id).value = objAjax.responseText;
		   	}
	}
	}
	objAjax.send(null);
}
*/
function CloseForm() {
    window.close();
    return true
}

function showHide(id){
	var objDisp = document.getElementById(id).style.display;
	var objDiv = document.getElementById(id);
	var objImgNome = 'img'+id;
	var objImgNome = 2
	var objImg = document.getElementById(objImgNome);

		if(objDisp == 'none')
		{
			objDiv.style.display = 'block';
			objImg.src = '../imagens/deleta.gif';
		}
		else
		{
			objDiv.style.display = 'none';
			objImg.src = '../imagens/adicionar.gif';
		}

}

function alteraVisible(id){
 if (document.getElementById(id).style.display  == 'block')
      document.getElementById(id).style.display  = 'none';
 else
      document.getElementById(id).style.display  = 'block';
}

var iCntr = 0;
var sCurId;
var sNewId;

function changeDivId() {
	if(iCntr == 0) {
		sCurId = 'hidden_div';
		sNewId = 'newId';
                iCntr++;
                document.getElementById(sCurId).id = newId;
	} else {
                document.getElementById(sCurId).id = newId;
        }

        var temp = sCurId;
        sCurId = newId;
        newId = temp;
}



////////////////////////////////////////////////////////////
// To call:
//     createXmlHttpObject('xmlHttppage2.htm', '', 'bla');
////////////////////////////////////////////////////////////

var g_xmlHttpObject = null;
var g_controlId = null;

function getXmlHttpObject()
{
 	var xmlHttpObject = null;
	try
 		{
  		// Firefox, Opera 8.0+, Safari...
  		xmlHttpObject = new XMLHttpRequest();
 	}
 	catch(ex)
 	{
  	// Internet Explorer...
		try
  		{
   			xmlHttpObject = new ActiveXObject('Msxml2.XMLHTTP');
  		}
  		catch(ex)
  		{
   			xmlHttpObject = new ActiveXObject('Microsoft.XMLHTTP');
  		}
 	}
	return xmlHttpObject;
}

function createXmlHttpObject(pageUrl, queryString, controlId)
{
 	g_controlId = controlId;
 	g_xmlHttpObject = getXmlHttpObject();
	 if ( g_xmlHttpObject == null )
	 {
	  alert('Your browser does not support AJAX.');
	  return;
	 }

	g_xmlHttpObject.onreadystatechange = readyStateChanged;
	if ( pageUrl.length > 0 )
 	{
  		pageUrl += '?q=' + queryString;
		pageUrl += '&sid=' + Math.random();
		g_xmlHttpObject.open('GET', pageUrl, true);
 	}
	g_xmlHttpObject.send(null);
}

function readyStateChanged()
{
 	if ( g_xmlHttpObject.readyState==4 )
 	{
  		var responseString = g_xmlHttpObject.responseText;
 		if ( (g_controlId) && (g_controlId.length > 0) )
   			document.getElementById(g_controlId).innerHTML = responseString;
 	}
}


function getElementByName(tag,nome){
	var els = document.getElementsByTagName(tag);
		for(var i=0;i<els.length;i++){
			if(els[i].name==nome)
				return els[i];
            };
}


function setFrame(frmName,frmSrc){
	var frm = getElementByName('iframe',frmName);
	frm.src = frmSrc;
}

function setArquivo(arq,nome,tipo){
	
	var arquivo = arq.value.substr(arq.value.lastIndexOf('\\')+1,arq.value.length);

	var prefixoNome = nome.substr(0,nome.lastIndexOf('_'));
	var prefixoTipo = tipo.substr(0,tipo.lastIndexOf('_'));
	
	var sufixo  = arq.name.substr(arq.name.lastIndexOf('_'),arq.name.length);
	
	var arqNome = arquivo.substr(0,(arquivo.lastIndexOf('.')!=-1)?arquivo.lastIndexOf('.'):arquivo.length);
	var arqTipo = arquivo.substr(arquivo.lastIndexOf('.')+1,(arquivo.lastIndexOf('.')!=-1)?arquivo.length:0);

	var objNome = getElementByName('input',prefixoNome+sufixo);
	var objTipo = getElementByName('input',prefixoTipo+sufixo);
	

	var spanObjNome = document.getElementById('span_'+prefixoNome+sufixo);
	var spanObjTipo = document.getElementById('span_'+prefixoTipo+sufixo);
	
	if (spanObjNome)
		spanObjNome.innerHTML = arqNome;
	if (spanObjTipo)		
		spanObjTipo.innerHTML = arqTipo;
		
	if (objNome)		
		objNome.value = arqNome;
	if (objTipo)				
		objTipo.value = arqTipo;

}

 
//$(document).ready(function () {
 
	//alert(document.getElementById('CTLSGMDOCANXSTRING_0001'));
	// alert($('#CTLSGMDOCANXSTRING_0001').attr('type'));
	// $('#CTLSGMDOCANXSTRING_0001').attr('value','file');
	
	
	//document.getElementById('CTLSGMDOCANXSTRING_0001').type = 'file';
	
 //}
 //);
 //YAHOO.util.Event.onDOMReady(init);




 function atualizatSDTTIPO(id,cod,origem){
 
	 var elm = $(id);
	 var vlr = elm.value;
  	if (elm.checked == true){
 		status = 'I';
	}
	else{
		status = 'E';
	}		

		proc = 'aPPSGTAtualizaCheckTipoInternet.aspx';
		var objAjax = createXMLHTTP();
		//objAjax.open("get", proc, true);
		objAjax.open("post", proc, true);
		//objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
		objAjax.setRequestHeader("Content-Type", "text/html; charset=ISO-8859-1");
		objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
		objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
		objAjax.setRequestHeader("Pragma", "no-cache");
		objAjax.setRequestHeader("SgtTpoIntCod", cod);
		objAjax.setRequestHeader("Status", status);
		objAjax.setRequestHeader("Origem", origem);
		objAjax.onreadystatechange = function()
		{
			if (objAjax.readyState == 4)
			{
			}
		}
	
	//objAjax.send();
	objAjax.send(null)
}

/*---------------------------------------------------------------------------
Nome......: showhideele
Objetivo..: Apresentar ou remover objeto conforme a marcação de um checkbox;
Autor.....: Fernando Vieira Duarte
Comentários
 Parâmetros -> id (checkbox/radiobutton verificado para executar)
 	       campo (objeto da tela que será apresentado ou removido)
-----------------------------------------------------------------------------*/

function showhideelem(id,campo,tpo,valor){
switch(tpo) {
   case 1:	
   	if ($("input[name="+id+"]").is(':checked')){
	    $('#'+campo).show();
		exit;
   	}else{
	    $('#'+campo).hide();
		exit;
   	}
  	
    case 2:	
	$("input[name="+id+"]").each(function() {
	if ( $(this).is(':checked') ) {		
		vlr = $(this).val();				
		if (vlr == valor){		
			$('#'+campo).show();
			exit;						
		}else{
			$('#'+campo).hide();
			exit;			
		}
	    }
	});

    case 3:		
	if ($("input[name="+id+"]").val() == valor){
	 	$('#'+campo).show();
		exit;
	}else{
	    $('#'+campo).hide();
		exit;
	}
    }
		
}


function sleep(milliseconds) {
  var start = new Date().getTime();
  for (var i = 0; i < 1e7; i++) {
    if ((new Date().getTime() - start) > milliseconds){
      break;
    }
  }
}

function escondeCampos(id,perg)
{
switch(perg) {
   case 1:
	$("input[name='_SGTQUESTCERTTPOCONTROL']").each(function() {
	if ( $(this).is(':checked') ) {		
		vlr = $(this).val();				
		if (vlr == 1){		
			$('#TBL_OPCAO').show();
			$('#TBL_INTERVALO').hide();
			exit;						
		}else{
			$('#TBL_INTERVALO').show();
			$('#TBL_OPCAO').hide();
			exit;			
		}
	    }
	});
	
   case 2:
	$("input[name='_SGTQUESTCERTFLGINTERVALO']").each(function() {
	if ( $(this).is(':checked') ) {		
		vlr = $(this).val();				
		if (vlr == 1){		
			$('#TBL_VALORINTERVALO').show();	
		}else{
			$('#TBL_VALORINTERVALO').hide();
		}
	    }
	});	
	}
}
	
function SetarSessao(AVAQuestCod,AVAQuestOpcCod,AvaQuestCodPai,Indice,nomOpcao){		
	$('#'+nomOpcao).each(function() {
		vlrCampo = $(this).val();
		vlrQuestao         =             vlrCampo;		
	});	

		jQuery.ajax({
		type: "POST",
	
		url:"apsetasessao.aspx",	
		data: "AVAQuestCod="+AVAQuestCod+"&AVAQuestOpcCod="+AVAQuestOpcCod+"&AvaQuestCodPai="+AvaQuestCodPai+"&Indice="+Indice+"&Resposta="+vlrQuestao
		
		});
	//} 
}

function CalculaTotCVSOrcamento(CodTR,AnoTR,CodOF,AnoOF,CodMS,QtdMS,VlrMS,FlagExtra){
                               var Qtd = $('#' + QtdMS).val();
                               var vlr = $('#' + VlrMS).val();
                               vlr = vlr.replace(',','.');
                               Qtd = Qtd.replace(',','.');              
                               //Content-Type: text/html; charset=iso-8859-1
                               proc = 'apcalculatotais.aspx';
                               var objAjax = createXMLHTTP();
                               //objAjax.open("get", proc, true);
                               objAjax.open("post", proc, true);
                               //objAjax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                               objAjax.setRequestHeader("Content-Type", "text/html; charset=ISO-8859-1");
                               objAjax.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
                               objAjax.setRequestHeader("Cache-Control", "post-check=0, pre-check=0");
                               objAjax.setRequestHeader("Pragma", "no-cache");
                               objAjax.setRequestHeader("CodTR", CodTR);
                               objAjax.setRequestHeader("AnoTR", AnoTR);
                               objAjax.setRequestHeader("CodOF", CodOF);
                               objAjax.setRequestHeader("AnoOF", AnoOF);
                               objAjax.setRequestHeader("CodMS", CodMS);
                               objAjax.setRequestHeader("QtdMS", Qtd);
                               objAjax.setRequestHeader("VlrMS", vlr);
                               objAjax.setRequestHeader("FlagExtra", FlagExtra);
                               objAjax.onreadystatechange = function()
                               {
                                               if (objAjax.readyState == 4)
                                               {// abaixo o texto do gerado no arquivo que e colocado no div
                                                               document.location.replace(document.location.href);
                                                               
                                               }
                               }
                
                //objAjax.send();
                objAjax.send(null);
}

function showhideelemNovo(id,campo,tpo,valor,campo2){
switch(tpo) { 	
    case 2:	
	$("input[name="+id+"]").each(function() {
	if ( $(this).is(':checked') ) {		
		vlr = $(this).val();				
		if (vlr == valor){		
			$('#'+campo).show();
			$('#'+campo2).hide();
			exit;						
		}else{
			$('#'+campo).hide();
			$('#'+campo2).show();
			exit;			
		}
	    }
	});		
}
	}
