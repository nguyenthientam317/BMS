var Check = {

    Init: function () {
        var IdBook = Check.GetIdBookByCurrentUrl();
        Check.LoadComment(IdBook);
    },
    RegisterEvent: function () {
        $('#btnComment').click(function () {
            Check.AddComment();
        });
    },
    ResetValueEmptyForInput: function (arrTextBox) {
        $.each(arrTextBox, function (index, value) {
            $(value).val('');
        });
    },
    GetIdBookByCurrentUrl: function () {
        var SearchUrl = $(location).attr('search'); // lấy url ngay dấu ?
        for (var i = 0; i < SearchUrl.length; i++) {
            if (SearchUrl[i] == '=') {
                return SearchUrl.substring(++i);
            }
        }
    },
    AddComment: function () {
        var IdBook = Check.GetIdBookByCurrentUrl();
        var Cmt = [];
        var Name = $('#Commenter').val();
        var Contents = $('#Content').val();
        if (Name != '' && Contents != '') {
            // Cmt = [Name, Contents];
            Cmt.push({
                CommenterName: Name,
                Content: Contents,
                idBook: IdBook
            }); // thành mảng list

            $.ajax({
                url: Host + 'Home/AddComment',
                data:
                {
                    comment: JSON.stringify(Cmt)
                },
                type: 'POST',
                success: function (response) {
                    if (response.status) {
                        alert('Add comment successfully');
                        if ($('#RenderComment').length) {
                            Check.LoadComment(IdBook);
                        }
                        else {
                            location.reload();
                        }
                        var ArrInput = ['#Content'];
                        Check.ResetValueEmptyForInput(ArrInput);

                    }
                    else {
                        alert('Add comment fail');
                    }
                }
            });
        }
        else {
            alert('Name & Content can not be null');
        }
    },
      
    LoadComment: function (idBook) {
        $.ajax({
            url: Host+'Home/LoadCommentByIdBook',
            data:
            {
                idBook : idBook
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var Template = $('#TemplateComment').html();
                    var Html = ''
                    $.each(JSON.parse(response.data), function (i, item) {
                        Html += Mustache.render(Template, {
                            Date: item.CreateDate,
                            Name: item.CommenterName,
                            Content: item.Content
                        });

                    });
                    $('#RenderComment').html(Html);
                }
                else {
                    alert('Can not load comments')
                }
            }

        });
    }


}
var Host = 'http://localhost:60528/';
Check.Init();
Check.RegisterEvent();
