(function () {

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