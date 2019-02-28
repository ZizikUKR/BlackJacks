import * as tslib_1 from "tslib";
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { GameService } from './shared/services/game.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HistoryService } from './shared/services/history.service';
import { LoginComponent } from './components/login/login.component';
import { RegisterService } from './shared/services/register.service';
var appRoutes = [
    {
        path: '',
        component: LoginComponent
    },
    {
        path: 'game/:id',
        loadChildren: './components/game/game.module#GameModule'
    },
    {
        path: 'history',
        loadChildren: './components/history/history.module#HistoryModule'
    },
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = tslib_1.__decorate([
        NgModule({
            declarations: [
                AppComponent,
                LoginComponent
            ],
            imports: [
                BrowserModule,
                FormsModule,
                HttpClientModule,
                RouterModule.forRoot(appRoutes)
            ],
            providers: [GameService, HistoryService, RegisterService],
            bootstrap: [AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
export { AppModule };
//# sourceMappingURL=app.module.js.map