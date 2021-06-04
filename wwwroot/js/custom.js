$('document').ready(function() {
    /* Site Map Open or Close */
    $( ".sitemap-arrow" ).click(function() {
        if ($( this ).css( "transform" ) == 'none'){
            $(this).css("transform","rotate(180deg)");
        } else {
            $(this).css("transform","" );
        }
    });
    /* Back to Top */
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    $('#back-to-top').click(function () {
        $('#back-to-top').tooltip('hide');
        $('body,html').animate({
            scrollTop: 0
        }, 800);
        return false;
    });
    /* Print */
    if($('#print').length > 0 && $('#div_print').length > 0) {
        $('#print').on('click', function() {
            let content = $('#div_print').html();
            $('body').html(content);
            window.print();
            return location.reload();
            // let origin_content = $('body').html();
            // $('body').html(origin_content);
            // return false;
        })
    }
});