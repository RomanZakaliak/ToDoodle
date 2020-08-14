$(document).ready(function () {
    $('.done-checkbox').on('click', function (e) {
        $("#partial").text("Mark this item as done?");

        $("#confirm-modal").modal('show');

        $("#btn-confirm").on('click', function () {
            markComplete(e.target);
        });

        $("#confirm-modal").on("hide.bs.modal", function () {
            $(".done-checkbox").prop('checked', false);
        });
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
