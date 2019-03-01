import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/app/environments/environment';
var GameService = /** @class */ (function () {
    function GameService(http) {
        this.http = http;
        this.gameApiUrl = '/api/Game/';
    }
    GameService.prototype.showMoves = function (id) {
        return this.http.get(environment.apiUrl + this.gameApiUrl + 'GetFirstTwoMoves/' + id);
    };
    GameService.prototype.nextMove = function (id) {
        return this.http.get(environment.apiUrl + this.gameApiUrl + 'NextRoundForPlayer/' + id);
    };
    GameService.prototype.dealRestOfCards = function (id) {
        return this.http.get(environment.apiUrl + this.gameApiUrl + 'DealRestOfCards/' + id);
    };
    GameService.prototype.getGameInfo = function (id) {
        return this.http.get(environment.apiUrl + this.gameApiUrl + 'GameResult/' + id);
    };
    GameService = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [HttpClient])
    ], GameService);
    return GameService;
}());
export { GameService };
//# sourceMappingURL=game.service.js.map