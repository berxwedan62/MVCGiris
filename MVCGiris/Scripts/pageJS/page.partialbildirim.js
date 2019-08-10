
$(setInterval(function myFunction() {

    $.ajax({
        method: "GET",
        url: "/Home/JsonMesssage/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (e) {

            $('.badge-counter').html($(e).length.toString());
            $('#yeniler').html("");
            $.each(e, function (i, item) {
                var html = '<a href="#" class="btnMessage" data-toggle="modal" data-target="#exampleModal" data-message-id="' + e[i].id + '">' + e[i].baslik + ' </a><hr/>';
                $('#yeniler').append(html);

            })




            $('.btnMessage').click(function () {
                var messageId = $(this).data('message-id');
                $.ajax({
                    method: "GET",
                    url: "/Message/BtnView/" + messageId

                }).done(function (d) {
                    $("#list").html(d);
                })



            })

        }

    }).done(function (d) {

        $("#yeniler").html(d.baslik);
    }).fail(function () {

    });
}, 1000));





