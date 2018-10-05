$(function () {
    $('#mainTable').editableTableWidget();
});
$(function() {
    var pickers = {};

    $('table tr').editable({
        dropdowns: {
            sex: ['Male', 'Female']
        },
        edit: function(values) {
            $(".edit i", this)
                .removeClass('fa-pencil')
                .addClass('fa-save')
                .attr('title', 'Save');

            pickers[this] = new Pikaday({
                field: $("td[data-field=date] input", this)[0],
                format: 'MMM D, YYYY'
            });
        },
        save: function(values) {
            $(".edit i", this)
                .removeClass('fa-save')
                .addClass('fa-pencil')
                .attr('title', 'Edit');

            if (this in pickers) {
                pickers[this].destroy();
                delete pickers[this];
            }
        },
        cancel: function(values) {
            $(".edit i", this)
                .removeClass('fa-save')
                .addClass('fa-pencil')
                .attr('title', 'Edit');

            if (this in pickers) {
                pickers[this].destroy();
                delete pickers[this];
            }
        }
    });
});
$(document).ready( function () {
    $('#bootstrap-table').bdt({
        showSearchForm: 0,
        showEntriesPerPageField: 0
    });
});