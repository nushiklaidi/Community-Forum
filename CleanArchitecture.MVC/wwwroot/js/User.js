var users = null;
var notyf = new Notyf();
function Users() {
    var self = this;
    const $addUserFrm = $('#addUserFrm');

    this.initializeDataTable = function () {

        let = tableOptions = {
            pageLength: 10
        };

        self.table = $('#tableResult').DataTable(tableOptions);
    };

    this.initializeEvents = function initializeEvents () {
        let $Results = $("#tableResult");

        $Results.on('click', '.deactivate', function (e) {
            e.preventDefault();
            let $user = $(this);
            let user = {
                id: $user.data('id')
            };
            self.deActivate($user, user);
        });

        $Results.on('click', '.activate', function (e) {
            e.preventDefault();
            let $user = $(this);
            let user = {
                id: $user.data('id')
            };
            self.activate($user, user);
        });

        $addUserFrm.on('click', '.update', function (e) {
            e.preventDefault();
            let $user = $(this);
            let user = {
                Id: $("#Id").val(),
                UserName: $("#UserName").val(),
                Email: $("#Email").val(),
                Rating: $("#Rating").val(),
                MemberSince: $("#MemberSince").val(),
                RoleId: $("#RoleId").val()
            };
            self.update($user, user);
        });
    };

    this.update = function ($user, user) {
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: "POST",
            url: "/User/Edit",
            data: {
                __RequestVerificationToken: token,
                model: user
            },
            success: function (data) {
                notyf.success('The user has been updated');
            },
            error: function (xhr) {
                swal({
                    title: xhr.responseText,
                    icon: "error",
                    button: "Ok"
                });
            }
        });
    };

    this.deActivate = function ($user, user) {
        $.ajax({
            type: "POST",
            url: "/User/DeactivateUser",
            data: { id: user.id },
            success: function (data) {
                self.getUsers();
                notyf.success('The user has been deactivated');
            },
            error: function (xhr) {
                swal({
                    title: xhr.responseText,
                    icon: "error",
                    button: "Ok"
                });
            }
        });
    };

    this.activate = function ($user, user) {
        $.ajax({
            type: "POST",
            url: "/User/ActivateUser",
            data: { id: user.id },
            success: function (data) {
                self.getUsers();
                notyf.success('The user has been activated');
            },
            error: function (xhr) {
                swal({
                    title: xhr.responseText,
                    icon: "error",
                    button: "Ok"
                });
            }
        });
    };

    this.getUsers = function () {
        $.ajax({
            type: "GET",
            accepts: 'application/html',
            contentType: 'application/html',
            dataType: "html",
            url: "/User/GetUsers",
            success: function (data) {
                $('.cbody').html(data);
                self.initializeDataTable();
                self.initializeEvents();
            }
        });
    };

    this.documentReady = function () {
        self.initializeDataTable();
        self.initializeEvents();
    };
}

$(document).ready(function () {
    users = new Users();
    users.documentReady();
});