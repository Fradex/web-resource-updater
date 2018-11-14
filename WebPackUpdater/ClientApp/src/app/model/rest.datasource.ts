import { Injectable } from "@angular/core";
import { Http, Request, RequestMethod } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { Build } from "./build.model";
import { WebResource } from "../model/webresource.model";
import { Inject } from '@angular/core';
import "rxjs/add/operator/map";

const PROTOCOL = "http";
const PORT = 3500;

@Injectable()
export class RestDataSource {
  baseUrl: string;
  auth_token: string;

  constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  authenticate(user: string, pass: string): Observable<boolean> {
    return this.http.request(new Request({
      method: RequestMethod.Post,
      url: this.baseUrl + "login",
      body: { name: user, password: pass }
    })).map(response => {
      let r = response.json();
      this.auth_token = r.success ? r.token : null;
      return r.success;
    });
  }

  getBuilds(): Observable<Build[]> {
    return this.sendRequest(RequestMethod.Get, "api/Build") as Observable<Build[]>;
  }

  getBuild(id: string): Observable<Build> {
    return this.sendRequest(RequestMethod.Get, "api/Build/" + id) as Observable<Build>;
  }

  getWebresources(): Observable<WebResource[]> {
    return this.sendRequest(RequestMethod.Get, "api/WebResources") as Observable<WebResource[]>;
  }

  saveBuild(build: Build): Observable<Build> {
    return this.sendRequest(RequestMethod.Post,
      "api/Build",
      build,
      true) as Observable<Build>;
  }

  updateBuild(build): Observable<Build> {
    return this.sendRequest(RequestMethod.Put,
      `api/Build/${build.id}`,
      build,
      true) as Observable<Build>;
  }

  deleteBuild(id: number): Observable<Build> {
    return this.sendRequest(RequestMethod.Delete,
      `api/Build/${id}`,
      null,
      true) as Observable<Build>;
  }

  private sendRequest(verb: RequestMethod,
    url: string,
    body?: Build,
    auth: boolean = false): Observable<Build | Build[] | WebResource | WebResource[]> {

    debugger;

    let request = new Request({
      method: verb,
      url: this.baseUrl + url,
      body: body
    });
    if (auth && this.auth_token != null) {
      request.headers.set("Authorization", `Bearer<${this.auth_token}>`);
    }
    return this.http.request(request).map(response => response.json());
  }
}
