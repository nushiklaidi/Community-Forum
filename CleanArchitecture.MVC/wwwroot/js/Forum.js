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
    };

    this.getForums = function () {
        $.ajax({
            type: "GET",
            accepts: 'application/html',
            contentType: 'application/html',
            dataType: "html",
            url: "/Forum/GetForums",
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
    forums = new Forums();
    forums.documentReady();
});