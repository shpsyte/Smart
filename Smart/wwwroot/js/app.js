// Write your JavaScript code.
bread_crumb = $("#ribbon ol.breadcrumb"),
    jsArray = {};

// UPDATE BREADCRUMB
function drawBreadCrumb(opt_breadCrumbs) {
    var nav_elems = $('nav li.active > a');
    //, count = nav_elems.length,
    //        a = $("nav li.active > a"),
    //      b = a.length;

    //console.log("breadcrumb")
    bread_crumb.empty();
    bread_crumb.append($("<li class='list-inline-item'>  <a href='/'>Home</a></li>"));
    var str = $(location).attr('pathname');
    str = document.title;
    var last = str.substring(str.lastIndexOf(" / ") + 1, str.length).replace('- Smart', '');

    var stringaux;
    nav_elems.each(function () {

        if (stringaux != $.trim(last)) {
            bread_crumb.append($("<li class='list-inline-item'></li>").html($.trim(last)));
        }
        stringaux = $.trim(last);
    });

    // Push breadcrumb manually -> drawBreadCrumb(["Users", "John Doe"]);
    // Credits: Philip Whitt | philip.whitt@sbcglobal.net
    //if (opt_breadCrumbs != undefined) {
    //    $.each(opt_breadCrumbs,
    //        function (index, value) {
    //            bread_crumb.append($("<li class='list-inline-item'></li>").html(value));
    //            //document.title = bread_crumb.find("li:last-child").text();
    //        });
    //}
}




/*
 * PAGE SETUP
 * Description: fire certain scripts that run through the page
 * to check for form elements, tooltip activation, popovers, etc...
 */
function pageSetUp() {

    // activate tooltips
    $("[rel=tooltip], [data-rel=tooltip]").tooltip();

    // activate popovers
    $("[rel=popover], [data-rel=popover]").popover();

    // activate popovers with hover states
    $("[rel=popover-hover], [data-rel=popover-hover]").popover({
        trigger: "hover"
    });


    // activate inline charts
    runAllCharts();

    // run form elements
    runAllForms();


}
/* ~ END: PAGE SETUP */



/*
 * ONE POP OVER THEORY
 * Keep only 1 active popover per trigger - also check and hide active popover if user clicks on document
 */
$("body").on("click",
    function (e) {
        $('[rel="popover"], [data-rel="popover"]').each(function () {
            //the 'is' for buttons that trigger popups
            //the 'has' for icons within a button that triggers a popup
            if (!$(this).is(e.target) &&
                $(this).has(e.target).length === 0 &&
                $(".popover").has(e.target).length === 0) {
                $(this).popover("hide");
            }
        });
    });
/* ~ END: ONE POP OVER THEORY */

/*
 * DELETE MODEL DATA ON HIDDEN
 * Clears the model data once it is hidden, this way you do not create duplicated data on multiple modals
 */
$("body").on("hidden.bs.modal",
    ".modal",
    function () {
        $(this).removeData("bs.modal");
    });
/* ~ END: DELETE MODEL DATA ON HIDDEN */



/*
 * INITIALIZE CHARTS
 * Description: Sparklines, PieCharts
 */

