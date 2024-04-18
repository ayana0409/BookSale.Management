(function () {
    const elementName = "#tbl-account";
    const column = [
        {
            data: 'id', name: 'id', width: '30',
            render: function (key) {
                return `
                    <span data-key=${key}>
                        <a href="/admin/account/savedata?id=${key}">
                            <svg role="presentation" viewBox="0 0 24 24" style="height: 1.5rem; width: 1.5rem;"><title>account-edit</title><path d="M21.7,13.35L20.7,14.35L18.65,12.3L19.65,11.3C19.86,11.09 20.21,11.09 20.42,11.3L21.7,12.58C21.91,12.79 21.91,13.14 21.7,13.35M12,18.94L18.06,12.88L20.11,14.93L14.06,21H12V18.94M12,14C7.58,14 4,15.79 4,18V20H10V18.11L14,14.11C13.34,14.03 12.67,14 12,14M12,4A4,4 0 0,0 8,8A4,4 0 0,0 12,12A4,4 0 0,0 16,8A4,4 0 0,0 12,4Z"style="fill: currentcolor;"></path></svg>
                        </a> &nbsp
                        <a href="#" class="btn-delete">
                            <svg role="presentation" viewBox="0 0 24 24" style="height: 1.5rem; width: 1.5rem;"><title>account-remove</title><path d="M15,14C17.67,14 23,15.33 23,18V20H7V18C7,15.33 12.33,14 15,14M15,12A4,4 0 0,1 11,8A4,4 0 0,1 15,4A4,4 0 0,1 19,8A4,4 0 0,1 15,12M5,9.59L7.12,7.46L8.54,8.88L6.41,11L8.54,13.12L7.12,14.54L5,12.41L2.88,14.54L1.46,13.12L3.59,11L1.46,8.88L2.88,7.46L5,9.59Z" style="fill: currentcolor;"></path></svg>
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