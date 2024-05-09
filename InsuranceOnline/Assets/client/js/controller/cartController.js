var cart = {
    init: function () {
        cart.regEvents();
    }
    , regEvents: function () {

        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productID = $(this).data('id');
            $.ajax({
                url: '/Cart/Add',
                data: {
                    productId: productID,
                },
                type: 'POST',
                dataType: 'json',
                success: function (res) {
                    if (res.status == true) {
                        alert('Thêm sản phẩm thành công');
                    }
                }
            })
        });

        $('#btnContinue').off('click').on('click',function () {
            window.location.href = "/";
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Cart/DeleteItem',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });
        $('#btn_paynow').off('click').on('click', function () {
            $('#payment_link').show();
            $(this).hide();
        });

    }
}
cart.init();
