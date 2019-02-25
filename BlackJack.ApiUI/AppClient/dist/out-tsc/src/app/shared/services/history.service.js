import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
var HistoryService = /** @class */ (function () {
    function HistoryService(http) {
        this.http = http;
    }
    HistoryService.prototype.getUsers = function () {
        return this.http.get('http://localhost:61994/api/History/GetPlayers');
    };
    HistoryService.prototype.getAllPlayerGames = function (body) {
        console.log(body);
        return this.http.post('http://localhost:61994/api/History/GetAllPlayerGames', body);
    };
    HistoryService.prototype.getAllRounds = function (id) {
        return this.http.get('http://localhost:61994/api/History/GetAllMovesForCurrentGame/' + id);
    };
    var _a;
    HistoryService = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [typeof (_a = typeof HttpClient !== "undefined" && HttpClient) === "function" ? _a : Object])
    ], HistoryService);
    return HistoryService;
}());
export { HistoryService };
//# sourceMappingURL=history.service.js.map