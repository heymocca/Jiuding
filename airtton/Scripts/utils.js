var Utils = function () {

    var ajaxLoad = function () {


    };

    var handleNotification = function () {

        var i = -1,
            getMessage = function () {
                var msgs = ['Hello, some notification sample goes here',
                    '<div><input class="form-control input-small" value="textbox"/>&nbsp;<a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" target="_blank">Check this out</a></div><div><button type="button" id="okBtn" class="btn blue">Close me</button><button type="button" id="surpriseBtn" class="btn default" style="margin: 0 8px 0 8px">Surprise me</button></div>',
                    'Did you like this one ? :)',
                    'Totally Awesome!!!',
                    'Yeah, this is the Metronic!',
                    'Explore the power of Metronic. Purchase it now!'
                ];
                i++;
                if (i === msgs.length) {
                    i = 0;
                }

                return msgs[i];
            };

        //show success alert
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }


    };


    var handleIsotop = function (content, file) {

        var $container = content;
        var imgLoaded = imagesLoaded('.image-content');

        imgLoaded.on('always', function (instance) {
            $container.isotope({
                filter: '*',
                containerStyle: {
                    position: 'relative',
                    overflow: 'visible'
                },
                resizable: true,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });
        });

        imgLoaded.on('progress', function (instance, image) {

            //console.log('imageloader');

            var result = image.isLoaded ? 'loaded' : 'broken';
            //image.img.className = image.isLoaded ? 'img-item1' : image.img.attributes.src.value = '/assets/dexa/admin/img/' + file;

            if (!image.isLoaded) {
                image.img.className = image.img.attributes.src.value = '/assets/dexa/admin/img/' + file;
            }

        });

        // check grid size on resize event
        $(window).resize(function () {
            //console.log('isotop');
            $container.isotope();
        });
    };

    var handleImageLoader = function (control) {
        var imgLoaded = imagesLoaded(control);

        imgLoaded.on('always', function (instance) {

        });

        imgLoaded.on('progress', function (instance, image) {

            console.log('imageloader');

            var result = image.isLoaded ? 'loaded' : 'broken';
            image.img.className = image.isLoaded ? 'img-item' : image.img.attributes.src.nodeValue = "/assets/img/broken/broken-img-link.png";

            if (!image.isLoaded) {
                image.img.className = image.img.attributes.src.nodeValue = "/assets/img/broken/broken-img-link.png";
            }

        });

    }

    return {
        //main function to initiate the module
        init: function () {
            ajaxLoad();




        },


        getLocation: function (callback) {
            var position = null;
            //debugger;

            var nav = null;
            if (nav == null) {
                nav = window.navigator;
            }
            if (nav != null) {
                var geoloc = nav.geolocation;
                if (geoloc != null) {
                    geoloc.getCurrentPosition(showPosition, showError);
                }
                else {
                    alert("Geolocation not supported");
                }
            }
            else {
                alert("Navigator not found");
            }

            //if (navigator.geolocation) {
            //    navigator.geolocation.getCurrentPosition(showPosition, showError, { timeout: 2000 });
            //} else {
            //    alert('Geolocation is not supported by this browser.');
            //    //x.innerHTML = "Geolocation is not supported by this browser.";
            //    return;
            //}


            function showPosition(position) {

                //debugger;

                var position = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };

                //alert(JSON.stringify(position));

                Utils.storeGeoToCookies(position);

                //document.cookie = "geo=" + JSON.stringify(position);


                if (callback && typeof (callback) === "function") {

                    callback(position);
                }
            }

            function showError(error) {
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        //x.innerHTML = "User denied the request for Geolocation."
                        alert('User denied the request for Geolocation.');

                        break;
                    case error.POSITION_UNAVAILABLE:
                        //x.innerHTML = "Location information is unavailable."
                        alert('Location information is unavailable.');
                        break;
                    case error.TIMEOUT:
                        //x.innerHTML = "The request to get user location timed out."
                        alert('The request to get user location timed out.');
                        break;
                    case error.UNKNOWN_ERROR:
                        //x.innerHTML = "An unknown error occurred."
                        alert('An unknown error occurred.');
                        break;
                }



                if (callback && typeof (callback) === "function") {

                    //alert('Unable to retrieve your location.');
                    callback(null);
                }
            }



        },

        storeGeoToCookies: function (position) {
            localStorage.setItem("geo", JSON.stringify(position));
            document.cookie = "geo=" + JSON.stringify(position);
        },

        readGeoFromCookies: function () {
            return JSON.parse(localStorage.getItem("geo"));
        },

        handleNotification: function () {
            handleNotification();
        },

        initIsotop: function (content, file) {
            handleIsotop(content, file);
        },

        getRandomNumber: function () {
            return Math.floor((Math.random() * 100) + 1);
        },

        loadingContent: function (content, message) {
            var globalImgPath = '../../assets/global/img/';

            content.append('<div id="loading-content" class="page-loading"><img src="' + globalImgPath + 'loading-spinner-grey.gif"/>&nbsp;&nbsp;<span>' + (message ? message : 'Loading...') + '</span></div>');

        },

        setScrollPosition: function (position) {
            localStorage.setItem("scroll_pos", JSON.stringify(position));
        },

        getScrollPosition: function () {
            return JSON.parse(localStorage.getItem("scroll_pos"));
        },

        startPageLoading: function (message) {

            Metronic.startPageLoading({ animate: true });

            //var globalImgPath = '/assets/global/img/';

            //$('.page-loading').remove();
            //$('body').append('<div class="page-loading"><img src="' + globalImgPath + 'loading-spinner-grey.gif"/>&nbsp;&nbsp;<span>' + (message ? message : 'Loading...') + '</span></div>');
        },


        stopPageLoading: function () {

            Metronic.stopPageLoading();
            //$('.page-loading').remove();
        },

        // wrMetronicer function to  block element(indicate loading)
        blockUI: function (options) {
            options = $.extend(true, {}, options);
            var html = '';
            if (options.animate) {
                html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '">' + '<div class="block-spinner-bar"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>' + '</div>';
            } else if (options.iconOnly) {
                html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="' + this.getGlobalImgPath() + 'loading-spinner-grey.gif" align=""></div>';
            } else if (options.textOnly) {
                html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><span>&nbsp;&nbsp;' + (options.message ? options.message : 'LOADING...') + '</span></div>';
            } else {
                html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="' + this.getGlobalImgPath() + 'loading-spinner-grey.gif" align=""><span>&nbsp;&nbsp;' + (options.message ? options.message : 'LOADING...') + '</span></div>';
            }

            if (options.target) { // element blocking
                var el = $(options.target);
                if (el.height() <= ($(window).height())) {
                    options.cenrerY = true;
                }
                el.block({
                    message: html,
                    baseZ: options.zIndex ? options.zIndex : 1000,
                    centerY: options.cenrerY !== undefined ? options.cenrerY : false,
                    css: {
                        top: '10%',
                        border: '0',
                        padding: '0',
                        backgroundColor: 'none'
                    },
                    overlayCSS: {
                        backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                        opacity: options.boxed ? 0.05 : 0.1,
                        cursor: 'wait'
                    }
                });
            } else { // page blocking
                $.blockUI({
                    message: html,
                    baseZ: options.zIndex ? options.zIndex : 1000,
                    css: {
                        border: '0',
                        padding: '0',
                        backgroundColor: 'none'
                    },
                    overlayCSS: {
                        backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                        opacity: options.boxed ? 0.05 : 0.1,
                        cursor: 'wait'
                    }
                });
            }
        },

        // wrMetronicer function to  un-block element(finish loading)
        unblockUI: function (target) {
            if (target) {
                $(target).unblock({
                    onUnblock: function () {
                        $(target).css('position', '');
                        $(target).css('zoom', '');
                    }
                });
            } else {
                $.unblockUI();
            }
        },



        checkSearchNameUnique: function (obj, items) {
            for (var i in items) {
                var item = items[i];

                if (item.name === obj.name) {
                    return true;
                }
            }
            return false;
        },

        // wrMetronicer function to scroll(focus) to an element
        scrollTo: function (el, offeset) {
            var pos = (el && el.size() > 0) ? el.offset().top : 0;

            if (el) {
                if ($('body').hasClass('page-header-fixed')) {
                    pos = pos - $('.page-header').height();
                }
                pos = pos + (offeset ? offeset : -1 * el.height());
            }

            jQuery('html,body').animate({
                scrollTop: pos
            }, 'slow');
        },

        // function to scroll to the top
        scrollTop: function () {
            Utils.scrollTo();
        },


        getScrollXY: function () {
            var x = 0, y = 0;
            if (typeof (window.pageYOffset) == 'number') {
                // Netscape
                x = window.pageXOffset;
                y = window.pageYOffset;
            } else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
                // DOM
                x = document.body.scrollLeft;
                y = document.body.scrollTop;
            } else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
                // IE6 standards compliant mode
                x = document.documentElement.scrollLeft;
                y = document.documentElement.scrollTop;
            }

            var pos = {
                x: x,
                y: y
            };

            return pos;
        },


        initFixHeaderWithPreHeader: function () {
            jQuery(window).scroll(function () {
                if (jQuery(window).scrollTop() > 37) {
                    jQuery("body").addClass("page-header-fixed");
                }
                else {
                    jQuery("body").removeClass("page-header-fixed");
                }
            });
        },

        initNavScrolling: function () {
            function NavScrolling() {
                if (jQuery(window).scrollTop() > 60) {
                    jQuery(".header").addClass("reduce-header");
                    jQuery(".page-container").css("margin-top", "40px");
                }
                else {
                    jQuery(".header").removeClass("reduce-header");
                    jQuery(".page-container").css("margin-top", "0px");
                }
            }
            NavScrolling();
            jQuery(window).scroll(function () {
                NavScrolling();
            });
        },

        loadChildContent: function (url, data) {

            Utils.scrollTop();

            var menuContainer = jQuery('.page-sidebar ul');
            var pageContent = $('.page-content');
            var pageContentBody = $('.page-content .page-content-body');

            //debugger;

            console.log("loading");
            console.log(url);


            Utils.startPageLoading();

            if ($(window).width() <= 991 && $('.page-sidebar').hasClass("in")) {
                $('.navbar-toggle').click();
            }

            $.ajax({
                type: "POST",
                cache: false,
                url: url,
                contentType: 'application/json',
                data: JSON.stringify(data),
                dataType: "html",
                success: function (res) {
                    Utils.stopPageLoading();
                    pageContentBody.html(res);
                    //App.fixContentHeight(); // fix content height
                    //App.initAjax(); // initialize core stuff
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    pageContentBody.html('<h4>Could not load the requested content.</h4>');
                    Utils.stopPageLoading();
                }
            });
        },


        getDataModalView: function (url, id, type, primary, echo, callback) {
            var data = {
                id: id,
                type: type,
                primary: primary,
                echo: echo
            };

            //debugger;

            console.log(data);

            Utils.ajaxCallJson(url, data, "json", false, function (source) {

                // debugger;

                if (source.echo !== echo)
                    return;

                if (source.result !== 'fail') {
                    if (callback && typeof (callback) === "function") {
                        callback(source);
                    }
                }
                else {
                    title = source.message;
                    msg = 'Please try again!';
                    toastr.error(msg, title)

                    $modal2.remove();
                }

            });
        },

        closeDexaModalDialog2: function () {
            $('#modal-view').modal('hide');
        },

        closeDexaModalDialog: function () {
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
            $('#modal-view').modal('hide');
        },

        ajaxCallJson: function (url, data, type, showLoading, callback) {
            if (showLoading)
                Utils.startPageLoading();

            Metronic.blockUI({
                //animate: false,
                overlayColor: '#7B7B7B'
            });

            //debugger;

            setTimeout(function () {
                $.ajax({
                    url: url,
                    type: "POST",
                    cache: false,
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    dataType: type,
                    success: function (source) {


                        Utils.stopPageLoading();

                        if (callback && typeof (callback) === "function") {
                            callback(source);

                            Metronic.unblockUI();
                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {

                        Utils.stopPageLoading();

                        if (callback && typeof (callback) === "function") {
                            var result = {
                                result: 'fail',
                                content: '<h4>Could not load the requested content.</h4>'
                            };

                            Utils.raiseErrorMessage('Could not connect to the server!');

                            Utils.unblockUI();

                            callback(result);
                        }
                    }
                });
            }, 300);
        },

        ajaxFormCall: function (form, showLoading, callback) {
            if (showLoading)
                Utils.startPageLoading();

            var token = $('[name=__RequestVerificationToken]').val();
            var headers = {};
            headers['__RequestVerificationToken'] = token;

            setTimeout(function () {

                $.ajax({
                    url: form.attr('action'),
                    type: form.attr('method'),
                    cache: false,
                    headers: headers,
                    data: form.serialize(),
                    success: function (source) {
                        //debugger;

                        Utils.stopPageLoading();

                        if (callback && typeof (callback) === "function") {
                            callback(source);
                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {

                        debugger;

                        Utils.stopPageLoading();

                        Utils.raiseErrorMessage(thrownError);

                    }
                });
            }, 300);
        },

        reverseGoogleGeocode: function (position, callback) {

            //debugger;

            // console.log('position');
            // console.log(position);

            if (position === null)
                return null;

            $.ajax({
                type: 'GET',
                dataType: "json",
                url: "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + position.lat + "," + position.lng + "&sensor=false",
                data: {},
                success: function (data) {

                    var city_name = null;
                    //$('#city').html(data);
                    $.each(data['results'], function (i, val) {
                        $.each(val['address_components'], function (i, val) {
                            if (val['types'] == "locality,political") {
                                if (val['long_name'] != "") {
                                    $('#city').html(val['long_name']);
                                }
                                else {
                                    $('#city').html("unknown");
                                }
                                city_name = val['long_name'];

                                //console.log(i + ", " + val['long_name']);
                                //console.log(i + ", " + val['types']);
                            }
                        });
                    });

                    if (callback && typeof (callback) === "function") {
                        callback(city_name);
                    }

                    //console.log('Success');
                },
                error: function () { console.log('error'); }
            });
        },

        imageLoader: function (file) {
            var imgLoaded = imagesLoaded('.image-content');

            imgLoaded.on('always', function (instance) {

            });

            imgLoaded.on('progress', function (instance, image) {

                //console.log('imageloader');

                var result = image.isLoaded ? 'loaded' : 'broken';
                //image.img.className = image.isLoaded ? 'img-item1' : image.img.attributes.src.value = '/assets/dexa/admin/img/' + file;

                if (!image.isLoaded) {
                    image.img.className = image.img.attributes.src.value = '/Scripts/dexa/admin/img/' + file;
                }

            });
        },

        url_decode: function (url) {
            // fixed -- + char decodes to space char
            var o = url;
            var binVal, t, b;
            var r = /(%[^%]{2}|\+)/;
            while ((m = r.exec(o)) != null && m.length > 1 && m[1] != '') {
                if (m[1] == '+') {
                    t = ' ';
                } else {
                    b = parseInt(m[1].substr(1), 16);
                    t = String.fromCharCode(b);
                }
                o = o.replace(m[1], t);
            }
            return o;
        },

        fixedEncodeURIComponent: function (str) {
            return encodeURIComponent(str).replace(/[!'()]/g, escape).replace(/\*/g, "%2A");
        },

        encodeRFC5987ValueChars: function (str) {
            return encodeURIComponent(str).
                // Note that although RFC3986 reserves "!", RFC5987 does not,
                // so we do not need to escape it
                replace(/['()]/g, escape). // i.e., %27 %28 %29
            replace(/\*/g, '%2A').
                // The following are not required for percent-encoding per RFC5987, 
                //  so we can allow for a little better readability over the wire: |`^
                replace(/%(?:7C|60|5E)/g, unescape);
        },

        urlParam: function (name) {
            var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
            if (results == null) {
                return null;
            }
            else {
                return results[1] || 0;
            }
        },

        initImageZoom: function () {
            $('.product-main-image').zoom({ url: $('.product-main-image img').attr('data-BigImgSrc') });
        },


        viewData: function (querry) {
            console.log(querry);
            window.location.href = querry;
        },

        encodeQueryData: function (data) {
            var ret = [];
            for (var d in data) {
                var val = encodeURIComponent(data[d]);

                if (val !== "null") {
                    ret.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                }
            }

            return ret.join("&");
        },

        reloadPage: function (querry, view) {

            debugger;

            var user_pos = Utils.readGeoFromCookies();
            var url = $('#get_search').val();
            var url_str = Utils.encodeQueryData(querry);
            var ur = url + '?' + url_str + '&latitude=' + user_pos.lat + '&longitude=' + user_pos.lng + '&v=' + view;
            var f = Utils.url_decode(ur);

            window.location.href = ur;
        },

        getLocationHash: function () {
            return window.location.hash.substring(1);
        },

        // Handles Bootstrap Tooltips.
        handleTooltips: function () {
            $('.tooltips').tooltip();
        },

        handleAdvanceTooltips: function (content) {
            $('.tooltips').tooltip({
                html: true,
                template: '<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>'
            });
        },

        handleContentHeight: function () {
            var height;
            //debugger;
            var b = $('body').height();
            var m_b = Metronic.getViewPort().height;

            // console.log('body:' + b);
            // console.log('viewport:' + m_b);
            height = Metronic.getViewPort().height -
                $('.page-header').outerHeight() -
                ($('.page-container').outerHeight() - $('.page-content').outerHeight()) -
                $('.page-prefooter').outerHeight() -
                $('.page-footer').outerHeight();

            $('.page-content').css('min-height', height);

        },

        handleMobiToggler: function () {
            $(".mobi-toggler").on("click", function (event) {
                event.preventDefault();//the default action of the event will not be triggered

                $(".header").toggleClass("menuOpened");
                $(".header").find(".header-navigation").toggle(300);
            });

            function SlideUpMenu() {
                $(".header-navigation a").on("click", function (event) {
                    if ($(window).width() < 1024) {
                        event.preventDefault();//the default action of the event will not be triggered
                        $(".header-navigation").slideUp(100);
                        $(".header").removeClass("menuOpened");
                    }
                });
                $(window).scroll(function () {
                    if (($(window).width() > 480) && ($(window).width() < 1024)) {
                        $(".header-navigation").slideUp(100);
                        $(".header").removeClass("menuOpened");
                    }
                });
            }
            SlideUpMenu();
            $(window).resize(function () {
                SlideUpMenu();
            });
        },

        handleFancybox: function () {
            jQuery(".fancybox-fast-view").fancybox();

            if (!jQuery.fancybox) {
                return;
            }

            if (jQuery(".fancybox-button").size() > 0) {
                jQuery(".fancybox-button").fancybox({
                    groupAttr: 'data-rel',
                    prevEffect: 'none',
                    nextEffect: 'none',
                    closeBtn: true,
                    helpers: {
                        title: {
                            type: 'inside'
                        }
                    }
                });

                $('.fancybox-video').fancybox({
                    type: 'iframe'
                });
            }
        },

        initDatePickers: function () {
            //init date pickers
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                autoclose: true
            });
        },


        handleScrollers: function () {
            $('.scroller').each(function () {
                var height;
                if ($(this).attr("data-height")) {
                    height = $(this).attr("data-height");
                } else {
                    height = $(this).css('height');
                }
                $(this).slimScroll({
                    allowPageScroll: false, // allow page scroll when the element scroll is ended
                    size: '7px',
                    color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : '#bbb'),
                    railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : '#eaeaea'),
                    height: height,
                    alwaysVisible: ($(this).attr("data-always-visible") == "1" ? true : false),
                    railVisible: ($(this).attr("data-rail-visible") == "1" ? true : false),
                    disableFadeOut: true
                });
            });
        },

        getListData: function (arg, stat, index, callback) {
            var elem = arg,
               url = elem.attr('data-url');

            //debugger;

            Utils.ajaxCallJson(url, null, "json", false, function (e) {
                var msg = e.data;
                //debugger;
                elem.get(0).options.length = 0;
                elem.get(0).options[0] = new Option(stat, "");

                $.each(msg, function (index, item) {
                    elem.get(0).options[elem.get(0).options.length] = new Option(item.Name, item.Id);
                });

                var myIndex = -1;
                if (index !== null) {
                    myIndex = index;
                }
                else {
                    myIndex = elem.data("loockup");
                    //debugger;
                    var i = elem.get(0).options.length - 1;
                    var reverseLookup = myIndex;
                    while (i >= 0) {
                        var option = elem.get(0).options[i];
                        if (option.value === String(myIndex)) {
                            var row = elem.get(0).options[i];
                            row.selected = true;
                            elem.trigger("liszt:updated");
                        }
                        i--;
                    }
                }

                if (callback && typeof (callback) === "function") {
                    callback('done');
                }

            });

        },

        setCookie: function (cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        },

        initFixHeaderWithPreHeader: function () {
            jQuery(window).scroll(function () {
                if (jQuery(window).scrollTop() > 37) {
                    jQuery("body").addClass("page-header-fixed");
                }
                else {
                    jQuery("body").removeClass("page-header-fixed");
                }
            });
        },

        initNavScrolling: function () {
            function NavScrolling() {
                if (jQuery(window).scrollTop() > 60) {
                    jQuery(".header").addClass("reduce-header");
                }
                else {
                    jQuery(".header").removeClass("reduce-header");
                }
            }

            NavScrolling();

            jQuery(window).scroll(function () {
                NavScrolling();
            });
        },

        raiseErrorMessage: function (message) {

            $('#error-content').css('display', 'block');

            var button = '<button type="button" class="close" data-dismiss="alert"></button>';
            var t = '<div id="error-message" class="alert alert-danger">';

            var msg = t + button + '<strong>Error!</strong> ' + message + '</div></div>';
            $('#error-content').html(msg);


        },

        // Handles Bootstrap switches
        handleBootstrapSwitch: function () {

            //  debugger;

            if (!$().bootstrapSwitch) {
                return;
            }
            $('.make-switch').bootstrapSwitch();
        },


        getURLParameter: function (name) {
            return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;
        },


        //add parameter without reload
        changeUrlParam: function (param, value) {

            debugger;

            var currentURL = window.location.href + '&';
            var change = new RegExp('(' + param + ')=(.*)&', 'g');
            var newURL = currentURL.replace(change, '$1=' + value + '&');

            var gg = decodeURIComponent((new RegExp('[?|&]' + param + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;

            if (Utils.getURLParameter(param) !== null) {
                try {
                    window.history.replaceState('', '', newURL.slice(0, -1));
                } catch (e) {
                    console.log(e);
                }
            } else {
                var currURL = window.location.href;
                if (currURL.indexOf("?") !== -1) {
                    window.history.replaceState('', '', currentURL.slice(0, -1) + '&' + param + '=' + value);
                } else {
                    window.history.replaceState('', '', currentURL.slice(0, -1) + '?' + param + '=' + value);
                }
            }
        },

        //update url without reload a page
        updateQueryString: function (key, value, options) {
            if (!options) options = {};

            var url = options.url || location.href;
            var re = new RegExp("([?&])" + key + "=.*?(&|#|$)(.*)", "gi"), hash;

            hash = url.split('#');
            url = hash[0];
            if (re.test(url)) {
                if (typeof value !== 'undefined' && value !== null) {
                    url = url.replace(re, '$1' + key + "=" + value + '$2$3');
                } else {
                    url = url.replace(re, '$1$3').replace(/(&|\?)$/, '');
                }
            } else if (typeof value !== 'undefined' && value !== null) {
                var separator = url.indexOf('?') !== -1 ? '&' : '?';
                url = url + separator + key + '=' + value;
            }

            if ((typeof options.hash === 'undefined' || options.hash) &&
                typeof hash[1] !== 'undefined' && hash[1] !== null)
                url += '#' + hash[1];

            history.pushState(null, null, url);
        },


        //add parameter and reload page 
        insertParam: function (key, value) {

            key = encodeURI(key); value = encodeURI(value);

            var kvp = document.location.search.substr(1).split('&');

            var i = kvp.length; var x; while (i--) {
                x = kvp[i].split('=');

                if (x[0] == key) {
                    x[1] = value;
                    kvp[i] = x.join('=');
                    break;
                }
            }

            if (i < 0) { kvp[kvp.length] = [key, value].join('='); }

            //this will reload the page, it's likely better to store this until finished
            document.location.search = kvp.join('&');

        },

        removeVariableFromURL: function (url_string, variable_name) {
            var URL = String(url_string);
            var regex = new RegExp("\\?" + variable_name + "=[^&]*&?", "gi");
            URL = URL.replace(regex, '?');
            regex = new RegExp("\\&" + variable_name + "=[^&]*&?", "gi");
            URL = URL.replace(regex, '&');
            URL = URL.replace(/(\?|&)$/, '');
            regex = null;
            return URL;
        },


        isInt: function (value) {
            return !isNaN(value) && (function (x) { return (x | 0) === x; })(parseFloat(value))
        },

        handleErrors: function (condition, message)
        {
            if (condition == true) {
                $('#error-content').css('display', 'block');

                var button = '<button type="button" class="close" data-dismiss="alert"></button>';
                var t = '<div id="error-message" class="alert alert-danger">';

                var msg = t + button + '<strong>Error!</strong> ' + message + '</div></div>';
                $('#error-content').html(msg);

            }
            else {
                $('#error-content').css('display', 'none');
            }

        }

    };
}();
