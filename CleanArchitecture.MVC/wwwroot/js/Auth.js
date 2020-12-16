var auth = null;
var notyf = new Notyf();
function Auth() {
    var self = this;
    const $loginFrm = $('#loginFrm');

    this.initializeEvents = function initializeEvents() {

        $loginFrm.on('click', '.login', function (e) {
            e.preventDefault();
            let $auth = $(this);
            let auth = {
                Email: $("#Email").val(),
                Password: $("#PassWord").val()
            };
            self.login($auth, auth);
        });
    };

    this.login = function ($auth, auth) {
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: "POST",
            url: "/Auth/Login",
            data: {
                __RequestVerificationToken: token,
                model: auth
            },
            success: function (data) {                
                notyf.success('The user has been logged in');
                setTimeout(function () { window.location = "/Home/Index"; }, 1000);
            },
            error: function (xhr) {
                Swal.fire({
                    title: xhr.responseText,
                    icon: "error",
                    button: "Ok"
                });
            }
        });
    };

    this.documentReady = function () {
        self.initializeEvents();
    };
}

$(document).ready(function () {
    auth = new Auth();
    auth.documentReady();
});