function runAllCharts() {
    /*
     * SPARKLINES
     * DEPENDENCY: js/plugins/sparkline/jquery.sparkline.min.js
     * See usage example below...
     */

    /* Usage:
     * 		<div class="sparkline-line txt-color-blue" data-fill-color="transparent" data-sparkline-height="26px">
     *			5,6,7,9,9,5,9,6,5,6,6,7,7,6,7,8,9,7
     *		</div>
     */

    if ($.fn.sparkline) {

        // variable declearations:

        var barColor,
            sparklineHeight,
            sparklineBarWidth,
            sparklineBarSpacing,
            sparklineNegBarColor,
            sparklineStackedColor,
            thisLineColor,
            thisLineWidth,
            thisFill,
            thisSpotColor,
            thisMinSpotColor,
            thisMaxSpotColor,
            thishighlightSpotColor,
            thisHighlightLineColor,
            thisSpotRadius,
            pieColors,
            pieWidthHeight,
            pieBorderColor,
            pieOffset,
            thisBoxWidth,
            thisBoxHeight,
            thisBoxRaw,
            thisBoxTarget,
            thisBoxMin,
            thisBoxMax,
            thisShowOutlier,
            thisIQR,
            thisBoxSpotRadius,
            thisBoxLineColor,
            thisBoxFillColor,
            thisBoxWhisColor,
            thisBoxOutlineColor,
            thisBoxOutlineFill,
            thisBoxMedianColor,
            thisBoxTargetColor,
            thisBulletHeight,
            thisBulletWidth,
            thisBulletColor,
            thisBulletPerformanceColor,
            thisBulletRangeColors,
            thisDiscreteHeight,
            thisDiscreteWidth,
            thisDiscreteLineColor,
            thisDiscreteLineHeight,
            thisDiscreteThrushold,
            thisDiscreteThrusholdColor,
            thisTristateHeight,
            thisTristatePosBarColor,
            thisTristateNegBarColor,
            thisTristateZeroBarColor,
            thisTristateBarWidth,
            thisTristateBarSpacing,
            thisZeroAxis,
            thisBarColor,
            sparklineWidth,
            sparklineValue,
            sparklineValueSpots1,
            sparklineValueSpots2,
            thisLineWidth1,
            thisLineWidth2,
            thisLineColor1,
            thisLineColor2,
            thisSpotRadius1,
            thisSpotRadius2,
            thisMinSpotColor1,
            thisMaxSpotColor1,
            thisMinSpotColor2,
            thisMaxSpotColor2,
            thishighlightSpotColor1,
            thisHighlightLineColor1,
            thishighlightSpotColor2,
            thisFillColor1,
            thisFillColor2;


        $('.sparkline').each(function () {
            var $this = $(this);
            var sparklineType = $this.data('sparkline-type') || 'bar';

            // BAR CHART
            if (sparklineType == 'bar') {

                barColor = $this.data('sparkline-bar-color') || $this.css('color') || '#0000f0';
                sparklineHeight = $this.data('sparkline-height') || '26px';
                sparklineBarWidth = $this.data('sparkline-barwidth') || 5;
                sparklineBarSpacing = $this.data('sparkline-barspacing') || 2;
                sparklineNegBarColor = $this.data('sparkline-negbar-color') || '#A90329';
                sparklineStackedColor = $this.data('sparkline-barstacked-color') || ["#A90329", "#0099c6", "#98AA56", "#da532c", "#4490B1", "#6E9461", "#990099", "#B4CAD3"];

                $this.sparkline('html', {
                    barColor: barColor,
                    type: sparklineType,
                    height: sparklineHeight,
                    barWidth: sparklineBarWidth,
                    barSpacing: sparklineBarSpacing,
                    stackedBarColor: sparklineStackedColor,
                    negBarColor: sparklineNegBarColor,
                    zeroAxis: 'false'
                });

            }

            // LINE CHART
            if (sparklineType == 'line') {

                sparklineHeight = $this.data('sparkline-height') || '20px';
                sparklineWidth = $this.data('sparkline-width') || '90px';
                thisLineColor = $this.data('sparkline-line-color') || $this.css('color') || '#0000f0';
                thisLineWidth = $this.data('sparkline-line-width') || 1;
                thisFill = $this.data('fill-color') || '#c0d0f0';
                thisSpotColor = $this.data('sparkline-spot-color') || '#f08000';
                thisMinSpotColor = $this.data('sparkline-minspot-color') || '#ed1c24';
                thisMaxSpotColor = $this.data('sparkline-maxspot-color') || '#f08000';
                thishighlightSpotColor = $this.data('sparkline-highlightspot-color') || '#50f050';
                thisHighlightLineColor = $this.data('sparkline-highlightline-color') || 'f02020';
                thisSpotRadius = $this.data('sparkline-spotradius') || 1.5;
                thisChartMinYRange = $this.data('sparkline-min-y') || 'undefined';
                thisChartMaxYRange = $this.data('sparkline-max-y') || 'undefined';
                thisChartMinXRange = $this.data('sparkline-min-x') || 'undefined';
                thisChartMaxXRange = $this.data('sparkline-max-x') || 'undefined';
                thisMinNormValue = $this.data('min-val') || 'undefined';
                thisMaxNormValue = $this.data('max-val') || 'undefined';
                thisNormColor = $this.data('norm-color') || '#c0c0c0';
                thisDrawNormalOnTop = $this.data('draw-normal') || false;

                $this.sparkline('html', {
                    type: 'line',
                    width: sparklineWidth,
                    height: sparklineHeight,
                    lineWidth: thisLineWidth,
                    lineColor: thisLineColor,
                    fillColor: thisFill,
                    spotColor: thisSpotColor,
                    minSpotColor: thisMinSpotColor,
                    maxSpotColor: thisMaxSpotColor,
                    highlightSpotColor: thishighlightSpotColor,
                    highlightLineColor: thisHighlightLineColor,
                    spotRadius: thisSpotRadius,
                    chartRangeMin: thisChartMinYRange,
                    chartRangeMax: thisChartMaxYRange,
                    chartRangeMinX: thisChartMinXRange,
                    chartRangeMaxX: thisChartMaxXRange,
                    normalRangeMin: thisMinNormValue,
                    normalRangeMax: thisMaxNormValue,
                    normalRangeColor: thisNormColor,
                    drawNormalOnTop: thisDrawNormalOnTop

                });

            }

            // PIE CHART
            if (sparklineType == 'pie') {

                pieColors = $this.data('sparkline-piecolor') || ["#B4CAD3", "#4490B1", "#98AA56", "#da532c", "#6E9461", "#0099c6", "#990099", "#717D8A"];
                pieWidthHeight = $this.data('sparkline-piesize') || 90;
                pieBorderColor = $this.data('border-color') || '#45494C';
                pieOffset = $this.data('sparkline-offset') || 0;

                $this.sparkline('html', {
                    type: 'pie',
                    width: pieWidthHeight,
                    height: pieWidthHeight,
                    tooltipFormat: '<span style="color: {{color}}">&#9679;</span> ({{percent.1}}%)',
                    sliceColors: pieColors,
                    borderWidth: 1,
                    offset: pieOffset,
                    borderColor: pieBorderColor
                });

            }

            // BOX PLOT
            if (sparklineType == 'box') {

                thisBoxWidth = $this.data('sparkline-width') || 'auto';
                thisBoxHeight = $this.data('sparkline-height') || 'auto';
                thisBoxRaw = $this.data('sparkline-boxraw') || false;
                thisBoxTarget = $this.data('sparkline-targetval') || 'undefined';
                thisBoxMin = $this.data('sparkline-min') || 'undefined';
                thisBoxMax = $this.data('sparkline-max') || 'undefined';
                thisShowOutlier = $this.data('sparkline-showoutlier') || true;
                thisIQR = $this.data('sparkline-outlier-iqr') || 1.5;
                thisBoxSpotRadius = $this.data('sparkline-spotradius') || 1.5;
                thisBoxLineColor = $this.css('color') || '#000000';
                thisBoxFillColor = $this.data('fill-color') || '#c0d0f0';
                thisBoxWhisColor = $this.data('sparkline-whis-color') || '#000000';
                thisBoxOutlineColor = $this.data('sparkline-outline-color') || '#303030';
                thisBoxOutlineFill = $this.data('sparkline-outlinefill-color') || '#f0f0f0';
                thisBoxMedianColor = $this.data('sparkline-outlinemedian-color') || '#f00000';
                thisBoxTargetColor = $this.data('sparkline-outlinetarget-color') || '#40a020';

                $this.sparkline('html', {
                    type: 'box',
                    width: thisBoxWidth,
                    height: thisBoxHeight,
                    raw: thisBoxRaw,
                    target: thisBoxTarget,
                    minValue: thisBoxMin,
                    maxValue: thisBoxMax,
                    showOutliers: thisShowOutlier,
                    outlierIQR: thisIQR,
                    spotRadius: thisBoxSpotRadius,
                    boxLineColor: thisBoxLineColor,
                    boxFillColor: thisBoxFillColor,
                    whiskerColor: thisBoxWhisColor,
                    outlierLineColor: thisBoxOutlineColor,
                    outlierFillColor: thisBoxOutlineFill,
                    medianColor: thisBoxMedianColor,
                    targetColor: thisBoxTargetColor

                });

            }

            // BULLET
            if (sparklineType == 'bullet') {

                var thisBulletHeight = $this.data('sparkline-height') || 'auto';
                thisBulletWidth = $this.data('sparkline-width') || 2;
                thisBulletColor = $this.data('sparkline-bullet-color') || '#ed1c24';
                thisBulletPerformanceColor = $this.data('sparkline-performance-color') || '#3030f0';
                thisBulletRangeColors = $this.data('sparkline-bulletrange-color') || ["#d3dafe", "#a8b6ff", "#7f94ff"];

                $this.sparkline('html', {

                    type: 'bullet',
                    height: thisBulletHeight,
                    targetWidth: thisBulletWidth,
                    targetColor: thisBulletColor,
                    performanceColor: thisBulletPerformanceColor,
                    rangeColors: thisBulletRangeColors

                });

            }

            // DISCRETE
            if (sparklineType == 'discrete') {

                thisDiscreteHeight = $this.data('sparkline-height') || 26;
                thisDiscreteWidth = $this.data('sparkline-width') || 50;
                thisDiscreteLineColor = $this.css('color');
                thisDiscreteLineHeight = $this.data('sparkline-line-height') || 5;
                thisDiscreteThrushold = $this.data('sparkline-threshold') || 'undefined';
                thisDiscreteThrusholdColor = $this.data('sparkline-threshold-color') || '#ed1c24';

                $this.sparkline('html', {

                    type: 'discrete',
                    width: thisDiscreteWidth,
                    height: thisDiscreteHeight,
                    lineColor: thisDiscreteLineColor,
                    lineHeight: thisDiscreteLineHeight,
                    thresholdValue: thisDiscreteThrushold,
                    thresholdColor: thisDiscreteThrusholdColor

                });

            }

            // TRISTATE
            if (sparklineType == 'tristate') {

                thisTristateHeight = $this.data('sparkline-height') || 26;
                thisTristatePosBarColor = $this.data('sparkline-posbar-color') || '#60f060';
                thisTristateNegBarColor = $this.data('sparkline-negbar-color') || '#f04040';
                thisTristateZeroBarColor = $this.data('sparkline-zerobar-color') || '#909090';
                thisTristateBarWidth = $this.data('sparkline-barwidth') || 5;
                thisTristateBarSpacing = $this.data('sparkline-barspacing') || 2;
                thisZeroAxis = $this.data('sparkline-zeroaxis') || false;

                $this.sparkline('html', {

                    type: 'tristate',
                    height: thisTristateHeight,
                    posBarColor: thisBarColor,
                    negBarColor: thisTristateNegBarColor,
                    zeroBarColor: thisTristateZeroBarColor,
                    barWidth: thisTristateBarWidth,
                    barSpacing: thisTristateBarSpacing,
                    zeroAxis: thisZeroAxis

                });

            }

            //COMPOSITE: BAR
            if (sparklineType == 'compositebar') {

                sparklineHeight = $this.data('sparkline-height') || '20px';
                sparklineWidth = $this.data('sparkline-width') || '100%';
                sparklineBarWidth = $this.data('sparkline-barwidth') || 3;
                thisLineWidth = $this.data('sparkline-line-width') || 1;
                thisLineColor = $this.data('sparkline-color-top') || '#ed1c24';
                thisBarColor = $this.data('sparkline-color-bottom') || '#333333';

                $this.sparkline($this.data('sparkline-bar-val'), {

                    type: 'bar',
                    width: sparklineWidth,
                    height: sparklineHeight,
                    barColor: thisBarColor,
                    barWidth: sparklineBarWidth
                    //barSpacing: 5

                });

                $this.sparkline($this.data('sparkline-line-val'), {

                    width: sparklineWidth,
                    height: sparklineHeight,
                    lineColor: thisLineColor,
                    lineWidth: thisLineWidth,
                    composite: true,
                    fillColor: false

                });

            }

            //COMPOSITE: LINE
            if (sparklineType == 'compositeline') {

                sparklineHeight = $this.data('sparkline-height') || '20px';
                sparklineWidth = $this.data('sparkline-width') || '90px';
                sparklineValue = $this.data('sparkline-bar-val');
                sparklineValueSpots1 = $this.data('sparkline-bar-val-spots-top') || null;
                sparklineValueSpots2 = $this.data('sparkline-bar-val-spots-bottom') || null;
                thisLineWidth1 = $this.data('sparkline-line-width-top') || 1;
                thisLineWidth2 = $this.data('sparkline-line-width-bottom') || 1;
                thisLineColor1 = $this.data('sparkline-color-top') || '#333333';
                thisLineColor2 = $this.data('sparkline-color-bottom') || '#ed1c24';
                thisSpotRadius1 = $this.data('sparkline-spotradius-top') || 1.5;
                thisSpotRadius2 = $this.data('sparkline-spotradius-bottom') || thisSpotRadius1;
                thisSpotColor = $this.data('sparkline-spot-color') || '#f08000';
                thisMinSpotColor1 = $this.data('sparkline-minspot-color-top') || '#ed1c24';
                thisMaxSpotColor1 = $this.data('sparkline-maxspot-color-top') || '#f08000';
                thisMinSpotColor2 = $this.data('sparkline-minspot-color-bottom') || thisMinSpotColor1;
                thisMaxSpotColor2 = $this.data('sparkline-maxspot-color-bottom') || thisMaxSpotColor1;
                thishighlightSpotColor1 = $this.data('sparkline-highlightspot-color-top') || '#50f050';
                thisHighlightLineColor1 = $this.data('sparkline-highlightline-color-top') || '#f02020';
                thishighlightSpotColor2 = $this.data('sparkline-highlightspot-color-bottom') ||
                    thishighlightSpotColor1;
                thisHighlightLineColor2 = $this.data('sparkline-highlightline-color-bottom') ||
                    thisHighlightLineColor1;
                thisFillColor1 = $this.data('sparkline-fillcolor-top') || 'transparent';
                thisFillColor2 = $this.data('sparkline-fillcolor-bottom') || 'transparent';

                $this.sparkline(sparklineValue, {

                    type: 'line',
                    spotRadius: thisSpotRadius1,

                    spotColor: thisSpotColor,
                    minSpotColor: thisMinSpotColor1,
                    maxSpotColor: thisMaxSpotColor1,
                    highlightSpotColor: thishighlightSpotColor1,
                    highlightLineColor: thisHighlightLineColor1,

                    valueSpots: sparklineValueSpots1,

                    lineWidth: thisLineWidth1,
                    width: sparklineWidth,
                    height: sparklineHeight,
                    lineColor: thisLineColor1,
                    fillColor: thisFillColor1

                });

                $this.sparkline($this.data('sparkline-line-val'), {

                    type: 'line',
                    spotRadius: thisSpotRadius2,

                    spotColor: thisSpotColor,
                    minSpotColor: thisMinSpotColor2,
                    maxSpotColor: thisMaxSpotColor2,
                    highlightSpotColor: thishighlightSpotColor2,
                    highlightLineColor: thisHighlightLineColor2,

                    valueSpots: sparklineValueSpots2,

                    lineWidth: thisLineWidth2,
                    width: sparklineWidth,
                    height: sparklineHeight,
                    lineColor: thisLineColor2,
                    composite: true,
                    fillColor: thisFillColor2

                });

            }

        });

    }// end if

    /*
     * EASY PIE CHARTS
     * DEPENDENCY: js/plugins/easy-pie-chart/jquery.easy-pie-chart.min.js
     * Usage: <div class="easy-pie-chart txt-color-orangeDark" data-pie-percent="33" data-pie-size="72" data-size="72">
     *			<span class="percent percent-sign">35</span>
     * 	  	  </div>
     */

    if ($.fn.easyPieChart) {

        $('.easy-pie-chart').each(function () {
            var $this = $(this);
            var barColor = $this.css('color') || $this.data('pie-color'),
                trackColor = $this.data('pie-track-color') || '#eeeeee',
                size = parseInt($this.data('pie-size')) || 25;


            $this.easyPieChart({

                barColor: barColor,
                trackColor: trackColor,
                scaleColor: false,
                lineCap: 'butt',
                lineWidth: parseInt(size / 8.5),
                animate: 1500,
                rotate: -90,
                size: size,
                onStep: function ($this, size, barColor) {

                    $(this.el).find('.percent').text(Math.round(barColor));
                }

            });
        });

    } // end if

}

