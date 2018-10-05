$(function () {
    //Multi-select
    $('#optgroup').multiSelect({ selectableOptgroup: true });

    //Spinner
    $('#customize-spinner').spinner('changed', function(e, newVal, oldVal) {
        $('#old-val').text(oldVal);
        $('#new-val').text(newVal);
    });

    //Select Splitter
    $('select[data-selectsplitter-selector]').selectsplitter();
});

//-----------Typeahead-----------------//
var substringMatcher = function(strs) {
    return function findMatches(q, cb) {
        var matches, substringRegex;

        // an array that will be populated with substring matches
        matches = [];

        // regex used to determine if a string contains the substring `q`
        substrRegex = new RegExp(q, 'i');

        // iterate through the pool of strings and for any string that
        // contains the substring `q`, add it to the `matches` array
        $.each(strs, function(i, str) {
            if (substrRegex.test(str)) {
                matches.push(str);
            }
        });

        cb(matches);
    };
};

var states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California',
    'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii',
    'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana',
    'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota',
    'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
    'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota',
    'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island',
    'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
    'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
];

$('#the-basics .typeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 1
    },
    {
        name: 'states',
        source: substringMatcher(states)
    });

// constructs the suggestion engine
var states = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.whitespace,
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    // `states` is an array of state names defined in "The Basics"
    local: states
});

$('#bloodhound .typeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 1
    },
    {
        name: 'states',
        source: states
    });

var countries = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.whitespace,
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    // url points to a json file that contains an array of country names, see
    // https://github.com/twitter/typeahead.js/blob/gh-pages/data/countries.json
    prefetch: 'data/countries.json'
});

// passing in `null` for the `options` arguments will result in the default
// options being used
$('#prefetch .typeahead').typeahead(null, {
    name: 'countries',
    source: countries
});
$('#scrollable-dropdown-menu .typeahead').typeahead(null, {
    name: 'countries',
    limit: 10,
    source: countries
});
//------------Typeahead----------------//


//------------Bootstrap Tags Input--------//
var ComponentsBootstrapTagsinput = function() {
    var a = function() {
            var cities = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.obj.whitespace('text'),
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                prefetch: 'data/city.json'
            });
            cities.initialize();

            var elt = $('#object-tags');
            elt.tagsinput({
                itemValue: 'value',
                itemText: 'text',
                typeaheadjs: {
                    name: 'cities',
                    displayKey: 'text',
                    source: cities.ttAdapter()
                }
            });
            elt.tagsinput('add', { "value": 1 , "text": "Amsterdam"   , "continent": "Europe"    });
            elt.tagsinput('add', { "value": 4 , "text": "Washington"  , "continent": "America"   });
            elt.tagsinput('add', { "value": 7 , "text": "Sydney"      , "continent": "Australia" });
            elt.tagsinput('add', { "value": 10, "text": "Beijing"     , "continent": "Asia"      });
            elt.tagsinput('add', { "value": 13, "text": "Cairo"       , "continent": "Africa"    });
        },
        b = function() {
            var cities = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.obj.whitespace('text'),
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                prefetch: 'data/city.json'
            });
            cities.initialize();

            var elt = $('#categorizing_tags');
            elt.tagsinput({
                tagClass: function(item) {
                    switch (item.continent) {
                        case 'Europe'   : return 'label label-primary';
                        case 'America'  : return 'label label-danger label-important';
                        case 'Australia': return 'label label-success';
                        case 'Africa'   : return 'label label-default';
                        case 'Asia'     : return 'label label-warning';
                    }
                },
                itemValue: 'value',
                itemText: 'text',
                typeaheadjs: {
                    name: 'cities',
                    displayKey: 'text',
                    source: cities.ttAdapter()
                }
            });
            elt.tagsinput('add', { "value": 1 , "text": "Amsterdam"   , "continent": "Europe"    });
            elt.tagsinput('add', { "value": 4 , "text": "Washington"  , "continent": "America"   });
            elt.tagsinput('add', { "value": 7 , "text": "Sydney"      , "continent": "Australia" });
            elt.tagsinput('add', { "value": 10, "text": "Beijing"     , "continent": "Asia"      });
            elt.tagsinput('add', { "value": 13, "text": "Cairo"       , "continent": "Africa"    });
        },
        j = function() {
            var t = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.obj.whitespace("name"),
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                prefetch: {
                    url: "data/citynames.json",
                    filter: function(t) {
                        return $.map(t, function(t) {
                            return {
                                name: t
                            }
                        })
                    }
                }
            });
            t.initialize(), $("#tagsinput_typeahead").tagsinput({
                typeaheadjs: {
                    name: "citynames",
                    displayKey: "name",
                    valueKey: "name",
                    source: t.ttAdapter()
                }
            })
        };
    return {
        init: function() {
            a(),b(),j()
        }
    }
}();
jQuery(document).ready(function() {
    ComponentsBootstrapTagsinput.init()
});
//------------Bootstrap Tags Input--------//

//------------Bootstrap Max length--------//

$(document).ready(function () {
    $('input#defaultconfig').maxlength()

    $('input#thresholdconfig').maxlength({
        threshold: 20
    });

    $('input#moreoptions').maxlength({
        alwaysShow: true,
        warningClass: "label label-success",
        limitReachedClass: "label label-danger"
    });

    $('input#alloptions').maxlength({
        alwaysShow: true,
        warningClass: "label label-success",
        limitReachedClass: "label label-danger",
        separator: ' out of ',
        preText: 'You typed ',
        postText: ' chars available.',
        validate: true
    });

    $('textarea#textarea').maxlength({
        alwaysShow: true
    });

    $('input#placement') .maxlength({
        alwaysShow: true,
        placement: 'top-left'
    });
});