function Clps(obj){
  for (var i=0; i<GlChld[obj.id].length; i++)
   if (
	   ((getBrowserType() == "MSIE") && obj.opnd == 1) ||
               ((getBrowserType() != "MSIE")  && obj.attributes.opnd.value == 1)
	  )
    {

//    if (obj.attributes.opnd.value == 1){
	var nextobj = document.getElementById(GlChld[obj.id][i]);
	if (nextobj)
	 {
	  nextobj.className = "menu_topic_closed";
	  Clps(nextobj);
	 }
    }
}
function Expand(obj){
 var parName = obj.id;
 parName=parName.substring(0,parName.lastIndexOf("_"));
 var GP = document.getElementById(parName);
 if (   (GP==null) ||     (GP!=null)
  &&  (
        ((getBrowserType() == "MSIE") && GP.opnd == 1) ||
        ((getBrowserType() != "MSIE")  && GP.attributes.opnd.value == 1)
	  )
	)
  {
   obj.className = "menu_topic_opened";
     for (var i=0; i<GlChld[obj.id].length; i++)
      {
	 var nextobj = document.getElementById(GlChld[obj.id][i]);
	  if (nextobj)
	    Expand(nextobj);
	}
  }
  else obj.className = "menu_topic_closed";
}
function IsClosed(obj){
 var Closed =(obj.className=="menu_topic_closed");
  for (var i=0; i<GlChld[obj.id].length; i++){
	var nextchild = document.getElementById(GlChld[obj.id][i]);
	if (nextchild)
  	Closed = Closed && IsClosed(nextchild);
  }
 return Closed;
}
function TglState(img_name, lnkd_obj_id){
 var lnkd_obj = document.getElementById(lnkd_obj_id);
 if (!lnkd_obj) return;
 lnkd_obj.className = "menu_topic_closed";
 img_obj = document.getElementById(img_name);
 getstate = IsClosed(lnkd_obj); 
  if (!getstate){
   Clps(lnkd_obj);
   lnkd_obj.className = "menu_topic_opened";
   attachMyAttrib(lnkd_obj, "opnd", 0);
   img_obj.src = img_obj.src.substring(0,img_obj.src.lastIndexOf("_opened.gif"))+".gif";

   var predImage = img_obj.parentNode.getElementsByTagName("img")[0];
   predImage.src = predImage.src.substring(0, predImage.src.lastIndexOf("minus"))+"plus.gif";
   predImage.alt = "+";
  }
  else {
   lnkd_obj.className = "menu_topic_opened";
   attachMyAttrib(lnkd_obj, "opnd", 1);
   Expand(lnkd_obj);
   lnkd_obj.className = "menu_topic_opened";
   img_obj.src = img_obj.src.substring(0,img_obj.src.lastIndexOf(".gif"))+"_opened.gif";
   var predImage = img_obj.parentNode.getElementsByTagName("img")[0];
   predImage.src = predImage.src.substring(0, predImage.src.lastIndexOf("plus"))+"minus.gif";
   predImage.alt = "-";
  }
}
function str2ar(str)
{
	var bits = new Array();
	for (var i = 0; i < str.length; ++i)
		bits.push(str.charCodeAt(i));
	return bits;
}
function ar2str(bits)
{
	var str = "";
	for (var i = 0; i < bits.length; ++i)
		str += String.fromCharCode(bits[i]);
	return str;
}
function attachMyAttrib(anElement, aName, aValue)
{
 if (getBrowserType().indexOf("MSIE") != -1)
    anElement[aName] = aValue;
 else
 {
	var myNewAttr = document.createAttribute(aName);
	myNewAttr.value = aValue;
	var myOldAttr = anElement.setAttributeNode(myNewAttr);
 }
}

