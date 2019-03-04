import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { AuthComponent } from "./auth.component";
import { AdminComponent } from "./admin.component";
import { ConnectionsTableComponent } from "./connectionsTable.component";
import { ConnectionEditorComponent } from "./connectionEditor.component";
import { AuthGuard } from "./auth.guard";
import { MatCardModule, MatIconModule, MatFormFieldModule, MatInputModule } from '@angular/material';

let routing = RouterModule.forChild([
  { path: "auth", component: AuthComponent },
  {
    path: "main", component: AdminComponent, canActivate: [AuthGuard],
    children: [
      { path: "connection/:mode/:id", component: ConnectionEditorComponent },
      { path: "connection/:mode", component: ConnectionEditorComponent },
      { path: "connection", component: ConnectionsTableComponent },
      { path: "**", redirectTo: "connection" }
    ]
  },
  { path: "**", redirectTo: "auth" }
]);

@NgModule({
  imports: [CommonModule, FormsModule, MatCardModule, MatFormFieldModule, MatIconModule, MatInputModule, routing],
  providers: [AuthGuard],
  declarations: [AuthComponent, AdminComponent, ConnectionsTableComponent, ConnectionEditorComponent]
})
export class AdminModule { }
