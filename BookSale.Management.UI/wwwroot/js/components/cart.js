(function () {
    function initial() {
        const amountItems = $('#tbody-cart tr').length - 1;

        if (!amountItems) {
            $('#btn-checkout').addClass('disabled');
        }
        registerEvents();
    }

    function registerEvents() {
        $(document).on('blur', '.txt-quantity', function () {

            const seft = $(this);
            const parentTr = seft.closest('tr');

            const price = parseFloat(parentTr.find('.txt-price').text().replaceAll('₫', '').replaceAll('.', ''));
            const quantity = parseInt(seft.val());
            const total = price * quantity;

            parentTr.find('.txt-total').text(total.toLocaleString('vi-VN', {
                style: 'currency',
                currency: 'VND'
            }));

            calculateCartTotal();
        });

        function calculateCartTotal() {
            let totalCart = 0;
            const trs = $('#tbody-cart tr');

            for (var i = 0; i < trs.length; i++) {
                if (i === trs.length - 1) {
                    break;
                }

                const total = parseFloat($(trs[i]).find('.txt-total').text().replaceAll('₫', '').replaceAll('.', ''));

                totalCart += total;
            }

            $('#txt-total-cart').text(totalCart.toLocaleString('vi-VN', {
                style: 'currency',
                currency: 'VND'
            }));
        }

        $(document).on('click', '#btn-save-cart', function () {
            $.blockUI();

            const trs = $('#tbody-cart tr');

            let books = [];

            for (var i = 0; i < trs.length; i++) {
                if (i === trs.length - 1) {
                    break;
                }

                const quantity = parseFloat($(trs[i]).find('.txt-quantity').val());

                const code = $(trs[i]).data('code');

                books.push({ bookCode: code, quantity });
            }

            $.ajax({
                url: '/cart/update',
                method: 'POST',
                data: JSON.stringify(books),
                contentType: 'application/json',
                success: function (response) {
                    if (response) {
                        showToaster('Success', 'Save cart successful');
                    }
                    else {
                        showToaster('Error', 'Save cart failed');
                    }
                    $.unblockUI();
                },
                error: function () {
                    $.blockUI();
                }
            })
        });

        $(document).on('click', '.btn-delete-cart', function () {

            const seft = $(this);
            const code = seft.closest('tr').data('code');

            $.ajax({
                url: `/cart/delete?code=${encodeURIComponent(code)}`,
                method: 'POST',
                success: function (response) {
                    if (response) {
                        seft.closest('tr').remove();
                        
                        calculateCartTotal();

                        const amountItems = $('#tbody-cart tr').length - 1;
                        $('.cart-number').text(amountItems);
                        showToaster('Success', 'Delete successful');

                        if (!amountItems) {
                            $('#btn-checkout').addClass('disabled');
                        }
                    }
                    else {
                        showToaster('Error', 'Delete failed');
                    }
                    $.unblockUI();
                },
                error: function () {
                    $.blockUI();
                }
            })


        })
    }

    initial();


})();