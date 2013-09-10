define(['jquery', 'lib/datatables/datatables', 'lib/datatables/bootstrap-paging'], function ($) {
  $.extend($.fn.dataTable.defaults, {
    sDom: "<'row'<'span6'l><'span6'f>r>t<'row'<'span6'i><'span6'p>>",
    bDeferRender: true,
    sPaginationType: "bootstrap",
    bSortClasses: false
  });
  $.extend($.fn.dataTableExt.oStdClasses, {
    sWrapper: "dataTables_wrapper form-inline"
  });
});