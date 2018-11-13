import { Injectable } from "@angular/core";
import { Build } from "./build.model";
import { RestDataSource } from "./rest.datasource";

@Injectable()
export class BuildRepository {
    private builds: Build[] = [];

    constructor(private dataSource: RestDataSource) {
        dataSource.getBuilds().subscribe(data => {
            this.builds = data;
        });
    }

    getProducts(category: string = null): Build[] {
      return this.builds;
    }

    getProduct(id: number): Build {
        return this.builds.find(p => p.id == id);
    }

    saveProduct(build: Build) {
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

    deleteProduct(id: number) {
        this.dataSource.deleteBuild(id).subscribe(p => {
          this.builds.splice(this.builds.
                findIndex(p => p.id == id), 1);            
        });
    }
}
