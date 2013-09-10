/*jslint unparam: true, indent: 2 */
/*global define */

define(['jquery', 'lib/bootstrap/bootbox', 'lib/bootstrap/modal', 'lib/jquery/form'], function ($, bootbox) {
  "use strict";

  var AjaxFormModal = function (container, options) {
    this.options = options;
    this.$container = $(container);
    this.$modal = null;
  };

  AjaxFormModal.prototype = {
    constructor: AjaxFormModal,

    showResponseMessage: function (response, callback) {
      var text,
        $modal = this.$modal;

      if ((response.Type === 1 && !this.options.showSuccessResponseMessage) ||
          (response.Type === 3 && !this.options.showErrorResponseMessage)) {
        if (callback) {
          callback();
        }
        return;
      }

      if (response.Type === 1) {
        text = '<div class="alert alert-success no-bottom-margin no-close">' + response.Message + '</div>';
      } else {
        text = '<div class="alert alert-error no-bottom-margin no-close">' + response.Message + '</div>';
      }

      if ($modal) {
        $modal.hide();
      }

      bootbox.alert(text, function () {
        if ($modal) {
          $modal.show();
        }
        if (callback) {
          callback();
        }
      });
    },

    success: function (response) {
      var that = this;

      this.showResponseMessage(response, function () {
        if (that.options.success) {
          that.options.success(response);
        }

        that.$modal.modal('hide');
      });
    },

    validationError: function (response) {
      var valSummary = this.$modal.find('div[data-valmsg-summary=true]'),
        valSummaryList = valSummary.find('ul');

      $.each(response.ValidationErrors, function (i, error) {
        $('<li />').text(error).appendTo(valSummaryList);
      });

      valSummary.addClass('validation-summary-errors').removeClass('validation-summary-valid');
    },

    error: function (response) {
      var that = this;

      this.showResponseMessage(response, function () {
        if (that.options.error) {
          that.options.error(response);
        }
      });
    },

    processResponse: function (response) {
      // 1 = Success
      // 2 = Validation Error
      // 3 = Error
      var responseHandlers = {
        '1': $.proxy(this.success, this),
        '2': $.proxy(this.validationError, this),
        '3': $.proxy(this.error, this)
      };

      this.$modal.find('div[data-valmsg-summary=true]')
        .addClass("validation-summary-valid")
        .removeClass("validation-summary-errors")
        .find('ul').empty();

      responseHandlers[response.Type](response);
    },

    submitWithCustomSerialization: function () {
      var that = this,
        form = this.$modal.find('form'),
        modalFooter = this.$modal.find('.modal-footer').first(),
        modalFooterChildren = modalFooter.children(),
        spinner = $('<i class="icon-spinner icon-spin icon-2x"></i>'),
        ajaxOptions = {
          url: form.attr('action'),
          type: form.attr('method') || 'POST',
          dataType: 'json'
        },
        jqXhr;

      if (this.options.beforeSubmit && !this.options.beforeSubmit(form, ajaxOptions)) {
        return;
      }

      ajaxOptions.data = this.options.serialize(form);

      modalFooterChildren.hide();
      spinner.appendTo(modalFooter);

      jqXhr = $.ajax(ajaxOptions);

      jqXhr.always(function () {
        spinner.remove();
        modalFooterChildren.show();
      });

      jqXhr.done(function (response) {
        that.processResponse(response);
      });

      jqXhr.fail(function () {
        that.error({ Type: 3, Message: 'An error occurred.' });
      });
    },

    submit: function () {
      if (this.options.serialize) {
        this.submitWithCustomSerialization();
      } else {
        this.$modal.find('form').submit();
      }
    },

    attachAjaxForm: function () {
      var that = this,
        form = this.$modal.find('form'),
        modalFooter = this.$modal.find('.modal-footer').first(),
        modalFooterChildren = modalFooter.children(),
        spinner = $('<i class="icon-spinner icon-spin icon-2x"></i>');

      form.ajaxForm({
        dataType: 'json',
        iframe: form.find('input[type="file"]:enabled[value]').length > 0,

        beforeSubmit: function (formData, $form, ajaxOptions) {
          if (that.options.beforeSubmit && !that.options.beforeSubmit(form, ajaxOptions)) {
            return false;
          }

          modalFooterChildren.hide();
          spinner.appendTo(modalFooter);

          return true;
        },

        complete: function () {
          spinner.remove();
          modalFooterChildren.show();
        },

        success: function (response) {
          that.processResponse(response);
        },

        error: function () {
          that.error({ Type: 3, Message: 'An error occurred.' });
        }
      });
    },

    attachContentEventHandlers: function () {
      var that = this;

      if (this.options.serialize) {
        this.$modal.on('submit.ajaxFormModal', 'form', function (e) { e.preventDefault(); });
      } else {
        this.attachAjaxForm();
      }

      this.$modal.on('click.ajaxFormModal', '[data-action="submit"]', $.proxy(this.submit, this));

      this.$modal.on('hidden.ajaxFormModal', function () {
        setTimeout(function () {
          that.$modal.remove();
        });
      });
    },

    hide: function () {
      this.$modal.modal('hide');
    },

    show: function () {
      var that = this;

      $.ajax({
        url: that.options.contentRequestUrl,
        type: that.options.contentRequestType,
        data: that.options.contentRequestData,
        contentType: that.options.contentRequestContentType,
        dataType: 'html',
        cache: false,

        success: function (content) {
          content = $.parseHTML(content, document, true);
          that.$modal = $(content).appendTo(that.$container).filter('.modal');

          that.attachContentEventHandlers();

          that.$modal.modal();
        },

        error: function (jqXhr, textStatus, errorThrown) {
          var message = jqXhr.status === 403 && (errorThrown === 'Permission Not Granted' || errorThrown === 'Forbidden')
            ? jqXhr.responseText : 'An error occured while loading the content of the dialog.';
          that.showResponseMessage({ Type: 3, Message: message });
        }
      });
    }
  };

  $.fn.ajaxFormModal = function (option) {
    var data;
    if (typeof option === 'string') {
      $(this).data('ajaxFormModal')[option]();
    } else {
      data = new AjaxFormModal(this, $.extend({}, $.fn.ajaxFormModal.defaults, typeof option === 'object' && option));
      $(this).data('ajaxFormModal', data);
      data.show();
    }

    return this;
  };

  $.fn.ajaxFormModal.defaults = {
    contentRequestUrl: '',
    contentRequestType: 'GET',
    contentRequestData: null,
    contentRequestContentType: 'application/x-www-form-urlencoded; charset=UTF-8',

    beforeSubmit: null,
    serialize: null,
    success: null,
    error: null,
    showSuccessResponseMessage: false,
    showErrorResponseMessage: true
  };
});