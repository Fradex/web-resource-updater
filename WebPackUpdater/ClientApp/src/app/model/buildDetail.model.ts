import { Injectable } from "@angular/core";
import { Build } from "./build.model";

@Injectable()
export class BuildDetails {
  public build: Build;
  OpenedBuild(build: Build) {
    this.build = build;
  }
}
