$(function () {
    $("#list").jqGrid({

        url: 'Data',
        contentType: "application/json",
        datatype: "json",
        jsonReader: {
            root: "Members",
            id: "ID",
            repeatitems: false
        },
        mtype: "GET",
        colNames: ["ID", "Name", "EMail Address", "Phone Number"],
        colModel: [
        { name: "ID", width: 80 },
        { name: "Name", width: 300, align: "center" },
        { name: "Mail", width: 350 },
        { name: "Phone", width: 300 },
    ],
        pager: "#pager",
        height: 'auto',
        rowNum: 2,
        rowList: [2, 6, 10],
        loadonce: true,
        sortname: "ID",
        sortorder: "desc",
        viewrecords: true,
        gridview: true,
        autoencode: true,
        caption: "Members Details"
    });
}).navGrid("#pager", { edit: false, add: false, del: false });