// Write your JavaScript code.
$(document).ready(function() {
    $('.form-link').click(function(e) {
        $(this).parent().submit();
    });
});