import { Component, OnInit } from "@angular/core";
import { BuildDetails } from "../model/buildDetail.model";
import { Build } from "../model/build.model";
import { BuildRepository } from "../model/build.repository";
import { ActivatedRoute } from "@angular/router";
import { WebResource } from "../model/webresource.model";

@Component({
  moduleId: module.id,
  templateUrl: "buildDetail.component.html"
})
export class BuildDetailComponent implements OnInit {

  public build: Build;
  public webResources: WebResource[];

  constructor(public buildDetails: BuildDetails, private repository: BuildRepository, private route: ActivatedRoute) {
  }

  public updateAndPublish() {
    var ids = this.webResources.filter(x => x.isAutoUpdate).map(x => { return x.id });
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.repository.getBuild(params['id']).subscribe(data => {
        this.build = data;
      });
    });

    this.repository.getChangedWebResources().subscribe(data => {
      this.webResources = data;
    });
  }
}
