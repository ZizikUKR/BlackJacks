import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HistoryComponent } from './history.component';
var routes = [
    {
        path: '',
        component: HistoryComponent
    },
    {
        path: 'gameRounds/:id',
        loadChildren: '../history-rounds/history-rounds.module#HistoryRoundsModule'
    }
];
var HistoryRoutingModule = /** @class */ (function () {
    function HistoryRoutingModule() {
    }
    HistoryRoutingModule = tslib_1.__decorate([
        NgModule({
            imports: [RouterModule.forChild(routes)],
            exports: [RouterModule]
        })
    ], HistoryRoutingModule);
    return HistoryRoutingModule;
}());
export { HistoryRoutingModule };
//# sourceMappingURL=history-routing.module.js.map