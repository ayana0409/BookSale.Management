(function () {
    const elementName = "#tbl-account";
    const column = [
        {
            data: 'id', name: 'id', width: '30',
            render: function (key) {
                return `
                    <span data-key=${key}>
                        <a href="/admin/account/savedata?id=${key}">
                            <span class="mdi mdi-account-edit ri-24px"></span>
                        </a> &nbsp
                        <a href="#" class="btn-delete">
                            <span class="mdi mdi-account-remove ri-24px"></span>   
                        </a>
                    </span>
                `;
            }
        },
        { data: 'userName', name: 'userName', autoWidth: true },
        { data: 'fullName', name: 'fullName', autoWidth: true },
        { data: 'email', name: 'email', autoWidth: true },
        { data: 'phone', name: 'phone', autoWidth: true },
        { data: 'isActived', name: 'isActived', autoWidth: true },
    ];
    const urlApi = "/admin/account/getaccountpagination";

    registerDatatable(elementName, column, urlApi);
    $(document).on('click', '.btn-delete', function () {
        const key = $(this).closest('span').data('key');

        $.ajax({
            url: `/admin/account/delete/${key}`,
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