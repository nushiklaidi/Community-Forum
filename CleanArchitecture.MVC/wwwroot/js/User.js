var users = null;
function Users() {
    var self = this;

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
    };

    this.deActivate = function ($user, user) {
        $.ajax({
            type: "GET",
            url: "/User/DeactivateUser",
            data: { id: user.id },
            success: function (data) {
                self.getUsers();
            }
        });
    };

    this.activate = function ($user, user) {
        $.ajax({
            type: "GET",
            url: "/User/ActivateUser",
            data: { id: user.id },
            success: function (data) {
                self.getUsers();
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
                $('.card-body').html(data);
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