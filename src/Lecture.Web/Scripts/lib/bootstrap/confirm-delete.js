/*global $, define */

define(['jquery'], function ($) {
  'use strict';

  return {
    confirmDelete: function (options) {
      var container = $('#ajax-form-modal-container');

      container.ajaxFormModal({
        contentRequestUrl: '/Shared/ConfirmDelete',
        contentRequestData: {
          url: options.url
        },
        success: options.success,
        error: function () {
          container.ajaxFormModal('hide');
        }
      });
    }
  };
});