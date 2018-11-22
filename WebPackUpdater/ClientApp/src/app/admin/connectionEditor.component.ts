import { Component } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { Connection } from "../model/connection.model";
import { ConnectionRepository } from "../model/connection.repository";

@Component({
  moduleId: module.id,
  templateUrl: "connectionEditor.component.html"
})
export class ConnectionEditorComponent {
  editing: boolean = false;
  connection: Connection = new Connection();

  constructor(private repository: ConnectionRepository,
    private router: Router,
    activeRoute: ActivatedRoute) {

    this.editing = activeRoute.snapshot.params["mode"] == "edit";
    if (this.editing) {
      Object.assign(this.connection,
        repository.getConnection(activeRoute.snapshot.params["id"]));
    }
  }

  save(form: NgForm) {
    this.repository.saveConnection(this.connection);
    this.router.navigateByUrl("/admin/main/connection");
  }
}
