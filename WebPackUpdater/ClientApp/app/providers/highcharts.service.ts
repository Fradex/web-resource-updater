import { Injectable } from '@angular/core';
import * as Highcharts from 'highcharts/highstock';

@Injectable()
export class HighchartsService {

	charts:any = [];
	defaultOptions = {
	}

	constructor() {
	}

	createChart(container: any, options?: Object, callback?: any) {
		let opts = <any>(!!options ? options : this.defaultOptions);
		let e = document.createElement("div");

		container.appendChild(e);

		if (!!opts.chart) {
			opts.chart['renderTo'] = e;
		} else {
			opts.chart = {
				'renderTo': e
			}
		}
		Highcharts.stockChart(container, opts, (chart) => { callback(); });
	}

	removeFirst() {
		this.charts.shift();
	}

	removeLast() {
		this.charts.pop();
	}

	getChartInstances(): number {
		return this.charts.length;
	}

	getCharts() {
		return this.charts;
	}
}