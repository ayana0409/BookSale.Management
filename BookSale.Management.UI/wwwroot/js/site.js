// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on('click', '.btn-add-cart', function () {
    $.blockUI();

    const code = $(this).data('code');

    $.ajax({
        url: '/cart/add',
        method: 'POST',
        data: { bookCode: code, quantity: 1},
        success: function (count) {

            if (count === -1) {
                showToaster("Error", 'Add cart failed');
                $.unblockUI();
            }
            else {
                $('.cart-number').text(count);
                showToaster("Success", 'Add cart successful');
            }
            $.unblockUI();

        }
    });
});

