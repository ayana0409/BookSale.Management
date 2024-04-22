(function () {
    const elementName = "#tbl-book";
    const column = [
        {
            data: 'id', name: 'id', width: '30',
            render: function (key) {
                return `
                    <span data-key=${key}>
                        <a href="#" class="btn-edit">
                            <svg role="presentation" viewBox="0 0 24 24" style="height: 1.5rem; width: 1.5rem;"><title>edit</title><path d="M20.71,7.04C20.37,7.38 20.04,7.71 20.03,8.04C20,8.36 20.34,8.69 20.66,9C21.14,9.5 21.61,9.95 21.59,10.44C21.57,10.93 21.06,11.44 20.55,11.94L16.42,16.08L15,14.66L19.25,10.42L18.29,9.46L16.87,10.87L13.12,7.12L16.96,3.29C17.35,2.9 18,2.9 18.37,3.29L20.71,5.63C21.1,6 21.1,6.65 20.71,7.04M3,17.25L12.56,7.68L16.31,11.43L6.75,21H3V17.25Z"style="fill: currentcolor;"></path></svg>
                        </a> &nbsp
                        <a href="#" class="btn-delete">
                            <svg role="presentation" viewBox="0 0 24 24" style="height: 1.5rem; width: 1.5rem;"><title>account-remove</title><path d="M20 6.91L17.09 4L12 9.09L6.91 4L4 6.91L9.09 12L4 17.09L6.91 20L12 14.91L17.09 20L20 17.09L14.91 12L20 6.91Z" style="fill: currentcolor;"></path></svg>
                        </a>
                    </span>
                `;
            }
        },
        { data: 'genreName', name: 'genreName', autoWidth: true },
        { data: 'code', name: 'code', autoWidth: true },
        { data: 'title', name: 'title', autoWidth: true },
        { data: 'available', name: 'available', autoWidth: true },
        { data: 'price', name: 'price', autoWidth: true },
        { data: 'publisher', name: 'publisher', autoWidth: true },
        { data: 'author', name: 'author', autoWidth: true },
        { data: 'createdOn', name: 'createdOn', autoWidth: true },
    ];
    const urlApi = "/admin/book/getbookpagination";

    registerDatatable(elementName, column, urlApi);
//    $(document).on('click', '.btn-delete', function () {
//        const key = $(this).closest('span').data('key');

//        $.ajax({
//            url: `/admin/account/delete/${key}`,
//            dataType: 'json',
//            method: 'POST',
//            success: function (response) {
//                if (!response) {
//                    showToaster("Error", "Delete failed");
//                    return;
//                }

//                $(elementName).DataTable().ajax.reload();
//                showToaster("Success", "Delete successful");
//            }
//        })

//    });
})()