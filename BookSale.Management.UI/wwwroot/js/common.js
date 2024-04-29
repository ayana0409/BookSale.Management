function showToaster(type, text, timeOut = 5000) {
    $.toast({
        heading: type,
        text: text,
        position: 'bottom-right',
        icon: type === 'Information' ? 'info' : type.toLowerCase(),
        hideAfter: timeOut
    })
}

function mapObjectToControlView(modelView) {
    if (typeof modelView !== 'object') {
        return;
    }

    for (var property in modelView) {
        if (modelView.hasOwnProperty(property)) {

            const [firstCharacter, ...resChar] = property;

            const capotalText = `${firstCharacter.toLocaleUpperCase()}${resChar.join('')}`

            $(`#${capotalText}`).val(modelView[property]);
        }

    }
}

(function () {

    $.blockUI.defaults.message = `<div class="spinner-border spinner-border-lg text-primary" role="status">
                                        <span class="visually-hidden">Loading...</span>
                                    </div>`
    $.blockUI.defaults.css = {
        padding: 0,
        margin: 0,
        width: '30%',
        top: '40%',
        left: '35%',
        textAlign: 'center',
        color: '#fff',
        border: 'none',
        backgroundColor: 'none',
        cursor: 'wait'
    }
})();