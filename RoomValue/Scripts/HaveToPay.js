$(document).ready(function () {
    $('form').submit(function (e) {
        if (isNaN($('#tbAmount').val())) {
            alert("Enter a Valid Amount, Please");
            e.preventDefault();
        }
        else if ($('#dListFor').val() != "Combined") {
            var res = $('#dListFor option:selected').text().split(" - Rs. ");
            if (parseFloat(res[res.length - 1]) < parseFloat($('#tbAmount').val())) {
                alert("Amount Should not higher than Cost!");
                e.preventDefault();
            }
        }
    });
    $('#dListTo').change(function () {
        $('#tbAmount').attr("disabled", "disabled");
        $('#tbAmount').val("");
        $('#btnSubmit').attr("disabled", "disabled");
        if ($(this).val() != "0") {
            $.ajax({
                type: 'GET',
                url: '/User/GetPayForListXML',
                data: { ID: $('#dListTo').val() },
                success: function (objectList) {
                    xmlDoc = $.parseXML(objectList),
                  $xml = $(xmlDoc),
                  $title = $xml.find("List");
                    $('#dListFor').removeAttr("disabled");
                    $('#dListFor').html("<option value=\"0\">Select an Item</option>");
                    $('#dListFor').append("<option value=\"Combined\">Combined Payment</option>");
                    $title.find('HaveToPay').each(function () {
                        var ExpenditureID = $(this).find('ExpenditureID').text();
                        var Item = $(this).find('Item').text();
                        var Amount = $(this).find('Amount').text();
                        $('#dListFor').append("<option value=\"" + ExpenditureID + "\">" + Item + " - Rs. " + Amount + "</option>");

                    });
                },
                error: function (err, data) {
                    alert("Error " + err.responseText);
                }
            });
        }
        else {
            $('#dListFor').attr("disabled", "disabled");
            $('#dListFor').html("<option value=\"0\">Select a Member</option>");
        }
    });


    $('#dListFor').change(function () {
        if ($(this).val() == "0") {
            $('#tbAmount').attr("disabled", "disabled");
            $('#btnSubmit').attr("disabled", "disabled");
            $('#tbAmount').val("");
        }
        else if ($(this).val() == "Combined") {
            $('#btnSubmit').removeAttr("disabled");
            $('#tbAmount').attr("disabled", "disabled");
            var totalCombinedAmount = 0.0;
            $('#dListFor option').each(function () {
                if ($(this).val() != "0" && $(this).val() != "Combined") {
                    var array = $(this).text().split(" - Rs. ");
                    totalCombinedAmount = totalCombinedAmount + parseFloat(array[array.length - 1]);
                }

            });
            $('#tbAmount').val(totalCombinedAmount);
        }
        else {
            str = $('#dListFor option:selected').text();
            var res = str.split(" - Rs. ");
            $('#tbAmount').val(res[res.length - 1]);
            $('#tbAmount').removeAttr("disabled");
            $('#btnSubmit').removeAttr("disabled");
        }
    });

});
