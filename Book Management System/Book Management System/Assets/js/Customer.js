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
                        console.log(item.Name);
                        console.log(item.Book.Title);
                        Html += Mustache.render(Template, {
                            Name: item.Book.Title,
                            Quantity: item.Quantity,
                            Price: item.Book.Price,
                            TotalPrice: item.Book.Price * item.Quantity
                        });
                    });
                    $('#RenderModalContent').html(Html);
                    $('#ModalViewDetail').modal('show');
                }
            }
        })
    },
    DeleteOrder: function (CrId) {
        var rep = confirm('Are you sure to cancel this order ?');
        if (rep == true) {
            $.ajax({
                url: Host + 'User/DeleteOrder',
                data: {
                    idCart: CrId
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
    }
   
}
var Host = 'http://localhost:60528/';
