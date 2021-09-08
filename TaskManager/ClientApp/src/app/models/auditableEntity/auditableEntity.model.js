"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.auditableEntity = void 0;
var auditableEntity = /** @class */ (function () {
    function auditableEntity() {
        this.CreatedDate = new Date();
        this.ModifiedDate = new Date();
        this.DeletedDate = new Date();
        this.CreatedBy = 0;
        this.ModifiedBy = 0;
        this.DeletedBy = 0;
        this.Total = 0;
        this.RowIndex = 0;
        this.IsActive = false;
        this.IsInserting = false;
        this.IsDeleted = false;
    }
    return auditableEntity;
}());
exports.auditableEntity = auditableEntity;
//# sourceMappingURL=auditableEntity.model.js.map