import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { ModelModule } from "../model/model.module";
import { BuildComponent } from "./build.component";
import { CounterDirective } from "./counter.directive";
import { CartDetailComponent } from "./cartDetail.component";
import { RouterModule } from "@angular/router";


@NgModule({
  imports: [ModelModule, BrowserModule, FormsModule, RouterModule],
  declarations: [BuildComponent, CounterDirective, CartDetailComponent],
  exports: [BuildComponent]
})
export class BuildModule { }
