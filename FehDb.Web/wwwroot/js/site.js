// Write your Javascript code.
// reset the modal when a new weapon's details is requested
$(function () {
    $("#details").on("hidden.bs.modal", function () {
        $("#details-content").empty();
    });
});

// load the weapon information dynamically based on the weapon id
$(".weapon-row").click(function () {
    var id = $(this).attr('id');

    var load = './Weapons/Partials/Details?id=' + id;

    console.log(load);
    $.ajax(
        {
            url: load,
            cache: false,
            success: function (data) {
                $("#details-content").append($.parseHTML(data));
            }
        }
    )
    $('#details').modal('show');
});