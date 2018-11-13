import { Component, Inject } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Build } from "../model/build.model";
import { BuildRepository } from "../model/build.repository";
import { Cart } from "../model/cart.model";
import { Router } from "@angular/router";
import { NgxSpinnerService } from 'ngx-spinner';

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
    private cart: Cart,
    private router: Router,
    private http: HttpClient,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  get builds(): Build[] {
    let pageIndex = (this.selectedPage - 1) * this.buildsPerPage;
    return this.repository.getProducts(this.selectedCategory)
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
    this.spinner.show();
    this.http.post(this.baseUrl + 'api/Webpack/Build', { description: 'Webpack Fast Build. Run from web.', name: 'Webpack Fast Build.' })
      .subscribe(result => {
          this.spinner.hide();
        },
      error => {
        console.error(error);
        this.spinner.hide();
      });
  }

  get pageCount(): number {
    return Math.ceil(this.repository
      .getProducts(this.selectedCategory).length /
      this.buildsPerPage);
  }

  addProductToCart(build: Build) {
    this.cart.addLine(build);
    this.router.navigateByUrl("/cart");
  }
}
