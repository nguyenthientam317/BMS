var Customer = {
    Init: function(){

    },
    RegisterEvent() {

    },

    ViewDetail: function (CurrentId) {
        $.ajax({
            url: Host + 'User/ViewDetail',
            data: {
                id: CurrentId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {

            };
        })
    }
}
var Host = 'http://localhost:60528/';