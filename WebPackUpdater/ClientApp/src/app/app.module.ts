import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BuildModule } from "./builds/build.module";
import { BuildComponent } from './builds/build.component';
import { BuildDetailComponent } from "./builds/buildDetail.component";
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';


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
        path: 'build/:id',        
        component: BuildDetailComponent
      },
      //{
      //  path: "admin",
      //  loadChildren: "app/admin/admin.module#AdminModule",
      //  canActivate: [StoreFirstGuard]
      //},
      { path: "**", redirectTo: "/build" }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
