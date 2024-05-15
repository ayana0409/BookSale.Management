(function () {

    function initial() {

        $(document).ready(function () {
            registerPlugin();
            registerPaypal();
            resisterEvent();
        });
    }

    function registerPlugin() {
        $('.address').tooltip({
            html: true,
            trigger: 'hover focus',
            template: '<div class="tooltip" role="tooltip"><div class="tooltip-inner bg-info"></div></div>',
        });
    }

    function registerPaypal() {
        paypal_sdk.Buttons({
            createOrder: function (data, action) {
                return action.order.create({
                    "purchase_units": [
                        {
                            "amount": {
                                "currency_code": "USD",
                                "value": "1"
                            },
                            "item": []
                        }
                    ]
                })
            },
            onApprove: function (data, action) {
                return action.order.capture().then(function (response) {
                    console.log(response);

                    if (response?.status === "COMPLETED") {
                        $('#OrderId').val(response.id);
                    }
                })
            },
            style: {
                layout: 'vertical',
                color: 'blue',
                shape: 'rect',
                label: 'paypal'
            }
        }).render('#paypal-button-container');
    }

    function resisterEvent() {
        $(document).on('click', '#btn-change-address', function () {
            $('#address-modal').modal('show');
        });
    }



    initial();
})()
