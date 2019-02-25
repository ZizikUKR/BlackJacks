import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { GameService } from 'src/app/shared/services/Game.service';
import { Router } from '@angular/router';
var LoginComponent = /** @class */ (function () {
    function LoginComponent(testService, router) {
        this.testService = testService;
        this.router = router;
    }
    LoginComponent.prototype.ngOnInit = function () {
        this.getUsers();
    };
    LoginComponent.prototype.getUsers = function () {
        var _this = this;
        this.testService.getUsers().subscribe(function (res) {
            _this.users = res;
        });
    };
    LoginComponent.prototype.registration = function () {
        var _this = this;
        var body = {
            username: this.userName,
            countOfBots: this.countOfBots
        };
        this.testService.startGame(body).subscribe(function (res) {
            console.log(res);
            _this.router.navigate(['/game', res]);
        });
    };
    var _a;
    LoginComponent = tslib_1.__decorate([
        Component({
            selector: 'app-login',
            templateUrl: './login.component.html',
            styleUrls: ['./login.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [GameService, typeof (_a = typeof Router !== "undefined" && Router) === "function" ? _a : Object])
    ], LoginComponent);
    return LoginComponent;
}());
export { LoginComponent };
//# sourceMappingURL=login.component.js.map