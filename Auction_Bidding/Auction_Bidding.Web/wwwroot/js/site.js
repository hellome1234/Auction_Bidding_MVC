// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

jQueryAjaxDelete = url => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                processData: false,
                contentType: false,
                success: function (res) {
                    if (res.isSuccess) {
                        $('#view-all').html(res.html);
                    } else {
                        $('#view-all').html(res.html);
                        alert("Coudn't delete the auction");
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
};

jQueryUploadAuction = form => {

    try {
        $.ajax({
            type: 'POST',
            url: "/Auction/UpdateAuction",
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else {
                    $('#form-modal .modal-body').html(res.html);
                }
            },
            error: function (err) {
                console.log(err)
            }
        })

        return false;
    }
    catch (ex) {
        console.log(ex);
    }
};

showInPopup = url => {
    $.ajax({
        type: 'GET',
        url: url,
        processData: false,
        contentType: false,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html("Update Auction");
            $('#form-modal').modal('show');
        }
    })
};
