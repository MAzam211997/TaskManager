import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Globals } from 'src/environments/Globals';

declare var $: any;

@Injectable()
export class SmartAppUtilities {

  constructor(private toastr: ToastrService) {
    $('[data-toggle="tooltip"]').tooltip({ trigger: 'hover' }); // for tooltips


  }

  GetJSONFromControls(formContainerSelector: string, isInserting?: boolean): any {

    var objectData;
    let formContainer: any = $(formContainerSelector);
    objectData = {};
    formContainer.find('input[type=text],input[type=hidden],input[type=password],input[type=checkbox],input[type=radio],textarea,select').each(function () {
      var value: any;
      var para = '';
      var type = $(this).attr('type');
      var ID = $(this).attr('name');
      if (ID === undefined)
        ID = $(this).attr('id');
      if (ID === undefined) // if still undefined, then we skip this input
      {
        alert($(this).parent().html() + ' has a control without name and ID');
      }

      if (ID.indexOf('CountryCode') == 0) {
        ID = ID.split('-')[0];
      }

      var p = ID.split('$');
      para = p[p.length - 1];
      if (!type) {
        if ($(this).is("textarea"))
          type = 'textarea';
      }
      if (para !== '') {
        switch (type) {
          case "text":
            if ($(this).data('scn')) {
              value = $(this).data('SCNCode');

              if (value === '' || value === undefined)

                value = "";
            }
            else {
              value = $.trim($(this).val());

              if ($(this).hasClass('Numeric')) {
                if (value === '' || value === undefined) {
                  value = "0";
                }
              }
            }

            break;
          case "password":
            value = $.trim($(this).val());
            break;
          case "hidden":
            value = $.trim($(this).val());
            break;
          case "textarea":
            value = $.trim($(this).val());
            break;
          case "checkbox":
            value = $(this).is(':checked') ? true : false;
            break;
          case "radio":
            if (!objectData[para])
              value = $("input[name='" + $(this).prop('name') + "']:checked").val();
            else
              return true;
            break;
        }
        if (para.indexOf('txtLanguage') > -1) {
          value = '';
        }
        if (!type) {
          if ($(this).is('select')) {
            value = $(this).val();
          }
        }
        if (objectData[para] != null) {
          if (!objectData[para].push) {
            objectData[para] = [objectData[para]];
          }
          objectData[para].push(value);
        }
        else {
          objectData[para] = value;
        }
      }
    });
    return objectData;
  }

  SetJSONToControls(containerForm: string, data: any) {
    if (data == '')
      return;

    if (data.RowIndex)
      $('#RowIndex').val(data.RowIndex);

    let $containerForm: any = $(containerForm);

    $.each(data, function (key, value) {

      var $ctrl = $containerForm.find('input[id=' + key + ']');

      if ($ctrl.length == 0)
        $ctrl = $containerForm.find('textarea[id=' + key + ']');

      if ($ctrl.length == 0)
        $ctrl = $containerForm.find('select[id=' + key + ']');

      if ($ctrl.length == 0) // Added By Hamza For Radio Button
        $ctrl = $containerForm.find('input[name=' + key + ']')


      switch ($ctrl.prop("type")) {
        case "text":
          if ($.isNumeric(value) && value == "0") {
            var x = +value;
            value = x.toFixed(2);
            $ctrl.val(value);
          }
          else {
            $ctrl.val(value);
          }
          break;
        case "hidden":
          $ctrl.val(value);
          break;

        case "textarea":
          $ctrl.text(value);
          $ctrl.val(value);
          break;

        case "select-one":
          var $this = $ctrl.children();
          for (var i = 0; i < $this.length; i++) {
            var opt = $this[i];
            if ($(opt).text() == value) {
              $(opt).prop("selected", true);
            }
            else {
              if ($(opt).val() == value) {
                $(opt).prop("selected", true);
              }
            }
          };
          break;


        case "select-multiple":
          var vall = value.trim();
          if (value.trim() != "") {
            var _formulakeys = [];
            _formulakeys = value.split(',');
            for (let i: number = 0; i < _formulakeys.length; i++) {
              let opt: string = "<option value='" + _formulakeys[i] + "'>" + _formulakeys[i] + "</option>";
              $ctrl.append(opt).attr('selected', 'selected');
            };
          }
          break;

        case "radio":
          $ctrl.each(function () {
            if ($(this).prop('value') == value.toString()) {
              $(this).prop("checked", value);
              return false;
            }
          });
          break
        case "checkbox":
          $ctrl.each(function () {

            $(this).prop("checked", value);
          });
          break;
      }
    });
  }

