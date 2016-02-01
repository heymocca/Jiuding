$(document).ready(function(){

    alert('a');

    var demo3 = function () {

        // Create variables (in this scope) to hold the API and image size
        var jcrop_api,
            boundx,
            boundy,
            // Grab some information about the preview pane
            $preview = $('#preview-pane'),
            $pcnt = $('#preview-pane .preview-container'),
            $pimg = $('#preview-pane .preview-container img'),

            xsize = $pcnt.width(),
            ysize = $pcnt.height();

        console.log('init', [xsize, ysize]);

        function cropInit(src) {
            $preview_original = $('#demo3');

            var factor;

            $preview_original.removeAttr('style');
            $preview_original.attr('src', src);
            $preview.css('display', 'block');
            $pimg.attr('src', src);

            console.log("height" + $preview.height());
            console.log("width" + $preview.width());

            if ($preview_original.height() < $preview_original.width()) {
                factor = $preview_original.height() / 4;
            }
            else {
                factor = $preview_original.width() / 4;
            }

            //console.log("factor:" + factor);
            var f = SetFactor(3, factor);
            var x = f.x;
            var y = f.y;


            $preview_original.Jcrop({
                setSelect: [0, 0, x, y],
                onChange: updatePreview,
                onSelect: updatePreview,
                aspectRatio: xsize / ysize
            }, function () {
                // Use the API to get the real image size
                var bounds = this.getBounds();
                boundx = bounds[0];
                boundy = bounds[1];
                // Store the API in the jcrop_api variable
                jcrop_api = this;
                // Move the preview into the jcrop container for css positioning
                $preview.appendTo(jcrop_api.ui.holder);
            });

            function updatePreview(c) {

                initPreview(c.x, c.y, c.w, c.h);

                if (parseInt(c.w) > 0) {
                    var rx = xsize / c.w;
                    var ry = ysize / c.h;

                    $pimg.css({
                        width: Math.round(rx * boundx) + 'px',
                        height: Math.round(ry * boundy) + 'px',
                        marginLeft: '-' + Math.round(rx * c.x) + 'px',
                        marginTop: '-' + Math.round(ry * c.y) + 'px'
                    });



                }
            };
        }

        function SetFactor(mode, factor) {
            if (mode === 1)
                return { x: factor * 4, y: factor * 3 };
            else if (mode === 2)
                return { x: factor * 3, y: factor * 4 };
            else if (mode === 3)
                return { x: factor * 4, y: factor * 4 };
            else
                return { x: factor * 4, y: factor * 3 };
        }

        var initPreview = function (x, y, w, h) {
            var scale = $preview_original[0].naturalWidth / $preview_original.width();
            //var scale = 1;
            var _f = Math.floor;
            setPreview(_f(scale * x), _f(scale * y), _f(scale * w), _f(scale * h));

        };
        //Set the 4 coordinates for the cropping
        var setPreview = function (x, y, w, h) {
            $('#X').val(x || 0);
            $('#Y').val(y || 0);
            $('#Width').val(w || $preview_original[0].naturalWidth);
            $('#Height').val(h || $preview_original[0].naturalHeight);
        };

        $('#remove-btn').on('click', function (e) {

            $preview_original.removeAttr('style');
            $preview_original.attr('src', '../../assets/global/plugins/jcrop/demos/demo_files/image3.jpg');

            if (jcrop_api) {
                jcrop_api.destroy();
            }

        });

        //What happens if the File changes?
        $('#File').change(function (evt) {

            var f = evt.target.files[0];
            var reader = new FileReader();

            if (jcrop_api) {
                jcrop_api.destroy();
            }

            if (f) {

                if (!f.type.match('image.*')) {
                    alert("The selected file does not appear to be an image.");
                    return;
                }

                reader.onload = function (e) {
                    var sr = e.target.result;

                    cropInit(sr);

                };
                reader.readAsDataURL(f);
            }

        });


    }

    var handleResponsive = function () {

        if ($(window).width() <= 1024 && $(window).width() >= 678) {
            $('.responsive-1024').each(function () {
                $(this).attr("data-class", $(this).attr("class"));
                $(this).attr("class", 'responsive-1024 col-md-12');
            });
        } else {
            $('.responsive-1024').each(function () {
                if ($(this).attr("data-class")) {
                    $(this).attr("class", $(this).attr("data-class"));
                    $(this).removeAttr("data-class");
                }
            });
        }
    }

    if (!jQuery().Jcrop) {;
        return;
    }

    demo3();

    handleResponsive();
})
