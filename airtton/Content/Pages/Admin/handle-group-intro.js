$(document).ready(function () {
  
    var title = $('#group_intro_title').text();
    var content = $('#group_intro_content').text();
    var id = $('#group_intro_id').val();

    $('#group_intro_edit_btn').on('click', function () {

        var url = $(this).data('url'),          //get server action url
            echo = Utils.getRandomNumber();     //get an random number to make sure it receive the correct response, prevent multiple clicking event
            
        var data = {
            title: title,
            content: content,
            id: id,
            echo : echo                         //JSON data, pass to the server side action with the url above
        };

        Utils.ajaxCallJson(url, data, 'json', false, function (e) {       //call Ajax with Json data, e is the Response result
            if (e.result == "success") {
                if (e.echo == data.echo) {                              
                    $('#group_intro_portlet').empty().html(e.html);         
                    //e.echo == data.echo make sure the click and response are for same request
                    //for example: if a user click edit button 10 times in 5 second, the server cannot response that quick, so how the server know which response is to which request? the random number echo solve the problem here
                    //if ajax request fail, show error message
                }                                                 
                else {
                    Console.log(data);
                }

                $('#group_intro_submit').on('click', function (e) {

                    e.preventDefault();
                    var new_title = $('#new_title').val();
                    var new_content = $('#new_content').val();

                    var url = $(this).data('url');
                    var echo = Utils.getRandomNumber();

                    var data = {
                        title: new_title,
                        content: new_content,
                        id: id,
                        echo: echo
                    };

                    Utils.ajaxCallJson(url, data, 'json', false, function (result) {

                        if (result.result == "success") {
                            if (result.echo == data.echo) {
                                $('#group_intro_portlet').empty().html(result.html);
                            }
                        }
                        else {
                            console.log(result.message);
                        }
                    });
                });                                                       
            } else {
                console.log(e.message);
            }
        });

    });

});