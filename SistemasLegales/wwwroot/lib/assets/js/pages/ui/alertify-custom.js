
$(function () {
    $('#alert').on('click', function () {
        alertify.theme("bootstrap").alert("This is an alert dialog");
        return false;
    });
    $('#confirm').on('click', function () {
        alertify.theme("bootstrap").confirm("This is a confirm dialog", function(ev) {
            ev.preventDefault();
            alertify.success("You've clicked OK");
        }, function(ev) {
            ev.preventDefault();
            alertify.error("You've clicked Cancel");
        });
    });
    $('#prompt').on('click', function () {
        alertify.theme("bootstrap").defaultValue("Default value").prompt("This is a prompt dialog", function(str, ev) {
            ev.preventDefault();
            alertify.success("You've clicked OK and typed: " + str);
        }, function(ev) {
            ev.preventDefault();
            alertify.error("You've clicked Cancel");
        });
    });
    $('#ajax').on('click', function () {
        alertify.theme("bootstrap").confirm("Confirm?", function(ev) {
            ev.preventDefault();
            alertify.alert("Successful AJAX after OK");
        }, function(ev) {
            ev.preventDefault();
            alertify.alert("Successful AJAX after Cancel");
        });
    });
    $('#custom-labels').on('click', function () {
        alertify
            .theme("bootstrap")
            .okBtn("Accept")
            .cancelBtn("Deny")
            .confirm("Confirm dialog with custom button labels", function (ev) {

                // The click event is in the
                // event variable, so you can use
                // it here.
                ev.preventDefault();
                alertify.success("You've clicked OK");

            }, function(ev) {

                // The click event is in the
                // event variable, so you can use
                // it here.
                ev.preventDefault();

                alertify.error("You've clicked Cancel");

            });
    });
    $('#promise').on('click', function () {
        if ("function" !== typeof Promise) {
            alertify.alert("Your browser doesn't support promises");
            return;
        }
        alertify.theme("bootstrap").confirm("Confirm?").then(function(resolvedValue) {
            resolvedValue.event.preventDefault();
            alertify.alert("You clicked the " + resolvedValue.buttonClicked + " button!");
        });
    });
    $('#click-to-close').on('click', function () {
        alertify.closeLogOnClick(true).log("Click me to close!");
    });
    $('#delay').on('click', function () {
        alertify.delay(10000).log("Hiding in 10 seconds");
    });
    $('#error-log-callback').on('click', function () {
        alertify.error("Standard log message with callback", function(ev) {
            alertify.error("You clicked the notification");

        });
    });
    $('#success-log-callback').on('click', function () {
        alertify.success("Standard log message with callback", function(ev) {
            ev.preventDefault();

            alertify.success("You clicked the notification");

        });
    });
    $('#standard-log-callback').on('click', function () {
        alertify.log("Standard log message with callback", function(ev) {
            ev.preventDefault();

            alertify.log("You clicked the notification");

        });
    });
    $('#reset').on('click', function () {
        // Notice the okay button is not the custom value, it was reset.
        alertify
            .okBtn("Go For It!")
            .reset()
            .theme("bootstrap")
            .alert("Custom values were reset!");
    });
});


