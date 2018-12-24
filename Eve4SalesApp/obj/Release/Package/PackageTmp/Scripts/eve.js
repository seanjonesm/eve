




var TeamDetailPostBackURL = '/EVE/ModelYearDeclarations/Edit';
$(function () {
    $(".CoverageEdit").click(function () {
        debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                debugger;

                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    //$("#closebtn").on('click',function(){
    //    $('#myModal').modal('hide');

    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip(); 
});


(function ($) {
    window.onbeforeunload = function (e) {
        window.name += ' [' + $(window).scrollTop().toString() + '[' + $(window).scrollLeft().toString();
    };

    $.maintainscroll = function () {
        if (window.name.indexOf('[') > 0) {
            var parts = window.name.split('[');
            window.name = $.trim(parts[0]);
            window.scrollTo(parseInt(parts[parts.length - 1]), parseInt(parts[parts.length - 2]));
        }
    };
    $.maintainscroll();
})(jQuery);

