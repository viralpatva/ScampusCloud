"use strict";
// Class definition

var KTDatatableHtmlTableDemo = function () {
    // Private functions

    //  BindDynamicTable initializer
    var BindDynamicTable = function () {

        var datatable = $('#kt_datatable').KTDatatable({
            data: {
                saveState: { cookie: false },
            },
            search: {
                input: $('#kt_datatable_search_query'),
                key: 'generalSearch'
            },
            columns: [
                {
                    field: 'Name',
                    title: 'Name',
                    autoHide: false,
                    width: 140,
                    textAlign: 'center'
                },
                {
                    field: 'Action',
                    title: 'Action',
                    autoHide: false,
                    width: 125,
                    textAlign: 'center'
                },
                {
                    field: 'Active/InActive',
                    title: 'Active/InActive',
                    autoHide: false,
                    width: 125,
                    textAlign: 'center'
                },
                {
                    field: 'Delete',
                    title: 'Delete',
                    autoHide: false,
                    width: 75,
                    textAlign: 'center'
                },
                {
                    field: 'bitActive',
                    title: 'Status',
                    autoHide: false,
                    // callback function support for column rendering
                    template: function (row) {
                        var status = {
                            Active: {
                                'title': 'Active',
                                'class': ' label-light-warning'
                            },
                            InActive: {
                                'title': 'InActive',
                                'class': ' label-light-danger'
                            }
                        };
                        return '<span class="label font-weight-bold label-lg' + status[row.Status].class + ' label-inline">' + status[row.Status].title + '</span>';
                    },
                },
                {
                    field: 'View Detail',
                    title: 'View Detail',
                    autoHide: false,
                    width: 75,
                    textAlign: 'center'
                },
            ],


        });

        $('#kt_datatable_search_status').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Status');
        });

        $('#kt_datatable_search_status, #kt_datatable_search_type').selectpicker();



    };

    return {
        // Public functions
        init: function () {
            // init BindDynamicTable
            BindDynamicTable();
        },
    };
}();

var POItemsKTDatatable = function () {
    // Private functions

    //  BindDynamicTable initializer
    var BindDynamicTable = function () {

        var datatable = $('#kt_datatable_po_items').KTDatatable({
            data: {
                saveState: { cookie: false },
            },
            search: {
                input: $('#kt_datatable_search_query'),
                key: 'generalSearch'
            },
            columns: [
                {
                    field: 'Action',
                    title: 'Action',
                    autoHide: false,
                    width: 250,
                    textAlign: 'center'
                },
            ],
        });
    };

    return {
        // Public functions
        init: function () {
            // init BindDynamicTable
            BindDynamicTable();
        },
    };
}();

//jQuery(document).ready(function () {
//    KTDatatableHtmlTableDemo.init();
//});


