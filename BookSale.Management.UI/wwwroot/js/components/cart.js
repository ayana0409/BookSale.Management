(function () {
    function initial() {
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
        });
    }

    initial();


})();