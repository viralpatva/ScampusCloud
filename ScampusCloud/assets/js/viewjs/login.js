jQuery(document).ready(function () {
    
});
function Passwordshowhide(el) {
    if ($(el).find('i').hasClass('fas fa-eye')) {
        var eye_slash_anchor = $(el).closest('div.form-group').find('.psw-slash-eyed');
        $(el).hide();
        $(eye_slash_anchor).show();
        $(el).closest('div.form-group').find('.Password').attr('type', 'text');
    }
    else if ($(el).find('i').hasClass('fas fa-eye-slash')) {
        var eye_anchor = $(el).closest('div.form-group').find('.psw-eyed');
        $(el).hide();
        $(eye_anchor).show();
        $(el).closest('div.form-group').find('.Password').attr('type', 'password');
    }
}
$(function () {
    if (localStorage.chkbx && localStorage.chkbx != '') {
        $('#remember_me').attr('checked', 'checked');
        $('#EmailId').val(localStorage.EmailId);
        $('#Password').val(localStorage.Password);
    }
    else {
        $('#remember_me').removeAttr('checked');
        $('#EmailId').val('');
        $('#Password').val('');
    }

    $('#remember_me').click(function () {
        
        if ($('#remember_me').is(':checked')) {
            // save username and password
            localStorage.EmailId = $('#EmailId').val();
            localStorage.Password = $('#Password').val();
            localStorage.chkbx = $('#remember_me').val();
        } else {
            localStorage.EmailId = '';
            localStorage.Password = '';
            localStorage.chkbx = '';
        }
    });});
