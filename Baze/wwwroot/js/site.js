$(document).ready(function () {
    $('.done-checkbox').on('click', function (e){
        markComplete(e.target);
    });
});

function markComplete(checkbox) {
    checkbox.disabled = true;

    var row = checkbox.closest('tr');
    $(row).addClass('done');

    var form = checkbox.closest('form');
    form.submit();
}

$(document).ready(function () {
    $('#message').keypress(function (event) {
        if (event.keyCode === 13 && !event.shiftKey) {
            event.preventDefault();
            $('#sendBtn').trigger('click');
            return true;
        }
    });
});
