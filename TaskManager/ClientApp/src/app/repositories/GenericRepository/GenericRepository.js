"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.GenericRepository = void 0;
var http_1 = require("@angular/common/http");
var rxjs_1 = require("rxjs");
var operators_1 = require("rxjs/operators");
var GenericRepository = /** @class */ (function () {
    function GenericRepository(httpClient, endpoint, endUrl) {
        this.httpClient = httpClient;
        this.endpoint = endpoint;
        this.endUrl = endUrl;
        this.httpOptions = {
            headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
        };
    }
    GenericRepository.prototype.Post = function (item) {
        return this.httpClient.post(this.endUrl + this.endpoint, item, this.httpOptions)
            .pipe(operators_1.map(this.extractData), operators_1.catchError(this.handleError));
    };
    GenericRepository.prototype.PostData = function (actionName, item) {
        return this.httpClient.post(this.endUrl + this.endpoint + actionName, item, this.httpOptions)
            .pipe(operators_1.map(this.extractData), operators_1.catchError(this.handleError));
    };
    GenericRepository.prototype.PostFile = function (actionName, formData) {
        return this.httpClient.post(this.endUrl + this.endpoint + actionName, formData)
            .pipe(operators_1.map(this.extractData), operators_1.catchError(this.handleError));
    };
    GenericRepository.prototype.Put = function (item) {
        return this.httpClient
            .put(this.endUrl + this.endpoint, item, this.httpOptions)
            .pipe(operators_1.map(this.extractData), operators_1.catchError(this.handleError));
    };
    GenericRepository.prototype.GetById = function (code) {
        return this.httpClient
            .get(this.endUrl + this.endpoint + '/' + code, this.httpOptions)
            .pipe(operators_1.map(this.extractData), operators_1.catchError(this.handleError));
    };
    GenericRepository.prototype.Delete = function (code) {
        return this.httpClient
            .delete(this.endUrl + this.endpoint + code, this.httpOptions)
            .pipe(operators_1.map(this.extractData), operators_1.catchError(this.handleError));
    };
    GenericRepository.prototype.Get = function (tableNames) {
        return this.httpClient.get(this.endUrl + this.endpoint + '/' + tableNames, this.httpOptions).pipe(operators_1.map(this.extractData), operators_1.catchError(this.handleError));
    };
    GenericRepository.prototype.autoSearch = function (table) {
        return this.httpClient.get(this.endUrl + this.endpoint + '/AutoSearch/' + '/' + table, this.httpOptions).pipe(operators_1.map(this.extractData), operators_1.catchError(this.handleError));
    };
    GenericRepository.prototype.extractData = function (res) {
        var body = res;
        return body || {};
    };
    GenericRepository.prototype.GetList = function (subUrl) {
        return this.httpClient.get(this.endUrl + subUrl);
    };
    GenericRepository.prototype.handleError = function (error) {
        if (error.status && typeof (error.error) == "object") {
            if (error.error._statusCode === 401) {
                localStorage.clear();
                localStorage.setItem('IsLoggingOut', "true");
                window.location.href = "/Login";
            }
        }
        else {
            var errorMessage = '';
            if (error.error instanceof ErrorEvent) {
                // client-side error
                errorMessage = 'Error: ' + error.error.message;
            }
            else {
                // server-side error
                errorMessage = 'Error Code: ' + error.status;
            }
            window.alert(errorMessage);
            return rxjs_1.of(errorMessage);
        }
    };
    return GenericRepository;
}());
exports.GenericRepository = GenericRepository;
//# sourceMappingURL=generic-repository.js.map