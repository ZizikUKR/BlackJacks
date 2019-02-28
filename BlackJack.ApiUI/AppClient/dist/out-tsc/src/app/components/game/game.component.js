import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { GameService } from 'src/app/shared/services/game.service';
import { ActivatedRoute } from '@angular/router';
var GameComponent = /** @class */ (function () {
    function GameComponent(gameService, activatedRoute) {
        this.gameService = gameService;
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
        this.gameService.showMoves(id).subscribe(function (res) {
            _this.moves = res;
            console.log(_this.moves);
        });
    };
    GameComponent.prototype.getCard = function (id) {
        var _this = this;
        this.gameService.nextMove(id).subscribe(function (res) {
            console.log(res);
            _this.getMoves(id);
            _this.isOver = res;
            _this.showResult(_this.isOver);
        });
    };
    GameComponent.prototype.getRestOfCards = function (id) {
        var _this = this;
        this.gameService.dealRestOfCards(id).subscribe(function (res) {
            _this.isOver = res;
            _this.getMoves(id);
            _this.showResult(_this.isOver);
        });
    };
    GameComponent.prototype.showResult = function (isFinish) {
        var _this = this;
        if (isFinish) {
            this.gameService.getGameInfo(this.gameId).subscribe(function (res) {
                _this.status = res.Status;
                return;
            });
        }
        this.status = '';
    };
    GameComponent = tslib_1.__decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html',
            styleUrls: ['./game.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [GameService, ActivatedRoute])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map