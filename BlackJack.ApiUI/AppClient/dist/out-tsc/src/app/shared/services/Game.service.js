import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var GameService = /** @class */ (function () {
    function GameService(http) {
        this.http = http;
    }
    GameService.prototype.getUsers = function () {
        return this.http.get('http://localhost:61994/api/Register/GetAllUser');
    };
    GameService.prototype.startGame = function (body) {
        return this.http.post('http://localhost:61994/api/Register/StartGame', [body.username, body.countOfBots]);
    };
    GameService.prototype.showMoves = function (id) {
        return this.http.get('http://localhost:61994/api/Game/GetFirsTwoMoves/' + id);
    };
    GameService.prototype.nextMove = function (id) {
        return this.http.get('http://localhost:61994/api/Game/NextRoundForPlayer/' + id);
    };
    GameService.prototype.dealRestOfCards = function (id) {
        console.log(id);
        return this.http.get('http://localhost:61994/api/Game/DealRestOfCards/' + id);
    };
    GameService.prototype.getGameInfo = function (id) {
        return this.http.get('http://localhost:61994/api/Game/GameResult/' + id);
    };
    var _a;
    GameService = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [typeof (_a = typeof HttpClient !== "undefined" && HttpClient) === "function" ? _a : Object])
    ], GameService);
    return GameService;
}());
export { GameService };
//# sourceMappingURL=Game.service.js.map