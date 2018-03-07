$(document).ready(function () {
    $('form').submit(function (e) {
        var VAL = $.trim($('#tbEmail').val());

        var email = new RegExp('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+([.][a-zA-Z]{2,4}){1,2}$');

        if (!email.test(VAL)) {
            alert('Enter a Valid EMail address, Please');
            e.preventDefault();
        }
        var VAL = $.trim($('#tbPhone').val());

        var phone = new RegExp('^[0-9]{6,15}$');

        if (!phone.test(VAL)) {
            alert('Enter a Valid Phone Number, Please');
            e.preventDefault();
        }
    });
});