  EnableDisableFormElement(formContainer: string, isEnabled: boolean = false) {
    //Note: .no_enable will be ignored
    //Note: .no_disable will be ignored
    $(formContainer + " input").each(function () {
      if (isEnabled) {
        if (!$(this).hasClass('no_enable'))
          $(this).removeAttr('disabled');
      }
      else {
        if (!$(this).hasClass('no_disable'))
          $(this).attr('disabled', 'disabled');
      }

    });

    $(formContainer + " textarea").each(function () {

      if (!$(this).hasClass('no_enable')) {
        if (isEnabled) {
          if (!$(this).hasClass('no_enable'))
            $(this).removeAttr('disabled');
        }
        else {
          if (!$(this).hasClass('no_disable'))
            $(this).attr('disabled', 'disabled');
        }
      }
    });

    $(formContainer + " button").each(function () {

      if (!$(this).hasClass('no_enable')) {
        if (isEnabled) {
          if (!$(this).hasClass('no_enable'))
            $(this).removeAttr('disabled');
        }
        else {
          if (!$(this).hasClass('no_disable'))
            $(this).attr('disabled', 'disabled');

        }
      }
    });
    $(formContainer + " select").each(function () {
      if (isEnabled) {
        if (!$(this).hasClass('no_enable'))
          $(this).removeAttr('disabled');
      }
      else {
        if (!$(this).hasClass('no_disable'))
          $(this).attr('disabled', 'disabled');
      }
    });
  }

