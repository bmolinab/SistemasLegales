$(function () {
    //CKEditor
    CKEDITOR.replace('ckeditor');
    CKEDITOR.config.height = 300;
    CKEDITOR.on( 'instanceCreated', function ( event ) {
        var editor = event.editor,
            element = editor.element;
        if ( element.is( 'h1', 'h2', 'h3' ) || element.getAttribute( 'id' ) == 'taglist' ) {
            editor.on( 'configLoaded', function () {

                // Remove redundant plugins to make the editor simpler.
                editor.config.removePlugins = 'colorbutton,find,flash,font,' +
                'forms,iframe,image,newpage,removeformat,' +
                'smiley,specialchar,stylescombo,templates';

                // Rearrange the toolbar layout.
                editor.config.toolbarGroups = [
                    { name: 'editing', groups: [ 'basicstyles', 'links' ] },
                    { name: 'undo' },
                    { name: 'clipboard', groups: [ 'selection', 'clipboard' ] },
                    { name: 'about' }
                ];
            } );
        }
    } );
});