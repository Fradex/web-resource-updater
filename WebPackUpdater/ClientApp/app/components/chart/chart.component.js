var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
import { Component, Inject, ViewChild, ElementRef } from '@angular/core';
import { Http } from '@angular/http';
import { HighchartsService } from "../../providers/highcharts.service";
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
var ChartComponent = /** @class */ (function () {
    function ChartComponent(http, baseUrl, hcs, spinnerService) {
        var _this = this;
        this.hcs = hcs;
        this.spinnerService = spinnerService;
        this.chart = {
            chart: {
                type: 'spline'
            },
            title: {
                text: 'Количество ошибок шины'
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                type: 'datetime',
                dateTimeLabelFormats: {
                    month: '%e. %b',
                    year: '%b'
                },
                title: {
                    text: 'Дата'
                }
            },
            yAxis: {
                title: {
                    text: 'Количество ошибок'
                },
                min: 0
            },
            tooltip: {
                pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b><br/>',
                valueDecimals: 2,
                split: true
            },
            plotOptions: {
                spline: {
                    marker: {
                        enabled: true
                    }
                }
            },
            colors: ['#6CF', '#39F', '#06C', '#036', '#000'],
            series: []
        };
        this.spinnerService.show();
        new Promise(function (resolve, reject) {
            http.get(baseUrl + 'api/Service/GetStatistics').subscribe(function (result) {
                _this.data = result.json();
                var serie = {
                    name: "Количество ошибок за день",
                    data: _this.data.map(function (x) {
                        return [new Date(x.date).getTime(), x.errorCount];
                    })
                };
                _this.chart.series.push(serie);
                resolve(_this.chart);
            }, function (error) { return console.error(error); });
        }).then(function (chart) {
            http.get(baseUrl + 'api/Service/GetGenericSqlErrorStatistics').subscribe(function (result) {
                _this.data = result.json();
                var serie = {
                    name: "Количество Generic SQL Error ошибок за день",
                    data: _this.data.map(function (x) {
                        return [new Date(x.date).getTime(), x.errorCount];
                    })
                };
                _this.chart.series.push(serie);
                var spinnerService = _this.spinnerService;
                _this.hcs.createChart(_this.chartEl.nativeElement, _this.chart, function () { spinnerService.hide(); });
                ;
            });
        });
    }
    ChartComponent.prototype.ngAfterViewInit = function () {
    };
    __decorate([
        ViewChild('charts'),
        __metadata("design:type", ElementRef)
    ], ChartComponent.prototype, "chartEl", void 0);
    ChartComponent = __decorate([
        Component({
            selector: 'chart',
            templateUrl: './chart.component.html'
        }),
        __param(1, Inject('BASE_URL')),
        __metadata("design:paramtypes", [Http, String, HighchartsService,
            Ng4LoadingSpinnerService])
    ], ChartComponent);
    return ChartComponent;
}());
export { ChartComponent };
//# sourceMappingURL=chart.component.js.map