function initMenuState()
{
var localvars = document.getElementById("Table2").getElementsByTagName("tr");
TopicCnt = localvars.length;
clsNames = new Array(TopicCnt);
VarTOpnd = new Array(TopicCnt);
GlChld=new Array();
GlChld["e"] = new Array ("e_1","e_2","e_3","e_4","e_5","e_6");

GlChld["e_1"] = new Array ("e_1_1","e_1_2");

GlChld["e_1_1"] = new Array ();

GlChld["e_1_2"] = new Array ();

GlChld["e_2"] = new Array ("e_2_1");

GlChld["e_2_1"] = new Array ();

GlChld["e_3"] = new Array ("e_3_1","e_3_2");

GlChld["e_3_1"] = new Array ();

GlChld["e_3_2"] = new Array ();

GlChld["e_4"] = new Array ("e_4_1","e_4_2","e_4_3");

GlChld["e_4_1"] = new Array ();

GlChld["e_4_2"] = new Array ();

GlChld["e_4_3"] = new Array ();

GlChld["e_5"] = new Array ("e_5_1","e_5_2");

GlChld["e_5_1"] = new Array ();

GlChld["e_5_2"] = new Array ();

GlChld["e_6"] = new Array ("e_6_1","e_6_2","e_6_3","e_6_4","e_6_5","e_6_6","e_6_7","e_6_8","e_6_9");

GlChld["e_6_1"] = new Array ();

GlChld["e_6_2"] = new Array ();

GlChld["e_6_3"] = new Array ();

GlChld["e_6_4"] = new Array ();

GlChld["e_6_5"] = new Array ();

GlChld["e_6_6"] = new Array ();

GlChld["e_6_7"] = new Array ();

GlChld["e_6_8"] = new Array ();

GlChld["e_6_9"] = new Array ();
  if (!location.search.substring(1)){ 
	opndState = new Array (1,1,0,0,1,0,1,0,0,1,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0);
	for(var i=0; i<TopicCnt; i++)
	 clsNames[i] = (opndState[i]==1)?"menu_topic_opened":"menu_topic_closed";
   VarTOpnd = new Array (1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);}
}
function processMenu()
{

 var pagesToOpen = new Array();
 var pagesToExpand = new Array();
 
 if (document.getElementsByClassName('a','menu_active').length)
 {
   var curPageID = document.getElementsByClassName('a','menu_active')[0].parentNode.parentNode.parentNode.id;
   if (curPageID.lastIndexOf('_') != -1)
	curPageID = curPageID.substring(0, curPageID.lastIndexOf('_'));
    do 
	{
		pagesToOpen[pagesToOpen.length] = curPageID;
		pagesToExpand[pagesToExpand.length] = curPageID;
        for (var i=0; i<GlChld[curPageID].length; i++)
        {
			pagesToOpen[pagesToOpen.length] = GlChld[curPageID][i];
	    }

		curPageID = curPageID.substring(0, curPageID.lastIndexOf('_'));
	}
	while (curPageID != "")	
  }

 
 var menuItems = document.getElementById("Table2").getElementsByTagName("tr");
 for (var i=0; i<TopicCnt; i++){	
 var curvar = menuItems[i];
	if (curvar){

	var bToOpen = 0;

     for (var j =0;j<pagesToExpand.length;j++)
      {
        if (curvar.id == pagesToExpand[j])
	    {
            VarTOpnd[i] = 1;
            /* Expand(curvar); */
            bToOpen = 1;
          }

      }
 
     for (var j =0;j<pagesToOpen.length;j++)
      {
        if (curvar.id == pagesToOpen[j])
	    {
            bToOpen = 1;
          }

      }

	if (bToOpen == 0 && curvar.id != "e")
	 curvar.className	= clsNames[i];
	else
	 curvar.className	= "menu_topic_opened";

	 
	 attachMyAttrib(curvar, "opnd", VarTOpnd[i]);
	 var imgid = "imag"+curvar.id;
	 if (imgid == "image")
			imgid = "AppImage";
		if (VarTOpnd[i]==1)
		    if (getBrowserType() == "MSIE")
				document.getElementById(imgid).style.cursor = "hand";
			else
				document.getElementById(imgid).style.cursor = "pointer";
		else
			document.getElementById(imgid).style.cursor = "default";
	 var imgpath = document.getElementById(imgid).src;
	 var newpath=imgpath;
	 var indexNum = imgpath.lastIndexOf("_opened.gif");
	 if(indexNum >-1)
		newpath = imgpath.substring(0,indexNum)+".gif";
	 var curTD = curvar.getElementsByTagName("td")[0];
 	 var DivTag = curTD.getElementsByTagName("div")[0];
	 	 if (!GlChld[curvar.id].length && imgid != "AppImage")
		 if (DivTag.style.paddingLeft.length != 0){
		 var iPadding = parseInt(DivTag.style.paddingLeft.substring(0,DivTag.style.paddingLeft.length-2),10);
		 DivTag.style.paddingLeft = iPadding * 1.5 + 15 + "px";
		 }
		if (GlChld[curvar.id].length > 0)
		{
			if (DivTag.style.paddingLeft)
			{
				var iPadding = parseInt(DivTag.style.paddingLeft.substring(0,DivTag.style.paddingLeft.length-2),10);
				DivTag.style.paddingLeft = iPadding * 1.5 + "px";
			}
			if (VarTOpnd[i] == 1){
 			 var sSRC = imgpath.substring(0, imgpath.lastIndexOf("bullet"))+"minus.gif";
			 var sStr = '<img style=\"cursor: pointer\" src=\"' + sSRC + '\" alt=\"-\" onclick=\"TglState(';
			 sStr += '\'' + imgid+'\',\''+curvar.id+'\')\" />';
 			 DivTag.innerHTML = sStr + DivTag.innerHTML;
			}
			else{
 			 var sSRC = imgpath.substring(0, imgpath.lastIndexOf("bullet"))+"plus.gif";
			 var sStr = '<img style=\"cursor: pointer\" src=\"' + sSRC + '\" alt=\"+\" onclick=\"TglState(';
			 sStr += '\'' + imgid+'\',\''+curvar.id+'\')\" />';
 			 DivTag.innerHTML = sStr + DivTag.innerHTML;
			};	
		}
	 curTD.style.whiteSpace = "nowrap";
	 var chldNum = GlChld[menuItems[i].id].length;
	 if (
			(
			((getBrowserType() == "MSIE")  && (curvar.opnd == "0")) ||
			((getBrowserType() != "MSIE")  &&  (curvar.attributes.opnd.value == "0")) 
			)
		&& chldNum)

		 if (indexNum>-1)
		 document.getElementById(imgid).src = newpath;
	};
 }
 
 if (document.getElementsByClassName('a','menu_active').length)
 {
 var curPageID = document.getElementsByClassName('a','menu_active')[0].parentNode.parentNode.parentNode.id;
 if (curPageID.lastIndexOf('_') != -1)
	curPageID = curPageID.substring(0, curPageID.lastIndexOf('_'));
 var pagesToOpen = new Array();
}
} 