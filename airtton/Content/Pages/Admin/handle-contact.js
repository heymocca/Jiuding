/// <reference path="~/Scripts/jquery-2.2.0.min.js" />
/// <reference path="~/Scripts/jquery-2.2.0.intellisense.js">

$(document).ready(function () {

    var address = $('#contact_address').text();
    var phone = $('#contact_phone').text();
    var fax = $('#contact_fax').text();
    var email = $('#contact_email').text();
    var link = $('#contact_link').text();
    var id = $('#contact_id').val();

    $('#contact_edit_btn').on('click', function () {

        var url = $(this).data('url'),
            echo = Utils.getRandomNumber();

        var data = {
            address: address,
            phone: phone,
            fax: fax,
            email: email,
            link: link,
            id: id,
            echo: echo
        };

        Utils.ajaxCallJson(url, data, 'json', false, function (e) {
            if (e.result == "success") {
                if (e.echo == data.echo) {
                    $('#contact_portlet').empty().html(e.html);
                } else {
                    Console.log(data);
                }

                $('#contact_submit').on('click', function (e) {
                   
                    e.preventDefault();
                    var new_address = $('#new_contact_address').val();
                    var new_phone = $('#new_contact_phone').val();
                    var new_fax = $('#new_contact_fax').val();
                    var new_email = $('#new_contact_email').val();
                    var new_link = $('#new_contact_link').val();
                    var url = $(this).data('url');
                    var echo = Utils.getRandomNumber();

                    var data = {
                        address: new_address,
                        phone: new_phone,
                        fax: new_fax,
                        email: new_email,
                        link: new_link,
                        id: id,
                        echo: echo
                    };

                    Utils.ajaxCallJson(url, data, 'json', false, function (result) {
                        if (result.result == "success") {
                            if (result.echo == data.echo) {
                                $('#contact_portlet').empty().html(result.html);
                            }
                        }
                        else {
                            console.log(result.message)
                        }
                    });
                });


            } else {
                Console.log(e.message);
            }
        });

    });

});