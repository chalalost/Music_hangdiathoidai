var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            var email = $('#txtEmail').val();
            var content = $('#txtContent').val();

            $.ajax({
                url: '/Main/Send',
                type: 'POST',
                dataType: 'json',
                data: {
                    email: email,
                    content: content
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert('Gửi thành công');
                        contact.resetForm();
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#txtEmail').val('');
        $('#txtContent').val('');
    }
}
contact.init();