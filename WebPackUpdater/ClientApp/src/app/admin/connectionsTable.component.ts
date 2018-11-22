import { Component } from "@angular/core";
import { Connection } from "../model/connection.model";
import { ConnectionRepository } from "../model/connection.repository";

@Component({
  moduleId: module.id,
  templateUrl: "connectionsTable.component.html"
})
export class ConnectionsTableComponent {

  constructor(private repository: ConnectionRepository) { }

  getConnections(): Connection[] {
    return this.repository.getConnections();
  }

  deleteConnection(id: string) {
    this.repository.deleteConnection(id);
  }
}
