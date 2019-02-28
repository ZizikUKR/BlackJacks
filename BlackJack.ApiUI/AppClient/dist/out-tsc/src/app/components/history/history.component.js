import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HistoryService } from 'src/app/shared/services/history.service';
var HistoryComponent = /** @class */ (function () {
    function HistoryComponent(historyService, router) {
        this.historyService = historyService;
        this.router = router;
        this.players = [];
        this.userName = '';
        this.games = [];
    }
    HistoryComponent.prototype.ngOnInit = function () {
        this.getAllPlayers();
    };
    HistoryComponent.prototype.getAllPlayers = function () {
        var _this = this;
        this.historyService.getUsers().subscribe(function (res) {
            _this.players = res;
        });
    };
    HistoryComponent.prototype.getAllGames = function (playerName) {
        var _this = this;
        var model = {
            name: playerName
        };
        this.historyService.getAllPlayerGames(model).subscribe(function (res) {
            _this.games = res;
        });
    };
    HistoryComponent.prototype.showGameMoves = function (id) {
        this.router.navigate(["history/gameRounds", id]);
    };
    HistoryComponent = tslib_1.__decorate([
        Component({
            selector: 'app-history',
            templateUrl: './history.component.html',
            styleUrls: ['./history.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [HistoryService, Router])
    ], HistoryComponent);
    return HistoryComponent;
}());
export { HistoryComponent };
//# sourceMappingURL=history.component.js.map