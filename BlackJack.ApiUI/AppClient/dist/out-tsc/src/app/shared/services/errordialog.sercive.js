import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { ErrordialogComponent } from 'src/app/components/errordialog/errordialog.component';
import { MatDialog } from '@angular/material';
var ErrorDialogService = /** @class */ (function () {
    function ErrorDialogService(dialog) {
        this.dialog = dialog;
    }
    ErrorDialogService.prototype.openDialog = function (data) {
        var dialogRef = this.dialog.open(ErrordialogComponent, {
            width: '300px',
            data: data
        });
        dialogRef.afterClosed().subscribe(function (result) {
            console.log('The dialog was closed');
            var animal;
            animal = result;
        });
    };
    ErrorDialogService = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [MatDialog])
    ], ErrorDialogService);
    return ErrorDialogService;
}());
export { ErrorDialogService };
//# sourceMappingURL=errordialog.sercive.js.map