(function () {
    function intial() {
        registerDatePicker();
        registerEvent();
    }


    function registerDatePicker() {
        function getCurrentDateFormatted() {
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, '0');
            const day = String(today.getDate()).padStart(2, '0');
            return `${year}-${month}-${day}`;
        };

        function getEndDateFormatted() {
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, '0');
            const day = String(today.getDate() - 7).padStart(2, '0');
            return `${year}-${month}-${day}`;
        };

        document.getElementById('end-date').value = getCurrentDateFormatted();
        document.getElementById('start-date').value = getEndDateFormatted();
    };

    function registerEvent() {
        $(document).on('click', '#btn-submit', function () {
            const { from, to, genreId, status } = getFilterData();
            location.href = `/admin/report?from=${from}&to=${to}&genreId=${genreId}&status=${status}`;
        });

        $(document).on('click', '#btn-export', function () {
            const { from, to, genreId, status } = getFilterData();
            location.href = `/admin/report/exportexcelorder?from=${from}&to=${to}&genreId=${genreId}&status=${status}`;
        });

        $(document).on('click', '#btn-reset', function () {
            $('#ddl-genre').val(0);
            $('#ddl-status').val(0);
        });
    };

    function getFilterData() {
        const from = $('#start-date').val();
        const to = $('#end-date').val();

        if (!from || !to) {
            showToaster('warning', 'Please select from and to');
            return;
        }

        const genreId = $('#ddl-genre').val();
        const status = $('#ddl-status').val();

        return { from, to, genreId, status };
    }

    intial();

}) ()