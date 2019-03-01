import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterService } from 'src/app/shared/services/register.service';
var LoginComponent = /** @class */ (function () {
    function LoginComponent(router, registerService) {
        this.router = router;
        this.registerService = registerService;
        this.users = [];
    }
    LoginComponent.prototype.ngOnInit = function () {
        this.getUsers();
    };
    LoginComponent.prototype.getUsers = function () {
        var _this = this;
        this.registerService.getUsers().subscribe(function (res) {
            _this.users = res.playerViewModels;
        }, function (err) {
            _this.router.navigate(["error"]);
        });
    };
    LoginComponent.prototype.registration = function () {
        var _this = this;
        var model = {
            userName: this.userName,
            countOfBots: parseInt(this.countOfBots)
        };
        this.registerService.startGame(model).subscribe(function (res) {
            _this.router.navigate(['/game', res]);
        }, function (err) {
            _this.router.navigate(["error"]);
        });
    };
    LoginComponent = tslib_1.__decorate([
        Component({
            selector: 'app-login',
            templateUrl: './login.component.html',
            styleUrls: ['./login.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [Router, RegisterService])
    ], LoginComponent);
    return LoginComponent;
}());
export { LoginComponent };
//# sourceMappingURL=login.component.js.map