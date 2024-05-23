
$(document).ready(function () {
    var chartDom = document.getElementById('main');
    var myChart = echarts.init(chartDom);

    initial();

    function initial() {
        registerEvent();
        loadChartOrder();
    }

    function initialChartOrder(dataSource, genreId) {

        myChart.setOption({
            legend: {
                show: parseInt(genreId) ? false : true
            }
        });

        var option = {
            legend: {
                top: 'bottom'
            },
            tooltip: {
                trigger: 'item'
            },
            toolbox: {
                show: true,
                feature: {
                    mark: { show: true },
                    dataView: { show: true, readOnly: false },
                    restore: { show: true },
                    saveAsImage: { show: true }
                }
            },
            series: [
                {
                    name: 'Order Chart',
                    type: 'pie',
                    radius: [50, 250],
                    center: ['50%', '50%'],
                    roseType: 'area',
                    itemStyle: {
                        borderRadius: 8
                    },
                    data: dataSource
                }
            ]
        };

        myChart.setOption(option);
    }

    function loadChartOrder() {
        const genreId = $('#ddl-genre').val();

        $.ajax({
            url: `/admin/chart/getchartorderbygenre?genreId=${genreId}`,
            method: 'GET',
            success: function (response) {
                if (!response?.length) {
                    return;
                }

                const colors = ['#670e94', '#f0d917', '#00c3e3', '#f9f962', '#000000', '#e52b50'];

                const result = response.map((item, index) => {
                    return { ...item, itemStyle: { color: colors[index] } }
                });

                initialChartOrder(result, genreId);
            }
        })
    }

    function registerEvent() {
        $(document).on('change', '#ddl-genre', function () {
            loadChartOrder();
        });
    }
});
