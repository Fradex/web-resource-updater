import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BuildModule } from "./builds/build.module";
import { BuildComponent } from './builds/build.component';
import { CartDetailComponent } from "./builds/cartDetail.component";
import { NgxSpinnerModule } from 'ngx-spinner';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BuildModule,
    RouterModule.forRoot([
      {
        path: "build", component: BuildComponent
      },
      {
        path: "cart", component: CartDetailComponent
      },
      //{
      //  path: "admin",
      //  loadChildren: "app/admin/admin.module#AdminModule",
      //  canActivate: [StoreFirstGuard]
      //},
      { path: "**", redirectTo: "/build" }
    ]),
    NgxSpinnerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
