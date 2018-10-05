$(function () {
    $('.basic-example').DataTable();
    //Exportable table
    $('.exportable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
});

$(document).ready(function() {
    $('#datatable').dataTable();
    $('#datatable-keytable').DataTable({
        keys: true
    });
    $('#datatable-responsive').DataTable();
    $('#datatable-scroller').DataTable({
        ajax: "assets/js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true
    });
    var table = $('#datatable-fixed-header').DataTable({
        fixedHeader: true
    });
});
TableManageButtons.init();
