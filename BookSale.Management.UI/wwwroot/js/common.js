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