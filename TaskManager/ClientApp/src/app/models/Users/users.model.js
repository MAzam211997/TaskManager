"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
exports.Users = void 0;
var auditableEntity_model_1 = require("../auditableEntity/auditableEntity.model");
var Users = /** @class */ (function (_super) {
    __extends(Users, _super);
    function Users() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.UserID = 0;
        _this.RoleID = 0;
        _this.RoleName = '';
        _this.Initial = '';
        _this.FullName = '';
        _this.EmailAddress = '';
        _this.Mobile = '';
        _this.Password = '';
        _this.CurrentPassword = '';
        _this.OldPassword = '';
        _this.NewPassword = '';
        _this.ConfirmNewPassword = '';
        _this.IsSigningPartner = false;
        _this.Signature = '';
        _this.ArabicName = '';
        _this.OldFile = '';
        return _this;
    }
    return Users;
}(auditableEntity_model_1.auditableEntity));
exports.Users = Users;
//# sourceMappingURL=users.model.js.map