  ClearForm(containerElement: string, clearType?: any): void {

    let container: any = $(containerElement);

    var extensions = $.extend({ extension1: '', extension2: 'clearall' }, clearType);

    var coloredClasses = [
      'Textbox_Required',
      'Textbox_EntryError',
      'Textbox_EntryReset',
      'Textbox_EntryGreen',
      'Textbox_EntryOk'
    ];

    if (extensions.extension2 === 'clearall') {
      container.find('input[type=text],input[type=hidden],textarea').not('.no_clear').each(function () {

        if (this.type === 'hidden') { // reset hidden value if "data-default" attribute exists else empty value
          var defaultValue = this.getAttribute('data-default');
          if (defaultValue)
            $(this).val(defaultValue);
          else
            $(this).val(extensions.extension1);
          return true;
        }
        if ($(this).hasClass('Numeric')) {
          $(this).val("0");
        }
        else {
          $(this).val(extensions.extension1);
        }

        var currentClasses = $(this).attr('class');

        var filteredClasses = '';

        if (currentClasses != undefined)
          filteredClasses = currentClasses.replace(new RegExp('(' + coloredClasses.join('|') + ')', 'g'), '');

        $(this).removeClass().addClass(filteredClasses).addClass('Textbox_EntryReset');
      });


      //container.find('input.OkDialog').removeAttr('disabled');

      container.find('input[type=checkbox]:not(.no_clear)').prop('checked', false);

      container.find('select:not(.no_clear)').each(function () {
        $(this).val($(this).find('option:first').val())
      });
      container.find('.asterisk_input').remove('.asterisk_input');
      container.find('.cell_required').removeClass('cell_required');
    }
    else {
      container.find('input[type=text],input[type=hidden],textarea').not('.no_clear').each(function () {

        if (this.type === 'hidden') {
          var defaultValue = $(this).data("default");
          if (defaultValue)
            $(this).val(defaultValue);
          else
            $(this).val(extensions.extension1);
        }
        else if ($(this).hasClass('Numeric')) {
          $(this).val("0");
        }
        else {
          $(this).val(extensions.extension1);
        }

        var currentClasses = $(this).attr('class');

        var filteredClasses = '';

        if (currentClasses != undefined)
          filteredClasses = currentClasses.replace(new RegExp('(' + coloredClasses.join('|') + ')', 'g'), '');

        $(this).removeClass().addClass(filteredClasses).addClass('Textbox_EntryReset');
      });

      //container.find('input.OkDialog').removeAttr('disabled');

      container.find('input[type=checkbox]:not(.no_clear)').prop('checked', false);

      container.find('select:not(.no_clear)').each(function () {
        $(this).val($(this).find('option:first').val())
      });
      container.find('.asterisk_input').remove('.asterisk_input');
      container.find('.cell_required').removeClass('cell_required');
    }
  }
  /**
  * @gridClass this will be used for finding rows in Grid; Default Value = .divRow
  */
  ValidateForm(frmID, gridContainer = undefined, cbgridContainer = undefined, optgridContainer = undefined, gridClass = '.divRow'): boolean {
    var $divBody = "";
    var errorCount = 0;
    let gb = new Globals();
    let rqrdMsg: string = "Required";

    $(frmID).find("input.Required,textarea.Required").each(function (e) {
      if (!($(this).hasClass('no_enable'))) {
        if ($(this).val().trim() === "") {
          // $divBody += $(this).attr('data-jsname') + ' is required<br/>';
          $divBody += $(this).attr('data-jsname') + ' ' + rqrdMsg + '<br/>';
          errorCount++;
        }
      }
    });

    $(frmID).find("ckeditor.Required").each(function (e) {
      if (!($(this).hasClass('no_enable'))) {
        if ($(this).find(".ck-content")[0].innerHTML.trim() === "<p><br data-cke-filler=\"true\"></p>") {
          // $divBody += $(this).attr('data-jsname') + ' is required<br/>';
          $divBody += $(this).attr('data-jsname') + ' ' + rqrdMsg + '<br/>';
          errorCount++;
        }
      }
    });

    $(frmID).find("select.Required").each(function (e) {
      if (!($(this).hasClass('no_enable'))) {
        if ($(this).val() == null || $(this).val() == "0" || $(this).val() == "") {
          $divBody += $(this).data('jsname') + ' ' + rqrdMsg + '<br/>';
          errorCount++;
        }
      }
    });

    $(frmID).find("input.Length").each(function (e) {
      if (!($(this).hasClass('no_enable'))) {
        var minValue = 0;
        var maxValue = 0;
        if ($(this).attr("minlength") !== undefined) {
          minValue = parseInt($(this).attr("minlength"));
        }
        if ($(this).attr("maxlength") !== undefined) {
          maxValue = parseInt($(this).attr("maxlength"));
        }

        if ($(this).val().length < minValue || $(this).val().length > maxValue) {
          if (minValue == maxValue) {
            $divBody += $(this).data('jsname') + ' length must be ' + maxValue + ' characters<br/>';
          }
          else {
            $divBody += $(this).data('jsname') + ' length will be minimum ' + minValue + ' and maximum ' + maxValue + ' characters<br/>';
          }
          errorCount++;
        }
      }
    });

    if (errorCount === 0) {
      //Check for other error generic Entry error for any other reason...
      $(frmID).find("input[type=text]").each(function (e) {

        if ($(this).hasClass('Textbox_EntryError')) {

          $divBody += $(this).data('jsname') + ' is not correct<br/>';
          errorCount++;
        }
      });
    }
    if (errorCount > 0) {

      this.toastr.error($divBody, "Problems");
    }

    /************************Mandatory grid you have to add atleast one row************************************ADDED BY YAMEEN*/
    if (errorCount === 0 && gridContainer != undefined && gridContainer != null && gridContainer.length > 0) {
      let fillRows = 0;
      $(gridContainer).find(gridClass).not('.skip_validation').each((e, row) => {
        let $row: any = $(row);
        if (!this.IsValidRow($row)) {
          fillRows++;
          $row.find("input.Required,textarea.Required").each((e, item) => {
            let $element: any = $(item);
            if (!($element.hasClass('no_enable'))) {
              var Isvalid = this.ValidateRequired($element);
              if (!(Isvalid))
                errorCount++;
            }
          });
          $row.find("select.Required").each((e, item) => {
            let $element: any = $(item);
            if (!($element.hasClass('no_enable'))) {
              var Isvalid = this.ValidateDdL($element);
              if (!(Isvalid))
                errorCount++;
            }
          });
        }
      });
      if (fillRows === 0) {
        errorCount++;
        this.toastr.error('Please fill at-least one row.<br/>', "Problems");
      }
    }

    /************************Optional grid if added any row than validate************************************ADDED BY YAMEEN*/
    if (errorCount === 0 && optgridContainer != undefined && optgridContainer != null && optgridContainer.length > 0) {
      let fillRows = 0;
      $(optgridContainer).find(".divRow").not('.skip_validation').each((e, row) => {
        let $row: any = $(row);
        if (!this.IsValidRow($row)) {
          fillRows++;
          $row.find("input.Required,textarea.Required").each((e, item) => {
            let $element: any = $(item);
            if (!($element.hasClass('no_enable'))) {
              var Isvalid = this.ValidateRequired($element);
              if (!(Isvalid))
                errorCount++;
            }
          });
          $row.find("select.Required").each((e, item) => {
            let $element: any = $(item);
            if (!($element.hasClass('no_enable'))) {
              var Isvalid = this.ValidateDdL($element);
              if (!(Isvalid))
                errorCount++;
            }
          });
        }
      });
      if (fillRows === 0 && $(optgridContainer).find(".divRow").not('.skip_validation').length > 0) {
        errorCount++;
        this.toastr.error('Please fill at-least one row.<br/>', "Problems");
      }
    }

    /************************Checkbox grid if checkbox true than validate************************************ADDED BY YAMEEN*/
    if (errorCount === 0 && cbgridContainer != undefined && cbgridContainer != null && cbgridContainer.length > 0) {
      let fillRows = 0;
      $(cbgridContainer).find('[cb-validation]:checked').parents('.divRow').not('.skip_validation').each((e, row) => {
        let $row: any = $(row);
        if (!this.IsValidRow($row)) {
          fillRows++;
          $row.find("input.Required,textarea.Required").each((e, item) => {
            let $element: any = $(item);
            if (!($element.hasClass('no_enable'))) {
              var Isvalid = this.ValidateRequired($element);
              if (!(Isvalid))
                errorCount++;
            }
          });
          $row.find("select.Required").each((e, item) => {
            let $element: any = $(item);
            if (!($element.hasClass('no_enable'))) {
              var Isvalid = this.ValidateDdL($element);
              if (!(Isvalid))
                errorCount++;
            }
          });
        }
      });
      if (fillRows === 0) {
        errorCount++;
        this.toastr.error('Please fill at-least one row.<br/>', "Problems");
      }
    }


    if (errorCount > 0)
      return false;
    return true;
  }

