import * as tslib_1 from "tslib";
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
var HttpErrorInterceptor = /** @class */ (function () {
    function HttpErrorInterceptor(router) {
        this.router = router;
    }
    HttpErrorInterceptor.prototype.intercept = function (request, next) {
        var _this = this;
        return next.handle(request)
            .pipe(catchError(function (error) {
            var errMsg = '';
            // Client Side Error
            if (error.error instanceof ErrorEvent) {
                errMsg = "Error: " + error.error.message;
            }
            else {
                _this.router.navigate(["error"]);
            }
            return throwError(errMsg);
        }));
    };
    HttpErrorInterceptor = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [Router])
    ], HttpErrorInterceptor);
    return HttpErrorInterceptor;
}());
export { HttpErrorInterceptor };
//# sourceMappingURL=error-handler.js.map