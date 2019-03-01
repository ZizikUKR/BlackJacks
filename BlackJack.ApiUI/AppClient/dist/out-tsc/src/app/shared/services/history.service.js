import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
var HistoryService = /** @class */ (function () {
    function HistoryService(http) {
        this.http = http;
        this.historyApiUrl = '/api/History/';
    }
    HistoryService.prototype.getUsers = function () {
        return this.http.get(environment.apiUrl + this.historyApiUrl + 'GetPlayers');
    };
    HistoryService.prototype.getAllPlayerGames = function (body) {
        console.log(body);
        return this.http.post(environment.apiUrl + this.historyApiUrl + 'GetAllPlayerGames', body);
    };
    HistoryService.prototype.getAllRounds = function (id) {
        return this.http.get(environment.apiUrl + this.historyApiUrl + 'GetAllMovesForCurrentGame/' + id);
    };
    HistoryService = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [HttpClient])
    ], HistoryService);
    return HistoryService;
}());
export { HistoryService };
//# sourceMappingURL=history.service.js.map