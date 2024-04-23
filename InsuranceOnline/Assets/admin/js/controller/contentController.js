﻿var content = {
    init: function () {
        content.registerEvents();
    },
    registerEvents: function () {
        $(document).on('click', 'a.btn-active', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Content/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response)
                    if (response.status == true)
                        btn.text('Kích hoạt');
                    else
                        btn.text('Khóa');
                }
            });
        });
    }
}
content.init();