import { NgModule } from "@angular/core";
import { RestDataSource } from "./rest.datasource";
import { BuildRepository } from "./build.repository";
import { HttpModule } from "@angular/http";
import { BuildDetails } from "./buildDetail.model";
import { AuthService } from "./auth.service";

@NgModule({
  imports: [HttpModule],
  providers: [BuildRepository, RestDataSource, AuthService, BuildDetails]
})
export class ModelModule {
}
