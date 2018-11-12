var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import * as Highcharts from 'highcharts/highstock';
var HighchartsService = /** @class */ (function () {
    function HighchartsService() {
        this.charts = [];
        this.defaultOptions = {};
    }
    HighchartsService.prototype.createChart = function (container, options, callback) {
        var opts = (!!options ? options : this.defaultOptions);
        var e = document.createElement("div");
        container.appendChild(e);
        if (!!opts.chart) {
            opts.chart['renderTo'] = e;
        }
        else {
            opts.chart = {
                'renderTo': e
            };
        }
        Highcharts.stockChart(container, opts, function (chart) { callback(); });
    };
    HighchartsService.prototype.removeFirst = function () {
        this.charts.shift();
    };
    HighchartsService.prototype.removeLast = function () {
        this.charts.pop();
    };
    HighchartsService.prototype.getChartInstances = function () {
        return this.charts.length;
    };
    HighchartsService.prototype.getCharts = function () {
        return this.charts;
    };
    HighchartsService = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [])
    ], HighchartsService);
    return HighchartsService;
}());
export { HighchartsService };
//# sourceMappingURL=highcharts.service.js.map