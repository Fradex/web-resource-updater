import { Component, Inject } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Build } from "../model/build.model";
import { BuildRepository } from "../model/build.repository";
import { BuildDetails } from "../model/buildDetail.model";
import { Router } from "@angular/router";
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

import { Message } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
  selector: "build",
  moduleId: module.id,
  templateUrl: "build.component.html"
})
export class BuildComponent {
  public selectedCategory = null;
  public buildsPerPage = 4;
  public selectedPage = 1;
  private baseUrl: string;
  public loading = false;

  constructor(private repository: BuildRepository,
    private buildDetail: BuildDetails,
    private router: Router,
    private http: HttpClient,
    private spinnerService: Ng4LoadingSpinnerService,
    private messageService: MessageService,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  get builds(): Build[] {
    let pageIndex = (this.selectedPage - 1) * this.buildsPerPage;
    return this.repository.getBuilds(this.selectedCategory)
      .slice(pageIndex, pageIndex + this.buildsPerPage);
  }

  changePage(newPage: number) {
    this.selectedPage = newPage;
  }

  changePageSize(newSize: number) {
    this.buildsPerPage = Number(newSize);
    this.changePage(1);
  }

  runFastBuild() {
    this.spinnerService.show();
    this.messageService.add({ severity: 'info', summary: 'Билд', detail: 'Билд запущен!', life: 5000 });
    this.http.post(this.baseUrl + 'api/Webpack/Build', { description: 'Билд скриптов при помощи webpack', name: 'Webpack' })
      .subscribe(result => {
        this.spinnerService.hide();
        this.repository.refreshProducts();
        this.messageService.add({ severity: 'success', summary: 'Билд', detail: 'Билд успешно завершен!', life: 5000 });
      },
        error => {
          console.error(error);
          this.messageService.add({ severity: 'error', summary: 'Билд', detail: 'Произошла ошибка при построении!', life: 5000 });
          this.spinnerService.hide();
          this.repository.refreshProducts();
        });
  }

  get pageCount(): number {
    return Math.ceil(this.repository
      .getBuilds(this.selectedCategory).length /
      this.buildsPerPage);
  }

  openBuildDetail(build: Build) {
    this.buildDetail.OpenedBuild(build);
    this.router.navigateByUrl("/build/" + build.id);
  }
}


export class MyModel {

  msgs: Message[] = [];

}
