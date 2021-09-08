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
exports.tasks = void 0;
var auditableEntity_model_1 = require("../auditableEntity/auditableEntity.model");
var tasks = /** @class */ (function (_super) {
    __extends(tasks, _super);
    function tasks() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.TaskId = 0;
        _this.TaskTitle = '';
        _this.TaskDescription = '';
        _this.IsCompleted = false;
        _this.CompletionDate = new Date();
        _this.CompletedBy = 0;
        return _this;
    }
    return tasks;
}(auditableEntity_model_1.auditableEntity));
exports.tasks = tasks;
//# sourceMappingURL=tasks.model.js.map