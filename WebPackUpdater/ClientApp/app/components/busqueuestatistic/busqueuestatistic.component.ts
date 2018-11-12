import { Component, Inject, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { Http } from '@angular/http';
import { Chart } from 'angular-highcharts';
import { HighchartsService } from "../../providers/highcharts.service";
import { DateError } from "../../model/DateError";
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
	selector: 'busqueuestatistic',
	templateUrl: './busqueuestatistic.component.html'
})
export class BusQueueStatisticComponent implements AfterViewInit {
	@ViewChild('charts')
	public chartEl: ElementRef;

	public data: any[];
	chart: any = {
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
			dateTimeLabelFormats: { // don't display the dummy year
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

	constructor(http: Http,
		@Inject('BASE_URL') baseUrl: string,
		private hcs: HighchartsService,
		private spinnerService: Ng4LoadingSpinnerService) {
		this.spinnerService.show();
		http.get(baseUrl + 'api/Service/GetBusQueueStatistic').subscribe(result => {
			this.data = result.json();

			this.data.forEach(item => {
				var array = item.array.map((x: any) => {
					return [(new Date(new Date(x[0]).getTime() - new Date().getTimezoneOffset() * 60000)).getTime(), x[1]];
				});
				var serie = <any>{
					name: item.messageName,
					data: array
				}
				this.chart.series.push(serie);
			});


			var spinnerService = this.spinnerService;
			this.hcs.createChart(this.chartEl.nativeElement, this.chart, () => { spinnerService.hide(); });;
		});
	}

	ngAfterViewInit(): void {
	}
}