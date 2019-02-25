import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HistoryService } from 'src/app/shared/services/history.service';
var HistoryRoundsComponent = /** @class */ (function () {
    function HistoryRoundsComponent(router, activatedRoute, historyService) {
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.historyService = historyService;
        this.rounds = [];
        this.gameId = '';
    }
    HistoryRoundsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.activatedRoute.params.subscribe(function (params) {
            _this.gameId = params['id'];
            _this.loadRounds(_this.gameId);
        });
    };
    HistoryRoundsComponent.prototype.loadRounds = function (id) {
        var _this = this;
        this.historyService.getAllRounds(id).subscribe(function (res) {
            console.log(res);
            console.log(res);
            console.log(res);
            _this.rounds = res;
        });
    };
    var _a, _b;
    HistoryRoundsComponent = tslib_1.__decorate([
        Component({
            selector: 'app-history-rounds',
            templateUrl: './history-rounds.component.html',
            styleUrls: ['./history-rounds.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [typeof (_a = typeof Router !== "undefined" && Router) === "function" ? _a : Object, typeof (_b = typeof ActivatedRoute !== "undefined" && ActivatedRoute) === "function" ? _b : Object, HistoryService])
    ], HistoryRoundsComponent);
    return HistoryRoundsComponent;
}());
export { HistoryRoundsComponent };
//# sourceMappingURL=history-rounds.component.js.map