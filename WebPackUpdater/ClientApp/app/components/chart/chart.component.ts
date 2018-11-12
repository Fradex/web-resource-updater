import { Component, Inject, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { Http } from '@angular/http';
import { Chart } from 'angular-highcharts';
import { HighchartsService } from "../../providers/highcharts.service";
import { DateError } from "../../model/DateError";
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
	selector: 'chart',
	templateUrl: './chart.component.html'
})
export class ChartComponent implements AfterViewInit {
	@ViewChild('charts')
	public chartEl: ElementRef;

	public data: any[];
	chart: any = {
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
			dateTimeLabelFormats: { // don't display the dummy year
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

	constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private hcs: HighchartsService,
		private spinnerService: Ng4LoadingSpinnerService) {
		this.spinnerService.show();
		new Promise((resolve, reject) => {
			http.get(baseUrl + 'api/Service/GetStatistics').subscribe(result => {
					this.data = result.json() as DateError[];

					var serie = <any>{
						name: "Количество ошибок за день",
						data: this.data.map(x => {
							return [new Date(x.date).getTime(), x.errorCount];
						})
					}
					this.chart.series.push(serie);
					resolve(this.chart);
				},
				error => console.error(error));
		}).then(chart => {
			http.get(baseUrl + 'api/Service/GetGenericSqlErrorStatistics').subscribe(result => {
				this.data = result.json() as DateError[];

				var serie = <any>{
					name: "Количество Generic SQL Error ошибок за день",
					data: this.data.map(x => {
						return [new Date(x.date).getTime(), x.errorCount];
					})
				}
				this.chart.series.push(serie);
				var spinnerService = this.spinnerService;
				this.hcs.createChart(this.chartEl.nativeElement, this.chart, () => { spinnerService.hide(); });
				;
			});
		});
	}

	ngAfterViewInit(): void {
	}
}