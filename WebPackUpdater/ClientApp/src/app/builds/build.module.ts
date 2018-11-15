import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { ModelModule } from "../model/model.module";
import { BuildComponent } from "./build.component";
import { CounterDirective } from "./counter.directive";
import { BuildDetailComponent } from "./buildDetail.component";
import { RouterModule } from "@angular/router";
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { GrowlModule } from 'primeng/growl';
import { MatCardModule } from '@angular/material/card';
import { MessageService } from 'primeng/components/common/messageservice';
import { PaginatorModule } from 'primeng/paginator';

@NgModule({
  imports: [MatCardModule, Ng4LoadingSpinnerModule.forRoot(), ModelModule, BrowserModule, FormsModule, RouterModule, GrowlModule, PaginatorModule],
  declarations: [BuildComponent, CounterDirective, BuildDetailComponent],
  exports: [BuildComponent],
  providers: [MessageService]
})
export class BuildModule { }
