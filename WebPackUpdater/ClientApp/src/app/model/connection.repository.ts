import { Injectable } from "@angular/core";
import { Connection } from "./connection.model";
import { RestDataSource } from "./rest.datasource";
import { Observable } from "rxjs/Observable";

@Injectable()
export class ConnectionRepository {
  private connections: Connection[] = [];

  constructor(private dataSource: RestDataSource) {
    dataSource.getConnections().subscribe(data => {
      this.connections = data;
    });
  }

  getConnections(): Connection[] {
    return this.connections;
  }

  getConnection(id: string): Observable<Connection> {
      return this.dataSource.getConnection(id);
  }

  saveConnection(connection: Connection) {
    if (connection.id) {
      this.dataSource.saveConnection(connection)
        .subscribe(p => this.connections.push(p));
    } else {
      this.dataSource.updateBuild(connection)
        .subscribe(p => {
          this.connections.splice(this.connections.findIndex(p => p.id == connection.id), 1, connection);
        });
    }
  }

  deleteConnection(id: string) {
    this.dataSource.deleteConnection(id).subscribe(p => {
      this.connections.splice(this.connections.
        findIndex(p => p.id == id), 1);
    });
  }
}
