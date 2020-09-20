﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


jQueryAjaxDelete = url => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                processData: false,
                contentType: URL,
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
}


