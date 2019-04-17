var Customer = {
    Init: function () {
        console.log($('#detailOrder').attr('data-confirm-deli'))
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
                if (response.status) {
                    var Template = $('#TemplateViewDetailModal').html();
                    var Html = '';
                    $.each(JSON.parse(response.data), function (i, item) {
                        Html += Mustache.render(Template, {
                            Name: item.Name,
                            Quantity: item.Quantity,
                            Price: item.Price,
                            TotalPrice: item.Price * item.Quantity
                        });
                    });
                    $('#RenderModalContent').html(Html);
                    $('#ModalViewDetail').modal('show');
                }
                else {
                    alert($('#detailOrder').attr('data-fail'));
                }
            }
        })
    },
    DeleteOrder: function (CrId) {
        var rep = confirm($('#detailOrder').attr('data-confirm-cancel'));
        if (rep == true) {
            $.ajax({
                url: Host+'User/DeleteOrder',
                data: {
                    id: CrId
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        alert($('#detailOrder').attr('data-cancel-success'));
                        location.reload();
                    }
                    else {
                        alert($('#detailOrder').attr('data-fail'));
                    }
                }
            })
        }
    },
    SetDelivering: function (CrId) {
        var rep = confirm($('#detailOrder').attr('data-confirm-deli'));
        if (rep == true) {
            $.ajax({
                url: Host + 'Admin/Orders/SetDelivering',
                data: {
                    id: CrId
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        alert($('#detailOrder').attr('data-success'));
                        location.reload();
                    }
                    else {
                        alert($('#detailOrder').attr('data-fail'));
                    }
                }
            })
        }
    },
    SetCompleted: function (CrId) {
        var rep = confirm($('#detailOrder').attr('data-confirm-complete'));
        if (rep == true) {
            $.ajax({
                url: Host + 'Admin/Orders/SetCompleted',
                data: {
                    id: CrId
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        alert($('#detailOrder').attr('data-success'));
                        location.reload();
                    }
                    else {
                        alert($('#detailOrder').attr('data-fail'));
                    }
                }
            })
        }
    }

   
}
var Host = 'http://localhost:60528/';
Customer.Init();
