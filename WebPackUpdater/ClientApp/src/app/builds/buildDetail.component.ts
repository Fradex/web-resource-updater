import { Component, OnInit, Inject } from "@angular/core";
import { BuildDetails } from "../model/buildDetail.model";
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { HttpClient } from '@angular/common/http';

import { Message } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

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
  private baseUrl: string;

  constructor(public buildDetails: BuildDetails,
    private repository: BuildRepository,
    private route: ActivatedRoute,
    private http: HttpClient,
    private spinnerService: Ng4LoadingSpinnerService,
    private messageService: MessageService,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public updateAndPublish() {

    this.spinnerService.show();
    var ids = this.webResources.filter(x => x.isAutoUpdate).map(x => { return x.id });
    this.http.post(this.baseUrl + 'api/Webresources/UpdateWebresources', { ids: ids })
      .subscribe(result => {
        this.spinnerService.hide();
        this.repository.refreshProducts();
        this.messageService.add({ severity: 'success', summary: 'Билд', detail: 'Ресурсы успешно опубликованы!', life: 5000 });
      },
        error => {
          console.error(error);
          this.messageService.add({ severity: 'error', summary: 'Билд', detail: 'Произошла ошибка при публикации!', life: 5000 });
          this.spinnerService.hide();
          this.repository.refreshProducts();
        });
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

export class MyModel {

  msgs: Message[] = [];
}
