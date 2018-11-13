import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { ModelModule } from "../model/model.module";
import { BuildComponent } from "./build.component";
import { CounterDirective } from "./counter.directive";
import { BuildDetailComponent } from "./buildDetail.component";
import { RouterModule } from "@angular/router";
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';

@NgModule({
  imports: [Ng4LoadingSpinnerModule.forRoot(), ModelModule, BrowserModule, FormsModule, RouterModule],
  declarations: [BuildComponent, CounterDirective, BuildDetailComponent],
  exports: [BuildComponent]
})
export class BuildModule { }