/* ~ END: INITIALIZE CHARTS */


function runAllForms() {

    /*
     * BOOTSTRAP SLIDER PLUGIN
     * Usage:
     * Dependency: js/plugin/bootstrap-slider
     */
    if ($.fn.slider) {
        $('.slider').slider();
    }


    //VMasker(document.querySelector(".valor")).maskMoney({
    //    // Decimal precision -> "90"
    //    precision: 2,
    //    // Decimal separator -> ",90"
    //    separator: ',',
    //    // Number delimiter -> "12.345.678"
    //    delimiter: '.',
    //    // Money unit -> "R$ 12.345.678,90"
    //    unit: '',
    //    // Money unit -> "12.345.678,90 R$"
    //    suffixUnit: '',
    //    // Force type only number instead decimal,
    //    // masking decimals with ",00"
    //    // Zero cents -> "R$ 1.234.567.890,00"
    //    zeroCents: false
    //});

    $(".valor").on("enter", function () {
        alert("test");
    });

    $('[data-decimal]').each(function () {
        var $this = $(this);

        var zero = $this.attr('data-zeroCents') || 'false';
      
       

        $this.maskMoney();

        //$this.maskMoney({
        //    // Decimal precision -> "90"
        //    precision: 2,
        //    // Decimal separator -> ",90"
        //    separator: ',',
        //    // Number delimiter -> "12.345.678"
        //    delimiter: '.',
        //    // Money unit -> "R$ 12.345.678,90"
        //    unit: 'R$',
        //    // Money unit -> "12.345.678,90 R$"
        //    suffixUnit: 'R$',
        //    // Force type only number instead decimal,
        //    // masking decimals with ",00"
        //    // Zero cents -> "R$ 1.234.567.890,00"
        //    zeroCents: true
        //});
        //$this.on("keypress keyup blur", function (event) {
        //    //this.value = this.value.replace(/[^0-9\.]/g,'');
        //    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        //    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        //        event.preventDefault();
        //    }
        //});
    });
    /*
     * SELECT2 PLUGIN
     * Usage:
     * Dependency: js/plugin/select2/
     */
    if ($.fn.select2) {
        $('.select2').each(function () {
            var $this = $(this);
            var width = $this.attr('data-select-width') || '100%';
            //, _showSearchInput = $this.attr('data-select-search') === 'true';
            $this.select2({
                //showSearchInput : _showSearchInput,
                allowClear: true,
                width: width
            });
        });
    }

    /*
     * MASKING
     * Dependency: js/plugin/masked-input/
     */
    if ($.fn.mask) {
        $('[data-mask]').each(function () {

            var $this = $(this);
            var mask = $this.attr('data-mask') || 'error...', mask_placeholder = $this.attr('data-mask-placeholder') || 'X';

            $this.mask(mask, {
                placeholder: mask_placeholder
            });
        });
    }

    /*
     * AUTOCOMPLETE
     * Dependency: js/jqui
     */
    if ($.fn.autocomplete) {
        $('[data-autocomplete]').each(function () {

            var $this = $(this);
            var availableTags = $this.data('autocomplete') || ["The", "Quick", "Brown", "Fox", "Jumps", "Over", "Three", "Lazy", "Dogs"];

            $this.autocomplete({
                source: availableTags
            });
        });
    }


    /*
     * JQUERY UI DATE
     * Dependency: js/libs/jquery-ui-1.10.3.min.js
     * Usage: <input class="datepicker" />
     */
    if ($.fn.datepicker) {
        $('.datepicker').each(function () {

            var $this = $(this);
            var dataDateFormat = $this.attr('data-dateformat') || 'dd/mm/yy';

            $this.datepicker({
                dateFormat: dataDateFormat,
                dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                nextText: 'Próximo',
                prevText: 'Anterior'
                //prevText : '<i class="fa fa-chevron-left"></i>',
                //nextText : '<i class="fa fa-chevron-right"></i>',
            });
        });
    }

    /*
     * AJAX BUTTON LOADING TEXT
     * Usage: <button type="button" data-loading-text="Loading..." class="btn btn-xs btn-default ajax-refresh"> .. </button>
     */
    $('button[data-loading-text]').on('click', function () {
        var btn = $(this);
        btn.button('loading');
        setTimeout(function () {
            btn.button('reset');
        }, 3000);
    });

}

