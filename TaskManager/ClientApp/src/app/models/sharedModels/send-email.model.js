"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EmailType = exports.SendEmail = void 0;
var SendEmail = /** @class */ (function () {
    function SendEmail() {
        this.EmailAddress = '';
        this.RecordNumber = '';
        this.EmailSubject = '';
        this.EmailBody = '';
        this.UserName = '';
        this.HasAttachment = false;
    }
    return SendEmail;
}());
exports.SendEmail = SendEmail;
var EmailType;
(function (EmailType) {
    EmailType[EmailType["Default"] = 0] = "Default";
    EmailType[EmailType["Reminder"] = 1] = "Reminder";
    EmailType[EmailType["Notify"] = 2] = "Notify";
})(EmailType = exports.EmailType || (exports.EmailType = {}));
//# sourceMappingURL=send-email.model.js.map