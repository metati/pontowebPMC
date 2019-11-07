$(document).ready(function(){
       $('.imagemenu').width("175px");
       $('.imagemenu').mouseover(function()
       {
          $(this).css("cursor","pointer");
          $(this).animate({width: "190px"}, 'slow');
       });
    
    $('.imagemenu').mouseout(function()
      {   
          $(this).animate({width: "175px"}, 'slow');
       });
   });
