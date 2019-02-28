import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HistoryRoundsComponent } from './history-rounds.component';
import { HistoryRoundsRoutingModule } from './history-rounds-routing.module';
var HistoryRoundsModule = /** @class */ (function () {
    function HistoryRoundsModule() {
    }
    HistoryRoundsModule = tslib_1.__decorate([
        NgModule({
            declarations: [
                HistoryRoundsComponent
            ],
            imports: [
                CommonModule,
                ReactiveFormsModule,
                HistoryRoundsRoutingModule
            ]
        })
    ], HistoryRoundsModule);
    return HistoryRoundsModule;
}());
export { HistoryRoundsModule };
//# sourceMappingURL=history-rounds.module.js.map