/* ~ END: INITIALIZE FORMS */



/*
 * SETUP DATATABLES
 */
class setupDataTable {
    printData(table) {
        var divToPrint = document.getElementById(table);
        var newWin = window.open("");
        newWin.document.write(divToPrint.outerHTML);
        newWin.print();
        newWin.close();
         
    }

    tableInit(table, pagelen, dateCol, defsortCol = 0, direction = 'asc', currencyCol) {
        var objeto = '#' + table;

        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "date-uk-pre": function (a) {
                if (a == null || a == "") {
                    return 0;
                }
                var ukDatea = a.split('/');
               
                return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
            },

            "date-uk-asc": function (a, b) {
                return ((a < b) ? -1 : ((a > b) ? 1 : 0));
            },

            "date-uk-desc": function (a, b) {
                return ((a < b) ? 1 : ((a > b) ? -1 : 0));
            }
        });

        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "currency-pre": function (a) {
                a = (a === "-") ? 0 : a.replace(/[^\d\-\,]/g, "").replace(/,/, ".");
                
                return parseFloat(a);
            },

            "currency-asc": function (a, b) {
                return a - b;
            },

            "currency-desc": function (a, b) {
                return b - a;
            }
        });

        var otable = $(objeto).dataTable({
            language: {
                url: '//cdn.datatables.net/plug-ins/1.10.12/i18n/Portuguese-Brasil.json',
            } ,
            "pageLength": pagelen || 10,
            "autoWidth": true,
            "searching": false,
            "order": [[defsortCol, direction]],
            "columnDefs": [
                { targets: 'no-sort', "orderable": false },
                { type: 'date-uk', targets: dateCol },
                { type: 'currency', targets: currencyCol }
                //"targets": 'no-sort', "orderable": false,
                //"targets": 'date', type: 'date-uk', targets: datecol
                //type: 'date-uk', targets: 1 
            ]
        });



    }

    tableInitExport(table, pagelen, dateCol, defsortCol = 0, direction = 'asc', currencyCol) {
        var objeto = '#' + table;

        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "date-uk-pre": function (a) {
                if (a == null || a == "") {
                    return 0;
                }
                var ukDatea = a.split('/');

                return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
            },

            "date-uk-asc": function (a, b) {
                return ((a < b) ? -1 : ((a > b) ? 1 : 0));
            },

            "date-uk-desc": function (a, b) {
                return ((a < b) ? 1 : ((a > b) ? -1 : 0));
            }
        });

        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "currency-pre": function (a) {
                a = (a === "-") ? 0 : a.replace(/[^\d\-\,]/g, "").replace(/,/, ".");

                return parseFloat(a);
            },

            "currency-asc": function (a, b) {
                return a - b;
            },

            "currency-desc": function (a, b) {
                return b - a;
            }
        });

        var otable = $(objeto).dataTable({
            language: {
                url: '//cdn.datatables.net/plug-ins/1.10.12/i18n/Portuguese-Brasil.json',
            },
            dom: 'Bfrtip',
            buttons: [
                'excel', 'pdf', {
                    extend: 'print',
                    customize: function (win) {
                        $(win.document.body)
                            .css('font-size', '10pt')
                            .prepend(
                            '<img src="http://datatables.net/media/images/logo-fade.png" style="position:absolute; top:0; left:0;" />'
                            );

                        $(win.document.body).find('table')
                            .addClass('compact')
                            .css('font-size', 'inherit');
                    }
                }
            ],
            "pageLength": pagelen || 10,
            "autoWidth": true,
            "searching": false,
            "order": [[defsortCol, direction]],
            "columnDefs": [
                { targets: 'no-sort', "orderable": false },
                { type: 'date-uk', targets: dateCol },
                { type: 'currency', targets: currencyCol }
                //"targets": 'no-sort', "orderable": false,
                //"targets": 'date', type: 'date-uk', targets: datecol
                //type: 'date-uk', targets: 1 
            ]
        });



    }
}
var setupTable = new setupDataTable();
/* ~ END: SETUP DATATABLES */




