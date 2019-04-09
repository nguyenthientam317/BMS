var TotalPrice = 0
var Payment = {
    Init: function () {
        TotalPrice = Payment.GetTotalPrice();
        console.log(TotalPrice);
    },
    RegisterEvent: function () {
        $('#cod').off('Click').on('click', function () {
            $('#btnFinishCheckOut').removeClass("hidden");
            $('#paypal-button-container').addClass("hidden");

        });
        $('#paypal').off('Click').on('click', function () {
            $('#btnFinishCheckOut').addClass("hidden");
            $('#paypal-button-container').removeClass("hidden");
        });
        $('#btnFinishCheckOut').click(function () {
             Payment.CompletePayment();
        });


        
        paypal.Button.render({
            // Set your environment
            env: 'sandbox', // sandbox | production

            // Specify the style of the button
            style: {
                layout: 'vertical',  // horizontal | vertical
                size: 'medium',    // medium | large | responsive
                shape: 'rect',      // pill | rect
                color: 'gold'       // gold | blue | silver | white | black
            },

            // Specify allowed and disallowed funding sources
            //
            // Options:
            // - paypal.FUNDING.CARD
            // - paypal.FUNDING.CREDIT
            // - paypal.FUNDING.ELV
            funding: {
                allowed: [
                    paypal.FUNDING.CARD,
                    paypal.FUNDING.CREDIT
                ],
                disallowed: []
            },

            // Enable Pay Now checkout flow (optional)
            commit: true,

            // PayPal Client IDs - replace with your own
            // Create a PayPal app: https://developer.paypal.com/developer/applications/create
            client: {
                sandbox: 'AfJuN_xqOL2GLmkZ9-fyNpjdwG-02cUURajbEDNXGxL9s73AoUayyRa0Z_nqLYtRjCUYSnEXcr3wHfuI',
                production: '<insert production client id>'
            },

            payment: function (data, actions) {
                return actions.payment.create({
                    payment: {
                        transactions: [
                            {
                                amount: {
                                    total: TotalPrice,
                                    currency: 'USD'
                                }
                            }
                        ]
                    }
                });
            },

            onAuthorize: function (data, actions) {
                return actions.payment.execute()
                    .then(function () {
                        window.alert('Payment Complete!');
                        Payment.CompletePayment();
                    });
            }
        }, '#paypal-button-container');
    },
    GetTotalPrice: function () {
        return $('#IdTotalPrice').text();
    },
    CompletePayment: function () {
        var idCart = $('#IdCart').text();
        var paymentMethod = $('#paypal').val();
        var checkPay = $('#cod').prop('checked');
        if (checkPay == true) {
            paymentMethod = $('#cod').val();
        }
        console.log(idCart)
        console.log(paymentMethod)

        var Order = { IdCard: idCart, MethodPayment: paymentMethod } // 1 đối tượng
        $.ajax({
            url: Host+'Payment/CompletePayment',
            data: {
                orderJson: JSON.stringify(Order)
            },
            type: 'POST',
            success: function (response) {
                if (response.status) {
                    alert('Payment Successfully');
                    window.location.href = "/";
                }
                else {
                    alert('Payment Fail');
                }
            }

        })

        
    }
}
var Host = 'http://localhost:60528/';
Payment.Init()
Payment.RegisterEvent();