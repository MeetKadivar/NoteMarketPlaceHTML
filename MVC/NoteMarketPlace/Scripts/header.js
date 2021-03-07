function sticky_header() {
    var header_height = jQuery('.home-img-container').innerHeight() / 10;
   var scrollTop = jQuery(window).scrollTop();
   if (scrollTop > header_height) {
       jQuery('nav').addClass('white-nav');
       jQuery('.list-link').addClass('white-nav-link');
       jQuery('.nav-login').addClass('white-nav-login');
       $(".navbar-brand img").attr("src","/Content/imges/logo.png");
       
   } else {
       jQuery('nav').removeClass('white-nav');
       jQuery('.list-link').removeClass('white-nav-link');
       jQuery('.nav-login').removeClass('white-nav-login');

       $(".navbar-brand img").attr("src","/Content/imges/top-logo.png");
       
   }
  
}

jQuery(document).ready(function () {
 sticky_header();
});

jQuery(window).scroll(function () {
 sticky_header();  
});
jQuery(window).resize(function () {
 sticky_header();
});