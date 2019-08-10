
$(document).ready(function () {
    $(".table").DataTable();



    //var table = $('.table').DataTable();
    //$('.table').on('page.dt', function () {
    //    var info = table.page.info();

    //    $('#tableInfo').html(
    //        'Currently showing page ' + (info.page + 1) + ' of ' + info.pages + ' pages.'
    //    );
    //    alert(info.page+1)
    //});
});

$(function () {
    $(".btnInbox").click(function (e) {

        $.ajax({
            method: "GET",
            url: "/Message/DropdownMesaj/"

        }).done(function (d) {
            $("#listeleme").html(d);
        }).fail(function () {
            alert("Hata oluştu..");
        }).always(function () {

        });
    });
});
