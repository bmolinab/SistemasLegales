
$('.chips-initial').material_chip({
    data: [{
        tag: 'Apple'
    }, {
        tag: 'Microsoft'
    }, {
        tag: 'Google'
    }]
});

$('.chips-placeholder').material_chip({
    placeholder: 'Enter a tag',
    secondaryPlaceholder: '+Tag'
});

$('.chips').material_chip();