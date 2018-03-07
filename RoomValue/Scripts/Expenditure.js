$(document).ready(function () {
    $('input[type=radio][name=rbMemberSelection]').change(function () {
        if ($('input[type=radio]:checked').val() == "All") {
            $('.checkBoxMemberList').attr('disabled', 'disabled');
            $('.checkBoxMemberList').removeAttr('checked');
        }
        else {
            $('.checkBoxMemberList').removeAttr('disabled');
            $('.checkBoxMemberList').removeAttr('checked');
        }
    });
    $('form').submit(function (e) {
        if ($.trim($('#tbItem').val()) == "") {
            alert("Enter Item, Please");
            e.preventDefault();
            return;
        }
        if ($.trim($('#tbTotalAmount').val()) == "" || isNaN($.trim($('#tbTotalAmount').val()))) {
            alert("Enter Valid Total Amount, Please");
            e.preventDefault();
            return;
        }
        if ($('input[type=radio]:checked').val() == "SomeWithoutMe" && isNaN($('.checkBoxMemberList:checked').first().val())) {
            alert("Select At least one Member, Please");
            e.preventDefault();
            return;
        }
    });
});