import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { GameService } from 'src/app/shared/services/Game.service';
import { ActivatedRoute } from '@angular/router';
var GameComponent = /** @class */ (function () {
    function GameComponent(testService, activatedRoute) {
        this.testService = testService;
        this.activatedRoute = activatedRoute;
        this.users = [];
        this.gameId = '';
        this.isOver = false;
        this.status = '';
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.activatedRoute.params.subscribe(function (params) {
            _this.gameId = params['id'];
        });
        this.getMoves(this.gameId);
    };
    GameComponent.prototype.getMoves = function (id) {
        var _this = this;
        this.testService.showMoves(id).subscribe(function (res) {
            _this.moves = res.Rounds;
        });
    };
    GameComponent.prototype.getCard = function (id) {
        var _this = this;
        this.testService.nextMove(id).subscribe(function (res) {
            _this.getMoves(id);
            _this.isOver = res;
            _this.showResult(_this.isOver);
        });
    };
    GameComponent.prototype.getRestOfCards = function (id) {
        var _this = this;
        this.testService.dealRestOfCards(id).subscribe(function (res) {
            _this.isOver = res;
            _this.getMoves(id);
            _this.showResult(_this.isOver);
        });
    };
    GameComponent.prototype.showResult = function (isFinish) {
        var _this = this;
        if (isFinish === true) {
            this.testService.getGameInfo(this.gameId).subscribe(function (res) {
                _this.status = res.Status;
            });
        }
    };
    var _a;
    GameComponent = tslib_1.__decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html',
            styleUrls: ['./game.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [GameService, typeof (_a = typeof ActivatedRoute !== "undefined" && ActivatedRoute) === "function" ? _a : Object])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map