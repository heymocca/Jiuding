$(document).ready(function () {
    debugger;

    var new_content = $('#new_content').val();
    var new_title = $('#new_title').val();
    var url = $(this).data('url');

    $('#group_intro_submit').on('click', function () {


        var data = {
            title: new_title,
            content: new_content,
            id: id
        };

        Utils.ajaxCallJson(url, data, 'json', false, function (e) {
        })

    })
})