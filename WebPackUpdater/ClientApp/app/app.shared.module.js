var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
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
var AppModuleShared = /** @class */ (function () {
    function AppModuleShared() {
    }
    AppModuleShared = __decorate([
        NgModule({
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
                ChartModule,
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
    ], AppModuleShared);
    return AppModuleShared;
}());
export { AppModuleShared };
//# sourceMappingURL=app.shared.module.js.map