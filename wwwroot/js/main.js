"use strict";(function(e){e(window).on("load",(function(){e(".loader").fadeOut();e("#preloder").delay(200).fadeOut("slow");e(".filter__controls li").on("click",(function(){e(".filter__controls li").removeClass("active");e(this).addClass("active")}));if(e(".filter__gallery").length>0){var t=document.querySelector(".filter__gallery");var a=mixitup(t)}}));e(".set-bg").each((function(){var t=e(this).data("setbg");e(this).css("background-image","url("+t+")")}));e(".search-switch").on("click",(function(){e(".search-model").fadeIn(400)}));e(".search-close-switch").on("click",(function(){e(".search-model").fadeOut(400,(function(){e("#search-input").val("")}))}));e(".mobile-menu").slicknav({prependTo:"#mobile-menu-wrap",allowParentLinks:true});var t=e(".hero__slider");t.owlCarousel({loop:true,margin:0,items:1,dots:true,nav:true,navText:["<span class='arrow_carrot-left'></span>","<span class='arrow_carrot-right'></span>"],animateOut:"fadeOut",animateIn:"fadeIn",smartSpeed:1200,autoHeight:false,autoplay:true,mouseDrag:false});const a=new Plyr("#player",{controls:["play-large","play","progress","current-time","mute","captions","settings","fullscreen"],seekTime:25});e("select").niceSelect();e("#scrollToTopButton").click((function(){e("html, body").animate({scrollTop:0},"slow");return false}))})(jQuery);