  ValidateRequired($element): boolean {
    $element.change(() => {
      this.RemoveValidator($element);
      if ($element.val().trim() === "") {
        this.GenerateValidator($element);
      }
    });
    if ($element.val().trim() === "") {
      this.GenerateValidator($element);
      return false;
    }
    return true;
  }
  ValidateDdL($element): boolean {
    $element.change(() => {
      this.RemoveValidator($element);
      if ($element.val() == null || $element.val() == "0" || $element.val() == "") {
        this.GenerateValidator($element);
      }
    });
    if ($element.val() == null || $element.val() == "0" || $element.val() == "") {
      this.GenerateValidator($element);
      return false;
    }
    return true;
  }
  GenerateValidator($element) {
    $element.parent().find('.asterisk_input').remove('.asterisk_input');
    $element.removeClass("cell_required");
    $element.before(`<span class="asterisk_input"></span>`)
    $element.addClass("cell_required");
  }
  RemoveValidator($element) {
    $element.removeClass("cell_required");
    $element.parent().find('.asterisk_input').remove('.asterisk_input');
  }
  IsValidRow($element): boolean {
    var totalBoxes = $element.find('input[type=text],input[type=file]').length;
    totalBoxes = totalBoxes + $element.find('select').length;
    var emtyBoxes = $element.find('input[type=text],input[type=file]').filter(function () {
      return $.trim($(this).val()) == "";
    });
    var emtyDdl = $element.find('select').filter(function () {
      return ($(this).val() == null || $(this).val() == '0' || $(this).val() == '');
    });
    console.log("totalBoxes:" + totalBoxes);
    console.log("emtyBoxes:" + emtyBoxes.length);
    console.log("emtyDdl:" + emtyDdl.length);
    return ((emtyBoxes.length + emtyDdl.length) === totalBoxes);

  };

