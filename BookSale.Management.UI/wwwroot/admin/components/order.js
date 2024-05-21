(function () {
    const elementName = "#tbl-order";
    const column = [
        {
            data: 'id', name: 'id', width: '30',
            render: function (key) {
                return `
                    <span data-key=${key}>
                        <a href="/admin/order/savedata?id=${key}" class="btn-edit">
                            <span class="mdi mdi-pen ri-24px"></span>
                        </a> &nbsp
                        <a href="/admin/report/exportpdforder?id=${key}" class="btn-export">
                            <span class="mdi mdi-file-export-outline ri-24px"></span>
                        </a>
                        <a href="#" class="btn-delete">
                            <span class="mdi mdi-close-thick ri-24px"></span>
                        </a>
                    </span>
                `;
            }
        },
        { data: 'fullName', name: 'fullName', autoWidth: true },
        { data: 'code', name: 'code', autoWidth: true },
        { data: 'paymentMethod', name: 'paymentMethod', width: "100px" },
        {
            data: 'createdOn',
            name: 'createdOn',
            width: "100px",
            render: function (data) {
                return `<div class="text-center">${moment(data).format("DD/MM/YYYY")}</div>`;
            }
        },
        {
            data: 'totalPrice', name: 'totalPrice', width: "100px",
            render: function (data) {
                return `<div class="text-center">${data.toLocaleString('vi-VN', {
                    style: 'currency',
                    currency: 'VND'
                })}</div>`;
            }
        },
        {
            data: 'status', name: 'status', width: "100px",
            render: function (data) {
                return `<div class="text-center">${formatStatus(data)}</div>`;
            }
        },
    ];
    const urlApi = "/admin/order/getbypagination";

    registerDatatable(elementName, column, urlApi);
    //$(document).on('click', '.btn-delete', function () {
    //    const key = $(this).closest('span').data('key');

    //    $.ajax({
    //        url: `/admin/book/delete/${key}`,
    //        dataType: 'json',
    //        method: 'POST',
    //        success: function (response) {
    //            if (!response) {
    //                showToaster("Error", "Delete failed");
    //                return;
    //            }

    //            $(elementName).DataTable().ajax.reload();
    //            showToaster("Success", "Delete successful");
    //        }
    //    })
    //});

    function formatStatus(data) {
        switch (data.toLowerCase()) {
            case 'new':
                return `<span class="badge bg-info">${data}</span>`;
            case 'processing':
                return `<span class="badge bg-warning">${data}</span>`;
            case 'cancel':
                return `<span class="badge bg-danger">${data}</span>`;
            default:
                return `<span class="badge bg-success">${data}</span>`;
        }
    }
})()