import { Injectable } from "@angular/core";
import { Build } from "./build.model";
import { RestDataSource } from "./rest.datasource";
import { Observable } from "rxjs/Observable";

@Injectable()
export class BuildRepository {
  private builds: Build[] = [];

  constructor(private dataSource: RestDataSource) {
    dataSource.getBuilds().subscribe(data => {
      this.builds = data;
    });
  }

  refreshProducts() {
    this.dataSource.getBuilds().subscribe(data => {
      this.builds = data;
    });
  }

  getBuilds(category: string = null): Build[] {
    return this.builds;
  }

  getBuild(id: string): Observable<Build> {
      return this.dataSource.getBuild(id);
  }

  saveBuild(build: Build) {
    if (build.id == null || build.id == 0) {
      this.dataSource.saveBuild(build)
        .subscribe(p => this.builds.push(p));
    } else {
      this.dataSource.updateBuild(build)
        .subscribe(p => {
          this.builds.splice(this.builds.
            findIndex(p => p.id == build.id), 1, build);
        });
    }
  }

  deleteBuild(id: number) {
    this.dataSource.deleteBuild(id).subscribe(p => {
      this.builds.splice(this.builds.
        findIndex(p => p.id == id), 1);
    });
  }
}
