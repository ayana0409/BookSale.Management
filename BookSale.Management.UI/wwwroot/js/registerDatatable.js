function registerDatatable(elementName, columns, urlApi) {
    $(elementName).DataTable({
        scrollX: true,
        processing: true,
        serverSide: true,
        columns: columns,
        ajax: {
            url: urlApi,
            type: 'POST',
            dataType: 'json'
        }
    });
}