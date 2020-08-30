// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function(){
    var PlaceHolderHereElement = $('#PlaceHolderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url)
        console.log(url)
        console.log(decodedUrl)
        $.get(decodedUrl).done(function (data) {
            PlaceHolderHereElement.html(data);
            PlaceHolderHereElement.find('.modal').modal('show');
        })
    })
    PlaceHolderHereElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parent('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        console.log(sendData)
        console.log(actionUrl)

        $.post(actionUrl, sendData).done(function (data)
        {
            PlaceHolderHereElement.find('.modal').modal('hide');

        }
            )
    })
})