  MakeRequired(inputClass: any) {
    $('.Required').parents().children('label').addClass("rqd");
  }

  Numberify(inputClass: any, options: any): void {

    let $this: any = $(inputClass);

    if (!$this || $this.length === 0)
      return;

    let settings: any = $.extend({ numberType: 'integer', maxLength: 15, allowNegative: false, min: 0, max: 0 }, options);

    if ($this.attr('maxlength') === undefined)
      $this.attr('maxlength', settings.maxLength);

    $this.addClass("Numeric");

    let ex;

    if (settings.numberType === 'integer') {
      ex = /[^\d].+/;

      if (settings.allowNegative)
        ex = /^-?\d*$/;

    }
    else if (settings.numberType === 'decimal')
      ex = /[^0-9\.]/g;


    $this.each((key, value) => {

      let currentElement: any = $(value);

      if (settings.numberType === 'integer') {
        currentElement.on("keypress keyup blur", (event) => {

          currentElement.val(currentElement.val().replace(ex, ''));

          if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
          }

        });
      }
      else if (settings.numberType === 'decimal') {

        currentElement.on("keypress keyup blur", (event) => {

          currentElement.val(currentElement.val().replace(ex, ''));

          if ((event.which != 46 || currentElement.val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
          }

        });

      }
    });
  }

  SpecialTextBox(inputClass: string, options: any): void {
    var $this = $(inputClass);
    var settings = $.extend({ textBoxType: '' }, options);

    settings.textBoxType = settings.textBoxType.toLowerCase();

    $this.each((key, value) => {
      let currentElement: any = $(value);
      if (settings.textBoxType === 'email')
        currentElement.attr('maxlength', '50');

      else if (settings.textBoxType === 'mobile')
        currentElement.attr('maxlength', '20');

      else if (settings.textBoxType === 'landline')
        currentElement.attr('maxlength', '20');

      else if (settings.textBoxType === 'phone')  /*can be mobile or landline*/
        currentElement.attr('maxlength', '20');
      else if (settings.textBoxType === 'fax')  /*can be mobile or landline*/
        currentElement.attr('maxlength', '20');

      currentElement.on("blur", () => {
        let reg: RegExp;

        if (currentElement.val() === '') {
          this.Colorify(currentElement);
          return false;
        }

        if (settings.textBoxType === 'email') {
          reg = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;

          if (!reg.test(currentElement.val())) {
            this.toastr.error($this.data('jsname') + ' is not correct');
            this.Colorify(currentElement, { errorType: 'entryerror' });
          } else {
            this.Colorify(currentElement);
          }

        } else {

          if (settings.textBoxType === 'mobile') {
            reg = /^\d[0-9]{6,19}/;

            if (!reg.test(currentElement.val())) {
              this.toastr.error($this.data('jsname') + ' is not correct');
              this.Colorify(currentElement, { errorType: 'entryerror' });
            } else {
              this.Colorify(currentElement);
            }

          } else {

            if (settings.textBoxType === 'landline') {
              reg = /^\d[0-9]{6,19}/;

              if (!reg.test(currentElement.val())) {
                this.toastr.error($this.data('jsname') + ' is not correct');
                this.Colorify(currentElement, { errorType: 'entryerror' });
              } else {
                this.Colorify(currentElement);
              }

            }
          }
        }
      });
    });
  }

