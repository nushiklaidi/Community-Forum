var forums = null;
var notyf = new Notyf();
function Forums() {
    var self = this;
    const $addOrEditFrm = $('#addOrEditFrm');

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

        $addOrEditFrm.on('click', '.addOrEdit', function (e) {
            e.preventDefault();
            let $forum = $(this);
            let forum = {
                Id: $("#Id").val(),
                Title: $("#Title").val(),
                Description: $("#Description").val(),
            };
            self.addOrEdit($forum, forum);
        });
    };

    this.delete = function ($forum, forum) {

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: "/Forum/Delete",
                    data: { id: forum.id },
                    success: function (data) {
                        self.getForums();
                        notyf.success('The forum has been deleted');
                    },
                    error: function (xhr) {
                        Swal.fire({
                            title: xhr.responseText,
                            icon: "error",
                            button: "Ok"
                        });
                    }
                });
            }
        });        
    };

    this.addOrEdit = function ($forum, forum) {
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: "POST",
            url: "/Forum/AddOrEdit",
            data: {
                __RequestVerificationToken: token,
                model: forum
            },
            success: function (data) {
                if (forum.Id == 0)
                    notyf.success('The forum has been created');
                else
                    notyf.success('The forum has been updated');
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