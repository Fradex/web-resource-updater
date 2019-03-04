import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material';
import { CookieService } from 'ngx-cookie-service';

import { AppComponent } from './app.component';
import { BuildModule } from "./builds/build.module";
import { BuildComponent } from './builds/build.component';
import { BuildDetailComponent } from "./builds/buildDetail.component";
import { AuthGuard } from './admin/auth.guard';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserAnimationsModule,
    FlexLayoutModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BuildModule,
    MatButtonModule,
    RouterModule.forRoot([
      {
        path: "build", component: BuildComponent,
        canActivate: [AuthGuard]
      },
      {
        path: "admin/main/build", component: BuildComponent
      },
      {
        path: 'build/:id',        
        component: BuildDetailComponent,
        canActivate: [AuthGuard]
      },
      {
        path: "admin",
        loadChildren: "app/admin/admin.module#AdminModule"
      },
      { path: "**", redirectTo: "/admin/auth" }
    ])
  ],
  providers: [CookieService, AuthGuard ],
  bootstrap: [AppComponent],
  exports: [MatButtonModule]
})
export class AppModule { }
