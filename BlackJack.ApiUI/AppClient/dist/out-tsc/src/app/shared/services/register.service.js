import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
var RegisterService = /** @class */ (function () {
    function RegisterService(http) {
        this.http = http;
        this.registerApiUrl = '/api/Register/';
    }
    RegisterService.prototype.getUsers = function () {
        return this.http.get(environment.apiUrl + this.registerApiUrl + 'GetAllUser');
    };
    RegisterService.prototype.startGame = function (model) {
        return this.http.post(environment.apiUrl + this.registerApiUrl + 'StartGame', model);
    };
    RegisterService = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [HttpClient])
    ], RegisterService);
    return RegisterService;
}());
export { RegisterService };
//# sourceMappingURL=register.service.js.map