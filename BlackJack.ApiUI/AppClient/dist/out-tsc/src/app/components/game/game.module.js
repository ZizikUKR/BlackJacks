import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { GameComponent } from './game.component';
import { GameRoutingModule } from './game-routing.module';
var GameModule = /** @class */ (function () {
    function GameModule() {
    }
    GameModule = tslib_1.__decorate([
        NgModule({
            declarations: [
                GameComponent
            ],
            imports: [
                CommonModule,
                ReactiveFormsModule,
                GameRoutingModule
            ]
        })
    ], GameModule);
    return GameModule;
}());
export { GameModule };
//# sourceMappingURL=game.module.js.map