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
                    alert('Fail to load detail order');
                }
            }
        })
    },
    DeleteOrder: function (CrId) {
        var rep = confirm('Are you sure to cancel this order ?');
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
                        alert('Cancel successfully');
                        location.reload();
                    }
                    else {
                        alert('Fail');
                    }
                }
            })
        }
    },
    SetDelivering: function (CrId) {
        var rep = confirm('Are you sure to change status to Delivering ?');
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
                        alert('Change status successfully');
                        location.reload();
                    }
                    else {
                        alert('Fail');
                    }
                }
            })
        }
    },
    SetCompleted: function (CrId) {
        var rep = confirm('Are you sure to change status to Completed ?');
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
                        alert('Change status successfully');
                        location.reload();
                    }
                    else {
                        alert('Fail');
                    }
                }
            })
        }
    }

   
}
var Host = 'http://localhost:60528/';
