import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { GameComponent } from './game.component';
var routes = [{
        path: '',
        component: GameComponent
    }];
var GameRoutingModule = /** @class */ (function () {
    function GameRoutingModule() {
    }
    GameRoutingModule = tslib_1.__decorate([
        NgModule({
            imports: [RouterModule.forChild(routes)],
            exports: [RouterModule]
        })
    ], GameRoutingModule);
    return GameRoutingModule;
}());
export { GameRoutingModule };
//# sourceMappingURL=game-routing.module.js.map