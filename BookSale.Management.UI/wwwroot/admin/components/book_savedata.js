(function () {

    $(document).on('click', '#btn-generate-code', function () {
        $.blockUI();

        $.ajax({
            url: '/admin/book/genaratecodebook',
            success: function (response) {
                $('#Code').val(response);

                $.unblockUI();
            },
            error: function () {
                showToaster('error', 'Generate code failed.');
                $.unblockUI();
            }
        })
    })

    const imgAvatar = document.getElementById('img-upload');

    document.getElementById('upload').onchange = function () {
        const input = this.files[0];

        if (input) {
            imgAvatar.src = URL.createObjectURL(input);
        }
    }

    imgAvatar.onerror = function () {
        onErrorImage();
    }

    function onErrorImage() {
        imgAvatar.src = "../assets/img/avatars/no-image.jpg";
        imgAvatar.alt = "no image";
    }
}

)()