import { NgModule } from "@angular/core";
import { RestDataSource } from "./rest.datasource";
import { BuildRepository } from "./build.repository";
import { HttpModule } from "@angular/http";
import { Cart } from "./cart.model";
import { AuthService } from "./auth.service";

@NgModule({
  imports: [HttpModule],
  providers: [BuildRepository, RestDataSource, AuthService, Cart]
})
export class ModelModule {
}
