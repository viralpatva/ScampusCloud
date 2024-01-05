"use strict";
var KTDatatablesAdvancedRowGrouping = function() {

	var ServerDepartmentinit = function() {
		var table = $('#kt_datatable');

		// begin first table
		table.DataTable({
			responsive: true,
			pageLength: 25,
			order: [[2, 'asc']],
			drawCallback: function(settings) {
				var api = this.api();
				var rows = api.rows({page: 'current'}).nodes();
				var last = null;

				api.column(0, {page: 'current'}).data().each(function(group, i) {
					if (last !== group) {
						var Total_Count = 0;
						var Total_CostPer = 0;
						var Total_Sum = 0;
						var displaySumValue;
						var cashierName = $('tbody').find('.hdnCahierName_' + group + '').val();
						$('tbody').find('.item_count_' + group + '').each(function () {
							Total_Count = Total_Count + parseFloat($(this).attr('data-count'));
							Total_Sum = Total_Sum + parseFloat($(this).closest('tr').find('.total_sum_' + group + '').attr('data-total'));
						});

						//$('tbody').find('.total_sum_' + group + '').each(function () {
						//	Total_Sum = Total_Sum + parseFloat($(this).attr('data-total'));
						//});
						if (Total_Sum < 0) {
							Total_Sum = parseFloat(Total_Sum.toString().replace('-', '')); //remove minus sign and append start and end brackets
							displaySumValue = '$(' + Total_Sum.toFixed(2) + ')';
						}
						else
							displaySumValue = '$' + Total_Sum.toFixed(2) + '';
						$(rows).eq(i).before(
							'<tr class="group"><td>' + cashierName + '</td><td></td><td class="text-right">' + Total_Count + '</td><td class="text-right">' + displaySumValue + '</td></tr>',
						);
						last = group;
					}
				});
			},
			columnDefs: [
				{
					// hide columns by index number
					targets: [2],
					visible: false,
				},
			],
		});
	};

	var ReceivedStockinit = function () {
		var table = $('#kt_datatable');

		table.DataTable({
			responsive: true,
			pageLength: 25,
			order: [[2, 'asc']],
			drawCallback: function (settings) {
				var api = this.api();
				var rows = api.rows({ page: 'current' }).nodes();
				var last = null;
				

				api.column(2, { page: 'current' }).data().each(function (group, i) {
					group = group.replace(/\//g, "-");
					if (last !== group) {
						var Total_Count = 0;
						var Total_CostPer = 0;
						var Total_Sum = 0;
						var displaySumValue;
						var displayCostPer;
							$('tbody').find('.item_count_' + group + '').each(function () {
								Total_Count = Total_Count + parseFloat($(this).attr('data-count'));
							});
							$('tbody').find('.cost_per_' + group + '').each(function () {
								Total_CostPer = Total_CostPer + parseFloat($(this).attr('data-costper'));
							});
							$('tbody').find('.total_sum_' + group + '').each(function () {
								Total_Sum = Total_Sum + parseFloat($(this).attr('data-total'));
							});
							if (Total_Sum < 0) {
								Total_Sum = parseFloat(Total_Sum.toString().replace('-', '')); //remove minus sign and append start and end brackets
								displaySumValue = '$(' + Total_Sum + ')';
							}
							else
								displaySumValue = '$' + Total_Sum + '';

							if (Total_CostPer < 0) {
								Total_CostPer = parseFloat(Total_CostPer.toString().replace('-', '')); //(Cost Per) remove minus sign and append start and end brackets
								displayCostPer = '$(' + Total_CostPer + ')';
							}
							else
								displayCostPer = '$' + Total_CostPer + '';
							if ($('#GroupByDate').text() != '' && $('#GroupByDate').text() != undefined) {
								$(rows).eq(i).before(
									'<tr class="group"><td>' + group + '</td><td colspan="2"></td><td class="text-right">' + Total_Count + '</td><td class="text-right">' + displayCostPer + '</td><td class="text-right">' + displaySumValue + '</td><td colspan="2"></td></tr>',
								);
							}
							last = group;
						}
					});
				},
					columnDefs: [
					{
						// hide columns by index number
						targets: [2],
						visible: false,
					},
				],
		});
	};

	var CCPaymentDetailinit = function () {
		var table = $('#kt_datatable');

		table.DataTable({
			responsive: true,
			pageLength: 25,
			order: [[2, 'asc']],
			drawCallback: function (settings) {
				var api = this.api();
				var rows = api.rows({ page: 'current' }).nodes();
				var last = null;


				api.column(2, { page: 'current' }).data().each(function (group, i) {
					group = group.replace(/\//g, "-");
					if (last !== group) {
						$(rows).eq(i).before(
							'<tr class="group"><td>Station ID : ' + group + '</td><td colspan="12"></td></tr>',
						);
						
						last = group;
					}
				});
			},
			columnDefs: [
				{
					// hide columns by index number
					targets: [2],
					visible: false,
				},
			],
		});
	};

	var SalesByServerCategoryinit = function () {
		var table = $('#kt_datatable');

		// begin first table
		table.DataTable({
			responsive: true,
			pageLength: 25,
			order: [[2, 'asc']],
			drawCallback: function (settings) {
				var api = this.api();
				var rows = api.rows({ page: 'current' }).nodes();
				var last = null;

				api.column(0, { page: 'current' }).data().each(function (group, i) {
					if (last !== group) {
						var Total_Count = 0;
						var Total_Sum = 0;
						var displaySumValue;
						var cashierName = $('tbody').find('.hdnCahierName_' + group + '').val();
						$('tbody').find('.item_count_' + group + '').each(function () {
							Total_Count = Total_Count + parseFloat($(this).attr('data-count'));
							Total_Sum = Total_Sum + parseFloat($(this).closest('tr').find('.total_sum_' + group + '').attr('data-total'));
						});
						if (Total_Sum < 0) {
							Total_Sum = parseFloat(Total_Sum.toString().replace('-', '')); //remove minus sign and append start and end brackets
							displaySumValue = '$(' + Total_Sum.toFixed(2) + ')';
						}
						else
							displaySumValue = '$' + Total_Sum.toFixed(2) + '';
						$(rows).eq(i).before(
							'<tr class="group"><td>' + cashierName + '</td><td></td><td class="text-right">' + Total_Count + '</td><td class="text-right">' + displaySumValue + '</td></tr>',
						);
						last = group;
					}
				});
			},
			columnDefs: [
				{
					// hide columns by index number
					targets: [2],
					visible: false,
				},
			],
		});
	};

	var CCDetailByCashierinit = function () {
		var table = $('#kt_datatable');

		table.DataTable({
			responsive: true,
			pageLength: 25,
			order: [[2, 'asc']],
			drawCallback: function (settings) {
				var api = this.api();
				var rows = api.rows({ page: 'current' }).nodes();
				var last = null;


				api.column(2, { page: 'current' }).data().each(function (group, i) {
					group = group.replace(/\//g, "-");
					if (last !== group) {
						var Total_Sum = 0; 
						var displaySumValue;
						$('tbody').find('.total_sum_' + group + '').each(function () {
							Total_Sum = Total_Sum + parseFloat($(this).attr('data-total'));
						});
						if (Total_Sum < 0) {
							Total_Sum = parseFloat(Total_Sum.toString().replace('-', '')); //remove minus sign and append start and end brackets
							displaySumValue = '$(' + Total_Sum.toFixed(2) + ')';
						}
						else
							displaySumValue = '$' + Total_Sum.toFixed(2) + '';

						$(rows).eq(i).before(
							'<tr class="group"><td>Date : ' + group + '</td><td colspan="2"></td><td class="text-right">' + displaySumValue + '</td><td colspan="3"></td></tr>',
						);

						last = group;
					}
				});
			},
			columnDefs: [
				{
					// hide columns by index number
					targets: [2],
					visible: false,
				},
			],
		});
	};

	var ItemListByPreferredVendorinit = function () {
		var table = $('#kt_datatable');

		table.DataTable({
			responsive: true,
			pageLength: 25,
			order: [[2, 'asc']],
			drawCallback: function (settings) {
				var api = this.api();
				var rows = api.rows({ page: 'current' }).nodes();
				var last = null;
				
				api.column(2, { page: 'current' }).data().each(function (group, i) {
					group = group.replace(/\//g, "-");
					if (last !== group) {
						var Total_CostPer = 0, CostPer = 0, InStock = 0, totalValue = 0, pricePerItem = 0;
						var displayVendorCostPer, displayCostPer, displayTotalValue, displayPricePerItem;
						
						$('tbody').find('.vendor_cost_per_' + group + '').each(function () {
							Total_CostPer = Total_CostPer + parseFloat($(this).attr('vendor-data-costper'));
						});
						$('tbody').find('.cost_per_' + group + '').each(function () {
							CostPer = CostPer + parseFloat($(this).attr('data-costper'));
						});
						$('tbody').find('.in_stock_' + group + '').each(function () {
							InStock = InStock + parseFloat($(this).attr('data-instock'));
						});
						$('tbody').find('.total_value_' + group + '').each(function () {
							totalValue = totalValue + parseFloat($(this).attr('data-totalvalue'));
						});
						$('tbody').find('.price_per_item_' + group + '').each(function () {
							pricePerItem = pricePerItem + parseFloat($(this).attr('data-priceperitem'));
						});
						if (Total_CostPer < 0) {
							Total_CostPer = parseFloat(Total_CostPer.toString().replace('-', '')); 
							displayVendorCostPer = '$(' + Total_CostPer.toFixed(2) + ')';
						}
						else
							displayVendorCostPer = '$' + Total_CostPer.toFixed(2) + '';
						if (CostPer < 0) {
							CostPer = parseFloat(CostPer.toString().replace('-', '')); 
							displayCostPer = '$(' + CostPer.toFixed(2) + ')';
						}
						else
							displayCostPer = '$' + CostPer.toFixed(2) + '';
						if (totalValue < 0) {
							totalValue = parseFloat(totalValue.toString().replace('-', '')); 
							displayTotalValue = '$(' + totalValue.toFixed(2) + ')';
						}
						else
							displayTotalValue = '$' + totalValue.toFixed(2) + '';
						if (pricePerItem < 0) {
							pricePerItem = parseFloat(pricePerItem.toString().replace('-', ''));
							displayPricePerItem = '$(' + pricePerItem.toFixed(2) + ')';
						}
						else
							displayPricePerItem = '$' + pricePerItem.toFixed(2) + '';
						$(rows).eq(i).before(
							'<tr class="group"><td>Vendor# : ' + group.replace("_", " - ") + '</td><td colspan="3"></td><td class="text-right">' + displayVendorCostPer + '</td><td class="text-right">' + displayCostPer + '</td><td>' + InStock + '</td><td>' + displayTotalValue + '</td><td>' + displayPricePerItem + '</td></tr>',
						);

						last = group;
					}
				});
			},
			columnDefs: [
				{
					// hide columns by index number
					targets: [2],
					visible: false,
				},
			],
		});
	};

	return {

		//main function to initiate the module
		init: function() {
			init();
		},
		ServerDepartmentinit: function () {
			ServerDepartmentinit();
		},
		ReceivedStockinit: function () {
			ReceivedStockinit();
		},
		CCPaymentDetailinit: function () {
			CCPaymentDetailinit();
			$('#kt_datatable_filter').find('input[type="search"]').on('keyup', function () {
				getInventory();
			});
		},
		SalesByServerCategoryinit: function () {
			SalesByServerCategoryinit();
		},
		CCDetailByCashierinit: function () {
			CCDetailByCashierinit();
		},
		ItemListByPreferredVendorinit: function () {
			ItemListByPreferredVendorinit();
		},
	};

}();

//jQuery(document).ready(function() {
//	KTDatatablesAdvancedRowGrouping.init();
//});