/*
 * LOAD SCRIPTS
 * Usage:
 * Define function = myPrettyCode ()...
 * loadScript("js/my_lovely_script.js", myPrettyCode);
 */

function loadScript(scriptName, callback) {

    if (!jsArray[scriptName]) {
        var promise = jQuery.Deferred();

        // adding the script tag to the head as suggested before
        var body = document.getElementsByTagName("body")[0],
            script = document.createElement("script");
        script.type = "text/javascript";
        script.src = scriptName;

        // then bind the event to the callback function
        // there are several events for cross browser compatibility
        script.onload = function () {
            promise.resolve();
        };

        // fire the loading
        body.appendChild(script);

        // clear DOM reference
        //body = null;
        //script = null;

        jsArray[scriptName] = promise.promise();

    } else if (debugState)
        root.root.console.log("This script was already loaded %c: " + scriptName, debugStyle_warning);

    jsArray[scriptName].then(function () {
        if (typeof callback === "function")
            callback();
    });
}





function runAllCharsts() {
    if ($.fn.sparkline) {
        var v, n, r, y, p, w, u, f, b, i, k, d, g, nt, tt, it, e, rt, ut, ft, et, ot, st, ht, ct, lt, at, vt, yt, pt, wt, bt, kt, dt, gt, ni, ti, ii, ri, ui, fi, ei, oi, si, hi, ci, sr, li, ai, vi, yi, pi, o, t, wi, bi, ki, di, gi, nr, tr, s, ir, h, c, rr, ur, l, a, fr, er, or;
        $(".sparkline:not(:has(>canvas))").each(function () {
            var hr = $(this),
                cr = hr.data("sparkline-type") || "bar",
                lr;
            cr == "bar" && (v = hr.data("sparkline-bar-color") || hr.css("color") || "#0000f0", n = hr.data("sparkline-height") || "26px", r = hr.data("sparkline-barwidth") || 5, y = hr.data("sparkline-barspacing") || 2, p = hr.data("sparkline-negbar-color") || "#A90329", w = hr.data("sparkline-barstacked-color") || ["#A90329", "#0099c6", "#98AA56", "#da532c", "#4490B1", "#6E9461", "#990099", "#B4CAD3"], hr.sparkline("html", {
                barColor: v,
                type: cr,
                height: n,
                barWidth: r,
                barSpacing: y,
                stackedBarColor: w,
                negBarColor: p,
                zeroAxis: "false"
            }), hr = null);
            cr == "line" && (n = hr.data("sparkline-height") || "20px", t = hr.data("sparkline-width") || "90px", u = hr.data("sparkline-line-color") || hr.css("color") || "#0000f0", f = hr.data("sparkline-line-width") || 1, b = hr.data("fill-color") || "#c0d0f0", i = hr.data("sparkline-spot-color") || "#f08000", k = hr.data("sparkline-minspot-color") || "#ed1c24", d = hr.data("sparkline-maxspot-color") || "#f08000", g = hr.data("sparkline-highlightspot-color") || "#50f050", nt = hr.data("sparkline-highlightline-color") || "f02020", tt = hr.data("sparkline-spotradius") || 1.5, thisChartMinYRange = hr.data("sparkline-min-y") || "undefined", thisChartMaxYRange = hr.data("sparkline-max-y") || "undefined", thisChartMinXRange = hr.data("sparkline-min-x") || "undefined", thisChartMaxXRange = hr.data("sparkline-max-x") || "undefined", thisMinNormValue = hr.data("min-val") || "undefined", thisMaxNormValue = hr.data("max-val") || "undefined", thisNormColor = hr.data("norm-color") || "#c0c0c0", thisDrawNormalOnTop = hr.data("draw-normal") || !1, hr.sparkline("html", {
                type: "line",
                width: t,
                height: n,
                lineWidth: f,
                lineColor: u,
                fillColor: b,
                spotColor: i,
                minSpotColor: k,
                maxSpotColor: d,
                highlightSpotColor: g,
                highlightLineColor: nt,
                spotRadius: tt,
                chartRangeMin: thisChartMinYRange,
                chartRangeMax: thisChartMaxYRange,
                chartRangeMinX: thisChartMinXRange,
                chartRangeMaxX: thisChartMaxXRange,
                normalRangeMin: thisMinNormValue,
                normalRangeMax: thisMaxNormValue,
                normalRangeColor: thisNormColor,
                drawNormalOnTop: thisDrawNormalOnTop
            }), hr = null);
            cr == "pie" && (it = hr.data("sparkline-piecolor") || ["#B4CAD3", "#4490B1", "#98AA56", "#da532c", "#6E9461", "#0099c6", "#990099", "#717D8A"], e = hr.data("sparkline-piesize") || 90, rt = hr.data("border-color") || "#45494C", ut = hr.data("sparkline-offset") || 0, hr.sparkline("html", {
                type: "pie",
                width: e,
                height: e,
                tooltipFormat: '<span style="color: {{color}}">&#9679;<\/span> ({{percent.1}}%)',
                sliceColors: it,
                borderWidth: 1,
                offset: ut,
                borderColor: rt
            }), hr = null);
            cr == "box" && (ft = hr.data("sparkline-width") || "auto", et = hr.data("sparkline-height") || "auto", ot = hr.data("sparkline-boxraw") || !1, st = hr.data("sparkline-targetval") || "undefined", ht = hr.data("sparkline-min") || "undefined", ct = hr.data("sparkline-max") || "undefined", lt = hr.data("sparkline-showoutlier") || !0, at = hr.data("sparkline-outlier-iqr") || 1.5, vt = hr.data("sparkline-spotradius") || 1.5, yt = hr.css("color") || "#000000", pt = hr.data("fill-color") || "#c0d0f0", wt = hr.data("sparkline-whis-color") || "#000000", bt = hr.data("sparkline-outline-color") || "#303030", kt = hr.data("sparkline-outlinefill-color") || "#f0f0f0", dt = hr.data("sparkline-outlinemedian-color") || "#f00000", gt = hr.data("sparkline-outlinetarget-color") || "#40a020", hr.sparkline("html", {
                type: "box",
                width: ft,
                height: et,
                raw: ot,
                target: st,
                minValue: ht,
                maxValue: ct,
                showOutliers: lt,
                outlierIQR: at,
                spotRadius: vt,
                boxLineColor: yt,
                boxFillColor: pt,
                whiskerColor: wt,
                outlierLineColor: bt,
                outlierFillColor: kt,
                medianColor: dt,
                targetColor: gt
            }), hr = null);
            cr == "bullet" && (lr = hr.data("sparkline-height") || "auto", ni = hr.data("sparkline-width") || 2, ti = hr.data("sparkline-bullet-color") || "#ed1c24", ii = hr.data("sparkline-performance-color") || "#3030f0", ri = hr.data("sparkline-bulletrange-color") || ["#d3dafe", "#a8b6ff", "#7f94ff"], hr.sparkline("html", {
                type: "bullet",
                height: lr,
                targetWidth: ni,
                targetColor: ti,
                performanceColor: ii,
                rangeColors: ri
            }), hr = null);
            cr == "discrete" && (ui = hr.data("sparkline-height") || 26, fi = hr.data("sparkline-width") || 50, ei = hr.css("color"), oi = hr.data("sparkline-line-height") || 5, si = hr.data("sparkline-threshold") || "undefined", hi = hr.data("sparkline-threshold-color") || "#ed1c24", hr.sparkline("html", {
                type: "discrete",
                width: fi,
                height: ui,
                lineColor: ei,
                lineHeight: oi,
                thresholdValue: si,
                thresholdColor: hi
            }), hr = null);
            cr == "tristate" && (ci = hr.data("sparkline-height") || 26, sr = hr.data("sparkline-posbar-color") || "#60f060", li = hr.data("sparkline-negbar-color") || "#f04040", ai = hr.data("sparkline-zerobar-color") || "#909090", vi = hr.data("sparkline-barwidth") || 5, yi = hr.data("sparkline-barspacing") || 2, pi = hr.data("sparkline-zeroaxis") || !1, hr.sparkline("html", {
                type: "tristate",
                height: ci,
                posBarColor: o,
                negBarColor: li,
                zeroBarColor: ai,
                barWidth: vi,
                barSpacing: yi,
                zeroAxis: pi
            }), hr = null);
            cr == "compositebar" && (n = hr.data("sparkline-height") || "20px", t = hr.data("sparkline-width") || "100%", r = hr.data("sparkline-barwidth") || 3, f = hr.data("sparkline-line-width") || 1, u = hr.data("data-sparkline-linecolor") || "#ed1c24", o = hr.data("data-sparkline-barcolor") || "#333333", hr.sparkline(hr.data("sparkline-bar-val"), {
                type: "bar",
                width: t,
                height: n,
                barColor: o,
                barWidth: r
            }), hr.sparkline(hr.data("sparkline-line-val"), {
                width: t,
                height: n,
                lineColor: u,
                lineWidth: f,
                composite: !0,
                fillColor: !1
            }), hr = null);
            cr == "compositeline" && (n = hr.data("sparkline-height") || "20px", t = hr.data("sparkline-width") || "90px", wi = hr.data("sparkline-bar-val"), bi = hr.data("sparkline-bar-val-spots-top") || null, ki = hr.data("sparkline-bar-val-spots-bottom") || null, di = hr.data("sparkline-line-width-top") || 1, gi = hr.data("sparkline-line-width-bottom") || 1, nr = hr.data("sparkline-color-top") || "#333333", tr = hr.data("sparkline-color-bottom") || "#ed1c24", s = hr.data("sparkline-spotradius-top") || 1.5, ir = hr.data("sparkline-spotradius-bottom") || s, i = hr.data("sparkline-spot-color") || "#f08000", h = hr.data("sparkline-minspot-color-top") || "#ed1c24", c = hr.data("sparkline-maxspot-color-top") || "#f08000", rr = hr.data("sparkline-minspot-color-bottom") || h, ur = hr.data("sparkline-maxspot-color-bottom") || c, l = hr.data("sparkline-highlightspot-color-top") || "#50f050", a = hr.data("sparkline-highlightline-color-top") || "#f02020", fr = hr.data("sparkline-highlightspot-color-bottom") || l, thisHighlightLineColor2 = hr.data("sparkline-highlightline-color-bottom") || a, er = hr.data("sparkline-fillcolor-top") || "transparent", or = hr.data("sparkline-fillcolor-bottom") || "transparent", hr.sparkline(wi, {
                type: "line",
                spotRadius: s,
                spotColor: i,
                minSpotColor: h,
                maxSpotColor: c,
                highlightSpotColor: l,
                highlightLineColor: a,
                valueSpots: bi,
                lineWidth: di,
                width: t,
                height: n,
                lineColor: nr,
                fillColor: er
            }), hr.sparkline(hr.data("sparkline-line-val"), {
                type: "line",
                spotRadius: ir,
                spotColor: i,
                minSpotColor: rr,
                maxSpotColor: ur,
                highlightSpotColor: fr,
                highlightLineColor: thisHighlightLineColor2,
                valueSpots: ki,
                lineWidth: gi,
                width: t,
                height: n,
                lineColor: tr,
                composite: !0,
                fillColor: or
            }), hr = null)
        })
    }
    $.fn.easyPieChart && $(".easy-pie-chart").each(function () {
        var n = $(this),
            i = n.css("color") || n.data("pie-color"),
            r = n.data("pie-track-color") || "rgba(0,0,0,0.04)",
            t = parseInt(n.data("pie-size")) || 25;
        n.easyPieChart({
            barColor: i,
            trackColor: r,
            scaleColor: !1,
            lineCap: "butt",
            lineWidth: parseInt(t / 8.5),
            animate: 1500,
            rotate: -90,
            size: t,
            onStep: function (n, t, i) {
                $(this.el).find(".percent").text(Math.round(i))
            }
        });
        n = null
    })
}
