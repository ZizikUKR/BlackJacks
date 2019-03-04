import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http';
var HttpConfigInterceptor = /** @class */ (function () {
    function HttpConfigInterceptor() {
    }
    HttpConfigInterceptor.prototype.intercept = function (request, next) {
        var _this = this;
        var token = localStorage.getItem('token');
        if (token) {
            request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
        }
        if (!request.headers.has('Content-Type')) {
            request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
        }
        request = request.clone({ headers: request.headers.set('Accept', 'application/json') });
        return next.handle(request).pipe(map(function (event) {
            if (event instanceof HttpResponse) {
                console.log('event--->>>', event);
            }
            return event;
        }), catchError(function (error) {
            var data = {};
            data = {
                reason: error && error.error.reason ? error.error.reason : '',
                status: error.status
            };
            _this.errorDialogService.openDialog(data);
            return throwError(error);
        }));
    };
    HttpConfigInterceptor = tslib_1.__decorate([
        Injectable()
    ], HttpConfigInterceptor);
    return HttpConfigInterceptor;
}());
export { HttpConfigInterceptor };
//# sourceMappingURL=httpconfig.interceptor.js.map