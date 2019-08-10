
    var count;
    var KacCount = 5;
    var pages;
    var counts
    function Count() {
        KacCount = document.getElementById("show").value;
    count = $('#HiddenCount').val();
    counts = Math.ceil(count / KacCount)
    pagination(count, KacCount);
};

    $(function () {
        count = $('#HiddenCount').val();
    counts = Math.ceil(count / KacCount)
    pagination(count, KacCount);

})
    function pagination(count, KacCount) {
        $('#pagination-demo').twbsPagination('destroy');
    var page = Math.ceil(count / KacCount);
        $('#pagination-demo').twbsPagination({
        totalPages: page,
    visiblePages: page,
            onPageClick: function (event, page) {
        getDataToDataTable(page, KacCount);

    }
});

}

    function getDataToDataTable(page, KacCount) {
        $.ajax({
            method: "GET",
            url: "/Home/JsonIndex?page=" + page + "&count=" + KacCount,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success:
                function (data0) {

                    $('#indexTable > tbody').html("");
                    $.each(data0, function (i, item) {
                        var html = '<tr><td>' + data0[i].baslik + '</td> <td>' + data0[i].icerik + '</td> <td>' + data0[i].kategori + '</td> <td>' + data0[i].tarih + '</td></tr >';
                        $('#indexTable  > tbody').append(html);

                    })
                }
        });
    }
