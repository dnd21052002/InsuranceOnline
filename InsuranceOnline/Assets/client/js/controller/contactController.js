var contact = {
    init: function () {
        contact.registerEvents();
    }
    , registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            var name = $('#txtName').val();
            var phone = $('#txtPhone').val();
            var address = $('#txtAddress').val();
            var email = $('#txtEmail').val();
            var content = $('#txtContent').val();
            var status = true;
            if (name == "") {
                $('#txtName').css('border-color', 'Red');
                status = false;
            }
            else {
                $('#txtName').css('border-color', '#ccc');
            }
            if (phone == "") {
                $('#txtPhone').css('border-color', 'Red');
                status = false;
            }
            else {
                $('#txtPhone').css('border-color', '#ccc');
            }
            if (address == "") {
                $('#txtAddress').css('border-color', 'Red');
                status = false;
            }
            else {
                $('#txtAddress').css('border-color', '#ccc');
            }
            if (email == "") {
                $('#txtEmail').css('border-color', 'Red');
                status = false;
            }
            else {
                $('#txtEmail').css('border-color', '#ccc');
            }
            if (content == "") {
                $('#txtContent').css('border-color', 'Red');
                status = false;
            }
            else {
                $('#txtContent').css('border-color', '#ccc');
            }

            $.ajax({
                url: '/Contact/Send',
                dataType: 'json',
                type: 'POST',
                data: {
                    name: name,
                    phone: phone,
                    address: address,
                    email: email,
                    content: content
                },
                success: function (response) {
                    if (response.status) {
                        window.alert('Gửi thành công');
                        contact.resetForm();
                    }
                }
            });
        })
    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtPhone').val('');
        $('#txtAddress').val('');
        $('#txtEmail').val('');
        $('#txtContent').val('');
    }
}
contact.init();