  Colorify(elementClass: string, options?: any): void {

    let settings: any = $.extend({ errorType: 'none' }, options);
    let $this: any = $(elementClass);


    var currentClasses = $this.attr('class');
    var filteredClasses = '';

    var coloredClasses = [
      'Textbox_Required',
      'Textbox_EntryError',
      'Textbox_EntryReset',
      'Textbox_EntryGreen',
      'Textbox_EntryOk'
    ];

    if (currentClasses != undefined) {
      filteredClasses = currentClasses.replace(new RegExp('(' + coloredClasses.join('|') + ')', 'g'), '');
    }

    $this.removeClass().addClass(filteredClasses);

    return $this.each(() => {
      if (settings.errorType === 'required')
        $this.addClass("Textbox_Required");
      else
        if (settings.errorType === 'entryerror')
          $this.addClass("Textbox_EntryError");
        else
          if (settings.errorType === 'uniqueerror')
            $this.addClass("Textbox_EntryError");
          else
            if (settings.errorType === 'green')
              $this.addClass('Textbox_EntryGreen');
            else
              $this.addClass("Textbox_EntryOk");
    });

  }

  ValidateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

  EnableDisableElement(elementIdentifier: string, isEnabled: boolean) {
    //Note: .not_enable will be ignored            
    $(elementIdentifier).each(function () {

      if (isEnabled) {
        if (!$(this).hasClass('no_enable'))
          $(this).removeAttr('disabled');
      }
      else {
        $(this).attr('disabled', 'disabled');
      }
    });

  }

  public ConvertToDecimal(value: any, decimals: number = 2) {

    if (this.isNumeric(value))
      return Number(value).toFixed(decimals);

    return (0).toFixed(decimals);
  }


  public DecimalFormat(value: any, decimals: number = 2): number {

    if (this.isNumeric(value))
      return Number(value.toFixed(decimals));

    return Number((0).toFixed(decimals));
  }

  public isNumeric(num: string): Boolean {
    return !isNaN(parseFloat(num));
  }

  parseStringToDate(value: any, days: number = 0): Date | null {
    if ((typeof value === 'string') && (value.includes('/'))) {
      const str = value.split('/');

      const year = Number(str[2]);
      const month = Number(str[1]) - 1;
      const date = Number(str[0]);

      return new Date(year, month, date + days);
    } else if ((typeof value === 'string') && value === '') {
      return new Date();
    }
    const timestamp = typeof value === 'number' ? value : Date.parse(value);
    return isNaN(timestamp) ? null : new Date(timestamp);
  }

  getNumericCellEditor() {

    function isCharNumeric(charStr) {
      return !!/\d/.test(charStr);
    }
    function isKeyPressedNumeric(event) {
      var charCode = getCharCodeFromEvent(event);
      var charStr = String.fromCharCode(charCode);
      if (charStr == "." && event.target.value.indexOf(".") < 0 && event.target.value.length > 0) {
        return true;
      }
      else {
        return isCharNumeric(charStr);
      }
    }
    function getCharCodeFromEvent(event) {
      event = event || window.event;
      return typeof event.which === "undefined" ? event.keyCode : event.which;
    }


    function NumericCellEditor() { }
    NumericCellEditor.prototype.init = function (params) {
      this.focusAfterAttached = params.cellStartedEdit;
      this.eInput = document.createElement("input");
      this.eInput.className = "ag-input-text-wrapper ag-cell-edit-input text-right";
      // this.eInput.style.width = "100%";
      // this.eInput.style.height = "100%";
      this.eInput.value = isCharNumeric(params.charPress) ? params.charPress : params.value;
      var that = this;
      this.eInput.addEventListener("keypress", function (event) {
        if (!isKeyPressedNumeric(event)) {
          that.eInput.focus();
          if (event.preventDefault) event.preventDefault();
        }
      });
    };
    NumericCellEditor.prototype.getGui = function () {
      return this.eInput;
    };
    NumericCellEditor.prototype.afterGuiAttached = function () {
      if (this.focusAfterAttached) {
        this.eInput.focus();
      }
    };
    NumericCellEditor.prototype.isCancelBeforeStart = function () {
      return this.cancelBeforeStart;
    };
    NumericCellEditor.prototype.isCancelAfterEnd = function () { };
    NumericCellEditor.prototype.getValue = function () {
      return this.eInput.value;
    };
    NumericCellEditor.prototype.focusIn = function () {
      var eInput = this.getGui();
      eInput.focus();
    };
    NumericCellEditor.prototype.focusOut = function () {
    };
    return NumericCellEditor;
  }


} // Smart Utilities Class End Point
