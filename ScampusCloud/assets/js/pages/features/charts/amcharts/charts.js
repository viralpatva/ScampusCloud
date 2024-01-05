"use strict";

// Class definition
var KTamChartsChartsDemo = function () {
    // private functions
    var Nooflicensebyreseller = function (arr_licensebyreseller) {
        var chartData = {
            "1995": arr_licensebyreseller
        };
        var currentYear = 1995;
        var chart = AmCharts.makeChart("kt_amcharts_licensebyreseller", {
            "type": "pie",
            "theme": "light",
            "dataProvider": arr_licensebyreseller,
            "valueField": "NoOfLicense",
            "titleField": "ResellerCompany",
            "startDuration": 0,
            "innerRadius": 30,
            "pullOutRadius": 0,
            "marginTop": 0,
            "labelText": "[[title]]: [[value]]",
            "titles": [{
                "text": ""
            }],
            "allLabels": [{
                "y": "54%",
                "align": "center",
                "size": 25,
                "bold": true,
                "text": "",
                "color": "#555"
            }, {
                "y": "49%",
                "align": "center",
                "size": 15,
                "text": "",
                "color": "#555"
            }],
            "listeners": [{
                "event": "init",
                "method": function (e) {
                    var chart = e.chart;
                    function getCurrentData() {
                        var data = chartData[currentYear];
                        currentYear++;
                        if (currentYear > 2014)
                            currentYear = 1995;
                        return data;
                    }

                    function loop() {
                        //chart.allLabels[0].text = currentYear;
                        var data = getCurrentData();
                        chart.animateData(data, {
                            duration: 1000
                            //complete: function () {
                            //    setTimeout(loop, 3000);
                            //}
                        });
                    }

                    loop();


                }
            }],
            "export": {
                "enabled": true
            },
            "responsive": {
                "enabled": true
            }
        });
    };

    var Nooflicensebyproduct = function (arr_licensebyproduct) {
        var chart = AmCharts.makeChart("kt_amcharts_licensebyproduct", {
            "type": "pie",
            "theme": "light",
            "dataProvider": arr_licensebyproduct,
            "pullOutRadius": 20,
            "marginTop": 10,
            "valueField": "NoOfLicense",
            "titleField": "ProductName",
            "labelText": "[[title]]: [[value]]",
            "balloon": {
                "fixedPosition": true
            },
            //"responsive": {
            //    "enabled": true
            //},
            //"legend": {
            //    "position": "bottom",
            //    "markerSize": 12,
            //    "valueWidth": 10,
            //},
            //"export": {
            //    "enabled": true
            //},
        });
    }

    //var Nooflicensebyclient = function (arr_licensebyclient) {
    //    var chartData = {
    //        "1995": arr_licensebyclient
    //    };
    //    var currentYear = 1995;
    //    var chart = AmCharts.makeChart("kt_amcharts_licensebyclient", {
    //        "type": "pie",
    //        "theme": "light",
    //        "dataProvider": arr_licensebyclient,
    //        "valueField": "NoOfLicense",
    //        "titleField": "Client",
    //        "startDuration": 0,
    //        "innerRadius": 30,
    //        "pullOutRadius": 0,
    //        "marginTop": 0,
    //        "labelText": "[[title]]: [[value]]",
    //        "titles": [{
    //            "text": ""
    //        }],
    //        "allLabels": [{
    //            "y": "54%",
    //            "align": "center",
    //            "size": 25,
    //            "bold": true,
    //            "text": "",
    //            "color": "#555"
    //        }, {
    //            "y": "49%",
    //            "align": "center",
    //            "size": 15,
    //            "text": "",
    //            "color": "#555"
    //        }],
    //        "listeners": [{
    //            "event": "init",
    //            "method": function (e) {
    //                var chart = e.chart;

    //                function getCurrentData() {
    //                    var data = chartData[currentYear];
    //                    currentYear++;
    //                    if (currentYear > 2014)
    //                        currentYear = 1995;
    //                    return data;
    //                }

    //                function loop() {
    //                    //chart.allLabels[0].text = currentYear;
    //                    var data = getCurrentData();
    //                    chart.animateData(data, {
    //                        duration: 1000
    //                        //complete: function () {
    //                        //    setTimeout(loop, 3000);
    //                        //}
    //                    });
    //                }

    //                loop();


    //            }
    //        }],
    //        "export": {
    //            "enabled": true
    //        },
    //        "responsive": {
    //            "enabled": true
    //        }
    //    });
    //};

    var Nooflicensebyclient = function (arr_licensebyclient) {
        var chart = AmCharts.makeChart("kt_amcharts_licensebyclient", {
            "type": "serial",
            "theme": "light",
            "handDrawn": false,
            "handDrawScatter": 3,
            //"legend": {
            //    "useGraphSettings": true,
            //    "markerSize": 12,
            //    "valueWidth": 0,
            //    "verticalGap": 0
            //},
            "dataProvider": arr_licensebyclient,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": false,
                "position": "top",
                "axisAlpha": 0
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "title": "itemname",
                "type": "column",
                "fillAlphas": 0.8,
                "valueField": "NoOfLicense"
            }
            ],
            "rotate": true,
            "categoryField": "Client",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
                "enabled": false
            }

        });
    }

    var Noofsalesbypaymenttype = function (arr_salesbypaymenttype) {
        var chart = AmCharts.makeChart("kt_amcharts_salesbypaymenttype", {
            "type": "pie",
            "theme": "light",
            "dataProvider": arr_salesbypaymenttype,
            "pullOutRadius": 20,
            "marginTop": 10,
            "valueField": "amount",
            "titleField": "display",
            "balloon": {
                "fixedPosition": true
            },
            //"responsive": {
            //    "enabled": true
            //},
            //"legend": {
            //    "position": "bottom",
            //    "markerSize": 12,
            //    "valueWidth": 10,
            //},
            "balloonText": "[[title]]: <b>$[[value]]</b>",
            //"export": {
            //    "enabled": false
            //},

        });
    }

    var TopSellingItems = function (arr_topsellingitem) {
        var chart = AmCharts.makeChart("kt_amcharts_sellingitems", {
            "type": "serial",
            "theme": "light",
            "handDrawn": false,
            "handDrawScatter": 3,
            //"legend": {
            //    "useGraphSettings": true,
            //    "markerSize": 12,
            //    "valueWidth": 0,
            //    "verticalGap": 0
            //},
            "dataProvider": arr_topsellingitem,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": false,
                "position": "top",
                "axisAlpha": 0
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "title": "itemname",
                "type": "column",
                "fillAlphas": 0.8,
                "valueField": "quantity"
            }
            ],
            "rotate": true,
            "categoryField": "Item_Name",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
                "enabled": false
            }

        });
    }

    var Noofsalesbyordertype = function (arr_salesbyordertype) {

        var chart = AmCharts.makeChart("kt_amcharts_salesbyordertype", {
            "rtl": KTUtil.isRTL(),
            "type": "serial",
            "theme": "light",
            //"legend": {
            //    "useGraphSettings": true,
            //    "markerSize": 12,
            //    "valueWidth": 0,
            //    "verticalGap": 0
            //},
            "dataProvider": arr_salesbyordertype,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>$[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "incomeammount",
                "title": "name",
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "name",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20
            },
            "export": {
                "enabled": false
            }

        });

    }

    var Last10DaySalesSummary = function (arr_10DaySalesSummary) {

        var chart = AmCharts.makeChart("kt_amcharts_salessummary", {
            "rtl": KTUtil.isRTL(),
            "type": "serial",
            "theme": "light",

            //"legend": {
            //    "useGraphSettings": true,
            //    "markerSize": 12,
            //    "valueWidth": 0,
            //    "verticalGap": 0
            //},

            "dataProvider": arr_10DaySalesSummary,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "GrossSales in [[category]]: <b>$[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "GrossSales",
                "title": "Date",
            },
            {
                "id": "graph2",
                "balloonText": "<span style='font-size:12px;color:black;'>Net Sales in [[category]]:<br><span style='font-size:20px;'>$[[value]]</span></span>",
                "bullet": "round",
                "dashLengthField": "dashLengthColumn",
                "lineThickness": 3,
                "bulletSize": 7,
                "bulletBorderAlpha": 1,
                "bulletColor": "#FFFFFF",
                "useLineColorForBulletBorder": true,
                "bulletBorderThickness": 3,
                "fillAlphas": 0,
                "lineAlpha": 1,
                "title": "NetSales",
                "valueField": "NetSales"
            }
            ],
            //"chartCursor": {
            //    "categoryBalloonEnabled": false,
            //    "cursorAlpha": 0,
            //    "zoomable": false
            //},
            "categoryField": "Date",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20
            },
            "export": {
                "enabled": false
            }

        });

    }

    var SalesByCategory = function (arr_salesByCategory) {
        var chart = AmCharts.makeChart("kt_amcharts_salesByCategory", {
            "type": "serial",
            "theme": "light",
            "handDrawn": false,
            "handDrawScatter": 3,
            //"legend": {
            //    "useGraphSettings": true,
            //    "markerSize": 12,
            //    "valueWidth": 0,
            //    "verticalGap": 0
            //},
            "dataProvider": arr_salesByCategory,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": false,
                "position": "top",
                "axisAlpha": 0
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "title": "Category",
                "type": "column",
                "fillAlphas": 0.8,
                "valueField": "quantity"
            }
            ],
            "rotate": true,
            "categoryField": "Category",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
                "enabled": false
            }

        });
    }

    var CouponSalesSummary = function (arr_couponSalesSummary) {
        var chart = AmCharts.makeChart("kt_amcharts_couponSalesSummary", {
            "type": "serial",
            "theme": "light",
            "handDrawn": false,
            "handDrawScatter": 3,
            //"legend": {
            //    "useGraphSettings": true,
            //    "markerSize": 12,
            //    "valueWidth": 0,
            //    "verticalGap": 0
            //},
            "dataProvider": arr_couponSalesSummary,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": false,
                "position": "top",
                "axisAlpha": 0,
                "title": "$ (Discounted Amount)"
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>$[[value]]</b>",
                "title": "CouponName",
                "type": "column",
                "fillAlphas": 0.8,
                "valueField": "Amount"
            }
            ],
            "rotate": true,
            "categoryField": "CouponName",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
                "enabled": false
            }

        });
    }

    var TopSellingDepartments = function (arr_topsellingdepartment) {
        var chart = AmCharts.makeChart("kt_amcharts_sellingDepartment", {
            "type": "serial",
            "theme": "light",
            "handDrawn": false,
            "handDrawScatter": 3,
            //"legend": {
            //    "useGraphSettings": true,
            //    "markerSize": 12,
            //    "valueWidth": 0,
            //    "verticalGap": 0
            //},
            "dataProvider": arr_topsellingdepartment,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": false,
                "position": "top",
                "axisAlpha": 0
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "title": "Department",
                "type": "column",
                "fillAlphas": 0.8,
                "valueField": "quantity"
            }
            ],
            "rotate": true,
            "categoryField": "Department",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
                "enabled": false
            }

        });
    }
    var TopPayoutByCashier = function (arr_TopPayoutByCashier) {
        var chart = AmCharts.makeChart("kt_amcharts_TopPayoutByCashier", {
            "type": "serial",
            "theme": "light",
            "handDrawn": false,
            "handDrawScatter": 3,
            "dataProvider": arr_TopPayoutByCashier,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": false,
                "position": "top",
                "axisAlpha": 0
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "title": "Cashier",
                "type": "column",
                "fillAlphas": 0.8,
                "valueField": "Amount"
            }
            ],
            "rotate": true,
            "categoryField": "Cashier",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
                "enabled": false
            }

        });
    }
    var TopSellingBrand = function (arr_topsellingbrand) {
        var chart = AmCharts.makeChart("kt_amcharts_sellingbrand", {
            "type": "serial",
            "theme": "light",
            "handDrawn": false,
            "handDrawScatter": 3,
            "dataProvider": arr_topsellingbrand,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": false,
                "position": "top",
                "axisAlpha": 0
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "title": "itemname",
                "type": "column",
                "fillAlphas": 0.8,
                "valueField": "quantity"
            }
            ],
            "rotate": true,
            "categoryField": "Description",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
                "enabled": false
            }

        });
    }

    var Noofclientbybusinesstype = function (arr_clientbybusinesstype) {
        var chart = AmCharts.makeChart("kt_amcharts_clientbybusinesstype", {
            "type": "pie",
            "theme": "light",
            "dataProvider": arr_clientbybusinesstype,
            "pullOutRadius": 20,
            "marginTop": 10,
            "valueField": "NoOfClient",
            "titleField": "BusinessType",
            "balloon": {
                "fixedPosition": true
            },
            "labelText": "[[title]]: [[value]]",
            //"responsive": {
            //    "enabled": true
            //},
            //"legend": {
            //    "position": "bottom",
            //    "markerSize": 12,
            //    "valueWidth": 10,
            //},
            //"export": {
            //    "enabled": true
            //},
        });
    }

    var Noofclientbyreseller = function (arr_clientbyreseller) {
        var chart = AmCharts.makeChart("kt_amcharts_clientbyreseller", {
            "type": "pie",
            "theme": "light",
            "dataProvider": arr_clientbyreseller,
            "pullOutRadius": 0,
            "marginTop": 10,
            "valueField": "NoOfClient",
            "titleField": "ResellerCompany",
            "labelText": "[[title]]: [[value]]",
            "balloon": {
                "fixedPosition": true
            },
            //"responsive": {
            //    "enabled": true
            //},
            //"legend": {
            //    "position": "bottom",
            //    "markerSize": 12,
            //    "valueWidth": 10,
            //},
            //"export": {
            //    "enabled": true
            //},
        });
    }

    return {
        // public functions
        initnooflicensebyreseller: function (arr_licensebyreseller) {
            if (typeof arr_licensebyreseller === "undefined" || arr_licensebyreseller === null || arr_licensebyreseller.length === 0 || arr_licensebyreseller === "NaN") {
                arr_licensebyreseller = null;
            }
            Nooflicensebyreseller(arr_licensebyreseller);
        },
        initlicensebyproduct: function (arr_licensebyproduct) {
            if (typeof arr_licensebyproduct === "undefined" || arr_licensebyproduct === null || arr_licensebyproduct.length === 0 || arr_licensebyproduct === "NaN") {
                arr_licensebyproduct = null;
            }
            Nooflicensebyproduct(arr_licensebyproduct);
        },
        initclientbybusinesstype: function (arr_clientbybusinesstype) {

            if (typeof arr_clientbybusinesstype === "undefined" || arr_clientbybusinesstype === null || arr_clientbybusinesstype.length === 0 || arr_clientbybusinesstype === "NaN") {
                arr_clientbybusinesstype = null;
            }
            Noofclientbybusinesstype(arr_clientbybusinesstype);
        },
        initlicensebyclient: function (arr_licensebyclient) {
            if (typeof arr_licensebyclient === "undefined" || arr_licensebyclient === null || arr_licensebyclient.length === 0 || arr_licensebyclient === "NaN") {
                arr_licensebyclient = null;
            }
            Nooflicensebyclient(arr_licensebyclient);
        },
        initsalesbyordertype: function (arr_salesbyordertype) {
            if (typeof arr_salesbyordertype === "undefined" || arr_salesbyordertype === null || arr_salesbyordertype.length === 0 || arr_salesbyordertype === "NaN") {
                arr_salesbyordertype = null;
            }
            Noofsalesbyordertype(arr_salesbyordertype);
        },
        initsalesbypaymenttype: function (arr_salesbypaymenttype) {
            if (typeof arr_salesbypaymenttype === "undefined" || arr_salesbypaymenttype === null || arr_salesbypaymenttype.length === 0 || arr_salesbypaymenttype === "NaN") {
                arr_salesbypaymenttype = null;
            }
            Noofsalesbypaymenttype(arr_salesbypaymenttype);
        },
        inittopsellingitem: function (arr_topsellingitem) {
            if (typeof arr_topsellingitem === "undefined" || arr_topsellingitem === null || arr_topsellingitem.length === 0 || arr_topsellingitem === "NaN") {
                arr_topsellingitem = null;
            }
            TopSellingItems(arr_topsellingitem);
        },
        initsalessummary: function (arr_10DaySalesSummary) {
            if (typeof arr_10DaySalesSummary === "undefined" || arr_10DaySalesSummary === null || arr_10DaySalesSummary.length === 0 || arr_10DaySalesSummary === "NaN") {
                arr_10DaySalesSummary = null;
            }
            Last10DaySalesSummary(arr_10DaySalesSummary);
        },
        inittopsellingdepartments: function (arr_topsellingdepartment) {
            if (typeof arr_topsellingdepartment === "undefined" || arr_topsellingdepartment === null || arr_topsellingdepartment.length === 0 || arr_topsellingdepartment === "NaN") {
                arr_topsellingdepartment = null;
            }
            TopSellingDepartments(arr_topsellingdepartment);
        },
        initTopPayoutByCashier: function (arr_TopPayoutByCashier) {
            if (typeof arr_TopPayoutByCashier === "undefined" || arr_TopPayoutByCashier === null || arr_TopPayoutByCashier === 0 || arr_TopPayoutByCashier === "NaN") {
                arr_TopPayoutByCashier = null;
            }
            TopPayoutByCashier(arr_TopPayoutByCashier);
        },
        initsalesbycategory: function (arr_salesbycategory) {
            if (typeof arr_salesbycategory === "undefined" || arr_salesbycategory === null || arr_salesbycategory.length === 0 || arr_salesbycategory === "NaN") {
                arr_salesbycategory = null;
            }
            SalesByCategory(arr_salesbycategory);
        },
        initcouponsalesummary: function (arr_couponSalesSummary) {
            if (typeof arr_couponSalesSummary === "undefined" || arr_couponSalesSummary === null || arr_couponSalesSummary.length === 0 || arr_couponSalesSummary === "NaN") {
                arr_couponSalesSummary = null;
            }
            CouponSalesSummary(arr_couponSalesSummary);
        },
        inittopsellingbrand: function (arr_topsellingbrand) {
            if (typeof arr_topsellingbrand === "undefined" || arr_topsellingbrand === null || arr_topsellingbrand.length === 0 || arr_topsellingbrand === "NaN") {
                arr_topsellingbrand = null;
            }
            TopSellingBrand(arr_topsellingbrand);
        },
        initclientbyreseller: function (arr_clientbyreseller) {
            if (typeof arr_clientbyreseller === "undefined" || arr_clientbyreseller === null || arr_clientbyreseller.length === 0 || arr_clientbyreseller === "NaN") {
                arr_clientbyreseller = null;
            }
            Noofclientbyreseller(arr_clientbyreseller);
        },
    };
}();

//jQuery(document).ready(function () {
//    KTamChartsChartsDemo.init();
//});