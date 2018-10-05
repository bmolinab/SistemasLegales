function dialog_animation(x) {
    $(".modal .modal-dialog").attr("class", "modal-dialog " + x + "  animated");
};
/**
 * This tiny script just helps us demonstrate
 * what the various example callbacks are doing
 * http://bootboxjs.com
 */
function notifyCallMe(message,type) {
    $.notify({
        icon: 'glyphicon glyphicon-exclamation-sign',
        message: message
    },{
        type: type,
        animate: {
            enter: 'animated fadeInUp',
            exit: 'animated fadeOutRight'
        }
    });
};
$(".btn-preview")
    .on("click", function() {
        var bootbox_type = $("#bootbox_type").val();
        var option = {
            title: 'Modal title',
            message: '<p>One fine body&hellip;</p>'
        };

        if ((bootbox_type == "bootbox_alert") || (bootbox_type == "bootbox_alert_cb")) {
            if (bootbox_type == "bootbox_alert_cb") {
                option.callback = function(result) {

                    notifyCallMe("Bootbox Alert with callback","info");
                };
            }
            var dialog = bootbox.alert(option);
        } else if (bootbox_type == "bootbox_confirm") {
            option.message = '<p>Are you sure?</p>';
            option.callback = function(result) {
                notifyCallMe("Bootbox Confirm result:"+ result,"info");
            };
            var dialog = bootbox.confirm(option);
        } else if ((bootbox_type == "bootbox_prompt") || (bootbox_type == "bootbox_prompt_dv")) {
            if ((bootbox_type == "bootbox_prompt_dv")) {
                option.value = "Bootbox";
                option.title = 'What is your real name?';
            } else {
                option.title = 'What is your name?';
            }
            option.callback = function(result) {
                if (result === null) {
                    notifyCallMe("Bootbox Prompt dismissed","info");
                } else {
                    notifyCallMe("Hi <b>"+ result + "</b>","info");
                }
            };
            var dialog = bootbox.prompt(option);
        } else if (bootbox_type == "bootbox_custom") {
            option.title = "Custom title";
            option.message = "I am a custom dialog";
            option.buttons = {
                success: {
                    label: "Success!",
                    className: "btn-success",
                    callback: function() {
                        notifyCallMe("great success","success");
                    }
                },
                danger: {
                    label: "Danger!",
                    className: "btn-danger",
                    callback: function() {
                        notifyCallMe("uh oh, look out!","danger");
                    }
                },
                main: {
                    label: "Click ME!",
                    className: "btn-primary",
                    callback: function() {
                        notifyCallMe("Primary button","info");
                    }
                }
            }
            var dialog = bootbox.dialog(option);
        }
        /*
         // not working!
         dialog
         .on('show.bs.modal', function (e) {
         var anim = $('#entrance').val();
         dialog_animation(anim);
         });
         */

        // working :)
        var anim = $('#entrance').val();
        $(".modal .modal-dialog").attr("class", "modal-dialog " + anim + "  animated");

        dialog
            .on('hide.bs.modal', function (e) {
                var anim = $('#exit').val();
                dialog_animation(anim);
            });
    });
