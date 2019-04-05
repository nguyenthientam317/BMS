var Payment = {
    Init: function () {

    },
    RegisterEvent: function () {
        $('#cod').off('Click').on('click', function () {
            $('#btnFinishCheckOut').removeClass("hidden");
            $('#paypal-button-container').addClass("hidden");

        });
        $('#online').off('Click').on('click', function () {
            $('#btnFinishCheckOut').addClass("hidden");
            $('#paypal-button-container').removeClass("hidden");
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
                                    total: 100,
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
                    });
            }
        }, '#paypal-button-container');
    }
}
Payment.Init()
Payment.RegisterEvent();