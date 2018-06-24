"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var PaginationComponent = /** @class */ (function () {
    function PaginationComponent() {
        this.totalItems = 0;
        this.pageSize = 10;
        this.pageChanged = new core_1.EventEmitter();
        this.pages = [];
        this.currentPage = 1;
    }
    PaginationComponent.prototype.ngOnChanges = function () {
        this.currentPage = 1;
        var pagesCount = Math.ceil(this.totalItems / this.pageSize);
        this.pages = [];
        for (var i = 1; i <= pagesCount; i++)
            this.pages.push(i);
    };
    PaginationComponent.prototype.changePage = function (page) {
        this.currentPage = page;
        this.pageChanged.emit(page);
    };
    PaginationComponent.prototype.previous = function () {
        if (this.currentPage == 1)
            return;
        this.currentPage--;
        this.pageChanged.emit(this.currentPage);
    };
    PaginationComponent.prototype.next = function () {
        if (this.currentPage == this.pages.length)
            return;
        this.currentPage++;
        this.pageChanged.emit(this.currentPage);
    };
    __decorate([
        core_1.Input('total-items')
    ], PaginationComponent.prototype, "totalItems", void 0);
    __decorate([
        core_1.Input('page-size')
    ], PaginationComponent.prototype, "pageSize", void 0);
    __decorate([
        core_1.Output('page-changed')
    ], PaginationComponent.prototype, "pageChanged", void 0);
    PaginationComponent = __decorate([
        core_1.Component({
            selector: 'pagination',
            template: "\n    <nav *ngIf=\"totalItems > pageSize\">\n        <ul class=\"pagination\">\n            <li [class.disabled]=\"currentPage == 1\">\n                <a (click)=\"previous()\" aria-label=\"Previous\">\n                <span aria-hidden=\"true\">&laquo;</span>\n                </a>\n            </li>\n            <li [class.active]=\"currentPage == page\" *ngFor=\"let page of pages\" (click)=\"changePage(page)\">\n                <a class=\"page-link\">{{ page }}</a>\n            </li>\n            <li [class.disabled]=\"currentPage == pages.length\">\n                <a (click)=\"next()\" aria-label=\"Next\">\n                <span aria-hidden=\"true\">&raquo;</span>\n                </a>\n            </li>\n        </ul>\n    </nav>  \n"
        })
    ], PaginationComponent);
    return PaginationComponent;
}());
exports.PaginationComponent = PaginationComponent;
//# sourceMappingURL=pagination.component.js.map