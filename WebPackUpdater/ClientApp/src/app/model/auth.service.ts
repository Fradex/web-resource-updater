import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { RestDataSource } from "./rest.datasource";
import "rxjs/add/operator/map";
import 'rxjs/add/observable/of';
import { CookieService } from 'ngx-cookie-service';


@Injectable()
export class AuthService {

  constructor(private datasource: RestDataSource, private cookieService: CookieService) { }

  authenticate(username: string, password: string): Observable<boolean> {
      return this.datasource.authenticate(username, password);
  }

  get authenticated(): boolean {
    let authToken = this.cookieService.get('auth_token');
    return !!authToken;
  }

  clear() {
    this.cookieService.set('auth_token', null);
    this.datasource.auth_token = null;
  }
}
