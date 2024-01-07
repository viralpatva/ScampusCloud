jQuery(document).ready(function () {
    
});
function Passwordshowhide(el) {
    debugger;
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