define([
    'jquery',
    'lib/handlebars',
    'lib/bootstrap/confirm-delete',
    'lib/bootstrap/ajax-form-modal',
    'lib/datatables/datatables',
    'lib/datatables/defaults',
    'lib/datatables/fnReloadAjax',
    'lib/bootstrap/dropdown'
  ],
  function ($, Handlebars, confirmDelete) {
    function initializeAddClickHandler(dataTable, addButtonSelector) {
      $(addButtonSelector).on('click', function (e) {
        e.preventDefault();

        $("#ajax-form-modal-container").ajaxFormModal({
          contentRequestUrl: this.href,
          success: function () {
            dataTable.fnReloadAjax();
          }
        });
      });
    }

    function initializeEditClickHandler(table, dataTable) {
      table.on("click", "a[data-action='edit']", function (e) {
        e.preventDefault();

        $("#ajax-form-modal-container").ajaxFormModal({
          contentRequestUrl: this.href,
          success: function () {
            dataTable.fnReloadAjax();
          }
        });
      });
    }

    function initializeDeleteClickHandler(table, dataTable) {
      table.on("click", "a[data-action='delete']", function (e) {
        e.preventDefault();

        confirmDelete.confirmDelete({
          url: this.href,
          success: function () {
            dataTable.fnReloadAjax();
          }
        });
      });
    }
    
    function buildGridRowActionsColumnDef(gridRowActionsTemplate) {
      return {
        bSortable: false,
        bSearchable: false,
        sWidth: "23px",
        mRender: function (data, type) {
          return type === "display" ? gridRowActionsTemplate(data) : data;
        },
        aTargets: [-1]
      };
    }

    function initializeGrid(table) {
      var gridRowActions = Handlebars.compile($("#edit-delete-grid-row-actions").html());

      return table.dataTable({
        sAjaxSource: table.data("ajaxSource"),
        aoColumnDefs: [buildGridRowActionsColumnDef(gridRowActions)]
      });
    }

    return {
      initialize: function (config) {
        config = $.extend({
          tableSelector: 'table',
          addButtonSelector: '#add',
          initializeGridOverride: null,
          initializeAddClickHandlerOverride: null
        }, config);

        var table = $(config.tableSelector),
          dataTable;

        if (config.initializeGridOverride) {
          dataTable = config.initializeGridOverride(table);
        } else {
          dataTable = initializeGrid(table);
        }

        if (config.initializeAddClickHandlerOverride) {
          config.initializeAddClickHandlerOverride(dataTable, config.addButtonSelector);
        } else {
          initializeAddClickHandler(dataTable, config.addButtonSelector);
        }
        initializeEditClickHandler(table, dataTable);
        initializeDeleteClickHandler(table, dataTable);
      },

      buildGridRowActionsColumnDef: buildGridRowActionsColumnDef
    };
  });