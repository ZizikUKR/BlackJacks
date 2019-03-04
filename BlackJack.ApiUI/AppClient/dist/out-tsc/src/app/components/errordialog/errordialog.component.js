import * as tslib_1 from "tslib";
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
var ErrordialogComponent = /** @class */ (function () {
    function ErrordialogComponent(data) {
        this.data = data;
        this.title = 'Angular-Interceptor';
    }
    ErrordialogComponent = tslib_1.__decorate([
        Component({
            selector: 'app-root',
            templateUrl: './errordialog.component.html'
        }),
        tslib_1.__param(0, Inject(MAT_DIALOG_DATA)),
        tslib_1.__metadata("design:paramtypes", [String])
    ], ErrordialogComponent);
    return ErrordialogComponent;
}());
export { ErrordialogComponent };
//# sourceMappingURL=errordialog.component.js.map