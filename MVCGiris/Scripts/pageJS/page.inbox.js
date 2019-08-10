
        $(function () {
            $(".btnViev").click(function (e) {
                var messageId = $(this).data('message-id');
                $.ajax({
                    method: "GET",
                    url: "/Message/BtnView/" + messageId

                }).done(function (d) {
                    $("#list").html(d);
                }).fail(function () {
                    alert("Hata oluştu..");
                }).always(function () {

                });
            });
        });
     