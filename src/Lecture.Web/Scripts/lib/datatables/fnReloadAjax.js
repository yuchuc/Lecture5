define(['jquery', 'lib/datatables/datatables'], function ($) {
  $.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
    var that = this,
      iOrigDisplayStart = oSettings._iDisplayStart,
      aData = [];

    if (sNewSource) {
      oSettings.sAjaxSource = sNewSource;
    }

    that.oApi._fnProcessingDisplay(oSettings, true);

    that.oApi._fnServerParams(oSettings, aData);

    oSettings.fnServerData(
      oSettings.sAjaxSource,
      aData,
      function (json) {
        var i;

        that.oApi._fnClearTable(oSettings);

        aData = (oSettings.sAjaxDataProp !== "") ? that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

        for (i = 0; i < aData.length; i++) {
          that.oApi._fnAddData(oSettings, aData[i]);
        }

        oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();

        that.fnDraw();

        if (bStandingRedraw === true) {
          oSettings._iDisplayStart = iOrigDisplayStart;
          that.fnDraw(false);
        }

        that.oApi._fnProcessingDisplay(oSettings, false);

        if (fnCallback && $.isFunction(fnCallback)) {
          fnCallback(oSettings);
        }
      },
      oSettings);
  };
});