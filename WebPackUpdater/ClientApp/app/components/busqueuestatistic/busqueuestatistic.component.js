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
var BusQueueStatisticComponent = /** @class */ (function () {
    function BusQueueStatisticComponent(http, baseUrl, hcs, spinnerService) {
        var _this = this;
        this.hcs = hcs;
        this.spinnerService = spinnerService;
        this.chart = {
            chart: {
                type: 'spline',
                height: '900px'
            },
            title: {
                text: 'Очередь шины'
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                type: 'datetime',
                dateTimeLabelFormats: {
                    minute: '%H:%M',
                    hour: '%H:%M',
                    day: '%e. %b',
                    week: '%e. %b',
                    month: '%b \'%y',
                    year: '%Y'
                },
                title: {
                    text: 'Дата'
                }
            },
            yAxis: {
                title: {
                    text: 'Количество сообщений'
                },
                min: 0,
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
        http.get(baseUrl + 'api/Service/GetBusQueueStatistic').subscribe(function (result) {
            _this.data = result.json();
            _this.data.forEach(function (item) {
                var array = item.array.map(function (x) {
                    return [(new Date(new Date(x[0]).getTime() - new Date().getTimezoneOffset() * 60000)).getTime(), x[1]];
                });
                var serie = {
                    name: item.messageName,
                    data: array
                };
                _this.chart.series.push(serie);
            });
            var spinnerService = _this.spinnerService;
            _this.hcs.createChart(_this.chartEl.nativeElement, _this.chart, function () { spinnerService.hide(); });
            ;
        });
    }
    BusQueueStatisticComponent.prototype.ngAfterViewInit = function () {
    };
    __decorate([
        ViewChild('charts'),
        __metadata("design:type", ElementRef)
    ], BusQueueStatisticComponent.prototype, "chartEl", void 0);
    BusQueueStatisticComponent = __decorate([
        Component({
            selector: 'busqueuestatistic',
            templateUrl: './busqueuestatistic.component.html'
        }),
        __param(1, Inject('BASE_URL')),
        __metadata("design:paramtypes", [Http, String, HighchartsService,
            Ng4LoadingSpinnerService])
    ], BusQueueStatisticComponent);
    return BusQueueStatisticComponent;
}());
export { BusQueueStatisticComponent };
//# sourceMappingURL=busqueuestatistic.component.js.map