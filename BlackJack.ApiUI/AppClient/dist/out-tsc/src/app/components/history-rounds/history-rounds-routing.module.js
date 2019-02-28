import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HistoryRoundsComponent } from './history-rounds.component';
var routes = [{
        path: '',
        component: HistoryRoundsComponent
    }];
var HistoryRoundsRoutingModule = /** @class */ (function () {
    function HistoryRoundsRoutingModule() {
    }
    HistoryRoundsRoutingModule = tslib_1.__decorate([
        NgModule({
            imports: [RouterModule.forChild(routes)],
            exports: [RouterModule]
        })
    ], HistoryRoundsRoutingModule);
    return HistoryRoundsRoutingModule;
}());
export { HistoryRoundsRoutingModule };
//# sourceMappingURL=history-rounds-routing.module.js.map