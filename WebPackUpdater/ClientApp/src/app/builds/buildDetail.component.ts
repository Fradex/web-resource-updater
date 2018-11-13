import { Component, OnInit } from "@angular/core";
import { BuildDetails } from "../model/buildDetail.model";
import { Build } from "../model/build.model";
import { BuildRepository } from "../model/build.repository";
import { ActivatedRoute } from "@angular/router";

@Component({
  moduleId: module.id,
  templateUrl: "buildDetail.component.html"
})
export class BuildDetailComponent implements OnInit {

  public build: Build;
  constructor(public buildDetails: BuildDetails, private repository: BuildRepository, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.repository.getBuild(params['id']).subscribe(data => {
        this.build = data;
        debugger;
      });
    });
  }
}