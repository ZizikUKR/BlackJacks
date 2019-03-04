import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HistoryService } from 'src/app/shared/services/history.service';
var HistoryRoundsComponent = /** @class */ (function () {
    function HistoryRoundsComponent(activatedRoute, historyService, router) {
        this.activatedRoute = activatedRoute;
        this.historyService = historyService;
        this.router = router;
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
            _this.rounds = res.roundViewModels;
        });
    };
    HistoryRoundsComponent = tslib_1.__decorate([
        Component({
            selector: 'app-history-rounds',
            templateUrl: './history-rounds.component.html',
            styleUrls: ['./history-rounds.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [ActivatedRoute, HistoryService, Router])
    ], HistoryRoundsComponent);
    return HistoryRoundsComponent;
}());
export { HistoryRoundsComponent };
//# sourceMappingURL=history-rounds.component.js.map