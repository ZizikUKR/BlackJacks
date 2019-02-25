import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { GameService } from './shared/services/Game.service';
var AppComponent = /** @class */ (function () {
    function AppComponent(testService) {
        this.testService = testService;
    }
    AppComponent = tslib_1.__decorate([
        Component({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [GameService])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map