var forums = null;
function Forums() {
    var self = this;

    this.initializeDataTable = function () {

        let = tableOptions = {
            searching: false,
            pageLength: 3,
            lengthChange: false
        };

        self.table = $('#tableResult').DataTable(tableOptions);
    };

    this.initializeEvents = function () {
        let $Results = $('#tableResult');

        $Results.on('click', '.delete', function (e) {
            e.preventDefault();
            let $forum = $(this);
            let forum = {
                id: $forum.data('id')
            };

            self.delete($forum, forum);
        });
    };

    this.delete = function ($forum, forum) {
        $.ajax({
            type: "POST",
            url: "/Forum/Delete",
            data: { id: forum.id },
            success: function (data) {
                self.getForums();
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

    this.getForums = function () {
        $.ajax({
            type: "GET",
            accepts: 'application/html',
            contentType: 'application/html',
            dataType: "html",
            url: "/Forum/GetForums",
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
    forums = new Forums();
    forums.documentReady();
});