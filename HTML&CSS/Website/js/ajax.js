$(function(){
    // don't cache ajax or content won't be fresh
    $.ajaxSetup ({
        cache: false
    });
    //var ajax_load = "<img src='http://automobiles.honda.com/images/current-offers/small-loading.gif' alt='loading...' />";

    // load() functions
    var loadUrl = "./about.html";
    $(".nav-links").click(function(){
        $("#a1").html(ajax_load).load(loadUrl);
    });

// end  
});