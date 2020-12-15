// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function(){
    var PlaceHolderHereElement = $('#PlaceHolderHere');
    $('button[data-toggle="edit-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url)
        $.get(decodedUrl).done(function (data) {
      
            PlaceHolderHereElement.html(data);
            PlaceHolderHereElement.find('.modal').modal('show');
        })
    })
    PlaceHolderHereElement.on('click', '[data-save="modalEdit"]', function (event) {
        event.preventDefault();
        var form = $(this).parent('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();

        $.post(actionUrl, sendData).done(function (data)
        {
            PlaceHolderHereElement.find('.modal').modal('hide');

        }
            )
    })
    $('#createModal').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url)
        $.get(decodedUrl).done(function (data) {
            PlaceHolderHereElement.html(data);
            PlaceHolderHereElement.find('.modal').modal('show');
        })
    })

    $('#answerModal').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url)
        $.get(decodedUrl).done(function (data) {
            PlaceHolderHereElement.html(data);
            PlaceHolderHereElement.find('.modal').modal('show');
        })
    })

    PlaceHolderHereElement.on('click', '[data-save="modalCreate"]', function (event) {
        event.preventDefault();
        var form = $(this).parent('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolderHereElement.find('.modal').modal('hide');

        }
        )
    })


    var OrderDetailsElement = $('#OrderDetailsHere');
    $('button[data-toggle="orderDetails"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url)
        $.get(decodedUrl).done(function (data) {
            OrderDetailsElement.html(data);
            OrderDetailsElement.find('.modal').modal('show');
        })
    })

    OrderDetailsElement.on('click', '[data-save="editUser"]', function (event) {
        event.preventDefault();
        var form = $(this).parent('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolderHereElement.find('.modal').modal('hide');

        }
        )
    })


})
