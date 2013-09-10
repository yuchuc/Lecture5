require(['jquery'], function ($) {
  "use strict";

  $(function () {
    $.ajaxSetup({
      type: 'POST',
      dataType: 'json',
      traditional: true
    });
  });
});