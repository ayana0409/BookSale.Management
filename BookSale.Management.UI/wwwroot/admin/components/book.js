(function () {
    const elementName = "#tbl-book";
    const column = [
        {
            data: 'id', name: 'id', width: '30',
            render: function (key) {
                return `
                    <span data-key=${key}>
                        <a href="/admin/book/savedata?id=${key}" class="btn-edit">
                            <span class="mdi mdi-pen ri-24px"></span>
                        </a> &nbsp
                        <a href="#" class="btn-delete">
                            <span class="mdi mdi-close-thick ri-24px"></span>
                        </a>
                    </span>
                `;
            }
        },
        { data: 'genreName', name: 'genreName', autoWidth: true },
        { data: 'code', name: 'code', autoWidth: true },
        { data: 'title', name: 'title', autoWidth: true },
        { data: 'available', name: 'available', autoWidth: true },
        {
            data: 'price', name: 'price', autoWidth: true,
            render: function (data) {
                return `<div class="text-center">${data.toLocaleString('vi-VN', {
                    style: 'currency',
                    currency: 'VND'
                })}</div>`;
            }
        },
        { data: 'publisher', name: 'publisher', autoWidth: true },
        { data: 'author', name: 'author', autoWidth: true },
        {
            data: 'createdOn',
            name: 'createdOn',
            autoWidth: true,
            render: function (data) {
                return `<div class="text-center">${moment(data).format("DD/MM/YYYY")}</div>`;
            }
        },
    ];
    const urlApi = "/admin/book/getbookpagination";

    registerDatatable(elementName, column, urlApi);
    $(document).on('click', '.btn-delete', function () {
        const key = $(this).closest('span').data('key');

        $.ajax({
            url: `/admin/book/delete/${key}`,
            dataType: 'json',
            method: 'POST',
            success: function (response) {
                if (!response) {
                    showToaster("Error", "Delete failed");
                    return;
                }

                $(elementName).DataTable().ajax.reload();
                showToaster("Success", "Delete successful");
            }
        })
    });
})()