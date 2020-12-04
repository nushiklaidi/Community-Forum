var forumDetails = null;
function ForumDetails() {
    var self = this;

    this.initializeDataTable = function () {

        let = tableOptions = {
            pageLength: 10,
            lengthChange: false
        };

        self.table = $('#tableResult').DataTable(tableOptions);
    };

    this.initializeEvents = function () {
        let $Results = $('#tableResult');

        $Results.on('click', '.delete', function (e) {
            e.preventDefault();
            let $post = $(this);
            let post = {
                id: $post.data('id'),
                forumId: $post.data('forumid')
            };

            self.delete($post, post);
        });
    };

    this.delete = function ($post, post) {
        $.ajax({
            type: "POST",
            url: "/Post/Delete",
            data: { id: post.id },
            success: function (data) {
                self.getForumDetails($post, post);
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

    this.getForumDetails = function ($post, post) {
        $.ajax({
            type: "GET",
            accepts: 'application/html',
            contentType: 'application/html',
            dataType: "html",
            url: "/Forum/GetForumDetails",
            data: { id: post.forumId },
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
    forumDetails = new ForumDetails();
    forumDetails.initializeDataTable();
    forumDetails.initializeEvents();
});