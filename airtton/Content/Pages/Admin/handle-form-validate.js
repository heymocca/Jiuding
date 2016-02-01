$(document).ready(function () {

    var form = $('#newsForm');
    var error1 = $('.alert-danger', form);
    var success1 = $('.alert-success', form);
   
    form.validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block help-block-error', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",
        rules: {
            Title: {
                required: true
            }
        },

        invalidHandler: function (event, validator) { //display error alert on form submit              
            success1.hide();
            error1.show();

            Utils.scrollTo(form, -20);
        },

        submitHandler: function (form) {
            form.submit();
        }
    });

})



