$(function () {
  $(".stylist").each(function() {
    $(this).click(function() {
      clicked = $(this).attr('id');
      $(".rest-" + clicked).toggle();
    });
  });
  $(".showForm").click(function(){
    $(".hideThis").show();
  });
});
