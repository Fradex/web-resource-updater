import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ChartModule } from 'angular-highcharts';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { BusQueueStatisticComponent } from './components/busqueuestatistic/busqueuestatistic.component';
import { ChartComponent } from './components/chart/chart.component';
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';

import { HighchartsService } from './providers/highcharts.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        ChartComponent,
	    BusQueueStatisticComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
		FormsModule,
	    ChartModule ,
        RouterModule.forRoot([
            { path: '', redirectTo: 'chart', pathMatch: 'full' },
			{ path: 'chart', component: ChartComponent },
			{ path: 'busqueuestatistic', component: BusQueueStatisticComponent },
            { path: '**', redirectTo: 'chart' }
		]),
	    Ng4LoadingSpinnerModule.forRoot()
	],
	providers: [HighchartsService]
})
export class AppModuleShared {
}
