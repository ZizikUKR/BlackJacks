import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HistoryComponent } from './history.component';
import { HistoryRoutingModule } from './history-routing.module';
var HistoryModule = /** @class */ (function () {
    function HistoryModule() {
    }
    HistoryModule = tslib_1.__decorate([
        NgModule({
            declarations: [
                HistoryComponent
            ],
            imports: [
                FormsModule,
                CommonModule,
                ReactiveFormsModule,
                HistoryRoutingModule
            ]
        })
    ], HistoryModule);
    return HistoryModule;
}());
export { HistoryModule };
//# sourceMappingURL=history.module.js.map