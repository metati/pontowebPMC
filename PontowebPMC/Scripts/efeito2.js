$(document).ready(function(){
       $('.imagemenu').width("95px");
       $('.imagemenu').mouseover(function()
       {
          $(this).css("cursor","pointer");
          $(this).animate({width: "100px"}, 'slow');
       });
    
    $('.imagemenu').mouseout(function()
      {   
          $(this).animate({width: "95px"}, 'slow');
       });
   });
