$(function () {
    $("#list").jqGrid({

        url: 'Data',
        contentType: "application/json",
        datatype: "json",
        jsonReader: {
            root: "List",
            id: "No",
            repeatitems: false
        },
        mtype: "GET",
        colNames: ["S.No.", "Date", "Item", "Amount", "Paid By", "Paid To", "Pay Type", "Status", "Balance Amount"],
//colNames: ["S.No."],
        colModel: [
        { name: "No", width: 30 },
        { name: "Date", width: 70, align: "center" },
        { name: "Item", width: 150 },
        { name: "Amount", width: 100 },
        { name: "PaidBy", width: 200 },
        { name: "PaidTo", width: 200 },
        { name: "PayType", width: 100 },
        { name: "Status", width: 100 },
        { name: "BalanceAmount", width: 100 },
    ],
        pager: "#pager",
        height: 'auto',
        rowNum: 5,
        rowList: [5, 10, 15],
        loadonce: true,
        sortname: "No",
        sortorder: "desc",
        viewrecords: true,
        gridview: true,
        autoencode: true,
        caption: "Expenditure Details"
    });
}).navGrid("#pager", { edit: false, add: false, del: false });