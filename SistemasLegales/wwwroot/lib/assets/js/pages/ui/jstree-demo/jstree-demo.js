
$('document').ready(function(){
    $('#jstree1').jstree();
    $('#jstree2').jstree({'plugins':["wholerow","checkbox"], 'core' : {
        'data' : [
            {
                "text" : "Same but with checkboxes", icon: "fa fa-folder-open col-amber",
                "children" : [
                    { "text" : "initially selected", icon: "fa fa-folder col-amber", "state" : { "selected" : true } },
                    { "text" : "custom icon URL", "icon" : "fa fa-tree col-green" },
                    { "text" : "initially open", icon: "fa fa-folder-open col-amber", "state" : { "opened" : true },
                        "children" : [ {"text" : "Another node", icon: "fa fa-folder col-amber"} ]},
                    { "text" : "Disabled Node", "icon" : "fa fa-folder col-grey", "state" : { "disabled" : true } },
                    { "text" : "custom icon class", "icon" :  "glyphicon glyphicon-leaf col-green" }
                ]
            },
            {
                "text" : "And wholerow selection", icon: "fa fa-folder-open col-amber"
            }
        ]
    }});
    // data format demo
    $('#frmt').jstree({
        'core' : {
            'data' : [
                {
                    "text" : "Root node", icon: "fa fa-folder-open col-amber",
                    "state" : { "opened" : true },
                    "children" : [
                        {
                            "text" : "Child node 1",
                            "state" : { "selected" : true },
                            "icon" : "fa fa-file-text col-cyan"
                        },
                        { "text" : "Child node 2", icon: "fa fa-folder col-grey", "state" : { "disabled" : true }},
                    ]
                },
                {
                    "text" : "Root node 2", icon: "fa fa-folder-open col-amber",
                    "state" : { "opened" : true },
                    "children" : [
                        {
                            "text" : "Child node 1",
                            "state" : { "selected" : true },
                            "icon" : "fa fa-file-text col-cyan"
                        },
                        { "text" : "Child node 2", icon: "fa fa-folder col-grey", "state" : { "disabled" : true }},
                    ]
                }
            ]
        }
    });

// inline data demo
    $('#data').jstree({
        'core' : {
            'data' : [
                { "text" : "Root node", "icon": "fa fa-folder-open col-amber", "children" : [
                    { "text" : "Child node 1", "icon" : "fa fa-folder col-amber" },
                    { "text" : "Child node 2", "icon" : "fa fa-folder col-amber" }
                ]},
                { "text" : "Root node 2", "icon": "fa fa-folder-open col-amber", "children" : [
                    { "text" : "Child node 1", "icon" : "fa fa-folder col-amber" },
                    { "text" : "Child node 2", "icon" : "fa fa-folder col-amber" }
                ]}
            ]
        }
    });
// ajax demo
    $('#ajax').jstree({
        'core' : {
            'data' : {
                "url" : "../../assets/js/pages/ui/jstree-demo/root.json",
                "dataType" : "json" // needed only if you do not supply JSON headers
            }
        }
    });

// interaction and events
    $('#evts_button').on("click", function () {
        var instance = $('#evts').jstree(true);
        instance.deselect_all();
        instance.select_node('1');
    });
    $('#evts')
        .on("changed.jstree", function (e, data) {
            if(data.selected.length) {
                alert('The selected node is: ' + data.instance.get_node(data.selected[0]).text);
            }
        })
        .jstree({
            'core' : {
                'multiple' : false,
                'data' : [
                    { "text" : "Root node", "icon": "fa fa-folder-open col-amber", "children" : [
                        { "text" : "Child node 1", "icon" : "fa fa-folder col-amber", "children" : [
                            { "text" : "Child node 1.1", "icon" : "fa fa-folder col-amber", "id" : 1 },
                            { "text" : "Child node 1.2", "icon" : "fa fa-folder col-amber" }]
                        },
                        { "text" : "Child node 2", "icon" : "fa fa-folder col-amber", "children" : [
                            { "text" : "Child node 2.1", "icon" : "fa fa-file col-amber", "children" : [
                                { "text" : "Child node 2.1.1", "icon" : "fa fa-file col-amber"},
                                { "text" : "No Child", "icon" : "fa fa-folder col-grey", "state" : { "disabled" : true } }]},
                            { "text" : "No Child", "icon" : "fa fa-folder col-grey", "state" : { "disabled" : true } }]
                        }
                    ]}
                ]
            }
        });
});
