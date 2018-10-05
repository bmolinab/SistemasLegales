
/* jsgrid- table ======================================================================================================
 */

$(function() {

 $("#jsGrid-basic").jsGrid({
 height: "70%",
 width: "100%",
 filtering: true,
 editing: true,
 inserting: true,
 sorting: true,
 paging: true,
 autoload: true,
 pageSize: 15,
 pageButtonCount: 5,
 deleteConfirm: "Do you really want to delete the client?",
 controller: db,
 fields: [
 { name: "Name", type: "text", width: 150 },
 { name: "Age", type: "number", width: 50 },
 { name: "Address", type: "text", width: 200 },
 { name: "Country", type: "select", items: db.countries, valueField: "Id", textField: "Name" },
 { name: "Married", type: "checkbox", title: "Is Married", sorting: false },
 { type: "control" }
 ]
 });

 });


/* jsgrid- Batch Delete table ======================================================================================================
 */


$(function() {

    $("#jsGrid-Batch-Delete").jsGrid({
        height: "50%",
        width: "100%",
        autoload: true,
        confirmDeleting: false,
        paging: true,
        controller: {
            loadData: function() {
                return db.clients;
            }
        },
        fields: [
            {
                headerTemplate: function() {
                    return $("<button>").attr("type", "button").text("Delete")
                        .on("click", function () {
                            deleteSelectedItems();
                        });
                },
                itemTemplate: function(_, item) {
                    return $("<input>").attr("type", "checkbox")
                        .prop("checked", $.inArray(item, selectedItems) > -1)
                        .on("change", function () {
                            $(this).is(":checked") ? selectItem(item) : unselectItem(item);
                        });
                },
                align: "center",
                width: 50
            },
            { name: "Name", type: "text", width: 150 },
            { name: "Age", type: "number", width: 50 },
            { name: "Address", type: "text", width: 200 }
        ]
    });


    var selectedItems = [];

    var selectItem = function(item) {
        selectedItems.push(item);
    };

    var unselectItem = function(item) {
        selectedItems = $.grep(selectedItems, function(i) {
            return i !== item;
        });
    };

    var deleteSelectedItems = function() {
        if(!selectedItems.length || !confirm("Are you sure?"))
            return;

        deleteClientsFromDb(selectedItems);

        var $grid = $("#jsGrid-Batch-Delete");
        $grid.jsGrid("option", "pageIndex", 1);
        $grid.jsGrid("loadData");

        selectedItems = [];
    };

    var deleteClientsFromDb = function(deletingClients) {
        db.clients = $.map(db.clients, function(client) {
            return ($.inArray(client, deletingClients) > -1) ? null : client;
        });
    };

});

/* jsgrid- Custom-View table ======================================================================================================
 */
$(function() {

    $("#jsGrid-Custom-View").jsGrid({
        height: "70%",
        width: "100%",
        filtering: true,
        editing: true,
        sorting: true,
        paging: true,
        autoload: true,
        pageSize: 15,
        pageButtonCount: 5,
        controller: db,
        fields: [
            { name: "Name", type: "text", width: 150 },
            { name: "Age", type: "number", width: 50 },
            { name: "Address", type: "text", width: 200 },
            { name: "Country", type: "select", items: db.countries, valueField: "Id", textField: "Name" },
            { name: "Married", type: "checkbox", title: "Is Married", sorting: false },
            { type: "control", modeSwitchButton: false, editButton: false }
        ]
    });

    $(".config-panel input[type=checkbox]").on("click", function() {
        var $cb = $(this);
        $("#jsGrid-Custom-View").jsGrid("option", $cb.attr("id"), $cb.is(":checked"));
    });
});

/* jsgrid- Custom Load Indicator ======================================================================================================
 */

$(function() {

    $("#jsGrid-Custom-Load-Indicator").jsGrid({
        height: "50%",
        width: "100%",
        sorting: true,
        paging: false,
        autoload: true,
        controller: {
            loadData: function() {
                var d = $.Deferred();

                $.ajax({
                    url: "http://services.odata.org/V3/(S(3mnweai3qldmghnzfshavfok))/OData/OData.svc/Products",
                    dataType: "json"
                }).done(function(response) {
                    setTimeout(function() {
                        d.resolve(response.value);
                    }, 2000);
                });

                return d.promise();
            }
        },
        loadIndicator: function(config) {
            var container = config.container[0];
            var spinner = new Spinner();

            return {
                show: function() {
                    spinner.spin(container);
                },
                hide: function() {
                    spinner.stop();
                }
            };
        },
        fields: [
            { name: "Name", type: "text" },
            { name: "Description", type: "textarea", width: 150 },
            { name: "Rating", type: "number", width: 50, align: "center",
                itemTemplate: function(value) {
                    return $("<div>").addClass("rating").append(Array(value + 1).join("&#9733;"));
                }
            },
            { name: "Price", type: "number", width: 50,
                itemTemplate: function(value) {
                    return value.toFixed(2) + "$"; }
            }
        ]
    });

});


/* jsgrid- Static Data ======================================================================================================
 */


$(function() {

    $("#jsGrid-Static-Data").jsGrid({
        height: "70%",
        width: "100%",
        sorting: true,
        paging: true,
        fields: [
            { name: "Name", type: "text", width: 150 },
            { name: "Age", type: "number", width: 50 },
            { name: "Address", type: "text", width: 200 },
            { name: "Country", type: "select", items: db.countries, valueField: "Id", textField: "Name" },
            { name: "Married", type: "checkbox", title: "Is Married" }
        ],
        data: db.clients
    });

});


/* jsgrid- Validation ======================================================================================================
 */

$(function() {

    $("#jsGrid-Validation").jsGrid({
        height: "70%",
        width: "100%",
        filtering: true,
        editing: true,
        inserting: true,
        sorting: true,
        paging: true,
        autoload: true,
        pageSize: 15,
        pageButtonCount: 5,
        deleteConfirm: "Do you really want to delete the client?",
        controller: db,
        fields: [
            { name: "Name", type: "text", width: 150, validate: "required" },
            { name: "Age", type: "number", width: 50, validate: { validator: "range", param: [18,80] } },
            { name: "Address", type: "text", width: 200, validate: { validator: "rangeLength", param: [10, 250] } },
            { name: "Country", type: "select", items: db.countries, valueField: "Id", textField: "Name",
                validate: { message: "Country should be specified", validator: function(value) { return value > 0; } } },
            { name: "Married", type: "checkbox", title: "Is Married", sorting: false },
            { type: "control" }
        ]
    });

});