var pagination = {
    show: function (id) {
        $(id).DataTable({
            search: {
                return: true,
            },
            // scrollY: height,
            scrollX: true,
            scrollCollapse: true,
            paging: true,
            pageLength: 10,
            lengthMenu: [[2, 5, 10, 15, 20, -1], [2, 5, 10, 15, 20, 'All']],
            order: false,
        });
    },
    showAll: function (id) {
        $(id).DataTable({
            //  "scrollY": (Number($(windows).height() / 4) + "px").toString(),
            "scrollY": "400px",
            "scrollCollapse": true,
            "paging": false,
            "fixedHeader": {
                header: true,
                footer: true
            }
        });
    },
    showClick: function (id) {
        var table = $(id).DataTable({
            //  "scrollY": (Number($(windows).height() / 4) + "px").toString(),
            "scrollY": "300px",
            "scrollCollapse": true,
            "paging": false,
            "fixedHeader": {
                header: true,
                footer: true
            },

        });
        $(id + ' tbody').on('click', 'tr', function () {
            // console.log(index)
            //  console.log(e.target.value)
            console.log(table.row(this).data().find("#pID").text());
            //alert(table.row(this).find("#pID").text());
            //console.log(table.row(this).find("#pID").val())
        })
    },

};