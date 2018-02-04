$(".filter").click(function () {
    $("#tablerows").collapse(!$("#filter").hasClass("show") ? 'hide' : 'show');
    $(this).text(function (i, old) {
        // return old == 'Filtros +' ? 'Filtros -' : 'Filtros +';
    });
});